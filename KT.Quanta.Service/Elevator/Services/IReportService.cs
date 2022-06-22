using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IServices
{
    public interface IReportService
    {
        Task<List<DeviceStateModel>> GetAllDeviceStatesAsync();
    }
}
