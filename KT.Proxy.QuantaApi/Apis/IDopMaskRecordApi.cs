using KT.Common.Data.Models;
using KT.Quanta.Model.Kone;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public interface IDopMaskRecordApi
    {
        Task<PageData<DopMaskRecordModel>> GetListAsync(PageQuery<DopMaskRecordQuery> pageQuery);
    }
}
