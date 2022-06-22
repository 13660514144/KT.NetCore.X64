using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.Core.Helpers;
using KT.Common.WebApi.HttpApi;
using KT.Proxy.BackendApi.Enums;
using KT.Proxy.BackendApi.Helpers;
using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public class FrontVisitorApi : BackendApiBase, IVisitorApi
    {
        private ILogger<FrontVisitorApi> _logger;
        private IMemoryCache _memoryCache;

        public FrontVisitorApi(ILogger<FrontVisitorApi> logger,
            IMemoryCache memoryCache) : base(logger)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 添加访客
        /// </summary>
        /// <param name="appoi"></param>
        /// <returns></returns>
        public async Task<DataResponse<object>> AddAsync(AppointmentModel appoint)
        {
            var codes = new List<int>() { 50001 };
            var result = await base.PostAsync<object>("/front/visitor/add", appoint, codes);
            return result;
        }

        /// <summary>
        /// 添加访客
        /// </summary>
        /// <param name="appoi"></param>
        /// <returns></returns>
        public async Task<List<RegisterResultModel>> AddDirectAsync(AppointmentModel appoint)
        {
            RegisterResultModel M = new RegisterResultModel();
            List<RegisterResultModel> LM = new List<RegisterResultModel>();
            try
            {
                var result = await base.PostAsync<List<RegisterResultModel>>("/front/visitor/add", appoint);
                return result;
            }
            catch (Exception e)
            {
                return LM;
            }
        }

        /// <summary>
        /// 验证查询
        /// </summary>
        /// <param name="ver"></param>
        /// <returns></returns>
        public async Task<List<VisitorInfoModel>> CheckAsync(AuthCheckModel model)
        {
            return await DebugHelper.DebugRunAsync(async () =>
            {
                await Task.Delay(2000);

                //Debug 测试
                var demoResults = new List<VisitorInfoModel>();
                var demoResult = new VisitorInfoModel();
                demoResult.Name = "吴丽贞";
                demoResult.IdNumber = "445221199407186960";
                demoResult.FaceImg = "/image/oss/202105/28/1622181936536mFC.jpg";
                demoResults.Add(demoResult);

                demoResult = new VisitorInfoModel();
                demoResult.Name = "吴丽贞";
                demoResult.IdNumber = "445221199407186960";
                demoResult.FaceImg = "/image/oss/202105/28/1622181936536mFC.jpg";
                demoResults.Add(demoResult);
                return demoResults;
            }, async () =>
            {
                var result = await PostAsync<List<VisitorInfoModel>>("/front/visitor/check", model);
                return result;
            });
        }

        /// <summary>
        /// 验证授权
        /// </summary>
        /// <param name="au"></param>
        /// <returns></returns>
        public async Task<List<RegisterResultModel>> AuthAsync(AuthInfoModel model)
        {
            return await DebugHelper.DebugRunAsync(async () =>
            {
                await Task.Delay(2000);

                //Debug 测试
                var demoResults = new List<RegisterResultModel>();
                var demoResult = new RegisterResultModel();
                demoResult.Name = "吴丽贞";
                demoResults.Add(demoResult);

                demoResult = new RegisterResultModel();
                demoResult.Name = "吴丽贞";
                demoResults.Add(demoResult);

                return demoResults;
            }, async () =>
            {
                var result = await PostAsync<List<RegisterResultModel>>("/front/visitor/auth", model);
                return result;
            });

        }

        /// <summary>
        /// 获取访客记录列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PageData<VisitorInfoModel>> GetVistorRecordsAsync(VisitorQuery query)
        {
            var result = await PostAsync<PageData<VisitorInfoModel>>("/front/visitor/list", query);
            return result;
        }

        /// <summary>
        /// 获取访客Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VisitorInfoModel> GetVisitorInfoAsync(long id)
        {
            var result = await PostAsync<VisitorInfoModel>("/front/visitor/info", new { id });
            return result;
        }

        /// <summary>
        /// 访客统计
        /// </summary>
        public async Task<VisitorStatisticModel> StatisticAsync()
        {
            if (string.IsNullOrEmpty(this.BaseUrl))
            {
                return null;
            }
            try
            {
                var result = await PostAsync<VisitorStatisticModel>("/front/visitor/statistic");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("获取配置信息错误：ex:{0} ", ex);
            }
            return null;
        }

        public async Task OperateAsync(long id, VisitOperateTypeEnum status)
        {
            await ExecutePostAsync("/front/visitor/operate", new { id, status = status.Value });            
        }
        
        public RestResponse OperateWait(long id, VisitOperateTypeEnum status)
        {
            return ExecutePost("/front/visitor/operate", new { id, status = status.Value });
  
        }
        
        public async Task<PageData<VisitorImportModel>> GetVisitorImportLogAsync(PageQuery query)
        {
            return await PostAsync<PageData<VisitorImportModel>>("/front/visitor/import/log", query);
        }

        public async Task<PageData<VisitorImportDetailModel>> GetVisitorImportDetailAsync(VisitorImportDetailQuery query)
        {
            return await PostAsync<PageData<VisitorImportDetailModel>>("/front/visitor/import/detail", query);
        }

        public async Task AddVisitorImportAsync(VisitorImportModel model)
        {
            var filePaths = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(model.Xls))
            {
                filePaths.Add("xls", model.Xls);
            }
            if (!string.IsNullOrEmpty(model.Zip))
            {
                filePaths.Add("zip", model.Zip);
            }

            var parmeters = new Dictionary<string, object>();
            parmeters.Add("id", model.Id);
            parmeters.Add("reason", model.Reason);
            parmeters.Add("beVisitCompanyName", model.BeVisitCompanyName);
            parmeters.Add("beVisitStaffName", model.BeVisitStaffName);
            //parmeters.Add("xls", model.Xls);
            //parmeters.Add("zip", model.Zip);
            parmeters.Add("endVisitDate", model.EndVisitDate);
            parmeters.Add("startVisitDate", model.StartVisitDate);
            parmeters.Add("status", model.Status);
            parmeters.Add("results", model.Results);
            parmeters.Add("createTime", model.CreateTime);
            parmeters.Add("updateTime", model.UpdateTime);
            parmeters.Add("beVisitFloorId", model.BeVisitFloorId);
            parmeters.Add("beVisitStaffId", model.BeVisitStaffId);
            parmeters.Add("once", model.Once);
            parmeters.Add("beVisitCompanyId", model.BeVisitCompanyId);

            await base.PostFileAsync("/front/visitor/import", filePaths, parmeters);
        }

        public async Task<List<CompanyStaffModel>> GetCompanyOrStaff(CompanyStaffQuery query)
        {
            var key = $"{CacheKeys.VisiotrImportCompanyStaff}:{JsonConvert.SerializeObject(query)}";
            var result = await _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                var setting = await PostAsync<List<CompanyStaffModel>>("/front/search/companyOrStaff", query);
                if (setting?.FirstOrDefault() == null)
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                }
                else
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30 * 60);
                }
                return setting;
            });
            return result;
        }

        public async Task<List<RegisterResultModel>> GetVisitorImportQrPrintAsync(VisitorImportQrPrintQuery query)
        {
            return await PostAsync<List<RegisterResultModel>>("/front/visitor/import/qr/print", query);
        }

        public async Task EquipmentHeartAsync()
        {
            await ExecutePostAsync("/openapi/equipment/heart");
        }
    }
}
