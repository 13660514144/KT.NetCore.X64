using KT.Common.WebApi.HttpApi;
using KT.Common.WebApi.HttpModel;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.WebApi.Common.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Elevator
{
    /// <summary>
    /// 用户
    /// </summary>
    [ApiController]
    [Route("[controller]")]
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
        /// 登录
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
        /// 退出登录
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
