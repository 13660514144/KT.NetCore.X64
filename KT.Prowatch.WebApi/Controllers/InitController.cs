using KT.Common.WebApi.HttpModel;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.Service.Models;
using KT.Prowatch.Service.Services;
using KT.Prowatch.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace KT.Prowatch.WebApi.Controllers
{
    /// <summary>
    /// 初始化数据库与服务器
    /// </summary>
    [ApiController]
    [Route("")]
    public class InitController : ControllerBase
    {
        private ILogger<InitController> _logger;
        private ILoginUserService _service;

        public InitController(ILogger<InitController> logger, ILoginUserService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// 1、初始化接口
        /// </summary>
        /// <returns>返回Token</returns> 
        [HttpPost("init")]
        public async Task<ResponseData<TokenResponse>> InitAsync([FromBody] LoginUserModel loginUser)
        {
            TokenResponse result = await _service.LoginAsync(loginUser);

            //返回结果
            return ResponseData<TokenResponse>.Ok(result);
        }

        /// <summary>
        /// 2、反初始化接口
        /// </summary>
        /// <returns>是否成功</returns>
        [HttpGet("uninit")]
        //[LoginUserAuthorize]//检查是否当前登录用户
        public ResponseData<bool> Uninit()
        {
            //停止事件监听
            ApiHelper.PWApi.StopRecvRealEvent();

            //反初始化接口
            ApiHelper.PWApi.Uninit();

            //设置当前Prowatch初始化的连接
            LoginHelper.Instance.CurrentConnect = null;

            return ResponseData<bool>.Ok(true);
        }
    }
}