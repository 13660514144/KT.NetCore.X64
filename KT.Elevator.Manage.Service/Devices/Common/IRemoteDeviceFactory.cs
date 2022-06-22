using KT.Elevator.Manage.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Devices.Common
{
    /// <summary>
    /// 分发数据服务工厂
    /// 在静态列表中引用，所以单例
    /// </summary>
    public interface IRemoteDeviceFactory
    {
        IRemoteDevice Creator(RemoteDeviceModel model);
    }
}
