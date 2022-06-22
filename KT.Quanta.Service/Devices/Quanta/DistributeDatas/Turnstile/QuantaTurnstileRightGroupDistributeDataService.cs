using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Hubs;
using KT.Quanta.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Turnstile.Unit.Entity.Entities;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Helpers;
using KT.Quanta.WebApi.Common.WsSocket;

namespace KT.Quanta.Service.Devices.Quanta.DistributeDatas
{
    public class QuantaTurnstileRightGroupDistributeDataService : IQuantaTurnstileRightGroupDistributeDataService
    {
        private ILogger<QuantaTurnstileRightGroupDistributeDataService> _logger;
        private IHubContext<QuantaDistributeHub> _distributeHub;
        private DistributeHelper _distributeHelper;
        private DistirbQueue _DistirbQueue;
        private WsSocket _WsSocket;
        public QuantaTurnstileRightGroupDistributeDataService(ILogger<QuantaTurnstileRightGroupDistributeDataService> logger,
            IHubContext<QuantaDistributeHub> distributeHub,
            DistributeHelper distributeHelper
             , DistirbQueue _distirbQueue, WsSocket _wsSocket)
        {
            _logger = logger;
            _distributeHub = distributeHub;
            _distributeHelper = distributeHelper;
            _DistirbQueue = _distirbQueue;
            _WsSocket = _wsSocket;
        }
        /// <summary>
        /// 实现分发推送
        /// </summary>
        /// <param name="remoteDevice"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddOrUpdateAsync(RemoteDeviceModel remoteDevice, TurnstileCardDeviceRightGroupModel model)
        {
            /*读卡器分组  真假修改*/
            var data = new TurnstileUnitRightGroupEntity();
            data.Id = model.Id;
            data.EditedTime = model.EditedTime;

            if (model.CardDevices != null)
            {
                foreach (var obj in model.CardDevices)
                {
                    var detail = new TurnstileUnitRightGroupDetailEntity();
                    detail.EditedTime = obj.EditedTime;

                    detail.CardDevice = new TurnstileUnitCardDeviceEntity();
                    detail.CardDevice.Id = obj.Id;
                    detail.CardDeviceId = obj.Id;

                    data.Details.Add(detail);
                }
            }

            await _distributeHelper.DistributeAsync(remoteDevice, data,
                async (remoteDevice) =>
                {
                    //ws socket
                    var T = _WsSocket.theServer.GetAllConnections();
                    for (int x = 0; x < T.Count; x++)
                    {
                        _WsSocket._DisbSend.ConnectionId = T[x];
                        _WsSocket._DisbSend.Mothed = "AddOrEditRightGroup";
                        _WsSocket._DisbSend.data = data;
                        _WsSocket._DisbSend.sourceReq = "TurnstileCardDeviceRightGroupService";
                        _WsSocket.ListDisbSend.Add(_WsSocket._DisbSend);
                    }
                    //ws socket
                    //分发数据
                    /*foreach (var item in remoteDevice.CommunicateDeviceInfos)
                    {
                        //2021-10-19  分流队列
                        _DistirbQueue._DisbSend.ConnectionId = item.ConnectionId;
                        _DistirbQueue._DisbSend.Mothed = "AddOrEditRightGroup";
                        _DistirbQueue._DisbSend.data = data;
                        _DistirbQueue._DisbSend.sourceReq = "TurnstileCardDeviceRightGroupService";
                        _DistirbQueue.ListDisbSend.Add(_DistirbQueue._DisbSend);
                        //2021-10-19  分流队列
                        //await _distributeHub.Clients.Client(item.ConnectionId).SendAsync("AddOrEditRightGroup", data);                       
                    }*/
                }, async (message, remoteDevice) =>
                {
                    //数据错误存储
                   // await AddDistributeErrorAsync(message, remoteDevice, data);
                });
        }



        /// <summary>
        /// 存储分发错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="processor"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task AddDistributeErrorAsync(string message, RemoteDeviceModel remoteDevice, TurnstileUnitRightGroupEntity data)
        {
            // 分发失败不重发,存储错误日记 
            var distributeErrorModel = new DistributeErrorModel();
            distributeErrorModel.Type = DistributeTypeEnum.TURNSTILE_ADD_OR_UPDATE_RIGHT_GROUP.Value;
            distributeErrorModel.PartUrl = "AddOrEditRightGroup";
            distributeErrorModel.DataModelName = data.GetType().Name;
            distributeErrorModel.DataId = data.Id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
            distributeErrorModel.DeviceId = remoteDevice.DeviceId;

            await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }

        public async Task DeleteAsync(RemoteDeviceModel remoteDevice, string id, long time)
        {
            /*读卡器分组  删除*/
            await _distributeHelper.DistributeAsync(remoteDevice, id,
                async (remoteDevice) =>
                {
                    //ws socket
                    var T = _WsSocket.theServer.GetAllConnections();
                    for (int x = 0; x < T.Count; x++)
                    {
                        _WsSocket._DisbSend.ConnectionId = T[x];
                        _WsSocket._DisbSend.Mothed = "DeleteRightGroup";
                        _WsSocket._DisbSend.data = null;
                        _WsSocket._DisbSend.sourceReq = "TurnstileCardDeviceRightGroupService";
                        _WsSocket._DisbSend.id = id;
                        _WsSocket._DisbSend.time = time;
                        _WsSocket.ListDisbSend.Add(_WsSocket._DisbSend);
                    }
                    //ws socket
                    //分发数据
                    /*foreach (var item in remoteDevice.CommunicateDeviceInfos)
                    {
                        //2021-10-19  分流队列
                        _DistirbQueue._DisbSend.ConnectionId = item.ConnectionId;
                        _DistirbQueue._DisbSend.Mothed = "DeleteRightGroup";
                        _DistirbQueue._DisbSend.data = null;
                        _DistirbQueue._DisbSend.sourceReq = "TurnstileCardDeviceRightGroupService";
                        _DistirbQueue._DisbSend.id = id;
                        _DistirbQueue._DisbSend.time = time;
                        _DistirbQueue.ListDisbSend.Add(_DistirbQueue._DisbSend);
                        //2021-10-19  分流队列
                        //await _distributeHub.Clients.Client(item.ConnectionId).SendAsync("DeleteRightGroup", id, time);                        
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
            distributeErrorModel.Type = DistributeTypeEnum.TURNSTILE_DELETE_RIGHT_GROUP.Value;
            distributeErrorModel.PartUrl = "DeleteRightGroup";
            distributeErrorModel.DataModelName = id.GetType().Name;
            distributeErrorModel.DataId = id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = id;
            distributeErrorModel.DeviceId = remoteDevice.DeviceId;

            await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }
    }
}