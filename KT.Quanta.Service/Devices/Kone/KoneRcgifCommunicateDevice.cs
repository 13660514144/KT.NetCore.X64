using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Kone.Clients;
using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Kone
{
    public class KoneRcgifCommunicateDevice : ICommunicateDevice
    {
        public CommunicateDeviceInfoModel CommunicateDeviceInfo { get; private set; }

        private IKoneRcgifClientHost _koneRcgifClientHost;
        public KoneRcgifCommunicateDevice(IKoneRcgifClientHost koneRcgifClientHost)
        {
            _koneRcgifClientHost = koneRcgifClientHost;
        }

        public async Task InitAsync(CommunicateDeviceInfoModel communicateDevice, RemoteDeviceModel remoteDevice)
        {
            CommunicateDeviceInfo = communicateDevice;

            await _koneRcgifClientHost.InitAsync(communicateDevice.IpAddress, communicateDevice.Port);
        }

        public async Task<bool> CheckAndLinkAsync()
        {
            CommunicateDeviceInfo.IsOnline = await _koneRcgifClientHost.CheckAndLinkAsync();
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
            return (T)_koneRcgifClientHost;
        }

        public async Task HeartbeatAsync()
        {
            await _koneRcgifClientHost.HeartbeatAsync();
        }

        public async Task CloseAsync()
        {
            await _koneRcgifClientHost.CloseAsync();
        }
    }
}
