using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Services
{
    public class HandleElevatorDeviceService : IHandleElevatorDeviceService
    {
        private ISystemConfigService _systemConfigService;
        private IFloorService _floorService;

        public HandleElevatorDeviceService(ISystemConfigService systemConfigService,
            IFloorService floorService)
        {
            _systemConfigService = systemConfigService;
            _floorService = floorService;
        }

        public async Task<UnitSystemConfigModel> AddOrEditAsync(UnitHandleElevatorDeviceModel model, UnitSystemConfigModel configModel)
        {
            if (model == null)
            {
                return configModel;
            }
            configModel.HandleElevatorDeviceId = model.Id;
            configModel.DeviceFloorId = model.DeviceFloorId;
            configModel.FaceAppId = model.FaceAppId;
            configModel.FaceSdkKey = model.FaceSdkKey;
            configModel.FaceActivateCode = model.FaceActivateCode;
            configModel.ElevatorGroupId = model.ElevatorGroupId;

            //存储数据库 
            await _systemConfigService.AddOrUpdateAsync(configModel);

            //可去楼层
            await _floorService.AddOrEditElevatorGroupFloorsAsync(model);

            return configModel;
        }

        public async Task DeleteAsync(string handleElevatorDeviceId)
        {
            //可去楼层
            await _floorService.DeleteElevatorGroupFloorsAsync(handleElevatorDeviceId);
        }
    }
}
