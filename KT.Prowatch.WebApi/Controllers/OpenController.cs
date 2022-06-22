using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.Service.Models;
using KT.Prowatch.Service.Services;
using KT.Prowatch.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KT.Prowatch.WebApi.Controllers
{
    /// <summary>
    /// 开放接口
    /// </summary>
    [ApiController]
    [Route("")]
    public class OpenController : ControllerBase
    {
        private ILoginUserService _loginUserService;

        public OpenController(ILoginUserService loginUserService)
        {
            _loginUserService = loginUserService;
        }

        /// <summary>
        /// 获取当前或默认连接字符
        /// </summary>
        /// <returns></returns>
        [HttpGet("open/getConnect")]
        public async Task<ResponseData<LoginUserModel>> GetConnectAsync()
        {
            var connect = LoginHelper.Instance.CurrentConnect;
            if (connect == null)
            {
                connect = await _loginUserService.GetLastAsync();
            }
            return ResponseData<LoginUserModel>.Ok(connect);
        }
    }
}