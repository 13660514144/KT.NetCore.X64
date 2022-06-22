using KT.Elevator.Unit.Entity.Entities;
using KT.Quanta.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IHubs
{
    public interface IDistributeHub
    {
        Task<bool> Link(SeekSocketModel seekSocket);
        Task<List<UnitCardDeviceEntity>> GetCardDevices(string processorId);
        Task PushPassRecord(UnitPassRecordEntity passRecord);
        Task<List<UnitPassRightEntity>> GetPassRights(int page, int size);
    }
}
