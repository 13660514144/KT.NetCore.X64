using KT.Common.WebApi.HttpApi;
using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    /// <summary>
    /// 公司
    /// </summary>
    public class FrontCompanyApi : BackendApiBase, ICompanyApi
    {
        public FrontCompanyApi(ILogger<FrontCompanyApi> logger) : base(logger)
        {
        }

        /// <summary>
        /// 判断公司能否访问指定楼层
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<VoidResponse> CheckVisitAsync(CompanyCheckVisitQuery query)
        {
            var result = await base.PostResponseAsync("/front/company/checkVisit", query);
            return result;
        }

        /// <summary>
        /// 查询公司--根据单元号或公司名称模糊搜索
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public async Task<List<CompanyModel>> GetCompaniesAsync(string keyValue)
        {
            var result = await PostAsync<List<CompanyModel>>("/front/base/search", new { search = keyValue });
            return result;
        }

        /// <summary>
        /// 获取树型结构公司列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<CompanyModel>> GetMapsAsync()
        {
            var result = await PostAsync<List<CompanyModel>>("/front/base/maps");
            return result;
        }

        /// <summary>
        /// 查询大厦列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<CompanyModel>> GetBuildingsAsync()
        {
            var result = await PostAsync<List<CompanyModel>>("/front/base/edifices");
            return result;
        }
    }
}
