using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Turnstile.Unit.ClientApp.Service.IServices;
using KT.Turnstile.Unit.Entity.Entities;
using KT.Turnstile.Unit.Entity.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Service.Services
{
    public class ErrorService : IErrorService
    {
        private ILogger _logger;
        private ICardDeviceService _cardDeviceService;
        private IRightGroupService _rightGroupService;
        private IPassRightService _passRightService;

        public ErrorService(ILogger logger,
            ICardDeviceService cardDeviceService,
             IRightGroupService rightGroupService,
             IPassRightService passRightService)
        {
            _logger = logger;
            _cardDeviceService = cardDeviceService;
            _rightGroupService = rightGroupService;
            _passRightService = passRightService;
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
            if (entity.Type == DistributeTypeEnum.TURNSTILE_ADD_OR_UPDATE_CARD_DEVICE.Value)
            {
                var datas = JsonConvert.DeserializeObject<TurnstileUnitCardDeviceEntity>(entity.DataContent, JsonUtil.JsonSettings);
                await _cardDeviceService.AddOrUpdateAsync(datas);
            }
            else if (entity.Type == DistributeTypeEnum.TURNSTILE_DELETE_CARD_DEVICE.Value)
            {
                await _cardDeviceService.DeleteAsync(entity.DataContent, entity.EditTime);
            }
            else if (entity.Type == DistributeTypeEnum.TURNSTILE_ADD_OR_UPDATE_PASS_RIGHT.Value)
            {
                var datas = JsonConvert.DeserializeObject<TurnstileUnitPassRightEntity>(entity.DataContent, JsonUtil.JsonSettings);
                await _passRightService.AddOrUpdateAsync(datas);
            }
            else if (entity.Type == DistributeTypeEnum.TURNSTILE_DELETE_PASS_RIGHT.Value)
            {
                await _passRightService.Delete(entity.DataContent, entity.EditTime);
            }
            else if (entity.Type == DistributeTypeEnum.TURNSTILE_ADD_OR_UPDATE_RIGHT_GROUP.Value)
            {
                var datas = JsonConvert.DeserializeObject<TurnstileUnitRightGroupEntity>(entity.DataContent, JsonUtil.JsonSettings);
                await _rightGroupService.AddOrUpdateAsync(datas);
            }
            else if (entity.Type == DistributeTypeEnum.TURNSTILE_DELETE_RIGHT_GROUP.Value)
            {
                await _rightGroupService.DeleteAsync(entity.DataContent, entity.EditTime);
            }
            else
            {
                _logger.LogError($"未找到的错误数据类型：{JsonConvert.SerializeObject(entity, JsonUtil.JsonPrintSettings)} ");
            }
        }
    }
}
