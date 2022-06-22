using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Helpers;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
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
        public async Task<List<DeviceStateModel>> GetAllDeviceStatesAsync()
        {
            var results = new List<DeviceStateModel>();
            await _remoteDeviceList.ExecuteAsync((remoteDevice) =>
            {
                var result = new DeviceStateModel();
                result.Id = remoteDevice.RemoteDeviceInfo.DeviceId;
                result.IsOnline = remoteDevice.CommunicateDevices?.Any(x => x.CommunicateDeviceInfo.IsOnline) == true;
                result.AddressText = remoteDevice.CommunicateDevices?
                    .Select(x => $"{x.CommunicateDeviceInfo?.IpAddress}{GetPortString(x.CommunicateDeviceInfo?.Port)}")
                    .ToList()
                    .ToCommaString();
                results.Add(result);

                return Task.CompletedTask;
            });

            return results;
        }

        private string GetPortString(int? port)
        {
            return (port.HasValue && port.Value > 0) ? $":{port }" : string.Empty;
        }
    }
}
