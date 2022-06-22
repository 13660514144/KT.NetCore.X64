using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.Core.Helpers;
using KT.Common.WebApi.HttpApi;
using KT.Proxy.BackendApi.Enums;
using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public class SelfVisitorApi : BackendApiBase, IVisitorApi
    {
        public SelfVisitorApi(ILogger<SelfVisitorApi> logger) : base(logger)
        {
        }

        /// <summary>
        /// 添加访客
        /// </summary>
        /// <param name="appoi"></param>
        /// <returns></returns>
        public async Task<DataResponse<object>> AddAsync(AppointmentModel appoint)
        {
            //await DebugHelper.DebugRunAsync(async () =>
            //{
            //    await Task.Delay(90000);
            //});

            var codes = new List<int>() { 50001 };
            var result = await base.PostAsync<object>("/self/visitor/add", appoint, codes);
            return result;
        }

        /// <summary>
        /// 添加访客
        /// </summary>
        /// <param name="appoi"></param>
        /// <returns></returns>
        public async Task<List<RegisterResultModel>> AddDirectAsync(AppointmentModel appoint)
        {
            //await DebugHelper.DebugRunAsync(async () =>
            //{
            //    await Task.Delay(90000);
            //});
            RegisterResultModel M = new RegisterResultModel();
            List<RegisterResultModel> LM = new List<RegisterResultModel>();
            try
            {
                var result = await base.PostAsync<List<RegisterResultModel>>("/self/visitor/add", appoint);
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
            
            var result = await PostAsync<List<VisitorInfoModel>>("/self/visitor/check", model);
            return result;
            //         return await DebugHelper.DebugRunAsync(
            //async () =>
            //{

            //    var result = await PostAsync<List<VisitorInfoModel>>("/self/visitor/check", model);
            //    return result;
            //},
            //             async () =>
            //         {
            //             await Task.Delay(2000);

            //             //Debug 测试  生产中取消
            //             var demoResults = new List<VisitorInfoModel>();
            //             var demoResult = new VisitorInfoModel();
            //             //demoResult.Name = "吴丽贞";
            //             //demoResult.IdNumber = "445221199407186960";
            //             //demoResult.FaceImg = "/image/oss/202105/28/1622181936536mFC.jpg";
            //             //demoResults.Add(demoResult);

            //             //demoResult = new VisitorInfoModel();
            //             //demoResult.Name = "吴丽贞";
            //             //demoResult.IdNumber = "445221199407186960";
            //             //demoResult.FaceImg = "/image/oss/202105/28/1622181936536mFC.jpg";
            //             //demoResults.Add(demoResult);
            //             return demoResults;
            //         }                 
            //             );
        }

        /// <summary>
        /// 验证授权
        /// </summary>
        /// <param name="au"></param>
        /// <returns></returns>
        public async Task<List<RegisterResultModel>> AuthAsync(AuthInfoModel model)
        {
            var result = await PostAsync<List<RegisterResultModel>>("/self/visitor/auth", model);
            return result;
            /*
            return await DebugHelper.DebugRunAsync(
                async () =>
                {
                    var result = await PostAsync<List<RegisterResultModel>>("/self/visitor/auth", model);
                    return result;
                },
                async () =>
            {
                await Task.Delay(2000);

                //Debug 测试
                var demoResults = new List<RegisterResultModel>();
                var demoResult = new RegisterResultModel();
                //demoResult.Name = "东==哥";
                //demoResults.Add(demoResult);
                //Debug 测试
                return demoResults;
            });
            */
        }

        public Task<VisitorInfoModel> GetVisitorInfoAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<PageData<VisitorInfoModel>> GetVistorRecordsAsync(VisitorQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<VisitorStatisticModel> StatisticAsync()
        {
            throw new NotImplementedException();
        }

        public async Task OperateAsync(long id, VisitOperateTypeEnum status)
        {
            await ExecutePostAsync("/self/visitor/operate", new { id, status = status.Value });
        }
        public RestResponse OperateWait(long id, VisitOperateTypeEnum status)
        {
            return ExecutePost("/self/visitor/operate", new { id, status = status.Value });
        }
        public Task<PageData<VisitorImportModel>> GetVisitorImportLogAsync(PageQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<PageData<VisitorImportDetailModel>> GetVisitorImportDetailAsync(VisitorImportDetailQuery query)
        {
            throw new NotImplementedException();
        }

        public Task AddVisitorImportAsync(VisitorImportModel model)
        {
            throw new NotImplementedException();
        }

        public Task<List<CompanyStaffModel>> GetCompanyOrStaff(CompanyStaffQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<List<RegisterResultModel>> GetVisitorImportQrPrintAsync(VisitorImportQrPrintQuery query)
        {
            throw new NotImplementedException();
        }

        public async Task EquipmentHeartAsync()
        {
            await ExecutePostAsync("/openapi/equipment/heart");
        }
    }
}
