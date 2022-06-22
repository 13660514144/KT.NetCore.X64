using KT.Quanta.Service.Devices.Mitsubishi.Toward.ElsgwServers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    public class MitsubishiTowardElsgwUdpServerHostList
    {
        private readonly ILogger<MitsubishiTowardElsgwUdpServerHostList> _logger;

        public MitsubishiTowardElsgwUdpServerHostList(ILogger<MitsubishiTowardElsgwUdpServerHostList> logger)
        {
            _logger = logger;
            _queue = new List<IMitsubishiTowardElsgwUdpServerHost>();
        }

        //所有设备信息 
        private List<IMitsubishiTowardElsgwUdpServerHost> _queue;

        public Task<IMitsubishiTowardElsgwUdpServerHost> AddAsync(IMitsubishiTowardElsgwUdpServerHost mitsubishiTowardGroup)
        {
            _queue.Add(mitsubishiTowardGroup);

            return Task.FromResult(mitsubishiTowardGroup);
        }

        /// <summary>
        /// 轮询所有边缘处理器执行
        /// </summary>
        /// <param name="execAction">执行的委托定义</param>
        public async Task ExecuteAsync(Func<IMitsubishiTowardElsgwUdpServerHost, Task> execAction)
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

        public async Task<IMitsubishiTowardElsgwUdpServerHost> GetByElevatorAddressAsync(EndPoint remoteAddress)
        {
            var ipEndPoint = (IPEndPoint)remoteAddress;

            return await GetByElevatorIpAndElevatorPortAsync(ipEndPoint.Address.ToString(), ipEndPoint.Port);

        }

        /// <summary>
        /// 根据条件对相应设备执行操作
        /// </summary>
        /// <param name="whereAction">设备过滤条件</param>
        /// <param name="execAction">执行的操作</param>
        public async Task ExecuteAsync(Func<IMitsubishiTowardElsgwUdpServerHost, bool> whereAction, Func<IMitsubishiTowardElsgwUdpServerHost, Task> execAction)
        {
            foreach (var item in _queue)
            {
                try
                {
                    var isCondition = whereAction.Invoke(item);
                    if (isCondition)
                    {
                        await execAction?.Invoke(item);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"执行通信设备出错误：{ex} ");
                }
            }
        }

        public Task<IMitsubishiTowardElsgwUdpServerHost> GetByElevatorIpAndElevatorPortAsync(string elevatorIp, int elevatorPort)
        {
            var val = _queue.FirstOrDefault(x => elevatorIp.Contains(x.MitsubishiElipClientHost.RemoteIp.ToString())
                    && x.MitsubishiElipClientHost.RemotePort == elevatorPort);

            return Task.FromResult(val);
        }
    }
}
