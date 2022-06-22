
using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.WinPak.Service.IServices;
using KT.WinPak.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace KT.WinPak.WebApi.Common.Filters
{
    public class LoginUserAttribute : ActionFilterAttribute
    {
        private ILogger _logger;
        private IUserService _userService;

        public LoginUserAttribute(ILoggerFactory logger, IUserService userService)
        {
            _logger = logger.CreateLogger("LoginUserAttribute");
            _userService = userService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate actionExecution)
        {
            //var factory = context.HttpContext.RequestServices.GetService<ILoggerFactory>();
            //var logger = factory.CreateLogger<GlobalActionFilter>();
            //logger.LogWarning("全局ActionFilter执行之前");

            await ActionExecutingAsync(context);

            await actionExecution();



            //logger.LogWarning("全局ActionFilter执行之后");
        }

        public async Task ActionExecutingAsync(ActionExecutingContext context)
        {
            //得到表头token的值
            if (!context.HttpContext.Request.Headers.ContainsKey("token"))
            {
                ResponseUnauthorized(context, "找不到给定的token头。");
                return;
            }
            string token = context.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                ResponseUnauthorized(context, "token值为空");
                return;
            }
            //获取登录用户
            var userToken = await _userService.GetByTokenAsync(token);
            if (userToken == null)
            {
                ResponseUnauthorized(context, "找不到登录的用户。");
                return;
            }
            //初始化当前数据库与服务器连接 ,用户不一样时才初始化登录
            bool? isInit = userToken?.LoginUser?.Id?.Equals(MasterInfo.LoginUser?.Id);
            if (!isInit.HasValue || !isInit.Value)
            {
                _logger.LogInformation($"用户切换登录：{JsonConvert.SerializeObject(userToken, JsonUtil.JsonPrintSettings)} ");
                //用户未登录，先登录,系统中已经存在用户，只登录就好,事件上传也重新登录
                bool initResult = _userService.LoginApp(userToken.LoginUser, true);
                if (!initResult)
                {
                    ResponseUnauthorized(context, "初始化数据库与服务器失败！");
                    return;
                }
            }
            //设置登录用户
            context.HttpContext.Request.Headers.Add("userToken", JsonConvert.SerializeObject(userToken, JsonUtil.JsonSettings));

            _logger.LogInformation("登录验证完成！user:{0} token:{1} userId:{2} ", userToken.LoginUser.PCUser, userToken.Token, userToken.LoginUser.Id);

            //base.OnActionExecuting(context);
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
