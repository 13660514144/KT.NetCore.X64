using KT.Common.Core.Utils;
using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public class OpenApi : BackendApiBase
    {
        public ILogger<OpenApi> _logger;

        public OpenApi(ILogger<OpenApi> logger) : base(logger)
        {
            _logger = logger;
        }

        //上传数据是完整地址，不能设置BaseUrl
        public override string BaseUrl
        {
            get
            {
                return string.Empty;
            }
        }

        public async Task PushRecord(string url, PushPassRecordModel passRecord)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("equipmentId", passRecord.EquipmentId);
            parameters.Add("extra", passRecord.Extra);
            parameters.Add("equipmentType", passRecord.EquipmentType);
            parameters.Add("accessType", passRecord.AccessType);
            parameters.Add("accessToken", passRecord.AccessToken);
            parameters.Add("accessDate", passRecord.AccessDate);
            parameters.Add("wayType", passRecord.WayType);
            parameters.Add("description", passRecord.Remark);
            parameters.Add("temperature", passRecord.Temperature);
            parameters.Add("mask", passRecord.Mask);

            _logger.LogInformation($"上传数据：cardNo:{passRecord.AccessToken} data:{JsonConvert.SerializeObject(parameters, JsonUtil.JsonPrintSettings)} ");

            await ExecutePostFileBytesAsync(url, passRecord.File, "file.png", parameters);
        }

        public async Task PushFailAsync(string url, FailModel model)
        {
            var uri = new Uri(url);
            await PostAsync($"{uri.Scheme}://{uri.Authority}/openapi/auth/fail/push", model);
        }

        public async Task<long> GetSystemTime(string url)
        {
            var uri = new Uri(url);
            return await PostAsync<long>($"{uri.Scheme}://{uri.Authority}/system/time");
        }

    }
}
