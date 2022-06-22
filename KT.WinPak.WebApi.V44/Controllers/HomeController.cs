using KT.Common.Core.Helpers;
using KT.Common.Core.Utils;
using KT.WinPak.SDK.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KT.WinPak.WebApi.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {

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


        /// <summary>
        /// 用来生成想要的类的Json数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("obj")]
        public List<object> GetTestObject()
        {
            List<object> list = new List<object>();
            list.Add(new AccessLevelModel());
            list.Add(new HWDeviceModel());
            list.Add(new CardModel());
            list.Add(new TimeZoneModel());
            list.Add(new CardHolderModel());
            list.Add(new CardAndCardHolderModel());
            list.Add(new List<CardAndCardHolderModel>());

            return list;
        }
    }
}
