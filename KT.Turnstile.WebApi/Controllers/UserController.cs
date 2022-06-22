using KT.Common.WebApi.HttpApi;
using KT.Common.WebApi.HttpModel;
using KT.Turnstile.Manage.Service.IServices;
using KT.Turnstile.Manage.WebApi.Common.Filters;
using KT.Turnstile.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.WebApi.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private ILogger<UserController> _logger;
        private ILoginUserService _service;

        public UserController(ILoginUserService loginUserService,
            ILogger<UserController> logger)
        {
            _service = loginUserService;
            _logger = logger;
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<LoginUserModel>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return DataResponse<List<LoginUserModel>>.Ok(result);
        }

        /// <summary>
        /// 初始登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<DataResponse<TokenResponse>> LoginAsync([FromBody] LoginUserModel user)
        {
            var result = await _service.LoginAsync(user);
            return DataResponse<TokenResponse>.Ok(result);
        }

        /// <summary>
        /// 反初始化
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        [TypeFilter(typeof(LoginUserAttribute))]
        public async Task<VoidResponse> LogoutAsync()
        {
            string token = "";
            await _service.LogoutAsync(token);
            return VoidResponse.Ok();
        }
    }
}
