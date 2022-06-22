using KT.Common.Core.Utils;
using KT.Turnstile.Unit.ClientApp.Service.IServices;
using KT.Turnstile.Unit.ClientApp.Service.Services;
using KT.Turnstile.Unit.Entity.Entities;
using KT.Turnstile.Unit.Entity.Enums;
using KT.Turnstile.Unit.Entity.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Service.Helpers
{
    public class HubDataHelper
    {
        private ConfigHelper _configHelper;
        private IContainerProvider _containerProvider;
        private AppSettings _appSettings;
        private readonly ILogger _logger;

        public HubDataHelper(ConfigHelper configHelper,
             IContainerProvider containerProvider,
             AppSettings appSettings,
             ILogger logger)
        {
            _configHelper = configHelper;
            _containerProvider = containerProvider;
            _appSettings = appSettings;
            _logger = logger;
        }

        /// <summary>
        /// 从服务端拉取数据更新到本地
        /// </summary>
        /// <param name="masterHub"></param>
        /// <param name="hasError">是否包含错误</param>
        /// <param name="isForce">是否强制更新</param>
        /// <returns></returns>
        public async Task InitAsync(HubConnection masterHub, bool isForce)
        {
            //更新错误数据??
            await InitErrorsAsync(masterHub);

            if (isForce || _configHelper.LocalConfig.LastSyncTime == 0)
            {
                var utcNow = DateTimeUtil.UtcNowMillis();

                //初始化读卡器
                await InitCardDevicesAsync(masterHub);
                await InitRightGroupsAsync(masterHub);
                await InitPassRightsAsync(masterHub);

                //更新本地配置
                _configHelper.LocalConfig.LastSyncTime = utcNow;
                var systemConfigSerivce = _containerProvider.Resolve<SystemConfigService>();
                await systemConfigSerivce.AddOrUpdateAsync(SystemConfigEnum.LAST_SYNC_TIME, utcNow);
            }
        }

        /// <summary>
        /// 从服务端拉取数据更新到本地
        /// </summary>
        /// <param name="masterHub"></param>
        /// <param name="hasError">是否包含错误</param>
        /// <param name="isForce">是否强制更新</param>
        /// <returns></returns>
        public async Task InitRightAsync(HubConnection masterHub, bool isForce)
        {
            if (isForce || _configHelper.LocalConfig.LastSyncTime == 0)
            {
                //初始化读卡器 
                await InitPassRightsAsync(masterHub);
            }
        }

        private async Task InitErrorsAsync(HubConnection masterHub)
        {
            try
            {
                int page = 1;
                int size = 20;
                while (true)
                {
                    _logger.LogInformation($"更新推送错误数据：page:{page} size:{size} ");
                    var entities = await masterHub.InvokeAsync<List<UnitErrorModel>>("GetUnitErrors", _appSettings.DeviceType, page, size);
                    var errorService = _containerProvider.Resolve<IErrorService>();
                    await errorService.AddOrUpdateAsync(entities);
                    if (entities.FirstOrDefault() != null)
                    {
                        page++;
                    }
                    else
                    {
                        break;
                    }
                }
                _logger.LogInformation($"更新推送错误数据完成！");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, $"更新推送错误数据失败！ ");
            }
        }

        private async Task InitCardDevicesAsync(HubConnection masterHub)
        {
            _logger.LogInformation($"更新读卡器");
            var entities = await masterHub.InvokeAsync<List<TurnstileUnitCardDeviceEntity>>("TurnstileGetCardDevices", _appSettings.DeviceType);
            var cardDeviceService = _containerProvider.Resolve<ICardDeviceService>();
            await cardDeviceService.AddOrUpdateAsync(entities);
            _logger.LogInformation($"更新读卡器完成！");
        }

        private async Task InitRightGroupsAsync(HubConnection masterHub)
        {
            int page = 1;
            int size = 1;
            while (true)
            {
                _logger.LogInformation($"更新权限组：page:{page} size:{size}");
                var entities = await masterHub.InvokeAsync<List<TurnstileUnitRightGroupEntity>>("TurnstileGetRightGroups", page, size);
                if (entities?.FirstOrDefault() != null)
                {
                    page++;
                }
                else
                {
                    break;
                }
                var rightGroupService = _containerProvider.Resolve<IRightGroupService>();
                await rightGroupService.AddOrUpdateAsync(entities);
            }
            _logger.LogInformation($"更新权限组完成！");
        }

        private async Task InitPassRightsAsync(HubConnection masterHub)
        {
            int page = 1;
            int size = 20;
            while (true)
            {
                _logger.LogInformation($"更新通行权限：page:{page} size:{size} ");
                var entities = await masterHub.InvokeAsync<List<TurnstileUnitPassRightEntity>>("TurnstileGetPassRights", page, size);
                if (entities?.FirstOrDefault() != null)
                {
                    page++;
                }
                else
                {
                    break;
                }
                var passRightService = _containerProvider.Resolve<IPassRightService>();
                await passRightService.AddOrUpdateAsync(entities);
            }
            _logger.LogInformation($"更新通行权限完成！");
        }

        public async Task DeleteAsync(HubConnection masterHub)
        {
            await DeletePassRight(masterHub);
        }

        private async Task DeletePassRight(HubConnection masterHub)
        {
            int page = 1;
            int size = 1;
            while (true)
            {
                var hasNotExists = false;
                _logger.LogInformation($"更新删除权限：page:{page} size:{size}");
                var passRightService = _containerProvider.Resolve<IPassRightService>();
                var passRightPage = await passRightService.GetPageWithDetailAsync(page, size);
                if (passRightPage.List?.FirstOrDefault() == null)
                {
                    //没有数据退出操作
                    break;
                }

                foreach (var item in passRightPage.List)
                {
                    var isExists = await masterHub.InvokeAsync<bool>("IsExistsPassRightById", item.Id);
                    if (!isExists)
                    {
                        hasNotExists = true;
                        _logger.LogInformation($"更新删除权限：id:{item.Id} cardNumber:{item.CardNumber}");
                        await passRightService.Deletesync(item);
                    }
                }
                //存在错误继续校验当前页面，否则删除了数据查询下一页会遗漏掉数据
                if (!hasNotExists)
                {
                    page++;
                }

            }
            _logger.LogInformation($"更新删除权限完成！");
        }
    }
}
