using KT.Quanta.Service.Devices.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Quanta
{
    /// <summary>
    /// 闸机显示远程服务
    /// </summary>
    public class QuantaTurnstileDisplayRemoteService : ITurnstileDisplayRemoteService
    {
        public Task PassShowAsync(string deviceId)
        {
            return Task.CompletedTask;
        }
    }
}
