using KT.Elevator.Manage.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.IServices
{
    public interface IReportService
    {
        List<DeviceStateModel> GetAllDeviceStates();
    }
}
