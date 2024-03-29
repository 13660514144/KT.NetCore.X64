﻿using KT.Common.Data.Daos;
using KT.Elevator.Manage.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IDaos
{
    public interface IHandleElevatorInputDeviceDao : IBaseDataDao<HandleElevatorInputDeviceEntity>
    {
        Task<HandleElevatorInputDeviceEntity> DeleteReturnWidthHandleElevatorDeviceAsync(string id);
        Task<List<HandleElevatorInputDeviceEntity>> GetAllAsync();
        Task<HandleElevatorInputDeviceEntity> GetByIdAsync(string id);
        Task<HandleElevatorInputDeviceEntity> AddAsync(HandleElevatorInputDeviceEntity entity);
        Task<HandleElevatorInputDeviceEntity> EditAsync(HandleElevatorInputDeviceEntity entity);
        Task<HandleElevatorInputDeviceEntity> GetByDeviceIdAndCardTypeAsync(string handleElevatorDeviceId, string deviceType);
    }
}
