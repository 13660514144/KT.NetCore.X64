using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    /// <summary>
    /// 闸机显示服务
    /// </summary>
    public interface ITurnstileDisplayRemoteService
    {
        Task PassShowAsync(string deviceId);
    }
}
