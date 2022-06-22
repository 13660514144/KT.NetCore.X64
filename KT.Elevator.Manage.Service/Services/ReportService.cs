using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Elevator.Manage.Service.Services
{
    /// <summary>
    /// 报表数据
    /// </summary>
    public class ReportService : IReportService
    {
        private RemoteDeviceList _remoteDeviceList;
        public ReportService(RemoteDeviceList processorDeviceList)
        {
            _remoteDeviceList = processorDeviceList;
        }

        /// <summary>
        /// 获取所有设备状态
        /// </summary>
        /// <returns>所有设备状态信息</returns>
        public List<DeviceStateModel> GetAllDeviceStates()
        {
            var results = new List<DeviceStateModel>();
            var processorStates = _remoteDeviceList.GetStates();
            if (processorStates != null && processorStates.FirstOrDefault() != null)
            {
                results.AddRange(processorStates);
            }
            return results;
        }
    }
}
