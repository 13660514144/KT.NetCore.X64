using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KT.Proxy.BackendApi.Models;
using KT.Common.WebApi.HttpApi;
using Microsoft.Extensions.Logging;

namespace KT.Proxy.BackendApi.Apis
{
    /// <summary>
    /// 公司
    /// </summary>
    public class SelfCompanyApi : BackendApiBase, ICompanyApi
    {
        public SelfCompanyApi(ILogger<SelfCompanyApi> logger) : base(logger)
        {
        }

        /// <summary>
        /// 判断公司能否访问指定楼层
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<VoidResponse> CheckVisitAsync(CompanyCheckVisitQuery query)
        {
            var result = await base.PostResponseAsync("/self/company/checkVisit", query);
            return result;
        }

        public Task<List<CompanyModel>> GetBuildingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<CompanyModel>> GetCompaniesAsync(string keyValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取树型结构公司列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<CompanyModel>> GetMapsAsync()
        {
            var result = await PostAsync<List<CompanyModel>>("/self/setting/maps");
            return result;
        }
    }
}
