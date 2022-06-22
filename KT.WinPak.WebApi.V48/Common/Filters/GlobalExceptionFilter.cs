using KT.Common.Core.Helpers;
using KT.Common.WebApi.HttpApi;
using KT.WinPak.SDK.V48.Settings;
using KT.WinPak.Service.V48.IServices;
using KT.WinPak.WebApi.V48.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KT.WinPak.WebApi.V48.Common.Filters
{
    /// <summary>
    /// 全局异常拦截器
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;
        private IUserService _userService;
        private AppSettings _appSettings;
        private IServiceProvider _serviceProvider;

        public GlobalExceptionFilter(ILoggerFactory logger, IServiceProvider serviceProvider, IOptions<AppSettings> appSettings)
        {
            _logger = logger.CreateLogger("GlobalExceptionFilter");
            _serviceProvider = serviceProvider;
            _appSettings = appSettings.Value;
        }
        public void OnException(ExceptionContext context)
        {
            //错误重新创建SDK对象
            //Task.Run(() =>
            //{
            //    Relogin(context.Exception.Message);
            //});

            ReloginAsync(context.Exception.Message).Wait();

            _logger.LogError("全局异常：{0} ", context.Exception);
            var error = VoidResponse.Error($"【WinPak服务】{context.Exception.Message} ");
            context.Result = new JsonResult(error);
        }
        public async Task ReloginAsync(string message)
        {
            //过程过程调用失败重新登录
            if (!ErrorHelper.IsRpcError(message))
            {
                return;
            }

            using (var socpe = _serviceProvider.CreateScope())
            {
                _userService = socpe.ServiceProvider.GetRequiredService<IUserService>();

                //重试登录SDK
                await RetryHelper.StartRetryAsync(_logger,
                    _appSettings.ReloginTimes,
                    new Action(() =>
                    {
                        _userService.Logout();
                    }),
                    new Action(() =>
                    {
                        _userService.ReloadAndLoginApp();
                    }),
                    new Action<Exception>((ex) =>
                    {
                        _logger.LogError("错误重新登录失败：retimes:{0} ex:{1} ", _appSettings.ReloginTimes, ex);
                    }));
            }
        }
    }
}

//        public void Relogin(string message)
//        {



//            //过程过程调用失败重新登录
//            if (message.Contains("0x800706BA")
//               || message.Contains("远程过程调用失败")
//               || message.Contains("0x800706BA")
//               || message.Contains("RPC 服务器不可用"))
//            {
//                using (var socpe = _serviceProvider.CreateScope())
//                {
//                    _userService = socpe.ServiceProvider.GetRequiredService<IUserService>();
//                    int reloginTimes = 0;
//                    Relogin(reloginTimes);
//                }
//            }
//        }

//        private void Relogin(int reloginTimes)
//        {
//            try
//            {
//                _userService.Logout();
//            }
//            finally
//            {
//                try
//                {
//                    //未超过重试次数重试登录
//                    if (reloginTimes <= _appSettings.ReloginTimes)
//                    {
//                        reloginTimes++;
//                        _userService.ReloadAndLoginApp();
//                    }
//                }
//                catch (Exception ex)
//                {
//                    //重试
//                    Thread.Sleep(reloginTimes * 1000);
//                    Relogin(reloginTimes);
//                    _logger.LogError("错误重新登录失败：times:{0} ex:{1} ", reloginTimes, ex);
//                }
//            }
//        }
//    }
//}
