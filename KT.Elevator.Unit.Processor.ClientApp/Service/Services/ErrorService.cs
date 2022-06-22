using KT.Common.Core.Utils;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Processor.ClientApp.Events;
using KT.Elevator.Unit.Processor.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Processor.ClientApp.Service.IServices;
using KT.Quanta.Common.Enums;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Services
{
    public class ErrorService : IErrorService
    {
        private ILogger _logger;
        private ICardDeviceService _cardDeviceService;
        private IPassRightService _passRightService;
        private IHandleElevatorDeviceService _handleElevatorDeviceService;
        private ConfigHelper _configHelper;
        private IEventAggregator _eventAggregator;
        private readonly IElevatorGroupService _elevatorGroupService;
        private readonly ISystemConfigService _systemConfigService;

        public ErrorService(ILogger logger,
            ICardDeviceService cardDeviceService,
             IPassRightService passRightService,
             IHandleElevatorDeviceService handleElevatorDeviceService,
            ConfigHelper configHelper,
            IEventAggregator eventAggregator,
            IElevatorGroupService elevatorGroupService,
            ISystemConfigService systemConfigService)
        {
            _logger = logger;
            _cardDeviceService = cardDeviceService;
            _passRightService = passRightService;
            _handleElevatorDeviceService = handleElevatorDeviceService;
            _configHelper = configHelper;
            _eventAggregator = eventAggregator;
            _elevatorGroupService = elevatorGroupService;
            _systemConfigService = systemConfigService;
        }

        public async Task AddOrUpdateAsync(List<UnitErrorModel> entities)
        {
            if (entities == null || entities.FirstOrDefault() == null)
            {
                return;
            }
            foreach (var item in entities)
            {
                await AddOrUpdateAsync(item);
            }
        }

        public async Task AddOrUpdateAsync(UnitErrorModel entity)
        {
            if (entity.Type == DistributeTypeEnum.ELEVATOR_ADD_OR_UPDATE_CARD_DEVICE.Value)
            {
                var datas = JsonConvert.DeserializeObject<UnitCardDeviceEntity>(entity.DataContent, JsonUtil.JsonSettings);
                await _cardDeviceService.AddOrUpdateAsync(datas);
            }
            else if (entity.Type == DistributeTypeEnum.ELEVATOR_DELETE_CARD_DEVICE.Value)
            {
                await _cardDeviceService.DeleteAsync(entity.DataContent, entity.EditedTime);
            }
            else if (entity.Type == DistributeTypeEnum.ELEVATOR_ADD_OR_UPDATE_PASS_RIGHT.Value)
            {
                var data = JsonConvert.DeserializeObject<UnitPassRightEntity>(entity.DataContent, JsonUtil.JsonSettings);
                data = await _passRightService.AddOrUpdateAsync(data);

                //发布权限更新事件，写入内存
                _eventAggregator.GetEvent<AddOrEditPassRightsEvent>().Publish(new List<UnitPassRightEntity>() { data });
            }
            else if (entity.Type == DistributeTypeEnum.ELEVATOR_DELETE_PASS_RIGHT.Value)
            {
                await _passRightService.Delete(entity.DataContent, entity.EditedTime);

                //发布权限更新事件，从内存中删除
                _eventAggregator.GetEvent<DeletePassRightEvent>().Publish(entity.DataContent);
            }
            else if (entity.Type == DistributeTypeEnum.ELEVATOR_ADD_OR_EDIT_HANDLE_ELEVATOR_DEVICE.Value)
            {
                var model = JsonConvert.DeserializeObject<UnitHandleElevatorDeviceModel>(entity.DataContent, JsonUtil.JsonSettings);

                //配置本地文件 
                _configHelper.LocalConfig = await _systemConfigService.AddOrEditFromDeviceAsync(model, _configHelper.LocalConfig);

                // 新增电梯组
                await _elevatorGroupService.AddOrEditFromDeviceAsync(model);

                // 新增或修改派梯设备
                await _handleElevatorDeviceService.AddOrEditAsync(model);
            }
            else
            {
                _logger.LogError("未找到的错误数据类型：{0} ", JsonConvert.SerializeObject(entity, JsonUtil.JsonPrintSettings));
            }
        }
    }
}
