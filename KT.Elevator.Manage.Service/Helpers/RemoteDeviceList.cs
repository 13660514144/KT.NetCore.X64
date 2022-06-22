using KT.Common.Core.Utils;
using KT.Elevator.Manage.Service.Models;
using KT.Quanta.Common.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Helpers
{
    /// <summary>
    /// 边缘处理器数据
    /// </summary>
    public class RemoteDeviceList
    {
        private ushort _terminalId = 1;
        private readonly ILogger<RemoteDeviceList> _logger;

        public RemoteDeviceList(ILogger<RemoteDeviceList> logger)
        {
            _logger = logger;
            _queue = new ConcurrentBag<RemoteDeviceModel>();
        }

        //所有设备信息 
        private ConcurrentBag<RemoteDeviceModel> _queue;

        public void Adds(List<RemoteDeviceModel> devices)
        {
            if (devices == null || devices.FirstOrDefault() == null)
            {
                _logger.LogWarning($"初始化不存在的远程设备！");
                return;
            }
            foreach (var item in devices)
            {
                item.TerminalId = _terminalId++;
                _queue.Add(item);
            }
            _logger.LogInformation($"初始化远程设备完成：{JsonConvert.SerializeObject(devices, JsonUtil.JsonSettings)} ");
        }

        public RemoteDeviceModel GetByTerminalId(ushort terminalId)
        {
            var val = _queue.FirstOrDefault(x => x.TerminalId == terminalId);
            return val;
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
                    Id = x.DeviceId,
                    IsOnline = x.IsOnline
                };
            }).ToList();

            return results;
        }

        /// <summary>
        /// 轮询所有边缘处理器执行
        /// </summary>
        /// <param name="action">执行的委托定义</param>
        public async Task ExecAllAsync(Action<RemoteDeviceModel> action)
        {
            await Task.Run(() =>
            {
                foreach (var item in _queue)
                {
                    action.Invoke(item);
                }
            });
        }

        /// <summary>
        /// 向队列中添加接收到的数据,如果存在，则先删除再加入
        /// </summary>
        /// <param name="data">接收到的数据</param>
        public RemoteDeviceModel AddOrUpdateContent(RemoteDeviceModel data)
        {
            RemoteDeviceModel remoteDevice = null;
            //查询已经存在的数据 1、Key 2、ip port 3、device id(后台id)
            if (!string.IsNullOrEmpty(data.DeviceKey))
            {
                remoteDevice = _queue.FirstOrDefault(x => x.DeviceKey == data.DeviceKey);
            }
            if (remoteDevice == null)
            {
                remoteDevice = GetByIpAndPortId(data.IpAddress, data.Port);
            }
            if (remoteDevice == null)
            {
                remoteDevice = GetByIdAndType(data.DeviceId, data.ProductType);
            }

            //如果存在修改内容
            if (remoteDevice != null)
            {
                //修改内容
                remoteDevice.RealId = data.RealId;
                remoteDevice.IpAddress = data.IpAddress;
                remoteDevice.Port = data.Port;

                //检查修改的值是否错误
                if (remoteDevice.ProductType != data.ProductType)
                {
                    _logger.LogError($"加入队列的设备类型错误：oldType:{remoteDevice.ProductType} newType:{data.ProductType} ");
                }
                if (remoteDevice.DeviceId != data.DeviceId)
                {
                    _logger.LogError($"加入队列的设备Id错误：oldId:{remoteDevice.DeviceId} newId:{data.DeviceId} ");
                }
            }
            else
            {
                //创建对像
                remoteDevice = data;
                remoteDevice.TerminalId = _terminalId++;

                //更改状态并加入队列
                _queue.Add(remoteDevice);
            }

            return remoteDevice;
        }

        public RemoteDeviceModel GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var val = _queue.FirstOrDefault(x => x.DeviceId == id);
            return val;
        }

        public RemoteDeviceModel GetByConnectionId(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
            {
                return null;
            }

            var val = _queue.FirstOrDefault(x => x.ConnectionId == connectionId);
            return val;
        }

        /// <summary>
        /// 根据Key称获取设备
        /// </summary>
        /// <param name="key">设备Key</param> 
        public RemoteDeviceModel GetByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            var val = _queue.FirstOrDefault(x => x.DeviceKey == key);
            return val;
        }

        /// <summary>
        /// 根据删除数据
        /// </summary>
        /// <param name="key">设备Key</param> 
        public RemoteDeviceModel RemoveByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            var oldItem = GetByKey(key);
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
        internal RemoteDeviceModel GetByIpAndPortId(string ipAddress, int port)
        {
            if (string.IsNullOrEmpty(ipAddress) || port < 0)
            {
                return null;
            }

            var val = _queue.FirstOrDefault(x => x.IpAddress == ipAddress && x.Port == port);
            return val;
        }

        internal RemoteDeviceModel AddOrUpdateLink(SeekSocketModel seekSocket, string connectionId)
        {
            var remoteDevice = GetByKey(seekSocket.DeviceKey);
            if (remoteDevice == null)
            {
                //根据Key查询不存在则找出地址相同的数据更改
                remoteDevice = GetByIpAndPortId(seekSocket.ClientIp, seekSocket.ClientPort);
            }
            if (remoteDevice == null)
            {
                _logger.LogError($"找不到连接的设备：SeekSocket:{JsonConvert.SerializeObject(seekSocket, JsonUtil.JsonSettings)} ");
                return null;
            }

            //修改数据
            remoteDevice.IsOnline = true;
            remoteDevice.ConnectionId = connectionId;

            //设备Key与设备类型，输入错误或更换新程序时改变，不中断
            if (string.IsNullOrEmpty(remoteDevice.DeviceKey))
            {
                remoteDevice.DeviceKey = seekSocket.DeviceKey;
            }
            else if (remoteDevice.DeviceKey != seekSocket.DeviceKey)
            {
                _logger.LogError($"连接的设备Key错误：sourceKey:{remoteDevice.DeviceKey} linkKey:{seekSocket.DeviceKey} ");
                remoteDevice.DeviceKey = seekSocket.DeviceKey;
            }

            //设备连接类型不一致，不中断
            if (remoteDevice.ProductType != seekSocket.DeviceType)
            {
                _logger.LogError($"连接的设备类型错误：sourceType:{remoteDevice.ProductType} linkType:{seekSocket.DeviceType} ");
            }

            return remoteDevice;
        }

        internal void Dislink(string connectionId)
        {
            var remoteDevice = GetByConnectionId(connectionId);
            if (remoteDevice == null)
            {
                _logger.LogInformation($"远程设备断开连接失败：connectionId:{connectionId} ");
                return;
            }

            //取消连接数据
            remoteDevice.ConnectionId = string.Empty;
            remoteDevice.IsOnline = false;
            _logger.LogInformation($"远程设备断开连接成功：device:{JsonConvert.SerializeObject(remoteDevice, JsonUtil.JsonSettings)} ");
        }

        /// <summary>
        /// 根据IP和端口查找
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal RemoteDeviceModel GetByIdAndType(string id, string type)
        {
            var val = _queue.FirstOrDefault(x => x.DeviceId == id && x.ProductType == type);
            return val;
        }
    }
}
