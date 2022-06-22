using KT.Common.Core.Helpers;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace KT.Quanta.WebApi.Controllers.Common
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private SerialConfigModel _serialConfig;
        private AppSettings _appSettings;

        public HomeController(ILogger<HomeController> logger,
            IOptions<SerialConfigModel> serialConfig,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _serialConfig = serialConfig.Value;
            _appSettings = appSettings.Value;
        }


        [HttpGet("")]
        public string Index()
        {
            return DateTimeUtil.NowSecondString();
        }

        [HttpGet("utc")]
        public long GetUtcString()
        {
            return DateTimeUtil.UtcNowMillis();
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

        [HttpGet("obj")]
        public List<object> Get()
        {
            List<object> resutls = new List<object>();
            resutls.Add(new CardDeviceModel());
            resutls.Add(new PassRightModel());
            resutls.Add(new TurnstileRelayDeviceModel());
            resutls.Add(new PassRecordModel());
            resutls.Add(new ProcessorModel());
            var serialConfig = TransExp<SerialConfigModel, SerialConfigModel>.Trans(_serialConfig);
            resutls.Add(serialConfig);

            return resutls;
        }
    }
}
