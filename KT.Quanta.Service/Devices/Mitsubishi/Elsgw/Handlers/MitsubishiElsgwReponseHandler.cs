using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;
using KT.Quanta.Service.Devices.Quanta.DistributeDatas;
using KT.Quanta.Service.Devices.Quanta.Models;
using KT.Quanta.Service.Helpers;
using KT.Quanta.Service.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Handlers
{
    /// <summary>
    /// 通力派梯返回结果操作
    /// 当前类对象对应每个连接只初始化一次
    /// </summary>
    public class MitsubishiElsgwReponseHandler : IMitsubishiElsgwReponseHandler
    {
        private ILogger<MitsubishiElsgwReponseHandler> _logger;
        private IServiceScopeFactory _serviceScopeFactory;
        private IMitsubishiElipHandleElevatorSequenceList _mitsubishiElipHandleElevatorSequenceList;
        private RemoteDeviceList _remoteDeviceList;
        private FloorHandleElevatorResponseList _floorHandleElevatorResponseList;

        public MitsubishiElsgwReponseHandler(ILogger<MitsubishiElsgwReponseHandler> logger,
            IServiceScopeFactory serviceScopeFactory,
            IMitsubishiElipHandleElevatorSequenceList mitsubishiElipHandleElevatorSequenceList,
            RemoteDeviceList remoteDeviceList,
            FloorHandleElevatorResponseList floorHandleElevatorResponseList)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _mitsubishiElipHandleElevatorSequenceList = mitsubishiElipHandleElevatorSequenceList;
            _remoteDeviceList = remoteDeviceList;
            _floorHandleElevatorResponseList = floorHandleElevatorResponseList;
        }

        /// <summary>
        /// 心跳 8000000011
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public Task HeartbeatAsync(MitsubishiElsgwTransmissionHeader header)
        {
            var data = new MitsubishiElsgwHeartbeatModel();
            data.ReadFromBytes(header.Assistant.Datas.ToArray());

            _logger.LogInformation($"派梯接收心跳数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

            return Task.CompletedTask;
        }

        public async Task SingleFloorCallAsync(MitsubishiElsgwTransmissionHeader header)
        {
            var data = new MitsubishiElsgwVerificationAcceptanceModel();
            data.ReadFromBytes(header.Assistant.Datas.ToArray());

            _logger.LogInformation($"三菱==>派梯接收派梯结果数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

            //获取数据来源
            var sequence = _mitsubishiElipHandleElevatorSequenceList.Get(data.SequenceNumber);
            if (sequence == null)
            {
                _logger.LogError($"派梯接收派梯结果数据：未找到派梯对列数据！");
                return;
            }

            //获取派梯设备
            var remoteDevice = await _remoteDeviceList.GetByIdAsync(sequence.HandleElevatorDeviceId);
            if (remoteDevice == null)
            {
                throw CustomException.Run($"获取派梯返回设备信息出错：找不到派梯设备！");
            }

            //电梯厅选层器
            if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SELECTOR.Value)
            {
                //三菱电梯厅选层器
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.MITSUBISHI_ELIP_SELECTOR.Value)
                {
                    //显示派梯结果
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var elevatorService = scope.ServiceProvider.GetRequiredService<IElevatorInfoService>();
                        var elevator = await elevatorService.GetByElevatorGroupIdAndRealIdAsync(remoteDevice.RemoteDeviceInfo.ParentId, data.AssignedElevatorCarNumber.ToString());
                        var elevatorName = string.IsNullOrEmpty(elevator?.Name) ? data.AssignedElevatorCarNumber.ToString() : elevator.Name;
                        _logger.LogError($"电梯名：elevatorName={elevatorName}");
                        //回调http派梯
                        _floorHandleElevatorResponseList.EndHandle(sequence.MessageId,
                            new Elevator.Dtos.FloorHandleElevatorSuccessModel()
                            {
                                FloorName = sequence.FloorName,
                                ElevatorName = elevatorName
                            });
                    }

                    //TODO:暂时改三菱电梯厅选层器返回数据
                    return;
                }
            }

            var elevatorDisplay = new ElevatorDisplayModel();
            elevatorDisplay.ElevatorId = data.AssignedElevatorCarNumber.ToString();
            elevatorDisplay.MessageId = sequence.MessageId.ToString();
            elevatorDisplay.HandleElevatorDeviceId = sequence.HandleElevatorDeviceId;
            elevatorDisplay.FloorName = sequence.FloorName;
            elevatorDisplay.MessageId = sequence.MessageId;

            //显示派梯结果
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var quantaDisplayDistributeDataService = scope.ServiceProvider.GetRequiredService<IQuantaDisplayDistributeDataService>();
                await quantaDisplayDistributeDataService.HandleElevatorSuccess(elevatorDisplay);
            }
        }

        public Task MultiFloorCallAsync(MitsubishiElsgwTransmissionHeader response)
        {
            throw new System.NotImplementedException();
        }
    }
}
