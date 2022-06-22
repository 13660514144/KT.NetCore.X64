using KT.Common.WebApi.HttpApi;
using KT.Proxy.BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public interface ICompanyApi
    {
        Task<List<CompanyModel>> GetBuildingsAsync();
        Task<List<CompanyModel>> GetCompaniesAsync(string keyValue);
        Task<List<CompanyModel>> GetMapsAsync();
        Task<VoidResponse> CheckVisitAsync(CompanyCheckVisitQuery query);
    }
}
