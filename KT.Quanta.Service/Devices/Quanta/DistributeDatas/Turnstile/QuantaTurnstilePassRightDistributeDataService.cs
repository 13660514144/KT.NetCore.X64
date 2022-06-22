using ContralServer.CfgFileRead;
using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Helpers;
using KT.Quanta.Service.Hubs;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Quanta.WebApi.Common.WsSocket;
using KT.Turnstile.Unit.Entity.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Quanta.DistributeDatas
{
    public class QuantaTurnstilePassRightDistributeDataService : IQuantaTurnstilePassRightDistributeDataService
    {
        private ILogger<QuantaTurnstilePassRightDistributeDataService> _logger;
        private IHubContext<QuantaDistributeHub> _distributeHub;
        private DistributeHelper _distributeHelper;
        private DistirbQueue _DistirbQueue;
        private WsSocket _WsSocket;
        public QuantaTurnstilePassRightDistributeDataService(ILogger<QuantaTurnstilePassRightDistributeDataService> logger,
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

        public async Task AddOrUpdateAsync(RemoteDeviceModel remoteDevice, TurnstilePassRightModel model, FaceInfoModel face)
        {
            if (model.CardDeviceRightGroups == null
                   || model.CardDeviceRightGroups.FirstOrDefault() == null)
            {
                _logger.LogWarning("分发权限通行权限信息不存在：data:{0} ", JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings));
                return;
            }

            var data = new TurnstileUnitPassRightEntity();
            data.Id = model.Id;
            data.CardNumber = model.Sign;
            data.TimeStart = model.TimeNow;
            data.TimeEnd = model.TimeOut;
            data.EditedTime = model.EditedTime;

            foreach (var item in model.CardDeviceRightGroups)
            {
                if (item == null)
                {
                    continue;
                }

                var passRightDetail = new TurnstileUnitPassRightDetailEntity();
                passRightDetail.EditedTime = item.EditedTime;

                passRightDetail.RightGroup = new TurnstileUnitRightGroupEntity();
                passRightDetail.RightGroup.Id = item.Id;
                passRightDetail.RightGroupId = item.Id;

                data.Details.Add(passRightDetail);
            }

            await _distributeHelper.DistributeAsync(remoteDevice, data,
                async (remoteDevice) =>
                {
                    //ws socket
                    var T = _WsSocket.theServer.GetAllConnections();
                    for (int x = 0; x < T.Count; x++)
                    {
                        _WsSocket._DisbSend.ConnectionId = T[x];
                        _WsSocket._DisbSend.Mothed = "AddOrEditPassRight";
                        _WsSocket._DisbSend.data = data;
                        _WsSocket._DisbSend.sourceReq = "Elevator_PassRightController_Qutan";
                        _WsSocket.ListDisbSend.Add(_WsSocket._DisbSend);                        
                    }
                    //ws socket 
                    //分发数据 2021-08-06  分发延时
                    /*foreach (var item in remoteDevice.CommunicateDeviceInfos)
                    {
                        //2021-10-19  分流队列
                        _DistirbQueue._DisbSend.ConnectionId = item.ConnectionId;
                        _DistirbQueue._DisbSend.Mothed = "AddOrEditPassRight";
                        _DistirbQueue._DisbSend.data = data;
                        _DistirbQueue._DisbSend.sourceReq = "Elevator_PassRightController_Qutan";
                        _DistirbQueue.ListDisbSend.Add(_DistirbQueue._DisbSend);
                        //2021-10-19  分流队列
                        //await _distributeHub.Clients.Client(item.ConnectionId).SendAsync("AddOrEditPassRight", data);                        
                    }*/ 
                }, async (message, remoteDevice) =>
                {
                    //数据错误存储
                    //await AddDistributeErrorAsync(message, remoteDevice, data);
                    
                });
        }

        /// <summary>
        /// 存储分发错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="processor"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task AddDistributeErrorAsync(string message, RemoteDeviceModel remoteDevice, TurnstileUnitPassRightEntity data)
        {
            // 分发失败不重发,存储错误日记 
            var distributeErrorModel = new DistributeErrorModel();
            distributeErrorModel.Type = DistributeTypeEnum.TURNSTILE_ADD_OR_UPDATE_PASS_RIGHT.Value;
            distributeErrorModel.PartUrl = "AddOrEditPassRight";
            distributeErrorModel.DataModelName = data.GetType().Name;
            distributeErrorModel.DataId = data.Id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
            distributeErrorModel.DeviceId = remoteDevice.DeviceId;

            await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }

        public async Task DeleteAsync(RemoteDeviceModel remoteDevice, TurnstilePassRightModel model)
        {
            await _distributeHelper.DistributeAsync(remoteDevice, model.Id,
            async (remoteDevice) =>
            {
                //ws socket
                var T = _WsSocket.theServer.GetAllConnections();
                for (int x = 0; x < T.Count; x++)
                {
                    _WsSocket._DisbSend.ConnectionId = T[x];
                    _WsSocket._DisbSend.Mothed = "DeletePassRight";
                    _WsSocket._DisbSend.data = model;
                    _WsSocket._DisbSend.sourceReq = "Elevator_PassRightController_Qutan";
                    _WsSocket._DisbSend.id = model.Id;
                    _WsSocket._DisbSend.time = model.EditedTime;
                    _WsSocket.ListDisbSend.Add(_WsSocket._DisbSend);
                }
                //ws socket
                //分发数据
                /*foreach (var item in remoteDevice.CommunicateDeviceInfos)
                {
                    //2021-10-19  分流队列
                    _DistirbQueue._DisbSend.ConnectionId = item.ConnectionId;
                    _DistirbQueue._DisbSend.Mothed = "DeletePassRight";
                    _DistirbQueue._DisbSend.data = model;
                    _DistirbQueue._DisbSend.sourceReq = "Elevator_PassRightController_Qutan";
                    _DistirbQueue._DisbSend.id = model.Id;
                    _DistirbQueue._DisbSend.time = model.EditedTime;
                    _DistirbQueue.ListDisbSend.Add(_DistirbQueue._DisbSend);
                    //2021-10-19  分流队列
                    //await _distributeHub.Clients.Client(item.ConnectionId).SendAsync("DeletePassRight", model.Id, model.EditedTime);

                }*/
            }, async (message, remoteDevice) =>
            {
                //数据错误存储 不存
                //await AddDistributeErrorAsync(message, remoteDevice, model.Id);
                //Thread.Sleep(5);
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
            distributeErrorModel.Type = DistributeTypeEnum.TURNSTILE_DELETE_PASS_RIGHT.Value;
            distributeErrorModel.PartUrl = "DeletePassRight";
            distributeErrorModel.DataModelName = id.GetType().Name;
            distributeErrorModel.DataId = id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = id;
            distributeErrorModel.DeviceId = remoteDevice.DeviceId;

            await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }
    }
}
