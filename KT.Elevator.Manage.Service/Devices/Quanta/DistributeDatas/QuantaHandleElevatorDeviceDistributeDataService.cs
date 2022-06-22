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
    public class QuantaHandleElevatorDeviceDistributeDataService : IQuantaHandleElevatorDeviceDistributeDataService
    {
        private ILogger<IQuantaHandleElevatorDeviceDistributeDataService> _logger;
        private IHubContext<DistributeHub> _distributeHub;
        private DistributeHelper _distributeHelper;

        public QuantaHandleElevatorDeviceDistributeDataService(ILogger<IQuantaHandleElevatorDeviceDistributeDataService> logger,
            IHubContext<DistributeHub> distributeHub,
            DistributeHelper distributeHelper)
        {
            _logger = logger;
            _distributeHub = distributeHub;
            _distributeHelper = distributeHelper;
        }

        public async Task AddOrEditAsync(RemoteDeviceModel remoteDevice, UnitHandleElevatorDeviceModel result)
        {
            await _distributeHelper.DistributeAsync(remoteDevice, result,
               async (remoteDevice) =>
               {
                   //分发数据
                   await _distributeHub.Clients.Client(remoteDevice.ConnectionId).SendAsync("AddOrEditHandleElevatorDevice", result);
               }, async (message, remoteDevice) =>
               {
                   //数据错误存储
                   await AddDistributeErrorAsync(message, remoteDevice, result);
               });
            //await _distributeHelper.DistributeAllAsync(result,
            //    (remoteDevice) => remoteDevice.DeviceId != result.Id || remoteDevice.ProductType != RemoteDeviceTypeEnum.ELEVATOR_SECONDARY.Value,
            //    async (remoteDevice) =>
            //    {
            //        //分发数据
            //        await _distributeHub.Clients.Client(remoteDevice.ConnectionId).SendAsync("AddOrEditHandleElevatorDevice", result);
            //    }, async (message, remoteDevice) =>
            //    {
            //        //数据错误存储
            //        await AddDistributeErrorAsync(message, remoteDevice, result);
            //    });
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
            distributeErrorModel.Type = DistributeTypeEnum.ADD_OR_EDIT_HANDLE_ELEVATOR_DEVICE.Value;
            distributeErrorModel.PartUrl = "AddOrEditHandleElevatorDevice";
            distributeErrorModel.DataModelName = data.GetType().Name;
            distributeErrorModel.DataId = data.Id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
            distributeErrorModel.DeviceKey = remoteDevice.DeviceKey;

            await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }


        public async Task DeleteAsync(RemoteDeviceModel remoteDevice, string id)
        {
            await _distributeHelper.DistributeAsync(remoteDevice, id,
            async (remoteDevice) =>
            {
                //分发数据
                await _distributeHub.Clients.Client(remoteDevice.ConnectionId).SendAsync("DeleteHandleElevatorDevice", id);
            }, async (message, remoteDevice) =>
            {
                //数据错误存储
                await AddDistributeErrorAsync(message, remoteDevice, id);
            });
            //await _distributeHelper.DistributeAllAsync(id,
            //(remoteDevice) =>
            //{
            //    return remoteDevice.DeviceId != id || remoteDevice.ProductType != RemoteDeviceTypeEnum.ELEVATOR_SECONDARY.Value;
            //},
            //async (remoteDevice) =>
            //{
            //    //分发数据
            //    await _distributeHub.Clients.Client(remoteDevice.ConnectionId).SendAsync("DeleteHandleElevatorDevice", id);
            //}, async (message, remoteDevice) =>
            //{
            //    //数据错误存储
            //    await AddDistributeErrorAsync(message, remoteDevice, id);
            //});
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
            distributeErrorModel.Type = DistributeTypeEnum.DELETE_HANDLE_ELEVATOR_DEVICE.Value;
            distributeErrorModel.PartUrl = "DeleteHandleElevatorDevice";
            distributeErrorModel.DataModelName = id.GetType().Name;
            distributeErrorModel.DataId = id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = id;
            distributeErrorModel.DeviceKey = remoteDevice.DeviceKey;

            await _distributeHelper.SaveDistributeError(remoteDevice, distributeErrorModel);
        }

    }
}
