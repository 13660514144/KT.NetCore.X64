using KT.WinPak.SDK.V48.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.IServices
{
    public interface ITimeZoneSqlService
    {
        /// <summary>
        /// 获取所有时区
        /// </summary>
        /// <returns></returns>
        List<TimeZoneModel> GetAll();
    }
}
