using KT.WinPak.WebApi.V48.Common.Queues;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.WinPak.WebApi.V48.Common.Filters
{
    public class SingletonExecuteFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        private SingletonExecuteQueue _singletonExecuteQueue;

        public SingletonExecuteFilter(ILoggerFactory logger, SingletonExecuteQueue singletonExecuteQueue)
        {
            _logger = logger.CreateLogger("SingletonExecuteFilter");
            _singletonExecuteQueue = singletonExecuteQueue;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //_logger.LogInformation("访问开始等待!!!");
            //var action = new Action(() =>
            //{
            //    base.OnActionExecuting(context);
            //    _logger.LogInformation("访问等待完成!!!");
            //});
            //_singletonExecuteQueue.Add(action); 

            base.OnActionExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }
    }
}
