using KT.WinPak.SDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.IServices
{
    public interface ITimeZoneSdkService
    {
        /// <summary>
        /// 获取所有时区
        /// </summary>
        /// <returns></returns>
        List<TimeZoneModel> GetAll();
    }
}
