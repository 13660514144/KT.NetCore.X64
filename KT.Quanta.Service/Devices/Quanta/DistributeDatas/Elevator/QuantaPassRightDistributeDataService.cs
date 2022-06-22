using ContralServer.CfgFileRead;
using KT.Common.Core.Utils;
using KT.Elevator.Unit.Entity.Entities;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Helpers;
using KT.Quanta.Service.Hubs;
using KT.Quanta.Service.Models;
using KT.Quanta.WebApi.Common.WsSocket;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Quanta.DistributeDatas
{
    public class QuantaPassRightDistributeDataService : IQuantaPassRightDistributeDataService
    {
        private ILogger<QuantaPassRightDistributeDataService> _logger;
        private IHubContext<QuantaDistributeHub> _distributeHub;
        private DistributeHelper _distributeHelper;
        private DistirbQueue _DistirbQueue;
        private WsSocket _WsSocket;
        public QuantaPassRightDistributeDataService(ILogger<QuantaPassRightDistributeDataService> logger,
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

        public async Task AddOrUpdateAsync(RemoteDeviceModel remoteDevice, PassRightModel model, FaceInfoModel face)
        {
            if (model.Floors == null
            || model.Floors.FirstOrDefault() == null)
            {
                _logger.LogWarning("分发权限通行权限信息不存在：data:{0} ", JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings));
                return;
            }

            var data = new UnitPassRightEntity();
            data.Id = model.Id;
            data.Sign = model.Sign;
            data.AccessType = model.AccessType;
            data.TimeStart = model.TimeNow;
            data.TimeEnd = model.TimeOut;
            data.EditedTime = model.EditedTime;

            if (model.AccessType == AccessTypeEnum.FACE.Value && face != null)
            {
                if (remoteDevice.DeviceType == DeviceTypeEnum.ELEVATOR_SECONDARY.Value)
                {
                    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, face.FaceUrl);

                    var faceFile = new FileStream(path, FileMode.Open);
                    byte[] faceBytes = new byte[faceFile.Length];
                    faceFile.Read(faceBytes, 0, faceBytes.Length);
                    faceFile.Close();

                    data.Feature = faceBytes;
                    data.FeatureSize = faceBytes.Length;
                }
            }

            foreach (var item in model.Floors)
            {
                if (item == null)
                {
                    continue;
                }

                var passRightDetail = new UnitPassRightDetailEntity();
                passRightDetail.FloorId = item.Id;
                passRightDetail.FloorName = item.Name;
                passRightDetail.EditedTime = item.EditedTime;

                data.PassRightDetails.Add(passRightDetail);
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
                        _WsSocket._DisbSend.sourceReq = "Elevator_PassRightController";
                        _WsSocket.ListDisbSend.Add(_WsSocket._DisbSend);
                    }
                    //ws socket
                    //分发数据
                    /*foreach (var item in remoteDevice.CommunicateDeviceInfos)
                    {
                        //2021-10-19  分流队列
                        _DistirbQueue._DisbSend.ConnectionId = item.ConnectionId;
                        _DistirbQueue._DisbSend.Mothed = "AddOrEditPassRight";
                        _DistirbQueue._DisbSend.data = data;
                        _DistirbQueue._DisbSend.sourceReq = "Elevator_PassRightController";
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
        private async Task AddDistributeErrorAsync(string message, RemoteDeviceModel remoteDevice, UnitPassRightEntity data)
        {
            // 分发失败不重发,存储错误日记 
            var distributeErrorModel = new DistributeErrorModel();
            distributeErrorModel.Type = DistributeTypeEnum.ELEVATOR_ADD_OR_UPDATE_PASS_RIGHT.Value;
            distributeErrorModel.PartUrl = "AddOrEditPassRight";
            distributeErrorModel.DataModelName = data.GetType().Name;
            distributeErrorModel.DataId = data.Id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
            distributeErrorModel.DeviceId = remoteDevice.DeviceId;

            await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }

        public async Task DeleteAsync(RemoteDeviceModel remoteDevice, PassRightModel model)
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
                    _WsSocket._DisbSend.sourceReq = "Elevator_PassRightController";
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
                    _DistirbQueue._DisbSend.sourceReq = "Elevator_PassRightController";
                    _DistirbQueue._DisbSend.id = model.Id;
                    _DistirbQueue._DisbSend.time = model.EditedTime;
                    _DistirbQueue.ListDisbSend.Add(_DistirbQueue._DisbSend);
                    //2021-10-19  分流队列
                    //await _distributeHub.Clients.Client(item.ConnectionId).SendAsync("DeletePassRight", model.Id, model.EditedTime);
                    
                }*/
            }, async (message, remoteDevice) =>
            {
                //数据错误存储
                //await AddDistributeErrorAsync(message, remoteDevice, model.Id);
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
            distributeErrorModel.Type = DistributeTypeEnum.ELEVATOR_DELETE_PASS_RIGHT.Value;
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
