using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KT.Quanta.WebApi.Class
{
    public class HttpFactoryOwner : IHttpFactoryOwner
    {
        /// <summary>
        /// 注入http请求
        /// </summary>
        private readonly IHttpClientFactory httpClientFactory;

        public HttpFactoryOwner(IHttpClientFactory httpClient)
        {
            httpClientFactory = httpClient;
        }

    

        // <summary>
        // Get请求数据
        // <para>最终以url参数的方式提交</para>
        // </summary>
        // <param name="parameters">参数字典,可为空</param>
        // <param name="requestUri">例如/api/Files/UploadFile</param>
        // <returns></returns>
        public async Task<string> GetFactory(Dictionary<string, string> parameters, string requestUri, string token)
        {
            //从工厂获取请求对象
            var client = httpClientFactory.CreateClient();
            //添加请求头
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json; charset=utf-8");
            //拼接地址
            if (parameters != null)
            {
                var strParam = string.Join("&", parameters.Select(o => o.Key + "=" + o.Value));
                requestUri = string.Concat(requestUri, '?', strParam);
            }
            client.BaseAddress = new Uri(requestUri);
            return client.GetStringAsync(requestUri).Result;
        }
        public async Task<string> PostFactory(string Json, string requestUri, string token)
        {
            var todoItemJson = new StringContent(
                Json,
                Encoding.UTF8,
                "application/json");
            var client = httpClientFactory.CreateClient();
            //添加请求头
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Add("Authorization", token);
            }
            HttpResponseMessage response = client.PostAsync(requestUri, todoItemJson).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
