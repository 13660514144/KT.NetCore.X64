using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Common.WebApi.HttpModel;
using KT.Proxy.QuantaApi.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public class BackendApiBase : HttpApiBase
    {
        public override string BaseUrl
        {
            get
            {
                return RequestHelper.BackendBaseUrl;
            }
        }

        private readonly ILogger _logger;
        public BackendApiBase(ILogger logger) : base(logger)
        {
            _logger = logger;
        }

        protected async Task<T> GetAsync<T>(string partUrl, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            var response = await base.ExecuteGetAsync(partUrl, appendHeaders, isHasBaseHeader);
            return GetResponseResult<T>(response);
        }

        protected async Task<T> PostAsync<T>(string partUrl, object data, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            var response = await base.ExecutePostAsync(partUrl, data, appendHeaders, isHasBaseHeader);
            return GetResponseResult<T>(response);
        }

        protected async Task<T> PostAsync<T>(string partUrl, string json = "{}", Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            var response = await base.ExecutePostAsync(partUrl, json, appendHeaders, isHasBaseHeader);
            return GetResponseResult<T>(response);
        }

        protected async Task<T> PostFileAsync<T>(string partUrl, string filePath, Dictionary<string, object> parameters = null, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            var filePaths = new Dictionary<string, string>();
            filePaths.Add("file", filePath);

            var response = await base.ExecutePostFilesAsync(partUrl, filePaths, parameters, appendHeaders, isHasBaseHeader);
            return GetResponseResult<T>(response);
        }

        protected async Task<T> PostFileAsync<T>(string partUrl, Dictionary<string, string> filePaths, Dictionary<string, object> parameters = null, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            var response = await base.ExecutePostFilesAsync(partUrl, filePaths, parameters, appendHeaders, isHasBaseHeader);
            return GetResponseResult<T>(response);
        }

        protected async Task<T> PostFileBytesAsync<T>(string partUrl, byte[] bytes, string fileName, Dictionary<string, object> parameters = null, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            var response = await base.ExecutePostFileBytesAsync(partUrl, bytes, fileName, parameters, appendHeaders, isHasBaseHeader);
            return GetResponseResult<T>(response);
        }

        protected async Task<DataResponse<T>> PostAsync<T>(string partUrl, object data, List<int> codes, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            var response = await base.ExecutePostAsync(partUrl, data, appendHeaders, isHasBaseHeader);
            return GetResponseResult<T>(response, codes);
        }

        protected async Task PostAsync(string partUrl, object data, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            await base.ExecutePostAsync(partUrl, data, appendHeaders, isHasBaseHeader);
        }

        protected async Task PostAsync(string partUrl, string json = "{}", Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            await base.ExecutePostAsync(partUrl, json, appendHeaders, isHasBaseHeader);
        }

        protected async Task PostFileAsync(string partUrl, string filePath, Dictionary<string, object> parameters = null, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            var filePaths = new Dictionary<string, string>();
            filePaths.Add("file", filePath);

            await base.ExecutePostFilesAsync(partUrl, filePaths, parameters, appendHeaders, isHasBaseHeader);
        }

        protected async Task PostFileAsync(string partUrl, Dictionary<string, string> filePaths, Dictionary<string, object> parameters = null, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            await base.ExecutePostFilesAsync(partUrl, filePaths, parameters, appendHeaders, isHasBaseHeader);
        }

        protected async Task PostFileBytesAsync(string partUrl, byte[] bytes, string fileName, Dictionary<string, object> parameters = null, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            await base.ExecutePostFileBytesAsync(partUrl, bytes, fileName, parameters, appendHeaders, isHasBaseHeader);
        }

        protected async Task PostFromAsync(string partUrl, string json, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            await base.ExecutePostFromAsync(partUrl, json, appendHeaders, isHasBaseHeader);
        }

        protected async Task<FileInfoModel> GetFileAsync(string partUrl, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            var response = await base.ExecuteGetFileAsync(partUrl, appendHeaders, isHasBaseHeader);

            var result = new FileInfoModel
            {
                Bytes = response.RawBytes,
                FileType = response.ContentType
            };

            return result;
        }

        private T GetResponseResult<T>(RestResponse response)
        {
            var reponseData = JsonConvert.DeserializeObject<DataResponse<T>>(response.Content, JsonUtil.JsonSettings);
            //统一处理错误
            if (reponseData.Code != 200)
            {
                throw CustomException.Run($"网络请求错误：{reponseData.Message} ");
            }
            return reponseData.Data;
        }

        private DataResponse<T> GetResponseResult<T>(RestResponse response, List<int> codes)
        {
            try
            {
                var reponseData = JsonConvert.DeserializeObject<DataResponse<T>>(response.Content, JsonUtil.JsonSettings);
                //指定状态码原样返回
                if (codes.Contains(reponseData.Code))
                {
                    return reponseData;
                }
                //统一处理错误
                else if (reponseData.Code != 200)
                {
                    throw CustomException.Run($"网络请求错误：{reponseData.Message} ");
                }
                else
                {
                    return reponseData;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("数据转换错误：ex:{0} ", ex);
                var reponseData = JsonConvert.DeserializeObject<VoidResponse>(response.Content, JsonUtil.JsonSettings);

                //统一处理错误 
                throw CustomException.Run($"网络请求错误：{reponseData.Message} ");
            }
        }

    }
}
