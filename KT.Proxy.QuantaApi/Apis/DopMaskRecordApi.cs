using KT.Common.Data.Models;
using KT.Quanta.Model.Kone;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public class DopMaskRecordApi : BackendApiBase, IDopMaskRecordApi
    {
        public DopMaskRecordApi(ILogger<DopMaskRecordApi> logger) : base(logger)
        {
        }

        public Task<PageData<DopMaskRecordModel>> GetListAsync(PageQuery<DopMaskRecordQuery> pageQuery)
        {
            return base.PostAsync<PageData<DopMaskRecordModel>>($"DopMaskRecord/list", pageQuery);
        }
    }
}
