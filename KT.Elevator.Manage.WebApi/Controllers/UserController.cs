using KT.Common.WebApi.HttpApi;
using KT.Common.WebApi.HttpModel;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Manage.WebApi.Common.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.WebApi.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private ILoginUserService _loginUserService;
        private ILogger<UserController> _logger;


        public UserController(ILoginUserService loginUserService,
            ILogger<UserController> logger)
        {
            _loginUserService = loginUserService;
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
            var result = await _loginUserService.GetAllAsync();
            return DataResponse<List<LoginUserModel>>.Ok(result);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<DataResponse<TokenResponse>> LoginAsync([FromBody] LoginUserModel user)
        {
            var result = await _loginUserService.LoginAsync(user);
            return DataResponse<TokenResponse>.Ok(result);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        [TypeFilter(typeof(LoginUserAttribute))]
        public async Task<VoidResponse> LogoutAsync()
        {
            string token = "";
            await _loginUserService.LogoutAsync(token);
            return VoidResponse.Ok();
        }
    }
}
