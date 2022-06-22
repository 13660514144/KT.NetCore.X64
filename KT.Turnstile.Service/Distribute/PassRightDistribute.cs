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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.Distribute
{
    public class PassRightDistribute : IPassRightDistribute
    {
        private IHubContext<DistributeHub> _distributeHub;
        private ProcessorDeviceList _processorDeviceList;
        private ILogger<PassRightDistribute> _logger;
        private IServiceProvider _serviceProvider;

        public PassRightDistribute(IHubContext<DistributeHub> distributeHub,
            ProcessorDeviceList processorDeviceList,
            ILogger<PassRightDistribute> logger,
            IServiceProvider serviceProvider)
        {
            _distributeHub = distributeHub;
            _processorDeviceList = processorDeviceList;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task AddOrUpdate(PassRightModel model)
        {
            return Task.Run(() =>
            {
                if (model.CardDeviceRightGroups == null
                || model.CardDeviceRightGroups.FirstOrDefault() == null)
                {
                    _logger.LogWarning("分发权限通行权限信息不存在：data:{0} ", JsonConvert.SerializeObject(model, JsonUtil.JsonSettings));
                    return;
                }

                var result = new TurnstileUnitPassRightEntity();
                result.Id = model.Id;
                result.CardNumber = model.CardNumber;
                result.TimeStart = model.TimeNow;
                result.TimeEnd = model.TimeOut;
                result.EditedTime = model.EditedTime;

                foreach (var item in model.CardDeviceRightGroups)
                {
                    if (item == null)
                    {
                        continue;
                    }

                    var passRightDetail = new TurnstileUnitPassRightDetailEntity();
                    passRightDetail.EditedTime = item.EditedTime;

                    passRightDetail.RightGroup = new TurnstileUnitRightGroupEntity();
                    passRightDetail.RightGroup.Id = item.Id;

                    result.Details.Add(passRightDetail);
                }

                //发送权限到关联的边缘处理器
                _processorDeviceList.ExecAll(async (processor) =>
                {
                    if (processor.IsOnline)
                    {
                        try
                        {
                            _logger.LogDebug("分发权限数据：processor:{0} data:{1} ", JsonConvert.SerializeObject(processor, JsonUtil.JsonSettings),
                                JsonConvert.SerializeObject(result, JsonUtil.JsonSettings));
                            await _distributeHub.Clients.Client(processor.ConnectionId).SendAsync("AddOrEditPassRight", result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning("分发通行权限错误：data:{0} ex:{1} ", JsonConvert.SerializeObject(result, JsonUtil.JsonSettings), ex);
                            await AddDistributeErrorAsync(ex.Message, processor, result);
                        }
                    }
                    else if (!string.IsNullOrEmpty(processor.ProcessorKey))
                    {
                        _logger.LogWarning("分发通行权限数据-边缘处理器离线：data:{0} ", JsonConvert.SerializeObject(result, JsonUtil.JsonSettings));
                        await AddDistributeErrorAsync("边缘处理器离线", processor, result);
                    }
                    else
                    {
                        _logger.LogWarning("分发权限组数据-边缘处理器未连接：data:{0} ", JsonConvert.SerializeObject(result, JsonUtil.JsonSettings));
                    }
                });
            });
        }

        /// <summary>
        /// 存储分发错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="processor"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task AddDistributeErrorAsync(string message, ProcessorModel processor, TurnstileUnitPassRightEntity data)
        {
            // 分发失败不重发,存储错误日记 
            var distributeErrorModel = new DistributeErrorModel();
            distributeErrorModel.Type = DistributeTypeEnum.TURNSTILE_ADD_OR_UPDATE_PASS_RIGHT.Value;
            distributeErrorModel.PartUrl = "AddOrEditPassRight";
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

        public Task DeleteAsync(string id, long time)
        {
            return Task.Run(() =>
            {
                //发送权限到关联的边缘处理器
                _processorDeviceList.ExecAll(async (processor) =>
                {
                    if (processor.IsOnline)
                    {
                        try
                        {
                            _logger.LogDebug("分发删除权限数据：id:{0} processor:{1} ", id, JsonConvert.SerializeObject(processor, JsonUtil.JsonSettings));
                            await _distributeHub.Clients.Client(processor.ConnectionId).SendAsync("DeletePassRight", id, time);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning("分发删除通行权限设备错误：id:{0} ex:{1} ", id, ex);
                            await AddDistributeErrorAsync(ex.Message, processor, id);
                        }
                    }
                    else if (!string.IsNullOrEmpty(processor.ProcessorKey))
                    {
                        _logger.LogWarning("分发删除通行权限数据-边缘处理器离线：id:{0} ", id);
                        await AddDistributeErrorAsync("边缘处理器离线", processor, id);
                    }
                    else
                    {
                        _logger.LogWarning("分发删除通行权限数据-边缘处理器未连接：id:{0} processor:{1} ", id, JsonConvert.SerializeObject(processor, JsonUtil.JsonSettings));
                    }
                });
            });
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
            distributeErrorModel.Type = DistributeTypeEnum.TURNSTILE_DELETE_PASS_RIGHT.Value;
            distributeErrorModel.PartUrl = "DeletePassRightByRightId";
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
