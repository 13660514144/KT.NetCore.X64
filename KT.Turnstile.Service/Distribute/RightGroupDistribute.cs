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
    public class RightGroupDistribute : IRightGroupDistribute
    {
        private IHubContext<DistributeHub> _distributeHub;
        private ProcessorDeviceList _processorDeviceList;
        private ILogger<RightGroupDistribute> _logger;
        private IServiceProvider _serviceProvider;

        public RightGroupDistribute(IHubContext<DistributeHub> distributeHub,
            ProcessorDeviceList processorDeviceList,
            ILogger<RightGroupDistribute> logger,
            IServiceProvider serviceProvider)
        {
            _distributeHub = distributeHub;
            _processorDeviceList = processorDeviceList;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task AddOrUpdate(CardDeviceRightGroupModel model)
        {
            return Task.Run(() =>
            {
                var result = new TurnstileUnitRightGroupEntity();
                result.Id = model.Id;
                result.EditedTime = model.EditedTime;

                if (model.CardDevices != null)
                {
                    foreach (var obj in model.CardDevices)
                    {
                        var detail = new TurnstileUnitRightGroupDetailEntity();
                        detail.EditedTime = obj.EditedTime;

                        //detail.RightGroup = new UnitRightGroupEntity();
                        //detail.RightGroup.Id = model.Id;

                        detail.CardDevice = new TurnstileUnitCardDeviceEntity();
                        detail.CardDevice.Id = obj.Id;

                        result.Details.Add(detail);
                    }
                }

                _processorDeviceList.ExecAll(async (processor) =>
                {
                    //发送到所有边缘处理器  
                    if (processor.IsOnline)
                    {
                        try
                        {
                            _logger.LogDebug("分发权限组数据：processor:{0} data:{1} ",
                                JsonConvert.SerializeObject(processor, JsonUtil.JsonSettings),
                                JsonConvert.SerializeObject(result, JsonUtil.JsonSettings));

                            var client = _distributeHub.Clients.Client(processor.ConnectionId);
                            await client.SendAsync("AddOrEditRightGroup", result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning("分发权限组错误：data:{0} ex:{1} ", JsonConvert.SerializeObject(result, JsonUtil.JsonSettings), ex);
                            await AddDistributeErrorAsync(ex.Message, processor, result);
                        }
                    }
                    else if (!string.IsNullOrEmpty(processor.ProcessorKey))
                    {
                        _logger.LogWarning("分发权限组数据-边缘处理器离线：data:{0} ", JsonConvert.SerializeObject(result, JsonUtil.JsonSettings));
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
        private async Task AddDistributeErrorAsync(string message, ProcessorModel processor, TurnstileUnitRightGroupEntity data)
        {
            // 分发失败不重发,存储错误日记 
            var distributeErrorModel = new DistributeErrorModel();
            distributeErrorModel.Type = DistributeTypeEnum.TURNSTILE_ADD_OR_UPDATE_RIGHT_GROUP.Value;
            distributeErrorModel.PartUrl = "AddOrEditRightGroup";
            distributeErrorModel.DataModelName = data.GetType().Name;
            distributeErrorModel.DataId = data.Id;
            distributeErrorModel.ErrorMessage = message;
            distributeErrorModel.DataContent = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
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

        public Task Delete(string id, long time)
        {
            return Task.Run(() =>
            {
                _processorDeviceList.ExecAll(async (processor) =>
                {
                    if (processor.IsOnline)
                    {
                        try
                        {
                            _logger.LogDebug("分发删除权限组数据：id:{0} processor:{1} ", id, JsonConvert.SerializeObject(processor, JsonUtil.JsonSettings));
                            var client = _distributeHub.Clients.Client(processor.ConnectionId);
                            await client.SendAsync("DeleteRightGroup", id, time);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning("分发删除权限组错误：id:{0} ex:{1} ", id, ex);
                            await AddDistributeErrorAsync(ex.Message, processor, id);
                        }
                    }
                    else if (!string.IsNullOrEmpty(processor.ProcessorKey))
                    {
                        _logger.LogWarning("分发删除权限组数据-边缘处理器离线：id:{0} ", id);
                        await AddDistributeErrorAsync("边缘处理器离线", processor, id);
                    }
                    else
                    {
                        _logger.LogWarning("分发权限组数据-边缘处理器未连接：id:{0} processor:{1} ", id, JsonConvert.SerializeObject(processor, JsonUtil.JsonSettings));
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
            distributeErrorModel.Type = DistributeTypeEnum.TURNSTILE_DELETE_RIGHT_GROUP.Value;
            distributeErrorModel.PartUrl = "DeleteRightGroup";
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
