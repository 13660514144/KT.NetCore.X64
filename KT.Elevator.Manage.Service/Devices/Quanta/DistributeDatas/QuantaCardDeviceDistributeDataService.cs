using KT.Common.Core.Utils;
using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Hubs;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Quanta.DistributeDatas
{
    public class QuantaCardDeviceDistributeDataService : IQuantaCardDeviceDistributeDataService
    {
        private ILogger<QuantaCardDeviceDistributeDataService> _logger;
        private IHubContext<DistributeHub> _distributeHub;
        private DistributeHelper _distributeHelper;

        public QuantaCardDeviceDistributeDataService(ILogger<QuantaCardDeviceDistributeDataService> logger,
            IHubContext<DistributeHub> distributeHub,
            DistributeHelper distributeHelper)
        {
            _logger = logger;
            _distributeHub = distributeHub;
            _distributeHelper = distributeHelper;
        }

        public async Task AddOrUpdateAsync(RemoteDeviceModel remoteDevice, CardDeviceModel model)
        {
            var data = new UnitCardDeviceEntity();
            data.Id = model.Id;
            data.BrandModel = model.BrandModel;
            data.DeviceType = model.DeviceType;
            data.PortName = model.PortName;
            data.HandleDeviceId = model.HandleElevatorDeviceId;
            data.EditedTime = model.EditedTime;

            if (model.SerialConfig != null)
            {
                data.SerialConfigId = model.SerialConfig.Id;
                data.Baudrate = model.SerialConfig.Baudrate;
                data.Databits = model.SerialConfig.Databits;
                data.Stopbits = model.SerialConfig.Stopbits;
                data.Parity = model.SerialConfig.Parity;
                data.ReadTimeout = model.SerialConfig.ReadTimeout;
                data.Encoding = model.SerialConfig.Encoding;
            }
            else
            {
                _logger.LogError("读卡器设备配置错误：data:{0} ", JsonConvert.SerializeObject(model, JsonUtil.JsonSettings));
            }

            await _distributeHelper.DistributeAsync(remoteDevice, data, async (remoteDevice) =>
            {
                //分发数据
                await _distributeHub.Clients.Client(remoteDevice.ConnectionId).SendAsync("AddOrEditCardDevice", data);
            }, async (message, remoteDevice) =>
            {
                //数据错误存储
                await AddDistributeErrorAsync(message, remoteDevice, data);
            });
        }

        /// <summary>
        /// 存储分发错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="processor"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task AddDistributeErrorAsync(string message, RemoteDeviceModel remoteDevice, UnitCardDeviceEntity data)
        {
            // 分发失败不重发,存储错误日记 
            var distributeErrorModel = new DistributeErrorModel();
            distributeErrorModel.Type = DistributeTypeEnum.ADD_OR_UPDATE_CARD_DEVICE.Value;
            distributeErrorModel.PartUrl = "AddOrEditCardDevice";
            distributeErrorModel.DataModelName = data.GetType().Name;
            distributeErrorModel.DataId = data.Id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
            distributeErrorModel.DeviceKey = remoteDevice.DeviceKey;

            await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }

        public async Task DeleteAsync(RemoteDeviceModel remoteDevice, string id, long time)
        {
            await _distributeHelper.DistributeAsync(remoteDevice, id, async (remoteDevice) =>
            {
                //分发数据
                await _distributeHub.Clients.Client(remoteDevice.ConnectionId).SendAsync("DeleteCardDevice", id, time);
            }, async (message, remoteDevice) =>
            {
                //数据错误存储
                await AddDistributeErrorAsync(message, remoteDevice, id);
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
            distributeErrorModel.Type = DistributeTypeEnum.DELETE_CARD_DEVICE.Value;
            distributeErrorModel.PartUrl = "DeleteCardDevice";
            distributeErrorModel.DataModelName = id.GetType().Name;
            distributeErrorModel.DataId = id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = id;
            distributeErrorModel.DeviceKey = remoteDevice.DeviceKey;

            await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }

    }
}
