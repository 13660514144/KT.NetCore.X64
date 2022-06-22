using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KT.Common.Version;
using KT.Common.Core.Utils;
using KT.Turnstile.Model.Models;
using KT.Turnstile.Model.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KT.Turnstile.Manage.WebApi.Controllers
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
         


        [HttpGet("obj")]
        public List<object> Get()
        {
            List<object> resutls = new List<object>();
            resutls.Add(new CardDeviceModel());
            resutls.Add(new PassRightModel());
            resutls.Add(new RelayDeviceModel());
            resutls.Add(new PassRecordModel());
            resutls.Add(new ProcessorModel());
            var serialConfig = TransExp<SerialConfigModel, SerialConfigModel>.Trans(_serialConfig);
            resutls.Add(serialConfig);
            resutls.Add(new CardReceiveDataModel());

            return resutls;
        }
    }
}
