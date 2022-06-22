using KT.Quanta.Common;
using KT.Quanta.Common.Models;
using KT.Turnstile.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.IHubs
{
    public interface IDistributeHub
    {
        Task<bool> Link(SeekSocketModel seekSocket);
        Task<List<TurnstileUnitCardDeviceEntity>> GetCardDevices(string processorId);
        Task PushPassRecord(TurnstileUnitPassRecordEntity passRecord);
        Task<List<TurnstileUnitRightGroupEntity>> GetRightGroups(int page, int size);
        Task<List<TurnstileUnitPassRightEntity>> GetPassRights(int page, int size);
    }
}
