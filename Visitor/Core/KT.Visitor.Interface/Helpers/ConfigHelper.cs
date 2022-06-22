using KT.Proxy.WebApi.Backend.Helpers;
using KT.Visitor.Data.IServices;
using KT.Visitor.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using System;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Helpers
{
    /// <summary>
    /// 配置帮助，静态
    /// </summary>
    public class ConfigHelper
    {
        private IContainerProvider _containerProvider;
        public ConfigHelper(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;
        }

        public void Refresh()
        {
            var service = _containerProvider.Resolve<ISystemConfigDataService>();
            var config = service.GetAsync().Result;
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
                RequestHelper.BackendBaseUrl = value.ServiceAddress;
            }
        }
    }
}
