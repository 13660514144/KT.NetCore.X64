using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Models;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Toward.Handlers
{
    public class MitsubishiTowardElsgwRequestHandler : IMitsubishiTowardElsgwRequestHandler
    {
        private readonly ILogger<MitsubishiTowardElsgwRequestHandler> _logger;
        private IMitsubishiElipHandleElevatorSequenceList _mitsubishiElipHandleElevatorSequenceList;

        public IMitsubishiElipClientHostBase MitsubishiElipClientHost { get; set; }

        public MitsubishiTowardElsgwRequestHandler(ILogger<MitsubishiTowardElsgwRequestHandler> logger,
            IMitsubishiElipHandleElevatorSequenceList mitsubishiElipHandleElevatorSequenceList)
        {
            _logger = logger;
            _mitsubishiElipHandleElevatorSequenceList = mitsubishiElipHandleElevatorSequenceList;
        }

        public async Task HeartbeatAsync(IChannelHandlerContext context, MitsubishiElsgwTransmissionHeader response, EndPoint endPoint)
        {
            _logger.LogInformation($"MitsubishiTowardElsgwRequestHandler转发心跳：{JsonConvert.SerializeObject(response, JsonUtil.JsonPrintSettings)} ");
            //await _mitsubishiTowardElipTcpClientHost.HeartbeatAsync();

            //心跳数据
            var elgswHeartbeat = new MitsubishiElsgwHeartbeatModel();
            elgswHeartbeat.ReadFromBytes(response.Assistant.Datas.ToArray());

            //心跳数据封装
            var elsgwHeartbeatHeader = new MitsubishiElsgwTransmissionHeader();
            elsgwHeartbeatHeader.Assistant.CommandNumber = (byte)MitsubishiElsgwCommandEnum.Heartbeat.Code;
            elsgwHeartbeatHeader.Assistant.Datas = elgswHeartbeat.GetBytes().ToList();

            //发送数据
            var byteBuffer = elsgwHeartbeatHeader.WriteToLocalBuffer();
            await context.WriteAndFlushAsync(new DatagramPacket(byteBuffer, endPoint));
        }

        public async Task SingleFloorCallAsync(MitsubishiElsgwTransmissionHeader response, EndPoint endPoint)
        {
            _logger.LogInformation($"MitsubishiTowardElsgwRequestHandler转发单楼层派梯：{JsonConvert.SerializeObject(response, JsonUtil.JsonPrintSettings)} ");

            //接收数据
            var elsgwSingleFloorCallModel = new MitsubishiElsgwSingleFloorCallDataModel();
            elsgwSingleFloorCallModel.ReadFromBytes(response.Assistant.Datas.ToArray());

            //派梯数据
            var elipAutomaticAccessRequest = new MitsubishiElipAutomaticAccessRequest();
            elipAutomaticAccessRequest.SwipedCardReaderNumber = elsgwSingleFloorCallModel.DeviceNumber;
            elipAutomaticAccessRequest.DestinationFloor = elsgwSingleFloorCallModel.DestinationFloor;

            //派梯数据封装
            var elipAutomaticAccessHeader = new MitsubishiElipCommunicationHeader();
            elipAutomaticAccessHeader.Assistant.Command = (byte)MitsubishiElipCommandEnum.AutomaticAccess.Code;
            elipAutomaticAccessHeader.Assistant.Datas = elipAutomaticAccessRequest.GetBytes().ToList();

            //存储访问通道与发送序号
            var sequence = new MitsubishiElipHandleElevatorSequenceModel();
            sequence.SourceSequenceNumber = elsgwSingleFloorCallModel.SequenceNumber;
            sequence.EndPoint = endPoint;
            sequence.HandleElevatorType = HandleElevatorTypeEnum.MitsubishiToward.Value;
            sequence.HandleElevatorMode = HandleElevatorModeEnum.SingleFloor.Value;
            _mitsubishiElipHandleElevatorSequenceList.Add(elipAutomaticAccessRequest.SequenceNumber, sequence);

            //发送派梯数据
            await MitsubishiElipClientHost.SendAsync(elipAutomaticAccessHeader);
        }

        public async Task MultiFloorCallAsync(MitsubishiElsgwTransmissionHeader response, EndPoint endPoint)
        {
            _logger.LogInformation($"MitsubishiTowardElsgwRequestHandler转发多楼层派梯：{JsonConvert.SerializeObject(response, JsonUtil.JsonPrintSettings)} ");

            //接收数据
            var elsgwMultiFloorCallModel = new MitsubishiElsgwMultiFloorCallDataModel();
            elsgwMultiFloorCallModel.ReadFromBytes(response.Assistant.Datas.ToArray());

            //派梯数据
            var elipManualAccessRequest = new MitsubishiElipManualAccessRequest();
            elipManualAccessRequest.SwipedCardReaderNumber = elsgwMultiFloorCallModel.DeviceNumber;
            elipManualAccessRequest.AccesibleFloors = elsgwMultiFloorCallModel.FrontDestinationFloors;

            //派梯数据封装
            var elipManualAccessRequestHeader = new MitsubishiElipCommunicationHeader();
            elipManualAccessRequestHeader.Assistant.Command = (byte)MitsubishiElipCommandEnum.ManualAccess.Code;
            elipManualAccessRequestHeader.Assistant.Datas = elipManualAccessRequest.GetBytes().ToList();

            //存储访问通道与发送序号
            var sequence = new MitsubishiElipHandleElevatorSequenceModel();
            sequence.SourceSequenceNumber = elsgwMultiFloorCallModel.SequenceNumber;
            sequence.EndPoint = endPoint;
            sequence.HandleElevatorType = HandleElevatorTypeEnum.MitsubishiToward.Value;
            sequence.HandleElevatorMode = HandleElevatorModeEnum.MultiFloor.Value;
            _mitsubishiElipHandleElevatorSequenceList.Add(elipManualAccessRequest.SequenceNumber, sequence);

            //发送派梯数据
            await MitsubishiElipClientHost.SendAsync(elipManualAccessRequestHeader);
        }
    }
}
