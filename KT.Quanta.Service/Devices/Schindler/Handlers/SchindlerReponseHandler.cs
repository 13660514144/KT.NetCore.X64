using KT.Common.Core.Utils;
using KT.Common.Event;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Quanta.DistributeDatas;
using KT.Quanta.Service.Devices.Schindler.Events;
using KT.Quanta.Service.Devices.Schindler.Helpers;
using KT.Quanta.Service.Devices.Schindler.Models;
using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Handlers;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Schindler
{
    /// <summary>
    /// 迅达派梯返回结果操作
    /// 当前类对象对应每个连接只初始化一次
    /// </summary>
    public class SchindlerReponseHandler : ISchindlerReponseHandler
    {
        private ILogger<SchindlerReponseHandler> _logger;
        private IServiceScopeFactory _serviceScopeFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISchindlerHandleElevatorSequenceList _schindlerHandleElevatorSequenceList;

        public SchindlerReponseHandler(ILogger<SchindlerReponseHandler> logger,
            IServiceScopeFactory serviceScopeFactory,
            IEventAggregator eventAggregator,
            ISchindlerHandleElevatorSequenceList schindlerHandleElevatorSequenceList)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _eventAggregator = eventAggregator;
            _schindlerHandleElevatorSequenceList = schindlerHandleElevatorSequenceList;
        }

        public Task ChangeInsertPersonAsync(SchindlerTelegramHeaderResponse headResponse)
        {
            _logger.LogInformation($"迅达新增或修改人员完成：response:{JsonConvert.SerializeObject(headResponse, JsonUtil.JsonPrintSettings)} ");

            _eventAggregator.GetEvent<DatabaseResponseEvent>().Publish();

            return Task.CompletedTask;
        }

        public Task ChangeInsertPersonNackAsync(SchindlerTelegramHeaderResponse headResponse)
        {
            var response = new SchindlerDatabaseChangeInsertPersonReponse();
            response.ReadFromValues(headResponse.Datas);

            _logger.LogError($"迅达新增或修改人员错误1：headResponse:{JsonConvert.SerializeObject(headResponse, JsonUtil.JsonPrintSettings)} ");
            _logger.LogError($"迅达新增或修改人员错误2：code:{response.Code} message:{SchindlerDatabaseErrorCodeEnum.GetByCode(response.Code)?.Text} ");

            _eventAggregator.GetEvent<DatabaseResponseEvent>().Publish();

            return Task.CompletedTask;
        }

        public Task SetZoneAccessResponseAsync(SchindlerTelegramHeaderResponse headResponse)
        {
            _logger.LogInformation($"迅达新增或修改ZoneAccess完成：response:{JsonConvert.SerializeObject(headResponse, JsonUtil.JsonPrintSettings)} ");

            _eventAggregator.GetEvent<DatabaseResponseEvent>().Publish();

            return Task.CompletedTask;
        }

        public Task DeletePersonAsync(SchindlerTelegramHeaderResponse headResponse)
        {
            _logger.LogInformation($"迅达删除人员完成：response:{JsonConvert.SerializeObject(headResponse, JsonUtil.JsonPrintSettings)} ");

            _eventAggregator.GetEvent<DatabaseResponseEvent>().Publish();

            return Task.CompletedTask;
        }

        public Task DeletePersonNackAsync(SchindlerTelegramHeaderResponse headResponse)
        {
            var response = new SchindlerDatabaseDeletePersonReponse();
            response.ReadFromValues(headResponse.Datas);

            _logger.LogError($"迅达删除人员错误1：headResponse:{JsonConvert.SerializeObject(headResponse, JsonUtil.JsonPrintSettings)} ");
            _logger.LogError($"迅达删除人员错误2：code:{response.Code} message:{SchindlerDatabaseErrorCodeEnum.GetByCode(response.Code)?.Text} ");

            _eventAggregator.GetEvent<DatabaseResponseEvent>().Publish();

            return Task.CompletedTask;
        }

        /// <summary>
        /// 心跳 8000000011
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public Task HeartbeatAsync(List<string> buffer)
        {
            var data = new SchindlerDatabaseHeartbeatResponse();
            data.ReadFromValues(buffer);

            _logger.LogInformation($"迅达派梯接收心跳数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

            return Task.CompletedTask;
        }

        public async Task PaddleAsync(SchindlerTelegramHeaderResponse headResponse)
        {
            var data = new SchindlerDispatchDirectCallReponse();
            data.ReadFromValues(headResponse.Datas);

            _logger.LogInformation($"迅达派梯接收派梯结果数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

            _eventAggregator.GetEvent<DatabaseResponseEvent>().Publish();

            //获取数据来源
            var sequence = _schindlerHandleElevatorSequenceList.Get(headResponse.MessageId);
            if (sequence == null)
            {
                _logger.LogError($"派梯接收派梯结果数据：未找到派梯对列数据！");
                return;
            }

            //显示派梯结果
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var quantaDisplayDistributeDataService = scope.ServiceProvider.GetRequiredService<IQuantaDisplayDistributeDataService>();
                await quantaDisplayDistributeDataService.HandleElevatorSuccess(sequence.MessageId);
            }
        }

        public async Task UploadPassAsync(SchindlerReportAllocationResponse result, CommunicateDeviceInfoModel communicateInfo)
        {
            //处理数据
            result.PersonId = result.PersonId.TrimStart('0');

            //显示派梯结果
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                //派梯数据
                var passRecord = new PassRecordModel();

                //获取权限信息
                PassRightModel passRight = null;
                var personService = scope.ServiceProvider.GetRequiredService<IPersonService>();
                var person = await personService.GetWithRights(result.PersonId);
                if (person != null && person.PassRights.FirstOrDefault() != null)
                {
                    passRight = person.PassRights.FirstOrDefault(x => x.AccessType == AccessTypeEnum.IC_CARD.Value);
                    if (passRight == null)
                    {
                        passRight = person.PassRights.FirstOrDefault();
                    }
                }
                if (passRight == null)
                {
                    var passRightService = scope.ServiceProvider.GetRequiredService<IPassRightService>();
                    passRight = await passRightService.GetBySignAsync(result.PersonId);
                }
                passRecord.AccessType = passRight?.AccessType;
                passRecord.PassRightSign = passRight?.Sign;

                // 获取设备  
                var remoteDeviceList = scope.ServiceProvider.GetRequiredService<RemoteDeviceList>();
                var remoteDevices = await remoteDeviceList.GetByConnectionIdAsync(communicateInfo.Id);
                var remoteDevice = remoteDevices?.FirstOrDefault(x => x.RemoteDeviceInfo.DeviceType
                                                                    == DeviceTypeEnum.ELEVATOR_SERVER_GROUP.Value);

                passRecord.DeviceType = remoteDevice?.RemoteDeviceInfo?.DeviceType;
                passRecord.DeviceId = remoteDevice?.RemoteDeviceInfo?.DeviceId;

                //楼层
                if (remoteDevice != null)
                {
                    var elevatorGroupFloorDao = scope.ServiceProvider.GetRequiredService<IElevatorGroupFloorDao>();
                    var elevatorGroupDestinationFloor = await elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndRealFloorIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId, result.DestinationFloor);
                    if (elevatorGroupDestinationFloor != null)
                    {
                        passRecord.Remark = elevatorGroupDestinationFloor.Floor?.Name;
                    }
                }

                //获取楼层 
                var floorService = scope.ServiceProvider.GetRequiredService<IFloorService>();

                //上传事件
                passRecord.PassTime = DateTimeUtil.UtcNowMillis();
                passRecord.PassLocalTime = passRecord.PassTime.ToZoneDateTimeByMillis()?.ToSecondString();

                var pushRecordHandler = scope.ServiceProvider.GetRequiredService<PushRecordHandler>();
                await pushRecordHandler.StartPushAsync(passRecord);
            }
        }
    }
}
