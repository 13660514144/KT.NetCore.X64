using KT.Common.Core.Exceptions;
using KT.Common.WebApi.HttpApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace KT.Quanta.WebApi.Common.Filters
{
    /// <summary>
    /// 全局异常拦截器
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;
        public GlobalExceptionFilter(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("GlobalExceptionFilter");
        }
        public void OnException(ExceptionContext context)
        {
            _logger.LogError("全局异常：{0} ", context.Exception);
            var message = context.Exception.GetInnerMessage();
            var error = VoidResponse.Error($"【QuantaData服务】{message} ");
            context.Result = new JsonResult(error);
        }
    }
}
