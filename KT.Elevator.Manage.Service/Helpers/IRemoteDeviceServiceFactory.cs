using KT.Elevator.Manage.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Helpers
{
    public interface IRemoteDeviceServiceFactory
    {
        IRemoteDeviceService CreateRemoteDeviceService(RemoteDeviceModel remoteDevice);
    }
}
