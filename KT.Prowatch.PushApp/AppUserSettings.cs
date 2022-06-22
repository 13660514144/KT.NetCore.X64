using KT.Prowatch.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Prowatch.PushApp
{
    public class AppUserSettings : LoginUserModel
    {
        /// <summary>
        /// 上传数据状态
        /// </summary>
        public List<string> PushStatus { get; set; } = new List<string>()
        {
            "本地授权",
            "主机准许",
            "防反传错误"
        };
    }
}
