using KT.Elevator.Unit.Entity.Entities;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Hitachi
{
    public class HitachiElevatorDataRemoteService : IElevatorDataRemoteService
    {
        public Task AddOrUpdateCardDeviceAsync(IRemoteDevice remoteDevice, CardDeviceModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddOrUpdateHandleElevatorDeviceAsync(IRemoteDevice remoteDevice, UnitHandleElevatorDeviceModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddOrUpdatePassRightAsync(IRemoteDevice remoteDevice, PassRightModel model, FaceInfoModel face, PassRightModel oldModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCardDeviceAsync(IRemoteDevice remoteDevice, string id, long time)
        {
            throw new NotImplementedException();
        }

        public Task DeleteHandleElevatorDeviceAsync(IRemoteDevice remoteDevice, string id)
        {
            throw new NotImplementedException();
        }

        public Task DeletePassRightAsync(IRemoteDevice remoteDevice, PassRightModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetOutputNumAsync(IRemoteDevice remoteDevice)
        {
            throw new NotImplementedException();
        }
    }
}
