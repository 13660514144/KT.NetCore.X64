using KT.WinPak.SDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.IServices
{
    public interface IHWDeviceSqlService
    {
        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns></returns>
        List<HWDeviceModel> GetAll();


        /// <summary>
        /// 获取所有卡
        /// </summary>
        /// <returns></returns>
        List<HWDeviceModel> GetAllReaders();
    }
}
