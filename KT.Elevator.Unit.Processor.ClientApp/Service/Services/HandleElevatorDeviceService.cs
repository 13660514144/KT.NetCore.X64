using KT.Elevator.Unit.Processor.ClientApp.Service.IServices;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KT.Elevator.Unit.Processor.ClientApp.Dao.IDaos;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Services
{
    public class HandleElevatorDeviceService : IHandleElevatorDeviceService
    {
        private ISystemConfigService _systemConfigService;
        private IFloorService _floorService;
        private IHandleElevatorDeviceDao _dao;
        private readonly IElevatorGroupDao _elevatorGroupDao;

        public HandleElevatorDeviceService(ISystemConfigService systemConfigService,
            IFloorService floorService,
            IHandleElevatorDeviceDao dao,
            IElevatorGroupDao elevatorGroupDao)
        {
            _systemConfigService = systemConfigService;
            _floorService = floorService;
            _dao = dao;
            _elevatorGroupDao = elevatorGroupDao;
        }

        public async Task AddOrEditAsync(UnitHandleElevatorDeviceModel model)
        {
            // 持久化派梯设备
            var entity = await _dao.SelectByIdAsync(model.Id);
            if (entity != null)
            {
                entity.ElevatorGroupId = model.ElevatorGroupId; 
                await _dao.UpdateAsync(entity);
            }
            else
            {
                entity = new UnitHandleElevatorDeviceEntity();
                entity.Id = model.Id;
                entity.ElevatorGroupId = model.ElevatorGroupId; 
                await _dao.InsertAsync(entity);
            }
        }

        public async Task DeleteAsync(string handleElevatorDeviceId)
        {
            //可去楼层
            await _floorService.DeleteElevatorGroupFloorsAsync(handleElevatorDeviceId);
        }
    }
}
