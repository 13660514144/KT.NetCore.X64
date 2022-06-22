using KT.Elevator.Manage.Service.Devices.Quanta.DistributeDatas;
using KT.Elevator.Manage.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace KT.Elevator.Manage.Service.Devices.Kone
{
    /// <summary>
    /// 电梯服务组
    /// </summary>
    public class KoneServerGroup
    {
        private List<KoneServer> _koneServers;
        private ElevatorGroupModel _elevatorGroup;

        private ILogger<KoneServerGroup> _logger;
        private IServiceProvider _serviceProvider;
        private QuantaDisplayDistributeDataService _pushDataHubApi;

        public KoneServerGroup(ILogger<KoneServerGroup> logger,
            IServiceProvider serviceProvider,
            QuantaDisplayDistributeDataService pushDataHubApi)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _pushDataHubApi = pushDataHubApi;

            _koneServers = new List<KoneServer>();
        }

        public void Init(ElevatorGroupModel elevatorGroup)
        {
            _elevatorGroup = elevatorGroup;

            InitElevatorGroup();

            //if (points == null || points.Count == 0)
            //{
            //    _logger.LogWarning("Kone客户端不存在！");
            //    return;
            //}
            //foreach (var item in points)
            //{
            //    var client = _serviceProvider.GetRequiredService<KoneServer>();
            //    client.InitSocket(item.Key, item.Value, ReceivedHandler, ErrorHandler);

            //    client.ListenHeartbit();
            //}
        }

        private void InitElevatorGroup()
        {
            foreach (var item in _elevatorGroup.ElevatorServers)
            {
                var koneServer = _serviceProvider.GetRequiredService<KoneServer>();
                koneServer.Init(item);

                _koneServers.Add(koneServer);
            }
        }

        internal void CallPaddle(int terminalId, int destCallType, int sourceSide, int sourceFloor, int destinationFloor, int destinationSide)
        {
            foreach (var item in _koneServers)
            {
                item.CallPaddle(terminalId, destCallType, sourceSide, sourceFloor, destinationFloor, destinationSide);
            }
        }

        //private async Task ReceivedHandlerAsync(MsgPaddle paddle)
        //{
        //    _logger.LogInformation("派梯接收数据：{0}", JsonConvert.SerializeObject(paddle, JsonUtil.JsonSettings));
        //    //using (var scope = _serviceProvider.CreateScope())
        //    //{
        //    //    var pushDataHubApi = scope.ServiceProvider.GetRequiredService<PushDataHubApi>();
        //    //    await pushDataHubApi.HandleElevatorSuccess(paddle);
        //    //}         

        //    await _pushDataHubApi.HandleElevatorSuccess(paddle);
        //} 
    }
}
