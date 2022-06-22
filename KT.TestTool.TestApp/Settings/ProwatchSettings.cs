using KT.Prowatch.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.TestTool.TestApp.Settings
{
    public class ProwatchSettings
    {
        public string BaseUrl { get; set; }
        public ProwatchLoginUserSettings LoginUser { get; set; }
    }

    public class ProwatchLoginUserSettings : LoginUserModel
    {

    }
}
