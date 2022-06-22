using KT.Common.Core.Utils;
using KT.Quanta.Common.Models;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    public class RemoteDeviceList
    {
        private readonly ILogger<RemoteDeviceList> _logger;
        private IServiceProvider _serviceProvider;
        private IRemoteDeviceFactory _remoteDeviceFactory;
        private CommunicateDeviceList _communicateDeviceList;
        private readonly AppSettings _appSettings;

        public RemoteDeviceList(ILogger<RemoteDeviceList> logger,
            IServiceProvider serviceProvider,
            IRemoteDeviceFactory remoteDeviceFactory,
            CommunicateDeviceList communicateDeviceList,
            IOptions<AppSettings> appSetings)
        {
            _logger = logger;
            _queue = new List<IRemoteDevice>();
            _serviceProvider = serviceProvider;
            _remoteDeviceFactory = remoteDeviceFactory;
            _communicateDeviceList = communicateDeviceList;
            _appSettings = appSetings.Value;
        }

        //所有设备信息 
        private List<IRemoteDevice> _queue;

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

        public Task<List<IRemoteDevice>> GetByConnectionIdAsync(string communicateId)
        {
            var results = _queue.Where(x => x.CommunicateDevices?.Any(y => y.CommunicateDeviceInfo.ConnectionId == communicateId) == true).ToList();
            return Task.FromResult(results);
        }

        public Task<List<IRemoteDevice>> GetByConnectionIdAndDeviceTypeAsync(string communicateId, string deviceType)
        {
            var results = _queue.Where(x => x.CommunicateDevices?.Any(y => y.CommunicateDeviceInfo.ConnectionId == communicateId) == true
                    && x.RemoteDeviceInfo.DeviceType == deviceType).ToList();
            return Task.FromResult(results);
        }

        public Task<List<IRemoteDevice>> GetByIpAndPortAsync(string ipAndPort)
        {
            var results = _queue.Where(x => x.CommunicateDevices?.Any(y =>
                ipAndPort == $"{y.CommunicateDeviceInfo.IpAddress}:{y.CommunicateDeviceInfo.Port}") == true)
            .ToList();
            return Task.FromResult(results);
        }

        private static object _locker = new object();
        public async Task<List<IRemoteDevice>> AddAsync(RemoteDeviceModel device)
        {
            if (device == null)
            {
                //_logger.LogWarning($"初始化不存在的远程设备：{JsonConvert.SerializeObject(device, JsonUtil.JsonPrintSettings)} ");
                _logger.LogWarning($"\r\n==>初始化不存在的远程设备：{device.DeviceType}:{device.DeviceId}:{device.ParentId} :{device.BrandModel}:{device.ParentId} ");
                return null;
            }

            var remoteDevices = await _remoteDeviceFactory.CreatorAsync(device);
            if (remoteDevices?.FirstOrDefault() != null)
            {
                foreach (var item in remoteDevices)
                {
                    lock (_locker)
                    {
                        _queue.Add(item);
                        //_logger.LogInformation($"初始化远程设备完成：hasCode:{device.GetHashCode()} device:{JsonConvert.SerializeObject(device, JsonUtil.JsonPrintSettings)} ");
                        _logger.LogWarning($"\r\n==>初始化远程设备完成：{device.DeviceType}:{device.DeviceId}:{device.ParentId} :{device.BrandModel}:{device.ParentId} ");
                    }
                }
            }

            return remoteDevices;
        }


        public Task<int> GetCountAsync(Func<IRemoteDevice, bool> where)
        {
            var count = _queue.Count(where);

            return Task.FromResult(count);
        }

        /// <summary>
        /// 获取所有设备
        /// </summary>
        /// <returns></returns> 
        public Task<List<IRemoteDevice>> GetAllAsync(Func<IRemoteDevice, bool> action)
        {
            var results = new List<IRemoteDevice>();

            foreach (var item in _queue)
            {
                if (action.Invoke(item))
                {
                    results.Add(item);
                }
            }

            return Task.FromResult(results);
        }

        /// <summary>
        /// 轮询所有边缘处理器执行
        /// </summary>
        /// <param name="execAction">执行的委托定义</param>
        public async Task ExecuteAsync(Func<IRemoteDevice, Task> execAction)
        {
            foreach (var item in _queue)
            {
                try
                {
                    await execAction.Invoke(item);
                }
                catch (Exception ex)
                {
                    //_logger.LogError($"执行远程操作错误：deivce:{JsonConvert.SerializeObject(item, JsonUtil.JsonPrintSettings)} ex:{ex} ");
                    _logger.LogError($"执行远程操作错误：deivce: ex:{ex} ");
                }
            }
        }

        /// <summary>
        /// 根据条件对相应设备执行操作
        /// </summary>
        /// <param name="whereAction">设备过滤条件</param>
        /// <param name="execAction">执行的操作</param>
        public async Task ExecuteAsync(Func<IRemoteDevice, bool> whereAction, Func<IRemoteDevice, Task> execAction)
        {
            foreach (var item in _queue)
            {
                await ItemExecuteAsync(whereAction, execAction, item);
            }
        }
        /// <summary>
        /// 根据条件对相应设备执行操作
        /// </summary>
        /// <param name="whereAction">设备过滤条件</param>
        /// <param name="execAction">执行的操作</param>
        public Task AsyncExecuteAsync(Func<IRemoteDevice, bool> whereAction, Func<IRemoteDevice, Task> execAction)
        {
            foreach (var item in _queue)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        await ItemExecuteAsync(whereAction, execAction, item);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "异步执行远程操作错误！");
                    }
                });
            }
            return Task.CompletedTask;
        }

        private async Task ItemExecuteAsync(Func<IRemoteDevice, bool> whereAction, Func<IRemoteDevice, Task> execAction, IRemoteDevice item)
        {
            try
            {
                //_logger.LogInformation($"执行远程操作条件过虑！=={JsonConvert.SerializeObject(item)}");
               
                var isCondition = whereAction.Invoke(item);
                //_logger.LogInformation($"执行远程操作条件过虑！isCondition=={JsonConvert.SerializeObject(isCondition)}");
                if (isCondition)
                {
                    //
                    //_logger.LogInformation($"开始执行远程操作：item.RemoteDeviceInfo:{item.RemoteDeviceInfo.CardDeviceType}:{item.RemoteDeviceInfo.BrandModel}:{item.RemoteDeviceInfo.DeviceType}:{item.RemoteDeviceInfo.DeviceId} ");
                    await execAction?.Invoke(item);
                }
                else
                {
                    //_logger.LogInformation($"执行远程操作条件不符：deivce:{JsonConvert.SerializeObject(item.RemoteDeviceInfo, JsonUtil.JsonPrintSettings)} ");
                    //_logger.LogInformation($"执行远程操作条件不符：item.RemoteDeviceInfo:{item.RemoteDeviceInfo.CardDeviceType}:{item.RemoteDeviceInfo.BrandModel}:{item.RemoteDeviceInfo.DeviceType}:{item.RemoteDeviceInfo.DeviceId} ");
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError($"执行远程操作错误：deivce:{JsonConvert.SerializeObject(item.RemoteDeviceInfo, JsonUtil.JsonPrintSettings)} ex:{ex} ");
                _logger.LogInformation($"执行远程操作错误：item.RemoteDeviceInfo:{ex.Message} ");
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
            if (remoteDevice == null)
            {
                remoteDevice = await GetByIdAsync(data.DeviceId);
            }

            //如果存在修改内容
            if (remoteDevice != null)
            {
                //修改内容
                remoteDevice.RemoteDeviceInfo.RealId = data.RealId;

                //检查修改的值是否错误
                if (remoteDevice.RemoteDeviceInfo.DeviceType != data.DeviceType)
                {
                    _logger.LogError($"加入队列的设备类型错误：oldType:{remoteDevice.RemoteDeviceInfo.DeviceType} newType:{data.DeviceType} ");                    
                }
                if (remoteDevice.RemoteDeviceInfo.DeviceId != data.DeviceId)
                {
                    _logger.LogError($"加入队列的设备Id错误：oldId:{remoteDevice.RemoteDeviceInfo.DeviceId} newId:{data.DeviceId} ");
                }

                //更新通信设备通信类型
                data = _remoteDeviceFactory.RefreshCommunicateTypeByDeviceType(data);

                //TODOO-更改连接设备
                await remoteDevice.InitAsync(data);
            }
            else
            {
                //创建对像
                var remoteDevices = await _remoteDeviceFactory.CreatorAsync(data);
                remoteDevice = remoteDevices?.FirstOrDefault();
                if (remoteDevice != null)
                {
                    //更改状态并加入队列
                    _queue.Add(remoteDevice);
                }
            }

            return remoteDevice;
        }

        public Task<IRemoteDevice> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Task.FromResult<IRemoteDevice>(null);
            }
            //_logger.LogInformation($"_queue==>>{JsonConvert.SerializeObject(_queue)}");

            var val = _queue.FirstOrDefault(x => x.RemoteDeviceInfo.DeviceId == id);
            return Task.FromResult(val);
        }

        /// <summary>
        /// 根据删除数据
        /// </summary>
        /// <param name="key">设备Key</param> 
        public async Task<IRemoteDevice> RemoveByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var oldItem = await GetByIdAsync(id);
            if (oldItem != null)
            {
                //_logger.LogWarning($"删除远程设备：{JsonConvert.SerializeObject(oldItem, JsonUtil.JsonPrintSettings)} ");
                //从队列中删除数据
                var isTake = _queue.Remove(oldItem);
                if (!isTake)
                {
                   // _logger.LogError($"删除远程设备内存数据错误：{JsonConvert.SerializeObject(oldItem, JsonUtil.JsonPrintSettings)} ");
                }

                //_logger.LogWarning($"删除远程设备：device:{JsonConvert.SerializeObject(oldItem, JsonUtil.JsonPrintSettings)} ");
                _logger.LogWarning($"删除远程设备成功 ");

                return oldItem;
            }
            return null;
        }
        internal async Task<IRemoteDevice> AddOrUpdateLinkAsync(SeekSocketModel seekSocket, string connectionId)
        {
            var remoteDevices = await GetByIpAndPortAsync(seekSocket.ClientIp, seekSocket.ClientPort);
            if (remoteDevices?.FirstOrDefault() == null)
            {
                _logger.LogError($"找不到连接的设备：SeekSocket:{JsonConvert.SerializeObject(seekSocket, JsonUtil.JsonPrintSettings)}===>{connectionId} ");
                return null;
            }

            //修改数据 
            await _communicateDeviceList.LinkAsync(seekSocket.ClientIp, seekSocket.ClientPort, connectionId);

            //再次更改设备连接状态
            foreach (var item in remoteDevices)
            {
                if (item.CommunicateDevices?.FirstOrDefault() != null)
                {
                    foreach (var obj in item.CommunicateDevices)
                    {
                        if (obj.CommunicateDeviceInfo.IpAddress == seekSocket.ClientIp
                            && obj.CommunicateDeviceInfo.Port == seekSocket.ClientPort)
                        {
                            obj.CommunicateDeviceInfo.ConnectionId = connectionId;
                            obj.CommunicateDeviceInfo.IsOnline = true;
                        }
                    }
                }
            }

            return remoteDevices.FirstOrDefault();
        }

        private Task<List<IRemoteDevice>> GetByIpAndPortAsync(string ip, int port)
        {
            //_logger.LogInformation($"设备列表=={JsonConvert.SerializeObject(_queue)}");
            var val = _queue.Where(x => x.CommunicateDevices?.Any(y =>
                y.CommunicateDeviceInfo.IpAddress == ip
                && y.CommunicateDeviceInfo.Port == port) == true)
                .ToList();
            //_logger.LogInformation($"查询结果==val{JsonConvert.SerializeObject(val)}\r\n==>ip:{ip}  port==>{port}");
            return Task.FromResult(val);
        }

        public async Task DislinkAsync(string connectionId)
        {
            var remoteDevice = await GetByConnectionIdAsync(connectionId);
            if (remoteDevice == null)
            {
                _logger.LogInformation($"远程设备断开连接失败：connectionId:{connectionId} ");
                return;
            }

            //取消连接数据
            await _communicateDeviceList.DislinkAsync(connectionId);

            _logger.LogInformation($"远程设备断开连接成功：device:{JsonConvert.SerializeObject(remoteDevice, JsonUtil.JsonPrintSettings)} ");
        }
    }
}
