using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Schindler.Clients;
using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Schindler
{
    public class SchindlerCommunicateDevice : ICommunicateDevice
    {
        public CommunicateDeviceInfoModel CommunicateDeviceInfo { get; private set; }

        private IServiceProvider _serviceProvider;
        private ISchindlerClientHostBase _schindlerClientHost;
        public SchindlerCommunicateDevice(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task InitAsync(CommunicateDeviceInfoModel communicateDevice, RemoteDeviceModel remoteDevice)
        {
            if (communicateDevice.CommunicateDeviceType == CommunicateDeviceTypeEnum.SCHINDLER_SYNC.Value)
            {
                _schindlerClientHost = _serviceProvider.GetRequiredService<ISchindlerDatabaseClientHost>();
                _schindlerClientHost.CommunicateDeviceInfo = communicateDevice;
            }
            else if (communicateDevice.CommunicateDeviceType == CommunicateDeviceTypeEnum.SCHINDLER_DISPATCH.Value)
            {
                _schindlerClientHost = _serviceProvider.GetRequiredService<ISchindlerDispatchClientHost>();
                _schindlerClientHost.CommunicateDeviceInfo = communicateDevice;
            }
            else if (communicateDevice.CommunicateDeviceType == CommunicateDeviceTypeEnum.SCHINDLER_RECORD.Value)
            {
                _schindlerClientHost = _serviceProvider.GetRequiredService<ISchindlerReportClientHost>();
                _schindlerClientHost.CommunicateDeviceInfo = communicateDevice;
            }

            CommunicateDeviceInfo = communicateDevice;

            await _schindlerClientHost.InitAsync(communicateDevice.IpAddress, communicateDevice.Port);
        }

        public async Task<bool> CheckAndLinkAsync()
        {
            CommunicateDeviceInfo.IsOnline = await _schindlerClientHost.CheckAndLinkAsync();
            if (CommunicateDeviceInfo.IsOnline)
            {
                //更新最后在线时间
                CommunicateDeviceInfo.LastOnlineTime = DateTimeUtil.UtcNowMillis();
                //统计连接失败次数
                CommunicateDeviceInfo.ReloginTimes = 0;
            }
            else
            {
                //统计连接失败次数
                CommunicateDeviceInfo.ReloginTimes++;
            }
            return CommunicateDeviceInfo.IsOnline;
        }

        public T GetLoginUserClient<T>()
        {
            return (T)_schindlerClientHost;
        }

        public async Task HeartbeatAsync()
        {
            await _schindlerClientHost.HeartbeatAsync();
        }

        public async Task CloseAsync()
        {
            await _schindlerClientHost.CloseAsync();
        }
    }
}
