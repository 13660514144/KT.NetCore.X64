using KT.Common.Core.Exceptions;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Data.IServices;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Setting
{
    public class SystemSettingWindowViewModel : PropertyChangedBase
    {
        public ConfigInfoViewModel ConfigInfo { get; set; }

        public ICommand CameraChangedCommand { get; private set; }

        private ILogger _logger;
        private ISystemConfigDataService _systemConfigDataService;
        private ConfigHelper _configHelper;
        private PrintHandler _printHandler;
        private IFunctionApi _frontBaseApi;

        public SystemSettingWindowViewModel()
        {
            _logger = ContainerHelper.Resolve<ILogger>();
            _systemConfigDataService = ContainerHelper.Resolve<ISystemConfigDataService>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _printHandler = ContainerHelper.Resolve<PrintHandler>();
            _frontBaseApi = ContainerHelper.Resolve<IFunctionApi>();

            ConfigInfo = ContainerHelper.Resolve<ConfigInfoViewModel>();

            CameraChangedCommand = new DelegateCommand(CameraChanged);
        }

        private void CameraChanged()
        {

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

            if (string.IsNullOrEmpty(config.ServerAddress))
            {
                throw CustomException.Run("后台服务器地址不能为空！");
            }
            if (string.IsNullOrEmpty(config.Reader))
            {
                throw CustomException.Run("证件阅读器不能为空！");
            }
            if (string.IsNullOrEmpty(config.Printer))
            {
                throw CustomException.Run("打印机不能为空！");
            }

            //更新数据
            await _systemConfigDataService.AddOrUpdateAsync(config);

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
            _printHandler.StartPrintAsync(new List<RegisterResultModel>() { item }, true);

            return Task.CompletedTask;
        }
    }
}