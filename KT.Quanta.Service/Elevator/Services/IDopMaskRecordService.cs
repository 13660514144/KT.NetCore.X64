using KT.Common.Data.Models;
using KT.Quanta.Model.Kone;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Elevator.Services
{
    public interface IDopMaskRecordService
    {
        Task<PageData<DopMaskRecordModel>> GetListAsync(PageQuery<DopMaskRecordQuery> pageQuery);
    }
}