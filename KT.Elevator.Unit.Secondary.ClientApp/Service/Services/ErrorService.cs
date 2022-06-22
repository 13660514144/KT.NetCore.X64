using KT.Common.Core.Utils;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Secondary.ClientApp.Events;
using KT.Elevator.Unit.Secondary.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using KT.Quanta.Common.Enums;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Services
{
    public class ErrorService : IErrorService
    {
        private ILogger _logger;
        private ICardDeviceService _cardDeviceService;
        private IPassRightService _passRightService;
        private IHandleElevatorDeviceService _handleElevatorDeviceService;
        private ConfigHelper _configHelper;
        private IEventAggregator _eventAggregator;

        public ErrorService(ILogger logger,
            ICardDeviceService cardDeviceService,
             IPassRightService passRightService,
             IHandleElevatorDeviceService handleElevatorDeviceService,
            ConfigHelper configHelper,
            IEventAggregator eventAggregator)
        {
            _logger = logger;
            _cardDeviceService = cardDeviceService;
            _passRightService = passRightService;
            _handleElevatorDeviceService = handleElevatorDeviceService;
            _configHelper = configHelper;
            _eventAggregator = eventAggregator;
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
                await _cardDeviceService.Delete(entity.DataContent, entity.EditedTime);
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
                _configHelper.LocalConfig = await _handleElevatorDeviceService.AddOrEditAsync(model, _configHelper.LocalConfig);
            }
            else
            {
                _logger.LogError($"未找到的错误数据类型：{JsonConvert.SerializeObject(entity, JsonUtil.JsonPrintSettings)} ");
            }
        }
    }
}
