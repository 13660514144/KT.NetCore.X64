using KT.Common.Core.Utils;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.Models;
using KT.Quanta.Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Common
{
    public class RemoteDeviceList
    {
        private ushort _terminalId = 1;
        private readonly ILogger<RemoteDeviceList> _logger;
        private IServiceProvider _serviceProvider;
        private IRemoteDeviceFactory _remoteDeviceFactory;

        public RemoteDeviceList(ILogger<RemoteDeviceList> logger,
            IServiceProvider serviceProvider,
            IRemoteDeviceFactory remoteDeviceFactory)
        {
            _logger = logger;
            _queue = new ConcurrentBag<IRemoteDevice>();
            _serviceProvider = serviceProvider;
            _remoteDeviceFactory = remoteDeviceFactory;
        }

        //所有设备信息 
        private ConcurrentBag<IRemoteDevice> _queue;

        public async Task AddsAsync(List<RemoteDeviceModel> devices)
        {
            if (devices?.FirstOrDefault() == null)
            {
                _logger.LogWarning($"不存在需要初始化的远程设备！");
                return;
            }
            foreach (var item in devices)
            {
                await AddAsync(item);
            }
        }

        public async Task AddAsync(RemoteDeviceModel device)
        {
            await Task.Run(() =>
            {
                if (device == null)
                {
                    _logger.LogWarning($"初始化不存在的远程设备：{JsonConvert.SerializeObject(device, JsonUtil.JsonSettings)} ");
                    return;
                }

                var remoteDevice = _remoteDeviceFactory.Creator(device);
                remoteDevice.RemoteDeviceInfo.TerminalId = _terminalId++;
                _queue.Add(remoteDevice);

                _logger.LogInformation($"初始化远程设备完成：{JsonConvert.SerializeObject(device, JsonUtil.JsonSettings)} ");
            });
        }

        public async Task<IRemoteDevice> GetByTerminalIdAsync(ushort terminalId)
        {
            return await Task.Run(() =>
            {
                var val = _queue.FirstOrDefault(x => x.RemoteDeviceInfo.TerminalId == terminalId);
                return val;
            });
        }

        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <returns></returns>
        public List<DeviceStateModel> GetStates()
        {
            var results = _queue.Select(x =>
            {
                return new DeviceStateModel()
                {
                    Id = x.RemoteDeviceInfo.DeviceId,
                    IsOnline = x.RemoteDeviceInfo.IsOnline
                };
            }).ToList();

            return results;
        }

        /// <summary>
        /// 轮询所有边缘处理器执行
        /// </summary>
        /// <param name="execAction">执行的委托定义</param>
        public async Task ExecAllAsync(Func<IRemoteDevice, Task> execAction)
        {
            foreach (var item in _queue)
            {
                await execAction.Invoke(item);
            }
        }

        /// <summary>
        /// 根据条件对相应设备执行操作
        /// </summary>
        /// <param name="whereAction">设备过滤条件</param>
        /// <param name="execAction">执行的操作</param>
        public async Task ExecByWhereAsync(Func<IRemoteDevice, bool> whereAction, Func<IRemoteDevice, Task> execAction)
        {
            foreach (var item in _queue)
            {
                var isCondition = whereAction.Invoke(item);
                if (isCondition)
                {
                    await execAction?.Invoke(item);
                }
            }
        }

        /// <summary>
        /// 向队列中添加接收到的数据,如果存在，则先删除再加入
        /// </summary>
        /// <param name="data">接收到的数据</param>
        public async Task<IRemoteDevice> AddOrUpdateContentAsync(RemoteDeviceModel data)
        {
            IRemoteDevice remoteDevice = null;
            //查询已经存在的数据 1、Key 2、ip port 3、device id(后台id)
            if (!string.IsNullOrEmpty(data.DeviceKey))
            {
                remoteDevice = _queue.FirstOrDefault(x => x.RemoteDeviceInfo.DeviceKey == data.DeviceKey);
            }
            if (remoteDevice == null)
            {
                remoteDevice = await GetByIpAndPortIdAsync(data.IpAddress, data.Port);
            }
            if (remoteDevice == null)
            {
                remoteDevice = await GetByIdAndTypeAsync(data.DeviceId, data.DeviceType);
            }

            //如果存在修改内容
            if (remoteDevice != null)
            {
                //修改内容
                remoteDevice.RemoteDeviceInfo.RealId = data.RealId;
                remoteDevice.RemoteDeviceInfo.IpAddress = data.IpAddress;
                remoteDevice.RemoteDeviceInfo.Port = data.Port;

                //检查修改的值是否错误
                if (remoteDevice.RemoteDeviceInfo.DeviceType != data.DeviceType)
                {
                    _logger.LogError($"加入队列的设备类型错误：oldType:{remoteDevice.RemoteDeviceInfo.DeviceType} newType:{data.DeviceType} ");
                }
                if (remoteDevice.RemoteDeviceInfo.DeviceId != data.DeviceId)
                {
                    _logger.LogError($"加入队列的设备Id错误：oldId:{remoteDevice.RemoteDeviceInfo.DeviceId} newId:{data.DeviceId} ");
                }
            }
            else
            {
                //创建对像
                remoteDevice = _remoteDeviceFactory.Creator(data);

                remoteDevice.RemoteDeviceInfo.TerminalId = _terminalId++;

                //更改状态并加入队列
                _queue.Add(remoteDevice);
            }

            return remoteDevice;
        }

        public IRemoteDevice GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var val = _queue.FirstOrDefault(x => x.RemoteDeviceInfo.DeviceId == id);
            return val;
        }

        public async Task<IRemoteDevice> GetByConnectionIdAsync(string connectionId)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(connectionId))
                {
                    return null;
                }

                var val = _queue.FirstOrDefault(x => x.RemoteDeviceInfo.ConnectionId == connectionId);
                return val;
            });
        }

        /// <summary>
        /// 根据Key称获取设备
        /// </summary>
        /// <param name="key">设备Key</param> 
        public async Task<IRemoteDevice> GetByKeyAsync(string key)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(key))
                {
                    return null;
                }

                var val = _queue.FirstOrDefault(x => x.RemoteDeviceInfo.DeviceKey == key);
                return val;
            });
        }

        /// <summary>
        /// 根据删除数据
        /// </summary>
        /// <param name="key">设备Key</param> 
        public async Task<IRemoteDevice> RemoveByKeyAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            var oldItem = await GetByKeyAsync(key);
            if (oldItem != null)
            {
                var isTake = _queue.TryTake(out oldItem);
                if (!isTake)
                {
                    _logger.LogError($"删除远程设备内存数据错误：{JsonConvert.SerializeObject(oldItem, JsonUtil.JsonSettings)} ");
                }
                return oldItem;
            }
            return null;
        }

        /// <summary>
        /// 根据IP和端口查找
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        internal async Task<IRemoteDevice> GetByIpAndPortIdAsync(string ipAddress, int port)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(ipAddress) || port < 0)
                {
                    return null;
                }

                var val = _queue.FirstOrDefault(x => x.RemoteDeviceInfo.IpAddress == ipAddress && x.RemoteDeviceInfo.Port == port);
                return val;
            });
        }

        internal async Task<IRemoteDevice> AddOrUpdateLinkAsync(SeekSocketModel seekSocket, string connectionId)
        {
            var remoteDevice = await GetByKeyAsync(seekSocket.DeviceKey);
            if (remoteDevice == null)
            {
                //根据Key查询不存在则找出地址相同的数据更改
                remoteDevice = await GetByIpAndPortIdAsync(seekSocket.ClientIp, seekSocket.ClientPort);
            }
            if (remoteDevice == null)
            {
                _logger.LogError($"找不到连接的设备：SeekSocket:{JsonConvert.SerializeObject(seekSocket, JsonUtil.JsonSettings)} ");
                return null;
            }

            //修改数据
            remoteDevice.RemoteDeviceInfo.IsOnline = true;
            remoteDevice.RemoteDeviceInfo.ConnectionId = connectionId;

            //设备Key与设备类型，输入错误或更换新程序时改变，不中断
            if (string.IsNullOrEmpty(remoteDevice.RemoteDeviceInfo.DeviceKey))
            {
                remoteDevice.RemoteDeviceInfo.DeviceKey = seekSocket.DeviceKey;
            }
            else if (remoteDevice.RemoteDeviceInfo.DeviceKey != seekSocket.DeviceKey)
            {
                _logger.LogError($"连接的设备Key错误：sourceKey:{remoteDevice.RemoteDeviceInfo.DeviceKey} linkKey:{seekSocket.DeviceKey} ");
                remoteDevice.RemoteDeviceInfo.DeviceKey = seekSocket.DeviceKey;
            }

            //设备连接类型不一致，不中断
            if (remoteDevice.RemoteDeviceInfo.DeviceType != seekSocket.DeviceType)
            {
                _logger.LogError($"连接的设备类型错误：sourceType:{remoteDevice.RemoteDeviceInfo.DeviceType} linkType:{seekSocket.DeviceType} ");
            }

            return remoteDevice;
        }

        internal async Task DislinkAsync(string connectionId)
        {
            await Task.Run(async () =>
            {
                var remoteDevice = await GetByConnectionIdAsync(connectionId);
                if (remoteDevice == null)
                {
                    _logger.LogInformation($"远程设备断开连接失败：connectionId:{connectionId} ");
                    return;
                }

                //取消连接数据
                remoteDevice.RemoteDeviceInfo.ConnectionId = string.Empty;
                remoteDevice.RemoteDeviceInfo.IsOnline = false;
                _logger.LogInformation($"远程设备断开连接成功：device:{JsonConvert.SerializeObject(remoteDevice, JsonUtil.JsonSettings)} ");
            });
        }

        /// <summary>
        /// 根据IP和端口查找
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal async Task<IRemoteDevice> GetByIdAndTypeAsync(string id, string type)
        {
            return await Task.Run(() =>
            {
                var val = _queue.FirstOrDefault(x => x.RemoteDeviceInfo.DeviceId == id && x.RemoteDeviceInfo.DeviceType == type);
                return val;
            });
        }
    }
}
