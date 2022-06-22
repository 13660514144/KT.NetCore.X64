using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Hubs;
using KT.Quanta.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using KT.Quanta.Service.Helpers;
using KT.Quanta.WebApi.Common.WsSocket;

namespace KT.Quanta.Service.Devices.Quanta.DistributeDatas
{
    public class QuantaHandleElevatorDeviceDistributeDataService : IQuantaHandleElevatorDeviceDistributeDataService
    {
        private ILogger<IQuantaHandleElevatorDeviceDistributeDataService> _logger;
        private IHubContext<QuantaDistributeHub> _distributeHub;
        private DistributeHelper _distributeHelper;
        private WsSocket _WsSocket;

        public QuantaHandleElevatorDeviceDistributeDataService(ILogger<IQuantaHandleElevatorDeviceDistributeDataService> logger,
            IHubContext<QuantaDistributeHub> distributeHub, 
            DistributeHelper distributeHelper, WsSocket _wsSocket)
        {
            _logger = logger;
            _distributeHub = distributeHub;
            _distributeHelper = distributeHelper;
            _WsSocket = _wsSocket;
        }

        public async Task AddOrEditAsync(RemoteDeviceModel remoteDevice, UnitHandleElevatorDeviceModel result)
        {
            await _distributeHelper.DistributeAsync(remoteDevice, result,
               async (remoteDevice) =>
               {
                   //ws socket
                   var T = _WsSocket.theServer.GetAllConnections();
                   for (int x = 0; x < T.Count; x++)
                   {
                       _WsSocket._DisbSend.ConnectionId = T[x];
                       _WsSocket._DisbSend.Mothed = "AddOrEditHandleElevatorDevice";
                       _WsSocket._DisbSend.data = result;
                       _WsSocket._DisbSend.sourceReq = "HandleElevatorDeviceController";
                       _WsSocket.ListDisbSend.Add(_WsSocket._DisbSend);
                   }
                   //ws socket

                   //分发数据
                   /*foreach (var item in remoteDevice.CommunicateDeviceInfos)
                   {                    
                       await _distributeHub.Clients.Client(item.ConnectionId).SendAsync("AddOrEditHandleElevatorDevice", result);                      
                   }*/

               }, async (message, remoteDevice) =>
               {
                   //数据错误存储
                   //await AddDistributeErrorAsync(message, remoteDevice, result);
               });
        }

        /// <summary>
        /// 存储分发错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="processor"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task AddDistributeErrorAsync(string message, RemoteDeviceModel remoteDevice, UnitHandleElevatorDeviceModel data)
        {
            // 分发失败不重发,存储错误日记 
            var distributeErrorModel = new DistributeErrorModel();
            distributeErrorModel.Type = DistributeTypeEnum.ELEVATOR_ADD_OR_EDIT_HANDLE_ELEVATOR_DEVICE.Value;
            distributeErrorModel.PartUrl = "AddOrEditHandleElevatorDevice";
            distributeErrorModel.DataModelName = data.GetType().Name;
            distributeErrorModel.DataId = data.Id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
            distributeErrorModel.DeviceId = remoteDevice.DeviceId;

            await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }


        public async Task DeleteAsync(RemoteDeviceModel remoteDevice, string id)
        {
            await _distributeHelper.DistributeAsync(remoteDevice, id,
            async (remoteDevice) =>
            {
                //ws socket
                var T = _WsSocket.theServer.GetAllConnections();
                for (int x = 0; x < T.Count; x++)
                {
                    _WsSocket._DisbSend.ConnectionId = T[x];
                    _WsSocket._DisbSend.Mothed = "DeleteHandleElevatorDevice";
                    _WsSocket._DisbSend.data = null;
                    _WsSocket._DisbSend.sourceReq = "HandleElevatorDeviceController";
                    _WsSocket._DisbSend.id = id;
                    _WsSocket.ListDisbSend.Add(_WsSocket._DisbSend);
                }
                //ws socket
                //分发数据
                /*foreach (var item in remoteDevice.CommunicateDeviceInfos)
                {
                    await _distributeHub.Clients.Client(item.ConnectionId).SendAsync("DeleteHandleElevatorDevice", id);                    
                }*/
            }, async (message, remoteDevice) =>
            {
                //数据错误存储
                //await AddDistributeErrorAsync(message, remoteDevice, id);
            });
        }

        /// <summary>
        /// 存储分发错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="processor"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task AddDistributeErrorAsync(string message, RemoteDeviceModel remoteDevice, string id)
        {
            // 分发失败不重发,存储错误日记 
            var distributeErrorModel = new DistributeErrorModel();
            distributeErrorModel.Type = DistributeTypeEnum.ELEVATOR_DELETE_HANDLE_ELEVATOR_DEVICE.Value;
            distributeErrorModel.PartUrl = "DeleteHandleElevatorDevice";
            distributeErrorModel.DataModelName = id.GetType().Name;
            distributeErrorModel.DataId = id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = id;
            distributeErrorModel.DeviceId = remoteDevice.DeviceId;

            await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }

    }
}
