using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Data.IServices;
using KT.Visitor.SelfApp.ViewModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Visitor.SelfApp.Views.Setting
{
    public class SettingPageViewModel : BindableBase
    {
        private ConfigInfoViewModel _configInfo;

        private ConfigHelper _configHelper;

        public SettingPageViewModel()
        {
            PasswordVM = ContainerHelper.Resolve<PasswordViewModel>();
            ConfigInfo = ContainerHelper.Resolve<ConfigInfoViewModel>();

            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <returns></returns>
        public async Task SaveSettingAsync()
        {
            var config = _configHelper.LocalConfig;

            config.Reader = ConfigInfo.Reader;
            config.Printer = ConfigInfo.Printer;
            config.CardDevice = ConfigInfo.CardDevice;
            config.CardIssueMethod = ConfigInfo.CardIssueMethod;
            config.ServerAddress = ConfigInfo.ServerAddress;

            //更新数据
            await ContainerHelper.Resolve<ISystemConfigDataService>().AddOrUpdateAsync(config);

            //刷新配置
            await _configHelper.RefreshAsync();
        }

        public Task TestPrintAsync()
        {
            if (string.IsNullOrEmpty(ConfigInfo.Printer))
            {
                ConfigInfo.IsPrinterError = true;

                return Task.CompletedTask;
            }

            //打印二维码
            var item = new RegisterResultModel();
            item.EdificeName = "康塔测试大厦";
            item.CompanyName = "康塔测试科技术公司";
            item.DateTime = "2020/01/20";
            item.Name = "测试";
            item.Phone = "13800000000";
            item.FloorName = "16F";
            item.Qr = "**5E5C7D0711270300EA17DCC513A9EB35A1C9802A7E078BEF28870AB9##";

            // 异步 打印二维码 
            ContainerHelper.Resolve<PrintHandler>().StartPrintAsync(new List<RegisterResultModel>() { item }, true);

            return Task.CompletedTask;
        }

        public ConfigInfoViewModel ConfigInfo
        {
            get
            {
                return _configInfo;
            }

            set
            {
                SetProperty(ref _configInfo, value);
            }
        }

        private PasswordViewModel passwordVM;

        public PasswordViewModel PasswordVM
        {
            get
            {
                return passwordVM;
            }

            set
            {
                SetProperty(ref passwordVM, value);
            }
        }


    }
}
