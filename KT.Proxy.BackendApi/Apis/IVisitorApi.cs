using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.WebApi.HttpApi;
using KT.Proxy.BackendApi.Enums;
using KT.Proxy.BackendApi.Models;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public interface IVisitorApi
    {
        Task<DataResponse<object>> AddAsync(AppointmentModel appoint);
        Task<List<RegisterResultModel>> AuthAsync(AuthInfoModel model);
        Task<List<VisitorInfoModel>> CheckAsync(AuthCheckModel model);
        Task<VisitorInfoModel> GetVisitorInfoAsync(long id);
        Task<PageData<VisitorInfoModel>> GetVistorRecordsAsync(VisitorQuery query);
        Task<VisitorStatisticModel> StatisticAsync();
        Task OperateAsync(long id, VisitOperateTypeEnum status);
        RestResponse OperateWait(long id, VisitOperateTypeEnum status);
        Task<List<RegisterResultModel>> AddDirectAsync(AppointmentModel appoint);
        Task<PageData<VisitorImportModel>> GetVisitorImportLogAsync(PageQuery query);
        Task<PageData<VisitorImportDetailModel>> GetVisitorImportDetailAsync(VisitorImportDetailQuery query);
        Task AddVisitorImportAsync(VisitorImportModel model);
        Task<List<CompanyStaffModel>> GetCompanyOrStaff(CompanyStaffQuery query);
        Task<List<RegisterResultModel>> GetVisitorImportQrPrintAsync(VisitorImportQrPrintQuery query);
        Task EquipmentHeartAsync();
    }
}
