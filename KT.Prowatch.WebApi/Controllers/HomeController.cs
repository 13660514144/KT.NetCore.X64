using KT.Common.Core.Helpers;
using KT.Common.Core.Utils;
using Microsoft.AspNetCore.Mvc;

namespace KT.Prowatch.WebApi.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public string Index()
        {
            return "UTC DateTime Now:" + DateTimeUtil.UtcNowMillis().ToString();
        }

        [HttpGet("version")]
        public long GetVersion()
        {
            return VersionSetting.Code;
        }

        [HttpGet("versionText")]
        public string GetVersionText()
        {
            return VersionSetting.Text;
        }

        [HttpGet("versionValue")]
        public string GetVersionValue()
        {
            return VersionSetting.Value;
        }

    }
}
