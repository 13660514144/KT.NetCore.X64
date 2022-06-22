using KT.Turnstile.Unit.ClientApp.Device.Common;
using KT.Turnstile.Unit.ClientApp.Device.IDevices;
using KT.Turnstile.Unit.Entity.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Device
{
    /// <summary>
    /// 所有继电器设备
    /// </summary>
    public class SocketDeviceList
    {
        private readonly ILogger _logger;
        private SocketDeviceFactory _socketDeviceFactory;

        public SocketDeviceList(ILogger logger,
            SocketDeviceFactory socketDeviceFactory)
        {
            _logger = logger;
            _socketDeviceFactory = socketDeviceFactory;
            _queue = new ConcurrentDictionary<string, ISocketDevice>();
        }

        //所有设备信息 
        private ConcurrentDictionary<string, ISocketDevice> _queue;

        //public void Init(List<ISocketDevice> devices)
        //{
        //    _queue = new ConcurrentDictionary<string, ISocketDevice>();

        //    if (devices == null)
        //    {
        //        return;
        //    }
        //    foreach (var item in devices)
        //    {
        //        var key = item.SocketDevice.RelayDeviceIp + ":" + item.SocketDevice.RelayDevicePort;
        //        _queue.TryRemove(key, out ISocketDevice oldValue);
        //        _queue.AddOrUpdate(key, item, updateValueFactory);
        //    }
        //}

        //private ISocketDevice updateValueFactory(string arg1, ISocketDevice arg2)
        //{
        //    return arg2;
        //}

        ///// <summary>
        ///// 向队列中添加接收到的数据
        ///// </summary>
        ///// <param name="data">接收到的数据</param>
        //public ISocketDevice AddOrUpdate(ISocketDevice data)
        //{
        //    _queue.TryRemove(data.SocketDevice.Id, out ISocketDevice oldValue);
        //    return _queue.AddOrUpdate(data.SocketDevice.Id, data, updateValueFactory);
        //}

        /// <summary>
        /// 根据串口名称获取设备
        /// </summary>
        /// <param name="fullAddress">串口名称</param>
        /// <returns></returns>
        public ISocketDevice GetByIpAndPort(string ip, int prot)
        {
            var key = ip + ":" + prot;
            _queue.TryGetValue(key, out ISocketDevice result);
            return result;
        }

        ///// <summary>
        ///// 根据串口名称获取设备
        ///// </summary>
        ///// <param name="key">串口名称</param>
        ///// <returns></returns>
        //public ISocketDevice GetByKey(string key)
        //{
        //    if (string.IsNullOrEmpty(key))
        //    {
        //        return null;
        //    }

        //    var val = _queue.FirstOrDefault(x => x.Key == key);
        //    return val.Value;
        //}

        public ISocketDevice RemoveByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            if (_queue.ContainsKey(key))
            {
                _queue.Remove(key, out ISocketDevice result);
                return result;
            }
            return null;
        }

        internal async Task<ISocketDevice> GetOrAddAsync(TurnstileUnitCardDeviceEntity cardDevice)
        {
            var key = cardDevice.RelayDeviceIp + ":" + cardDevice.RelayDevicePort;
            var device = GetByIpAndPort(cardDevice.RelayDeviceIp, cardDevice.RelayDevicePort);
            if (device == null)
            {
                device = await _socketDeviceFactory.CreateAsync(cardDevice);
                _queue.TryAdd(key, device);
            }
            return device;
        }
        /// <summary>
        /// 数据库取值
        /// </summary>
        /// <param name="Ip"></param>
        /// <param name="Port"></param>
        /// <returns></returns>
        internal async Task<ISocketDevice> GetOrAddIpAndPortAsync(string Ip,string Port)
        {
            var key = Ip + ":" + Port;
            var device = GetByIpAndPort(Ip, Convert.ToInt16(Port));
            if (device == null)
            {
                TurnstileUnitCardDeviceEntity cardDevice = new TurnstileUnitCardDeviceEntity();
                device = await _socketDeviceFactory.CreateAsync(cardDevice);
                _queue.TryAdd(key, device);
            }
            return device;
        }
    }
}
