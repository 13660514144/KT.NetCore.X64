using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    public class CommunicateDeviceList
    {
        private readonly ILogger<CommunicateDeviceList> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly ICommunicateDeviceFactory _communicateDeviceFactory;

        public CommunicateDeviceList(ILogger<CommunicateDeviceList> logger,
            IServiceProvider serviceProvider,
            ICommunicateDeviceFactory communicateDeviceFactory)
        {
            _logger = logger;
            _queue = new List<ICommunicateDevice>();
            _serviceProvider = serviceProvider;
            _communicateDeviceFactory = communicateDeviceFactory;
        }

        //所有设备信息 
        private List<ICommunicateDevice> _queue;
        private static object _locker = new object();

        public async Task<ICommunicateDevice> AddAsync(CommunicateDeviceInfoModel communicateDeviceModel, RemoteDeviceModel remoteDevice)
        {
            if (communicateDeviceModel == null)
            {
                //throw CustomException.Run($"初始化不存在的远程设备：{JsonConvert.SerializeObject(communicateDeviceModel, JsonUtil.JsonPrintSettings)} ");
                throw CustomException.Run($"初始化不存在的远程设备：ip=>{communicateDeviceModel.IpAddress} type=>{communicateDeviceModel.CommunicateDeviceType} {communicateDeviceModel.CommunicateModeType} port=>{communicateDeviceModel.Port} connectedid=>{communicateDeviceModel.ConnectionId}");
            }

            var communicateDevice = await _communicateDeviceFactory.CreatorAsync(communicateDeviceModel, remoteDevice);

            lock (_locker)
            {
                _queue.Add(communicateDevice);

                //_logger.LogInformation($"初始化远程设备完成：{JsonConvert.SerializeObject(communicateDeviceModel, JsonUtil.JsonPrintSettings)} ");
                _logger.LogInformation($"初始化不存在的远程设备：ip=>{communicateDeviceModel.IpAddress} type=>{communicateDeviceModel.CommunicateDeviceType} {communicateDeviceModel.CommunicateModeType} port=>{communicateDeviceModel.Port} connectedid=>{communicateDeviceModel.ConnectionId}");
            }

            return communicateDevice;
        }

        /// <summary>
        /// 轮询所有边缘处理器执行
        /// </summary>
        /// <param name="execAction">执行的委托定义</param>
        public async Task ExecuteAsync(Func<ICommunicateDevice, Task> execAction)
        {
            foreach (var item in _queue)
            {
                try
                {
                    await execAction.Invoke(item);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"执行通信设备出错误：{ex} ");
                }
            }
        }

        /// <summary>
        /// 根据条件对相应设备执行操作
        /// </summary>
        /// <param name="whereAction">设备过滤条件</param>
        /// <param name="execAction">执行的操作</param>
        public async Task ExecuteAsync(Func<ICommunicateDevice, bool> whereAction, Func<ICommunicateDevice, Task> execAction)
        {
            foreach (var item in _queue)
            {
                try
                {
                    await ItemExecuteAsync(whereAction, execAction, item);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"执行通信设备出错误：{ex} ");
                }
            }
        }

        public Task<int> GetCountAsync()
        {
            return Task.FromResult(_queue.Count);
        }
        public Task<int> GetCountAsync(Func<ICommunicateDevice, bool> where)
        {
            var count = _queue.Count(where);
            return Task.FromResult(count);
        }

        /// <summary>
        /// 根据条件对相应设备执行操作
        /// </summary>
        /// <param name="whereAction">设备过滤条件</param>
        /// <param name="execAction">执行的操作</param>
        public Task AsyncExecuteAsync(Func<ICommunicateDevice, bool> whereAction, Func<ICommunicateDevice, Task> execAction)
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
                        _logger.LogError(ex, "异步执行通信设备出错！");
                    }
                });
            }
            return Task.CompletedTask;
        }

        private async Task ItemExecuteAsync(Func<ICommunicateDevice, bool> whereAction, Func<ICommunicateDevice, Task> execAction, ICommunicateDevice item)
        {
            
            try
            {
                _logger.LogInformation($"执行通信设备条件过虑！");
                var isCondition = whereAction.Invoke(item);
                if (isCondition)
                {
                    //_logger.LogInformation($"开始执行通信设备：deivce:{JsonConvert.SerializeObject(item.CommunicateDeviceInfo, JsonUtil.JsonPrintSettings)} ");
                    _logger.LogInformation($"开始执行通信设备：deivce:{item.CommunicateDeviceInfo.IpAddress}:{item.CommunicateDeviceInfo.Port}:{item.CommunicateDeviceInfo.ConnectionId}:{item.CommunicateDeviceInfo.CommunicateDeviceType}:{item.CommunicateDeviceInfo.CommunicateModeType} ");
                    await execAction?.Invoke(item);
                }
                else
                {
                    //_logger.LogInformation($"执行通信设备条件不符：deivce:{JsonConvert.SerializeObject(item.CommunicateDeviceInfo, JsonUtil.JsonPrintSettings)} ");
                    _logger.LogInformation($"执行通信设备条件不符：deivce:{item.CommunicateDeviceInfo.IpAddress}:{item.CommunicateDeviceInfo.Port}:{item.CommunicateDeviceInfo.ConnectionId}:{item.CommunicateDeviceInfo.CommunicateDeviceType}:{item.CommunicateDeviceInfo.CommunicateModeType} ");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"执行通信设备错误-==>：deivce:ex:{ex} ");
                _logger.LogInformation($"执行通信设备条件错误==>：deivce:{item.CommunicateDeviceInfo.IpAddress}:{item.CommunicateDeviceInfo.Port}:{item.CommunicateDeviceInfo.ConnectionId}:{item.CommunicateDeviceInfo.CommunicateDeviceType}:{item.CommunicateDeviceInfo.CommunicateModeType} ");
            }
            
        }


        public Task<ICommunicateDevice> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var val = _queue.FirstOrDefault(x => x.CommunicateDeviceInfo.Id == id);

            return Task.FromResult(val);
        }

        public Task<ICommunicateDevice> GetByConnectionIdAsync(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
            {
                return null;
            }

            var val = _queue.FirstOrDefault(x => x.CommunicateDeviceInfo.ConnectionId == connectionId);

            return Task.FromResult(val);
        }

        internal async Task DislinkAsync(string connectionId)
        {
            var remoteDevice = await GetByConnectionIdAsync(connectionId);
            if (remoteDevice == null)
            {
                _logger.LogInformation($"远程设备断开连接失败：connectionId:{connectionId} ");
                return;
            }

            //取消连接数据
            remoteDevice.CommunicateDeviceInfo.ConnectionId = string.Empty;
            remoteDevice.CommunicateDeviceInfo.IsOnline = false;

            if (remoteDevice.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_ELI.Value
            || remoteDevice.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_RCGIF.Value)
            {
                _logger.LogError("Not can delete kone server!");
                return;
            }

            //_logger.LogWarning($"删除通迅设备：{JsonConvert.SerializeObject(remoteDevice, JsonUtil.JsonPrintSettings)} ");
            _logger.LogWarning($"删除通迅设备：{remoteDevice.CommunicateDeviceInfo.IpAddress}:{remoteDevice.CommunicateDeviceInfo.Port}:{remoteDevice.CommunicateDeviceInfo.ConnectionId} ");
            //从队列中删除数据
            var isTake = _queue.Remove(remoteDevice);
            if (!isTake)
            {
                //_logger.LogError($"删除通迅设备内存数据错误：{JsonConvert.SerializeObject(remoteDevice, JsonUtil.JsonPrintSettings)} ");
                _logger.LogWarning($"删除通迅设备内存数据错误：{remoteDevice.CommunicateDeviceInfo.IpAddress}:{remoteDevice.CommunicateDeviceInfo.Port}:{remoteDevice.CommunicateDeviceInfo.ConnectionId} ");
            }

            //_logger.LogWarning($"删除通迅设备：device:{JsonConvert.SerializeObject(remoteDevice, JsonUtil.JsonPrintSettings)} ");
            _logger.LogWarning($"删除通迅设备：{remoteDevice.CommunicateDeviceInfo.IpAddress}:{remoteDevice.CommunicateDeviceInfo.Port}:{remoteDevice.CommunicateDeviceInfo.ConnectionId} ");
        }

        public async Task<ICommunicateDevice> AddOrEditByAddressAndUserAsync(CommunicateDeviceInfoModel communicateDeviceInfo, RemoteDeviceModel remoteDeviceModel)
        {
            var result = GetByAddressAndUserAsync(communicateDeviceInfo.CommunicateDeviceType,
                communicateDeviceInfo.CommunicateModeType,
                communicateDeviceInfo.IpAddress,
                communicateDeviceInfo.Port,
                communicateDeviceInfo.Account,
                communicateDeviceInfo.Password);

            if (result == null)
            {
                result = await AddAsync(communicateDeviceInfo, remoteDeviceModel);
            }
            else
            {
                //_logger.LogError($"存在的通信息设备！{communicateDeviceInfo.IpAddress}:{communicateDeviceInfo.Port} ");
                _logger.LogError($"存在的通信息设备！{communicateDeviceInfo.IpAddress}:{communicateDeviceInfo.Port} ");
            }

            return result;
        }

        public async Task<ICommunicateDevice> AddOrEditByAddressAsync(CommunicateDeviceInfoModel communicateDeviceInfo, RemoteDeviceModel remoteDeviceModel)
        {
            var result = GetByAddressAsync(communicateDeviceInfo.CommunicateDeviceType,
                communicateDeviceInfo.CommunicateModeType,
                communicateDeviceInfo.IpAddress,
                communicateDeviceInfo.Port);

            if (result == null)
            {
                result = await AddAsync(communicateDeviceInfo, remoteDeviceModel);
            }
            else
            {
                // _logger.LogError($"存在的通信息设备！{communicateDeviceInfo.IpAddress}:{communicateDeviceInfo.Port} ");
                _logger.LogError($"存在的通信息设备！{communicateDeviceInfo.IpAddress}:{communicateDeviceInfo.Port}:{communicateDeviceInfo.ConnectionId}:{communicateDeviceInfo.CommunicateDeviceType}:{communicateDeviceInfo.CommunicateModeType}");
            }

            return result;
        }

        public ICommunicateDevice GetByAddressAndUserAsync(string communicateDeviceType, string communicateModeType, string ipAddress = "127.0.0.1", int port = 80, string account = "", string password = "")
        {
            var result = _queue.FirstOrDefault(x => x.CommunicateDeviceInfo.CommunicateDeviceType == communicateDeviceType
                && x.CommunicateDeviceInfo.CommunicateModeType == communicateModeType
                && x.CommunicateDeviceInfo.IpAddress == ipAddress
                && x.CommunicateDeviceInfo.Port == port
                && x.CommunicateDeviceInfo.Account == account
                && x.CommunicateDeviceInfo.Password == password);

            return result;
        }

        public ICommunicateDevice GetByAddressAsync(string communicateDeviceType, string communicateModeType, string ipAddress = "127.0.0.1", int port = 80)
        {
            var result = _queue.FirstOrDefault(x => x.CommunicateDeviceInfo.CommunicateDeviceType == communicateDeviceType
                && x.CommunicateDeviceInfo.CommunicateModeType == communicateModeType
                && x.CommunicateDeviceInfo.IpAddress == ipAddress
                && x.CommunicateDeviceInfo.Port == port);

            return result;
        }

        /// <summary>
        /// 根据删除数据
        /// </summary>
        /// <param name="key">设备Key</param> 
        public async Task<ICommunicateDevice> RemoveByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var oldItem = await GetByIdAsync(id);
            if (oldItem != null)
            {
                //_logger.LogWarning($"删除通迅设备：{JsonConvert.SerializeObject(oldItem, JsonUtil.JsonPrintSettings)} ");
                _logger.LogWarning($"删除通迅设备：{oldItem.CommunicateDeviceInfo.IpAddress} {oldItem.CommunicateDeviceInfo.Port} {oldItem.CommunicateDeviceInfo.ConnectionId}");
                var isTake = _queue.Remove(oldItem);
                if (!isTake)
                {
                    _logger.LogError($"删除通迅设备内存数据错误！");
                }

                //_logger.LogWarning($"删除通迅设备：device:{JsonConvert.SerializeObject(oldItem, JsonUtil.JsonPrintSettings)} ");
                return oldItem;
            }
            return null;
        }

        /// <summary>
        /// 根据IP和端口查找
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal Task<ICommunicateDevice> GetByValueAsync(string ipAddress, int port, string account, string password)
        {
            ipAddress = ipAddress.IsNull();
            port = port.IsNull();
            account = account.IsNull();
            password = password.IsNull();

            var val = _queue.FirstOrDefault(x => x.CommunicateDeviceInfo.IpAddress == ipAddress
                    && x.CommunicateDeviceInfo.Port == port
                    && x.CommunicateDeviceInfo.Account == account
                    && x.CommunicateDeviceInfo.Password == password);

            return Task.FromResult(val);
        }
        /// <summary>
        /// 根据IP和端口查找
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        internal Task<List<ICommunicateDevice>> GetByIdAndPortAsync(string ipAddress, int port)
        {
            ipAddress = ipAddress.IsNull();
            port = port.IsNull();

            var val = _queue.Where(x => x.CommunicateDeviceInfo.IpAddress == ipAddress
                    && x.CommunicateDeviceInfo.Port == port)
                .ToList();

            return Task.FromResult(val);
        }

        internal async Task LinkAsync(string ipAddress, int port, string connectionId)
        {
            var devices = await GetByIdAndPortAsync(ipAddress, port);
            if (devices != null)
            {
                foreach (var item in devices)
                {
                    item.CommunicateDeviceInfo.ConnectionId = connectionId;
                    item.CommunicateDeviceInfo.IsOnline = true;
                }
            }
        }
    }
}
