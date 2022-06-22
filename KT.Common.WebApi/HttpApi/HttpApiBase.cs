using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KT.Common.WebApi.HttpApi
{
    /// <summary>
    /// Http Api 访问操作类
    /// </summary>
    public class HttpApiBase
    {
        private ILogger _logger;

        /// <summary>
        /// 访问URL地址
        /// </summary>
        public virtual string BaseUrl { get; }

        /// <summary>
        /// 表头
        /// </summary>
        public virtual Dictionary<string, string> BaseHeaders { get; }

        public HttpApiBase(ILogger logger)
        {
            _logger = logger;
        }

        private RestClient _client;

        private RestClient client
        {
            get
            {
                if (_client == null)
                {
                    _client = new RestClient();
                }
                return _client;
            }
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <typeparam name="T">反回数据Data</typeparam>
        /// <returns></returns>
        protected virtual async Task<RestResponse> ExecuteGetAsync(string partUrl, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            //发送请求
            var request = CreateRequest(partUrl, Method.GET, appendHeaders, isHasBaseHeader);
            var result = await SendAsync(request, partUrl);
            return result;
        }

        /// <summary>
        /// Post请求,带返回值
        /// </summary>
        /// <typeparam name="T">反回数据Data</typeparam>
        /// <returns>要获取的数据内部结果</returns>
        protected virtual async Task<RestResponse> ExecutePostAsync(string partUrl, object data, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            //_logger.LogInformation($"start time={DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");
            var request = CreateRequest(partUrl, Method.POST, appendHeaders, isHasBaseHeader);
            //_logger.LogInformation($"start time={DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");
            string json = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            //发送请求    
            var result = await SendAsync(request, json);
            //_logger.LogInformation($"start time={DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");
            return result;
        }
        protected virtual RestResponse ExecutePost(string partUrl, object data, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {

            var request = CreateRequest(partUrl, Method.POST, appendHeaders, isHasBaseHeader);
            string json = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            //发送请求    
            RestResponse result =  SendWait(request, json);
            return result;
        }
        /// <summary>
        /// Post请求,带返回值
        /// </summary>
        /// <typeparam name="T">反回数据Data</typeparam>
        /// <returns>要获取的完整结果</returns>
        protected virtual async Task<RestResponse> ExecutePostAsync(string partUrl, object data, List<HttpStatusCode> codes, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            var request = CreateRequest(partUrl, Method.POST, appendHeaders, isHasBaseHeader);
            string json = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

            //发送请求
            var result = await SendAsync(request, codes, json);
            return result;
        }

        ///// <summary>
        ///// Post请求,不带返回值
        ///// </summary> 
        //protected virtual async Task<RestResponse> PostAsync(string partUrl, object data, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        //{
        //    var request = CreateRequest(partUrl, Method.POST, appendHeaders, isHasBaseHeader);
        //    string json = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
        //    request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

        //    //发送请求
        //    return await SendAsync(request, json);
        //}

        /// <summary>
        /// Post请求
        /// </summary>
        protected virtual async Task<RestResponse> ExecutePostAsync(string partUrl, string json = "{}", Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            var request = CreateRequest(partUrl, Method.POST, appendHeaders, isHasBaseHeader);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

            //发送请求
            var result = await SendAsync(request, json);
            return result;
        }

        ///// <summary>
        ///// Post请求，不带返回值
        ///// </summary>
        //protected virtual async Task<RestResponse> PostAsync(string partUrl, string json = "{}", Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        //{
        //    var request = CreateRequest(partUrl, Method.POST, appendHeaders, isHasBaseHeader);
        //    request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
        //    //发送请求
        //    return await SendAsync(request, json);
        //}

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="partUrl">上传地址</param>
        /// <param name="filePath">本地文件路径</param>
        /// <param name="appendHeaders">增加的表头，在原有的表头上增加</param>
        /// <param name="isHasBaseHeader">是否使用原有表头，一般只有登录不用</param>
        /// <returns>反回数据结果</returns>
        protected virtual async Task<RestResponse> ExecutePostFilesAsync(string partUrl, Dictionary<string, string> filePaths, Dictionary<string, object> parameters = null, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            var request = CreateRequest(partUrl, Method.POST, appendHeaders, isHasBaseHeader);

            request.AddHeader("Content-Type", "multipart/form-data");

            //添加文件
            foreach (var item in filePaths)
            {
                request.AddFile(item.Key, item.Value);
            }

            //添加参数
            if (parameters != null && parameters.Count > 0)
            {
                foreach (var item in parameters)
                {
                    request.AddParameter(item.Key, item.Value);
                }
            }

            //发送请求
            var result = await SendAsync(request, filePaths.ToCommaString());
            return result;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="partUrl">上传地址</param>
        /// <param name="bytes">文件</param>
        /// <param name="fileName">文件名</param>
        /// <param name="appendHeaders">增加的表头，在原有的表头上增加</param>
        /// <param name="isHasBaseHeader">是否使用原有表头，一般只有登录不用</param>
        /// <returns>返回数据结果</returns>
        protected virtual async Task<RestResponse> ExecutePostFileBytesAsync(string partUrl,
            byte[] bytes,
            string fileName,
            Dictionary<string, object> parameters = null,
            Dictionary<string, string> appendHeaders = null,
            bool isHasBaseHeader = true,
            string fileBytesName = "file")
        {
            var request = CreateRequest(partUrl, Method.POST, appendHeaders, isHasBaseHeader);

            request.AddHeader("Content-Type", "multipart/form-data");

            if (bytes == null)
            {
                bytes = new byte[0];
            }
            request.AddFileBytes(fileBytesName, bytes, fileName);

            //添加参数
            if (parameters != null && parameters.Count > 0)
            {
                foreach (var item in parameters)
                {
                    request.AddParameter(item.Key, item.Value);
                }
            }

            //发送请求
            var result = await SendAsync(request);
            return result;
        }

        ///// <summary>
        ///// 上传文件，不带返回值
        ///// </summary> 
        ///// <param name="partUrl">上传地址</param>
        ///// <param name="bytes">文件</param>
        ///// <param name="fileName">文件名</param>
        ///// <param name="appendHeaders">增加的表头，在原有的表头上增加</param>
        ///// <param name="isHasBaseHeader">是否使用原有表头，一般只有登录不用</param> 
        //protected virtual async Task<RestResponse> PostFileBytesAsync(string partUrl, byte[] bytes, string fileName, Dictionary<string, object> parameters = null, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        //{
        //    var request = CreateRequest(partUrl, Method.POST, appendHeaders, isHasBaseHeader);

        //    request.AddHeader("Content-Type", "multipart/form-data");
        //    if (bytes == null)
        //    {
        //        bytes = new byte[0];
        //    }
        //    request.AddFileBytes("file", bytes, fileName);

        //    //添加参数
        //    if (parameters != null && parameters.Count > 0)
        //    {
        //        foreach (var item in parameters)
        //        {
        //            request.AddParameter(item.Key, item.Value);
        //        }
        //    }

        //    //发送请求
        //    return await SendAsync(request);
        //}

        /// <summary>
        /// Get请求
        /// </summary>
        /// <typeparam name="T">反回数据Data</typeparam>
        /// <returns></returns>
        protected virtual async Task<RestResponse> ExecuteGetFileAsync(string partUrl, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            //发送请求
            var request = CreateRequest(partUrl, Method.GET, appendHeaders, isHasBaseHeader);
            var result = await SendGetFileAsync(request);
            return result;
        }

        /// <summary>
        /// Post请求，不带返回值
        /// </summary>
        /// <param name="partUrl"></param>
        /// <param name="data"></param> 
        protected virtual async Task<RestResponse> ExecutePostFromAsync(string partUrl, string json, Dictionary<string, string> appendHeaders = null, bool isHasBaseHeader = true)
        {
            var request = CreateRequest(partUrl, Method.POST, appendHeaders, isHasBaseHeader);
            request.AddParameter("application/x-www-form-urlencoded; charset=utf-8", json, ParameterType.RequestBody);
            //发送请求
            return await SendAsync(request, json);
        }

        /// <summary>
        /// 创建请求对象
        /// </summary>
        /// <param name="partUrl"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private RestRequest CreateRequest(string partUrl, Method method, Dictionary<string, string> appendHeaders, bool isHasBaseHeader)
        {
            var url = (BaseUrl?.TrimEnd('/') + "/" + partUrl.TrimStart('/')).TrimStart('/');
            _logger.LogInformation($"appendHeaders===>{JsonConvert.SerializeObject(appendHeaders)}");
            _logger.LogInformation($"BaseHeaders===>{JsonConvert.SerializeObject(BaseHeaders)}");
            if (url.Length < 6 || url.Substring(0, 4).ToUpper() != "HTTP")
            {
                throw CustomException.Run("访问Api地址不正确：url:{0} ", url);
            }
            var request = new RestRequest(url, method);
            if (isHasBaseHeader && BaseHeaders != null && BaseHeaders.Count > 0)
            {
                request.AddHeaders(BaseHeaders);
            }
            if (appendHeaders != null && appendHeaders.Count > 0)
            {
                request.AddHeaders(appendHeaders);
            }
            return request;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="request">请求Request</param> 
        /// <param name="json">Post数据，只用于记录日记</param>
        /// <returns></returns>
        private async Task<RestResponse> SendAsync(RestRequest request, string json = "")
        {
            request.Timeout = 120000;
            var response = await RequestExecuteAsync(request, json);
            //过滤系统错误
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response;
            }
            else
            {
                throw CustomException.Run("网络访问错误：访问地址：{0} 状态码：{1} 错误信息：{2} ", client.BaseUrl + request.Resource, response.StatusCode, response.ErrorMessage);
            }
        }
        private  RestResponse SendWait(RestRequest request, string json = "")
        {

            request.Timeout = 120000;
            RestResponse response = RequestExecuteWait(request, json);
            //过滤系统错误
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response;
            }
            else
            {
                throw CustomException.Run("网络访问错误：访问地址：{0} 状态码：{1} 错误信息：{2} ", client.BaseUrl + request.Resource, response.StatusCode, response.ErrorMessage);
            }
            return response;
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="request">请求Request</param> 
        /// <param name="data">Post数据，只用于记录日记</param>
        /// <returns></returns>
        private async Task<RestResponse> SendGetFileAsync(RestRequest request)
        {
            request.Timeout = 120000;
            var response = await RequestExecuteAsync(request, string.Empty);
            //过滤系统错误
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //统一处理错误
                if (string.IsNullOrEmpty(response.Content))
                {
                    throw CustomException.Run("未获取到指定的文件！");
                }
                return response;
            }
            else
            {
                throw CustomException.Run("网络访问错误：访问地址：{0} 状态码：{1} 错误信息：{2} ", client.BaseUrl + request.Resource, response.StatusCode, response.ErrorMessage);
            }
        }

        ///// <summary>
        ///// 发送请求
        ///// </summary>
        ///// <param name="request">请求Request</param> 
        ///// <param name="data">Post数据，只用于记录日记</param>
        ///// <returns></returns>
        //private async Task<T> SendAsync<T>(RestRequest request, string json = "")
        //{
        //    request.Timeout = 120000;
        //    var response = await RequestExecuteAsync(request, json);
        //    //过滤系统错误
        //    if (response.StatusCode == HttpStatusCode.OK)
        //    {
        //        try
        //        {
        //            var reponseData = JsonConvert.DeserializeObject<DataResponse<T>>(response.Content, JsonUtil.JsonSettings);
        //            //统一处理错误
        //            if (reponseData.Code != 200)
        //            {
        //                throw CustomException.Run(reponseData.Message);
        //            }
        //            return reponseData.Data;
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError("数据转换错误：ex:{0} ", ex);
        //            var reponseData = JsonConvert.DeserializeObject<VoidResponse>(response.Content, JsonUtil.JsonSettings);
        //            //统一处理错误 
        //            throw CustomException.Run(reponseData.Message);
        //        }
        //    }
        //    else
        //    {
        //        throw CustomException.Run("网络访问错误：访问地址：{0} 状态码：{1} 错误信息：{2} ", client.BaseUrl + request.Resource, response.StatusCode, response.ErrorMessage);
        //    }
        //}

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="request">请求Request</param> 
        /// <param name="data">Post数据，只用于记录日记</param>
        /// <returns></returns>
        private async Task<RestResponse> SendAsync(RestRequest request, List<HttpStatusCode> codes, string json = "")
        {
            request.Timeout = 120000;
            var response = await RequestExecuteAsync(request, json);
            //过滤系统错误
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response;
            }
            else
            {
                if (codes?.Contains(response.StatusCode) == true)
                {
                    return response;
                }
                else
                {
                    throw CustomException.Run("网络访问错误：访问地址：{0} 状态码：{1} 错误信息：{2} ", client.BaseUrl + request.Resource, response.StatusCode, response.ErrorMessage);
                }
            }
        }

        ///// <summary>
        ///// 发送请求
        ///// </summary>
        ///// <param name="request">请求Request</param> 
        ///// <param name="data">Post数据，只用于记录日记</param>
        ///// <returns></returns>
        //private async Task<DataResponse<T>> SendAsync<T>(RestRequest request, List<int> codes, string json = "")
        //{
        //    request.Timeout = 120000;
        //    var response = await RequestExecuteAsync(request, json);
        //    //过滤系统错误
        //    if (response.StatusCode == HttpStatusCode.OK)
        //    {
        //        try
        //        {
        //            var reponseData = JsonConvert.DeserializeObject<DataResponse<T>>(response.Content, JsonUtil.JsonSettings);
        //            //指定状态码原样返回
        //            if (codes.Contains(reponseData.Code))
        //            {
        //                return reponseData;
        //            }
        //            //统一处理错误
        //            else if (reponseData.Code != 200)
        //            {
        //                throw CustomException.Run(reponseData.Message);
        //            }
        //            else
        //            {
        //                return reponseData;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError("数据转换错误：ex:{0} ", ex);
        //            var reponseData = JsonConvert.DeserializeObject<VoidResponse>(response.Content, JsonUtil.JsonSettings);
        //            //统一处理错误 
        //            throw CustomException.Run(reponseData.Message);
        //        }
        //    }
        //    else
        //    {
        //        throw CustomException.Run("网络访问错误：访问地址：{0} 状态码：{1} 错误信息：{2} ", client.BaseUrl + request.Resource, response.StatusCode, response.ErrorMessage);
        //    }
        //}

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="json">请求数据，用于打印</param>
        /// <returns>请求结果</returns>
        private async Task<RestResponse> RequestExecuteAsync(RestRequest request, string json = "")
        {
            var url = client.BaseUrl + request.Resource;
            _logger?.LogInformation($"提交数据：url:{url} data:{json} parameters:{request.Parameters.Select(x => $"{x.Name}:{x.Value}").ToList().ToCommaString()} ");
            var startTime = DateTimeUtil.UtcNowMillis();

            RestResponse response = await client.ExecuteAsync(request) as RestResponse;

            var endTime = DateTimeUtil.UtcNowMillis();
            var timeSecondSpan = (endTime - startTime) / 1000M;
            _logger?.LogInformation($"接收数据：timeSpan:{timeSecondSpan}s url:{url} data:{ response.Content} ");

            return response;
        }
        private RestResponse RequestExecuteWait(RestRequest request, string json = "")
        {
            var url = client.BaseUrl + request.Resource;
            _logger?.LogInformation($"提交数据：url:{url} data:{json} parameters:{request.Parameters.Select(x => $"{x.Name}:{x.Value}").ToList().ToCommaString()} ");
            var startTime = DateTimeUtil.UtcNowMillis();
            RestResponse response = (RestResponse)client.ExecuteAsPost(request,"POST");            
            var endTime = DateTimeUtil.UtcNowMillis();
            var timeSecondSpan = (endTime - startTime) / 1000M;
            _logger?.LogInformation($"接收数据：timeSpan:{timeSecondSpan}s url:{url} data:{ response.Content} ");
            return response;
        }
    }
}
