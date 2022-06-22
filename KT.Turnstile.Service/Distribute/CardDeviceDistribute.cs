using KT.Common.Core.Helpers;
using KT.Common.Core.Utils;
using KT.Turnstile.Manage.Service.IServices;
using KT.Turnstile.Common.Enums;
using KT.Turnstile.Model.Models;
using KT.Turnstile.Manage.Service.Helpers;
using KT.Turnstile.Manage.Service.Hubs;
using KT.Turnstile.Manage.Service.IDistribute;
using KT.Turnstile.Unit.Entity.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.Distribute
{
    public class CardDeviceDistribute : ICardDeviceDistribute
    {
        private IHubContext<DistributeHub> _distributeHub;
        private ProcessorDeviceList _processorDeviceList;
        private ILogger<CardDeviceDistribute> _logger;
        private IServiceProvider _serviceProvider;

        public CardDeviceDistribute(IHubContext<DistributeHub> distributeHub,
            ProcessorDeviceList processorDeviceList,
            ILogger<CardDeviceDistribute> logger,
            IServiceProvider serviceProvider)
        {
            _distributeHub = distributeHub;
            _processorDeviceList = processorDeviceList;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task AddOrUpdateAsync(CardDeviceModel model)
        {
            var processor = _processorDeviceList.GetByKey(model.Processor.ProcessorKey);
            if (processor == null)
            {
                _logger.LogWarning("分发读卡器设备-边缘处理器不存在！");
                return;
            }

            var data = new TurnstileUnitCardDeviceEntity();
            data.Id = model.Id;
            data.ProductType = model.Type;
            data.DeviceType = model.CardType;
            data.PortName = model.PortName;
             
            data.EditedTime = model.EditedTime;

            data.RelayDeviceIp = model.RelayDevice?.IpAddress;
            data.RelayDevicePort = model.RelayDevice?.Port ?? 0;
            data.RelayDeviceOut = model.RelayDeviceOut;
            data.HandleElevatorDeviceId = model.HandleElevatorDeviceId;

            if (model.SerialConfig != null)
            {
                data.Baudrate = model.SerialConfig.Baudrate;
                data.Databits = model.SerialConfig.Databits;
                data.Stopbits = model.SerialConfig.Stopbits;
                data.Parity = model.SerialConfig.Parity;
                data.ReadTimeout = model.SerialConfig.ReadTimeout;
                data.Encoding = model.SerialConfig.Encoding;
            }
            else
            {
                _logger.LogError("读卡器设备配置错误：data:{0} ", JsonConvert.SerializeObject(model, JsonUtil.JsonSettings));
            }

            if (processor == null)
            {
                _logger.LogWarning("分发读卡器设备-边缘处理器不存在：data:{0} ", JsonConvert.SerializeObject(data, JsonUtil.JsonSettings));
                return;
            }

            if (processor.IsOnline)
            {
                try
                {
                    _logger.LogDebug("分发读卡器数据：processor:{0} data:{1} ", JsonConvert.SerializeObject(processor, JsonUtil.JsonSettings),
                        JsonConvert.SerializeObject(data, JsonUtil.JsonSettings));
                    await _distributeHub.Clients.Client(processor.ConnectionId).SendAsync("AddOrEditCardDevice", data);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("分发读卡器设备错误：data:{0} ex:{1} ", JsonConvert.SerializeObject(data, JsonUtil.JsonSettings), ex);
                    await AddDistributeErrorAsync(ex.Message, processor, data);
                }
            }
            else
            {
                _logger.LogWarning("分发读卡器数据-边缘处理器离线：data:{0} ", JsonConvert.SerializeObject(data, JsonUtil.JsonSettings));
                await AddDistributeErrorAsync("边缘处理器离线", processor, data);
            }
        }

        /// <summary>
        /// 存储分发错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="processor"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task AddDistributeErrorAsync(string message, ProcessorModel processor, TurnstileUnitCardDeviceEntity data)
        {
            // 分发失败不重发,存储错误日记 
            var distributeErrorModel = new DistributeErrorModel();
            distributeErrorModel.Type = DistributeTypeEnum.TURNSTILE_ADD_OR_UPDATE_CARD_DEVICE.Value;
            distributeErrorModel.PartUrl = "AddOrEditCardDevice";
            distributeErrorModel.DataModelName = data.GetType().Name;
            distributeErrorModel.DataId = data.Id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
            distributeErrorModel.ProcessorId = processor.Id;

            //时间记录为数据时间
            distributeErrorModel.EditedTime = data.EditedTime;

            using (var scope = _serviceProvider.CreateScope())
            {
                var distributeErrorDataService = scope.ServiceProvider.GetRequiredService<IDistributeErrorService>();
                await distributeErrorDataService.AddRetryAsync(distributeErrorModel);
            }

            processor.HasDistributeError = true;
        }

        public async Task DeleteAsync(string processorKey, string id, long time)
        {
            var processor = _processorDeviceList.GetByKey(processorKey);
            if (processor == null)
            {
                _logger.LogWarning("分发读卡器设备-边缘处理器不存在！");
                return;
            }

            if (processor.IsOnline)
            {
                try
                {
                    _logger.LogDebug("分发删除读卡器数据：processor:{0} id:{1} ", JsonConvert.SerializeObject(processor, JsonUtil.JsonSettings), id);
                    await _distributeHub.Clients.Client(processor.ConnectionId).SendAsync("DeleteCardDevice", id, time);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("分发删除读卡器设备错误：id:{0} ex:{1} ", id, ex);
                    await AddDistributeErrorAsync(ex.Message, processor, id);
                }
            }
            else
            {
                _logger.LogWarning("分发删除读卡器数据-边缘处理器离线：id:{0} ", id);
                await AddDistributeErrorAsync("边缘处理器离线", processor, id);
            }
        }

        /// <summary>
        /// 存储分发错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="processor"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task AddDistributeErrorAsync(string message, ProcessorModel processor, string id)
        {
            // 分发失败不重发,存储错误日记 
            var distributeErrorModel = new DistributeErrorModel();
            distributeErrorModel.Type = DistributeTypeEnum.TURNSTILE_DELETE_CARD_DEVICE.Value;
            distributeErrorModel.PartUrl = "DeleteCardDevice";
            distributeErrorModel.DataModelName = id.GetType().Name;
            distributeErrorModel.DataId = id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = id;
            distributeErrorModel.ProcessorId = processor.Id;

            //时间记录为数据时间
            distributeErrorModel.EditedTime = DateTimeUtil.UtcNowMillis();

            using (var scope = _serviceProvider.CreateScope())
            {
                var distributeErrorDataService = scope.ServiceProvider.GetRequiredService<IDistributeErrorService>();
                await distributeErrorDataService.AddRetryAsync(distributeErrorModel);
            }

            processor.HasDistributeError = true;
        }
    }
}
