using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KT.Common.WebApi.HttpApi;
using KT.WinPak.WebApi.Common.Filters;
using KT.WinPak.Data.Models;
using KT.WinPak.Service.IServices;
using KT.WinPak.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KT.WinPak.WebApi.Common;
using KT.WinPak.SDK.Settings;
using Microsoft.Extensions.Options;
using KT.Common.Core.Helpers;
using KT.WinPak.WebApi.Common.Helpers;
using KT.Common.WebApi.HttpModel;

namespace KT.WinPak.WebApi.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private ILogger<UserController> _logger;
        private AppSettings _appsettings;

        public UserController(IUserService userService,
            ILogger<UserController> logger,
            IOptions<AppSettings> appsettings)
        {
            _userService = userService;
            _logger = logger;
            _appsettings = appsettings.Value;
        }

        /// <summary>
        /// 初始登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<DataResponse<TokenResponse>> LoginAsync([FromBody] LoginUserModel user)
        {
            var result = await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<Task<DataResponse<TokenResponse>>>(async () =>
                {
                    //登录
                    var result = await _userService.LoginAsync(user);
                    return DataResponse<TokenResponse>.Ok(result);
                }),
                new Action(() =>
                {
                    _userService.ReloadAndLoginApp();
                }),
                new Func<Exception, bool>((ex) =>
                {
                    return ErrorHelper.IsRpcError(ex);
                }));

            return result;
        }

        /// <summary>
        /// 反初始化
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        [TypeFilter(typeof(LoginUserAttribute))]
        public async Task<VoidResponse> LogoutAsync()
        {
            var result = await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<Task<VoidResponse>>(async () =>
                {
                    return await Task.Run(() => { 
                        _userService.Logout();
                        return VoidResponse.Ok();
                    });
                }),
                new Action(() =>
                {
                    _userService.ReloadAndLoginApp();
                }),
                new Func<Exception, bool>((ex) =>
                {
                    return ErrorHelper.IsRpcError(ex);
                }));

            return result;
        }

        /// <summary>
        /// 从数据库中获取最新的记录登录
        /// 通常用于启动时事件上传
        /// </summary>
        /// <returns></returns>
        [HttpGet("loginLastApp")]
        public async Task<VoidResponse> LoginLastAppAsync()
        {
            var result = await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<Task<VoidResponse>>(async () =>
                {
                    return await Task.Run(() => { 
                    _userService.Login();
                    return VoidResponse.Ok();
                    });
                }),
                new Action(() =>
                {
                    _userService.ReloadAndLoginApp();
                }),
                new Func<Exception, bool>((ex) =>
                {
                    return ErrorHelper.IsRpcError(ex);
                }));

            return result;
        }

    }
}
