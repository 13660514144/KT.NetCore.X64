using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpUtil;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace KT.Common.WebApi.Filters
{
    public class GlobalRecordFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public GlobalRecordFilter(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("GlobalRecordFiler");
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate actionExecution)
        {
            var recored = Executing(context);

            await actionExecution.Invoke();

            Executed(context, recored.DateTimeStart, recored.Url);
        }

        private (DateTime DateTimeStart, string Url) Executing(ActionExecutingContext context)
        {
            var dateTimeStart = DateTime.Now;
            var url = $"request:{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}{context.HttpContext.Request.Path} ";
            var from = $"httpMethod:{context.HttpContext.Request.Method} ip:{context.HttpContext.GetIpAddress()} userAgent：{context.HttpContext.GetUserAgent()} ";

            _logger.LogInformation($"开始请求：{ url }{ from } -----------------------------------------------------------------------------------------");

            //Body请求并有数据打印请求数据
            if (context.ActionArguments != null
                && context.ActionArguments.Count > 0)
            {
                _logger.LogInformation($"请求数据：{JsonConvert.SerializeObject(context.ActionArguments, JsonUtil.JsonPrintSettings)} ");
            }

            return (dateTimeStart, url);
        }

        private void Executed(ActionExecutingContext context, DateTime dateTimeStart, string url)
        {
            //打印返回数据 
            _logger.LogInformation($"返回结果：{JsonConvert.SerializeObject(context.Result, JsonUtil.JsonPrintSettings)} ");
            var times = (DateTime.Now - dateTimeStart).TotalSeconds;

            _logger.LogInformation($"结束请求：{ times }s {url} =========================================================================================");
        }
    }
}
