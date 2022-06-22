using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpUtil;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KT.Common.WebApi.Helpers
{
    public static class GlobalRecordMiddleware
    {
        public static IApplicationBuilder UseGlobalRecord(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Use(async (context, next) =>
            {
                var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("UseGlobalRecord");
                var dateTimeStart = DateTime.Now;

                var traceIdentifier = context.TraceIdentifier;
                var fromUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
                var fromInfo = $"httpMethod:{context.Request.Method} remoteAddress:{context.Connection.RemoteIpAddress.MapToIPv4()}:{context.Connection.RemotePort} userAgent：{context.GetUserAgent()} ";

                logger.LogInformation($"{traceIdentifier}-开始请求：{ fromUrl } -----------------------------------------------------------------------------------------");
                logger.LogInformation($"{traceIdentifier}-请求信息：{ fromInfo } ");

                ////Body请求并有数据打印请求数据
                if (context.Request.ContentLength > 0)
                {
                    context.Request.EnableBuffering();

                    var reader = new StreamReader(context.Request.Body, Encoding.UTF8);
                    var requestBodyContent = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;

                    logger.LogInformation($"{traceIdentifier}-请求数据：{requestBodyContent} ");
                }

                //打印返回数据 
                var originalResponseStream = context.Response.Body;
                using var responseBodyMs = new MemoryStream();
                context.Response.Body = responseBodyMs;

                //向下执行
                await next.Invoke();

                responseBodyMs.Position = 0;
                var responseReader = new StreamReader(responseBodyMs);
                var responseBodyContent = await responseReader.ReadToEndAsync();
                responseBodyMs.Position = 0;

                await responseBodyMs.CopyToAsync(originalResponseStream);
                context.Response.Body = originalResponseStream;

                logger.LogInformation($"{traceIdentifier}-返回结果：{responseBodyContent} ");

                var times = (DateTime.Now - dateTimeStart).TotalSeconds;

                logger.LogInformation($"{traceIdentifier}-结束请求：{times}s {fromUrl} =========================================================================================");
            });
            return applicationBuilder;
        }


    }
}
