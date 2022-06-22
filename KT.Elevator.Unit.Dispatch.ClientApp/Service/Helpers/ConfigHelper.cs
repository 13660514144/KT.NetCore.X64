﻿using KT.Common.Core.Utils;
using KT.Elevator.Unit.Dispatch.ClientApp.Service.IServices;
using KT.Elevator.Unit.Dispatch.Entity.Enums;
using KT.Elevator.Unit.Dispatch.Entity.Models;
using Prism.Ioc;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Service.Helpers
{
    /// <summary>
    /// 配置帮助，静态
    /// </summary>
    public class ConfigHelper
    {
        private IContainerProvider _containerProvider;
        private AppSettings _appSettings;

        public ConfigHelper(IContainerProvider containerProvider,
            AppSettings appSettings)
        {
            _containerProvider = containerProvider;
            _appSettings = appSettings;
        }

        public void Refresh()
        {
            _ = RefreshAsync();
        }

        public async Task RefreshAsync()
        {
            var service = _containerProvider.Resolve<ISystemConfigService>();
            var config = await service.GetAsync();
            LocalConfig = config;
        }
        public UnitDispatchSystemConfigModel LocalConfig { get; set; }
    }
}
