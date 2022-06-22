using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    /// <summary>
    /// 分发数据服务工厂
    /// 在静态列表中引用，所以单例
    /// </summary>
    public interface ICommunicateDeviceFactory
    {
        Task<ICommunicateDevice> CreatorAsync(CommunicateDeviceInfoModel model, RemoteDeviceModel remoteDevice);
    }
}
