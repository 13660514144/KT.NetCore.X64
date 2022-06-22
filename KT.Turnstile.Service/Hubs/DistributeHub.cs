using KT.Common.Core.Utils;
using KT.Quanta.Common.Models;
using KT.Turnstile.Manage.Service.Helpers;
using KT.Turnstile.Manage.Service.IHubs;
using KT.Turnstile.Manage.Service.IServices;
using KT.Turnstile.Manage.Service.Services;
using KT.Turnstile.Unit.Entity.Entities;
using KT.Turnstile.Unit.Entity.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.Hubs
{
    public class DistributeHub : Hub, IDistributeHub
    {
        private ProcessorDeviceList _processorDeviceList;
        private IServiceProvider _serviceProvider;
        private ILogger<DistributeHub> _logger;

        public DistributeHub(ProcessorDeviceList processorDeviceList,
            IServiceProvider serviceProvider,
            ILogger<DistributeHub> logger)
        {
            _processorDeviceList = processorDeviceList;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        /// <summary>
        /// 边缘处理器连接
        /// </summary>
        /// <param name="processorKey"></param>
        /// <returns>是否存在错误数据</returns>
        public async Task<bool> Link(SeekSocketModel seekSocket)
        {
            _logger.LogInformation("边缘处理器连接：seek:{0} ", JsonConvert.SerializeObject(seekSocket, JsonUtil.JsonSettings));
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IProcessorService>();
                return await service.LinkReturnHasErrorAsync(seekSocket, Context.ConnectionId);
            }
        }

        /// <summary>
        /// 获取所有读卡器
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        public async Task<List<TurnstileUnitCardDeviceEntity>> GetCardDevices(string processorKey)
        {
            _logger.LogInformation("边缘处理器获取读卡器信息：processorKey:{0} ", processorKey);
            var processor = _processorDeviceList.GetByConnectionId(processorKey);
            if (processor == null)
            {
                processor = _processorDeviceList.GetByConnectionId(Context.ConnectionId);
            }
            if (processor == null)
            {
                return new List<TurnstileUnitCardDeviceEntity>();
            }
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ICardDeviceService>();
                var results = await service.GetUnitByProcessorId(processor.Id);
                _logger.LogInformation("边缘处理器获取读卡器信息：data:{0} ", JsonConvert.SerializeObject(results, JsonUtil.JsonSettings));
                return results;
            }
        }

        /// <summary>
        /// 获取所有通行权限
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        public async Task<List<TurnstileUnitRightGroupEntity>> GetRightGroups(int page, int size)
        {
            _logger.LogInformation("边缘处理器获取权限组信息：page:{0} size:{1} ", page, size);
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ICardDeviceRightGroupService>();
                var results = await service.GetUnitAllAsync(page, size);
                _logger.LogInformation("边缘处理器获取权限组信息：data:{0} ", JsonConvert.SerializeObject(results, JsonUtil.JsonSettings));
                return results;
            }
        }

        /// <summary>
        /// 获取所有通行权限
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        public async Task<List<TurnstileUnitPassRightEntity>> GetPassRights(int page, int size)
        {
            _logger.LogInformation("边缘处理器获取通行权限信息：page:{0} size:{1} ", page, size);
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IPassRightService>();
                var results = await service.GetUnitPageAsync(page, size);
                _logger.LogInformation("边缘处理器获取通行权限信息：data:{0} ", JsonConvert.SerializeObject(results, JsonUtil.JsonSettings));
                return results;
            }
        }

        /// <summary>
        /// 获取所有通行权限
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        public async Task<bool> IsExistsPassRightById(string id)
        {
            _logger.LogInformation($"查询是否存在通行权限信息：id:{id} ");
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IPassRightService>();
                var result = await service.IsExistsAsync(id);
                _logger.LogInformation($"查询是否存在通行权限信息：data:{result} ");
                return result;
            }
        }

        /// <summary>
        /// 获取所有通行权限
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        public async Task<List<UnitErrorModel>> GetUnitErrors(string processorKey, int page, int size)
        {
            _logger.LogInformation("边缘处理器获取推送错误数据信息：processorKey:{0} page:{1} size:{2} ", processorKey, page, size);
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IDistributeErrorService>();
                var results = await service.GetUnitPageAndDeleteByProcessorKeyAsync(processorKey, page, size);
                _logger.LogInformation("边缘处理器获取推送错误数据信息：data:{0} ", JsonConvert.SerializeObject(results, JsonUtil.JsonSettings));
                return results;
            }
        }

        /// <summary>
        /// 推送通行记录
        /// </summary>
        /// <param name="passRecord"></param>
        /// <returns></returns>
        public async Task PushPassRecord(TurnstileUnitPassRecordEntity passRecord)
        {
            _logger.LogInformation("边缘处理器上传通行记录：data:{0} ", JsonConvert.SerializeObject(passRecord, JsonUtil.JsonSettings));
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IPassRecordService>();
                await service.PushPassRecord(passRecord);
                _logger.LogInformation("边缘处理器上传通行记录完成！");
            }
        }

        /// <summary>
        /// 连接事件
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("边缘处理器连接：connectionId:{0} ", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation("边缘处理器断开连接：connectionId:{0} ", Context.ConnectionId);
            using (var scope = _serviceProvider.CreateScope())
            {
                var processor = _processorDeviceList.GetByConnectionId(Context.ConnectionId);
                if (processor != null)
                {
                    var service = scope.ServiceProvider.GetRequiredService<IProcessorService>();
                    service.DisLink(processor.ProcessorKey);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

    }
}
