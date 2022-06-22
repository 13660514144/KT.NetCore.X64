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
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Quanta.DistributeDatas
{
    public class QuantaTurnstileCardDeviceDistributeDataService : IQuantaTurnstileCardDeviceDistributeDataService
    {
        private ILogger<QuantaTurnstileCardDeviceDistributeDataService> _logger;
        private IHubContext<QuantaDistributeHub> _distributeHub;
        private DistributeHelper _distributeHelper;
        private DistirbQueue _DistirbQueue;
        private WsSocket _WsSocket;
        public QuantaTurnstileCardDeviceDistributeDataService(ILogger<QuantaTurnstileCardDeviceDistributeDataService> logger,
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


        public async Task AddOrUpdateAsync(RemoteDeviceModel remoteDevice, TurnstileCardDeviceModel model)
        {
            /*设备 读卡器 分发 真假修改*/
            var data = new TurnstileUnitCardDeviceEntity();
            data.Id = model.Id;
            data.BrandModel = model.BrandModel;
            data.DeviceType = model.CardDeviceType;
            data.PortName = model.PortName;

            data.EditedTime = model.EditedTime;

            data.RelayDeviceIp = model.RelayDevice?.IpAddress;
            data.RelayDevicePort = model.RelayDevice?.Port ?? 0;
            data.RelayDeviceOut = model.RelayDeviceOut;
            data.HandleElevatorDeviceId = model.HandleElevatorDeviceId;

            if (model.SerialConfig != null)
            {
                data.Baudrate = model.SerialConfig.Baudrate;
                data.Databits = model.SerialConfig.Databits;
                data.Stopbits = model.SerialConfig.Stopbits;
                data.Parity = model.SerialConfig.Parity;
                data.ReadTimeout = model.SerialConfig.ReadTimeout;
                data.Encoding = model.SerialConfig.Encoding;
            }
            else
            {
                _logger.LogError("读卡器设备配置错误：data:{0} ", JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings));
            }

            await _distributeHelper.DistributeAsync(remoteDevice, data, async (remoteDevice) =>
            {
                //ws socket
                var T = _WsSocket.theServer.GetAllConnections();
                for (int x = 0; x < T.Count; x++)
                {
                    _WsSocket._DisbSend.ConnectionId = T[x];
                    _WsSocket._DisbSend.Mothed = "AddOrEditCardDevice";
                    _WsSocket._DisbSend.data = data;
                    _WsSocket._DisbSend.sourceReq = "TurnstileCardDeviceController";
                    _WsSocket.ListDisbSend.Add(_WsSocket._DisbSend);
                }
                //ws socket
                //分发数据
                /*foreach (var item in remoteDevice.CommunicateDeviceInfos)
                {
                    //2021-10-24 分流
                    _DistirbQueue._DisbSend.ConnectionId = item.ConnectionId;
                    _DistirbQueue._DisbSend.Mothed = "AddOrEditCardDevice";
                    _DistirbQueue._DisbSend.data = data;
                    _DistirbQueue._DisbSend.sourceReq = "TurnstileCardDeviceController";
                    _DistirbQueue.ListDisbSend.Add(_DistirbQueue._DisbSend);
                    //2021-10-24 分流
                    //await _distributeHub.Clients.Client(item.ConnectionId).SendAsync("AddOrEditCardDevice", data);

                }*/
            }, async (message, remoteDevice) =>
            {
                //数据错误存储  filter 
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
        private async Task AddDistributeErrorAsync(string message, RemoteDeviceModel remoteDevice, TurnstileUnitCardDeviceEntity data)
        {
            // 分发失败不重发,存储错误日记 
            var distributeErrorModel = new DistributeErrorModel();
            distributeErrorModel.Type = DistributeTypeEnum.TURNSTILE_ADD_OR_UPDATE_CARD_DEVICE.Value;
            distributeErrorModel.PartUrl = "AddOrEditCardDevice";
            distributeErrorModel.DataModelName = data.GetType().Name;
            distributeErrorModel.DataId = data.Id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
            distributeErrorModel.DeviceId = remoteDevice.DeviceId;

            //await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }

        public async Task DeleteAsync(RemoteDeviceModel remoteDevice, string id, long time)
        {
            /*设备 读卡器 分发 删除*/
            await _distributeHelper.DistributeAsync(remoteDevice, id, async (remoteDevice) =>
            {
                //ws socket
                var T = _WsSocket.theServer.GetAllConnections();
                for (int x = 0; x < T.Count; x++)
                {
                    _WsSocket._DisbSend.ConnectionId = T[x];
                    _WsSocket._DisbSend.Mothed = "DeleteCardDevice";
                    _WsSocket._DisbSend.data = null;
                    _WsSocket._DisbSend.sourceReq = "TurnstileCardDeviceController";
                    _WsSocket._DisbSend.id = id;
                    _WsSocket._DisbSend.time = time;
                    _WsSocket.ListDisbSend.Add(_WsSocket._DisbSend);
                }
                //ws socket
                //分发数据
                /*foreach (var item in remoteDevice.CommunicateDeviceInfos)
                {
                    //2021-10-24 分流
                    _DistirbQueue._DisbSend.ConnectionId = item.ConnectionId;
                    _DistirbQueue._DisbSend.Mothed = "DeleteCardDevice";
                    _DistirbQueue._DisbSend.data = null;
                    _DistirbQueue._DisbSend.sourceReq = "TurnstileCardDeviceController";
                    _DistirbQueue._DisbSend.id = id;
                    _DistirbQueue._DisbSend.time = time;
                    _DistirbQueue.ListDisbSend.Add(_DistirbQueue._DisbSend);
                    //2021-10-24 分流
                    //await _distributeHub.Clients.Client(item.ConnectionId).SendAsync("DeleteCardDevice", id, time);
                    
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
            distributeErrorModel.Type = DistributeTypeEnum.TURNSTILE_DELETE_CARD_DEVICE.Value;
            distributeErrorModel.PartUrl = "DeleteCardDevice";
            distributeErrorModel.DataModelName = id.GetType().Name;
            distributeErrorModel.DataId = id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = id;
            distributeErrorModel.DeviceId = remoteDevice.DeviceId;

            //await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }

    }
}
