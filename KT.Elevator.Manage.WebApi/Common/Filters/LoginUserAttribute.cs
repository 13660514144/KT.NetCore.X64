using KT.Common.WebApi.HttpApi;
using KT.Elevator.Manage.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace KT.Elevator.Manage.WebApi.Common.Filters
{
    public class LoginUserAttribute : ActionFilterAttribute
    {
        private ILogger _logger;
        private ILoginUserService _loginUserService;

        public LoginUserAttribute(ILoggerFactory logger, ILoginUserService loginUserService)
        {
            _logger = logger.CreateLogger("LoginUserAttribute");
            _loginUserService = loginUserService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 返回错误结果
        /// </summary>
        /// <param name="context">Action Executing Context</param>
        /// <param name="meg">错误信息</param>
        private void ResponseUnauthorized(ActionExecutingContext context, string meg)
        {
            _logger.LogError("Unauthorized：{0} ", meg);
            var error = VoidResponse.Error(string.Format("未受权的请求:{0}", meg));
            context.Result = new JsonResult(error);
        }
    }
}
