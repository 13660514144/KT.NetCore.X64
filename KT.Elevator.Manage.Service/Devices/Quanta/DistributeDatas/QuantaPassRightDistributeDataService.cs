using KT.Common.Core.Utils;
using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Hubs;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Quanta.DistributeDatas
{
    public class QuantaPassRightDistributeDataService : IQuantaPassRightDistributeDataService
    {
        private ILogger<QuantaPassRightDistributeDataService> _logger;
        private IHubContext<DistributeHub> _distributeHub;
        private DistributeHelper _distributeHelper;

        public QuantaPassRightDistributeDataService(ILogger<QuantaPassRightDistributeDataService> logger,
            IHubContext<DistributeHub> distributeHub,
            DistributeHelper distributeHelper)
        {
            _logger = logger;
            _distributeHub = distributeHub;
            _distributeHelper = distributeHelper;
        }

        public async Task AddOrUpdateAsync(RemoteDeviceModel remoteDevice, PassRightModel model, FaceInfoModel face)
        {
            if (model.Floors == null
            || model.Floors.FirstOrDefault() == null)
            {
                _logger.LogWarning("分发权限通行权限信息不存在：data:{0} ", JsonConvert.SerializeObject(model, JsonUtil.JsonSettings));
                return;
            }

            var data = new UnitPassRightEntity();
            data.Id = model.Id;
            data.Sign = model.Sign;
            data.AccessType = model.AccessType;
            data.TimeStart = model.TimeNow;
            data.TimeEnd = model.TimeOut;
            data.EditedTime = model.EditedTime;

            foreach (var item in model.Floors)
            {
                if (item == null)
                {
                    continue;
                }

                var passRightDetail = new UnitPassRightDetailEntity();
                passRightDetail.FloorId = item.Id;
                passRightDetail.FloorName = item.Name;
                passRightDetail.RealFloorId = item.RealFloorId;
                passRightDetail.IsPublic = item.IsPublic;
                passRightDetail.EditedTime = item.EditedTime;

                data.PassRightDetails.Add(passRightDetail);
            }
            await _distributeHelper.DistributeAsync(remoteDevice, data,
                async (remoteDevice) =>
                {
                    //分发数据
                    await _distributeHub.Clients.Client(remoteDevice.ConnectionId).SendAsync("AddOrEditPassRight", data);
                }, async (message, remoteDevice) =>
                {
                    //数据错误存储
                    await AddDistributeErrorAsync(message, remoteDevice, data);
                });
            //await _distributeHelper.DistributeAllAsync(data, x => true, async (remoteDevice) =>
            //{
            //    //分发数据
            //    await _distributeHub.Clients.Client(remoteDevice.ConnectionId).SendAsync("AddOrEditPassRight", data);
            //}, async (message, remoteDevice) =>
            //{
            //    //数据错误存储
            //    await AddDistributeErrorAsync(message, remoteDevice, data);
            //});
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
            distributeErrorModel.Type = DistributeTypeEnum.ADD_OR_UPDATE_PASS_RIGHT.Value;
            distributeErrorModel.PartUrl = "AddOrEditPassRight";
            distributeErrorModel.DataModelName = data.GetType().Name;
            distributeErrorModel.DataId = data.Id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
            distributeErrorModel.DeviceKey = remoteDevice.DeviceKey;

            await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }

        public async Task DeleteAsync(RemoteDeviceModel remoteDevice, PassRightModel model)
        {
            await _distributeHelper.DistributeAsync(remoteDevice, model.Id,
            async (remoteDevice) =>
            {
                //分发数据
                await _distributeHub.Clients.Client(remoteDevice.ConnectionId).SendAsync("DeletePassRight", model.Id, model.EditedTime);
            }, async (message, remoteDevice) =>
            {
                //数据错误存储
                await AddDistributeErrorAsync(message, remoteDevice, model.Id);
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
            distributeErrorModel.Type = DistributeTypeEnum.DELETE_PASS_RIGHT.Value;
            distributeErrorModel.PartUrl = "DeletePassRight";
            distributeErrorModel.DataModelName = id.GetType().Name;
            distributeErrorModel.DataId = id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = id;
            distributeErrorModel.DeviceKey = remoteDevice.DeviceKey;

            await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }
    }
}
