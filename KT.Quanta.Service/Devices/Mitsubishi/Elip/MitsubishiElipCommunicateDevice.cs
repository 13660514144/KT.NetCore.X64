using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients;
using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Models;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip
{
    public class MitsubishiElipCommunicateDevice : ICommunicateDevice
    {
        public CommunicateDeviceInfoModel CommunicateDeviceInfo { get; private set; }

        private IMitsubishiElipClientHost _mitsubishiClientHost;
        public MitsubishiElipCommunicateDevice(IMitsubishiElipClientHost mitsubishiClientHost)
        {
            _mitsubishiClientHost = mitsubishiClientHost;
        }

        public async Task InitAsync(CommunicateDeviceInfoModel communicateDevice, RemoteDeviceModel remoteDevice)
        {
            CommunicateDeviceInfo = communicateDevice;

            await _mitsubishiClientHost.InitAsync(communicateDevice.IpAddress, communicateDevice.Port);
        }

        public async Task<bool> CheckAndLinkAsync()
        {
            CommunicateDeviceInfo.IsOnline = await _mitsubishiClientHost.CheckAndLinkAsync();
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
            return (T)_mitsubishiClientHost;
        }

        public async Task HeartbeatAsync()
        {
            await _mitsubishiClientHost.HeartbeatAsync();
        }

        public async Task CloseAsync()
        {
            await _mitsubishiClientHost.CloseAsync();
        }
    }
}
