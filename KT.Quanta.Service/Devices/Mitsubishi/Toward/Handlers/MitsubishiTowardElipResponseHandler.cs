using DotNetty.Transport.Channels;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Models;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.ElsgwServers;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Toward.Handlers
{
    public class MitsubishiTowardElipResponseHandler : IMitsubishiTowardElipResponseHandler
    {
        private ILogger<MitsubishiTowardElipResponseHandler> _logger;
        private MitsubishiTowardElsgwUdpServerHostList _mitsubishiTowardElsgwUdpServerHostList;
        public MitsubishiTowardElipResponseHandler(ILogger<MitsubishiTowardElipResponseHandler> logger,
            MitsubishiTowardElsgwUdpServerHostList mitsubishiTowardElsgwUdpServerHostList)
        {
            _logger = logger;
            _mitsubishiTowardElsgwUdpServerHostList = mitsubishiTowardElsgwUdpServerHostList;
        }

        public Task HeartbeatAsync(MitsubishiElipCommunicationHeader header)
        {
            throw new NotImplementedException();
        }

        public async Task AcceptanceAsync(IChannelHandlerContext context,
            MitsubishiElipVerificationAcceptanceModel data,
            MitsubishiElipHandleElevatorSequenceModel sequence)
        {
            //根据派梯来源路径返回数据
            var mitsubishiTowardElsgwUdpServerHost = await _mitsubishiTowardElsgwUdpServerHostList.GetByElevatorAddressAsync(context.Channel.RemoteAddress);
            if (mitsubishiTowardElsgwUdpServerHost == null)
            {
                _logger.LogError($"派梯转换回调失败：找不到派梯接收服务！remoteAddress:{context.Channel.RemoteAddress} ");
                return;
            }

            //返回结果
            var elsgwAcceptance = new MitsubishiElsgwVerificationAcceptanceModel();
            elsgwAcceptance.SequenceNumber = sequence.SourceSequenceNumber;
            elsgwAcceptance.AssignedElevatorCarNumber = (byte)data.AssignedCarNumber;

            if (sequence.HandleElevatorMode == HandleElevatorModeEnum.SingleFloor.Value)
            {
                //单楼层派梯
                elsgwAcceptance.AcceptanceStatus = 0;
            }
            else if (sequence.HandleElevatorMode == HandleElevatorModeEnum.MultiFloor.Value)
            {
                //多楼层派梯
                elsgwAcceptance.AcceptanceStatus = 1;
            }
            else
            {
                _logger.LogError($"派梯转换回调失败：找不到派梯类型！");
                return;
            }


            //返回结果封装
            var elsgwAcceptanceHeader = new MitsubishiElsgwTransmissionHeader();
            elsgwAcceptanceHeader.Assistant.CommandNumber = (byte)MitsubishiElsgwCommandEnum.HandleResult.Code;
            elsgwAcceptanceHeader.Assistant.Datas = elsgwAcceptance.GetBytes().ToList();
            elsgwAcceptanceHeader.AddressDeviceNumber = data.AssignedBankNumber;

            //电梯参数
            elsgwAcceptanceHeader.AddressDeviceType = 17;
            elsgwAcceptanceHeader.AddressDeviceNumber = data.AssignedBankNumber;
            elsgwAcceptanceHeader.SenderDeviceType = 1;
            elsgwAcceptanceHeader.SenderDeviceNumber = data.AssignedBankNumber;

            //发送返回结果
            await mitsubishiTowardElsgwUdpServerHost.SendAsync(elsgwAcceptanceHeader, sequence.EndPoint);
        }
    }
}
