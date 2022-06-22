using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Helpers;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Data.IServices;
using KT.Visitor.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Threading.Tasks;

namespace KT.Visitor.Common.Helpers
{
    /// <summary>
    /// 配置帮助，静态
    /// </summary>
    public class ConfigHelper
    {
        private ILogger _logger;

        public ConfigHelper()
        {
        }

        public async Task RefreshAsync()
        {
            var service = ContainerHelper.Resolve<ISystemConfigDataService>();
            var config = await service.GetAsync();
            LocalConfig = config;
        }

        private SystemConfigModel _localConfig;

        public SystemConfigModel LocalConfig
        {
            get
            {
                return _localConfig;
            }

            set
            {
                _localConfig = value;
                //设置界面内容
                StaticProperty.SetValue(value);
                //设备Api链接地址
                RequestHelper.BackendBaseUrl = value.ServerAddress;
            }
        }
    }
}
