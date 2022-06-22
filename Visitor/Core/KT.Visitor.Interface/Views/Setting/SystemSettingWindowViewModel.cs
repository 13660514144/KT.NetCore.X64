using CommonUtils;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Proxy.WebApi.Backend.Helpers;
using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.Data.IServices;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Tools.Printer.DocumentRenderer;
using KT.Visitor.Interface.Tools.Printer.Models;
using KT.Visitor.Interface.Tools.Printer.PrintOperator;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver.Core;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Views.Setting
{
    public class SystemSettingWindowViewModel : PropertyChangedBase
    {
        private ILogger<SystemSettingWindowViewModel> _logger;
        private ISystemConfigDataService _systemConfigDataService;
        private ConfigHelper _configHelper;
        private SmallTicketOperator _smallTicketOperator;
        private FrontBaseApi _frontBaseApi;

        public SystemSettingWindowViewModel(ILogger<SystemSettingWindowViewModel> logger,
            ISystemConfigDataService systemConfigDataService,
             ConfigHelper configHelper,
            SmallTicketOperator smallTicketOperator,
            FrontBaseApi frontBaseApi)
        {
            _logger = logger;
            _systemConfigDataService = systemConfigDataService;
            _configHelper = configHelper;
            _smallTicketOperator = smallTicketOperator;
            _frontBaseApi = frontBaseApi;

            Init();
        }

        public void Init()
        {
            _logger.LogDebug("SystemSettingWindow.SystemSettingWindowViewModel.Start!");
            //初始化阅读器
            this.Readers = new ObservableCollection<ReaderEnum>(ReaderEnum.Items);
            _logger.LogDebug("SystemSettingWindow.Readers.Inited!");
            this.Printers = new ObservableCollection<string>();
            this.CardDevices = new ObservableCollection<string>();
            this.CardIssueMethods = new ObservableCollection<string>();

            _logger.LogDebug("SystemSettingWindow.List.Inited!");

            var config = _configHelper.LocalConfig;

            //本地配置赋值
            this.ServiceAddress = config.ServiceAddress;
            this.Printer = config.Printer;
            this.Reader = config.Reader;
            this.CardDevice = config.CardDevice;
            this.CardIssueMethod = config.CardIssueMethod;

            _logger.LogDebug("SystemSettingWindow.Local.Inited!");
        }

        private string serviceAddress = "http://192.168.0.219:8080";
        // 正件阅读器
        private string reader;
        private ObservableCollection<ReaderEnum> readers;
        // 打印机
        private string printer;
        private ObservableCollection<string> printers;
        //发卡机
        private string cardDevice;
        private ObservableCollection<string> cardDevices;
        private string cardIssueMethod;
        private ObservableCollection<string> cardIssueMethods;

        private bool _isPrinterError;

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <returns></returns>
        internal async Task SaveSettingAsync()
        {
            var config = _configHelper.LocalConfig;

            config.Reader = this.Reader;
            config.Printer = this.Printer;
            config.CardDevice = this.CardDevice;
            config.CardIssueMethod = this.CardIssueMethod;
            config.ServiceAddress = this.ServiceAddress;

            //更新数据
            await _systemConfigDataService.AddOrUpdateAsync(config);

            //刷新配置
            _configHelper.Refresh();
        }

        internal async Task TestPrintAsync()
        {
            if (string.IsNullOrEmpty(Printer))
            {
                IsPrinterError = true;
                return;
            }

            //打印二维码
            var item = new RegisterResultModel();
            item.EdificeName = "康塔测试大厦";
            item.CompanyName = "康塔测试科技术公司";
            item.DateTime = "2020/01/20";
            item.Name = "测试";
            item.Phone = "13800000000";
            item.FloorName = "16F";
            item.Qr = "9C2409F5F198C0A3341D6FFFAB9DEEA9836F6F52B0990FF71438C221E50A50CB0FA1568FE5A81C7B9EF5AB608EC8858C";

            var imageInfo = await _frontBaseApi.GetQrAsync(item.Qr);
            var bitmap = ImageConvert.BytesToBitmap(imageInfo.Bytes);

            _smallTicketOperator.Init(PrintConfig.VISIT_QR_CODE_DOCUMENT_PATH, VisitQRCodePrintModel.FromVisitorResult(item, bitmap), new VisitQRCodeRenderer());
            await _smallTicketOperator.StartPrintAsync(Printer);
        }

        public string Reader
        {
            get
            {
                return reader;
            }

            set
            {
                reader = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<ReaderEnum> Readers
        {
            get
            {
                return readers;
            }

            set
            {
                readers = value;
                NotifyPropertyChanged();
            }
        }

        public string Printer
        {
            get
            {
                return printer;
            }

            set
            {
                printer = value;
                IsPrinterError = false;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<string> Printers
        {
            get
            {
                return printers;
            }

            set
            {
                printers = value;
                NotifyPropertyChanged();
            }
        }

        public string CardDevice
        {
            get
            {
                return cardDevice;
            }

            set
            {
                cardDevice = value;
                NotifyPropertyChanged();
            }
        }

        public string CardIssueMethod
        {
            get
            {
                return cardIssueMethod;
            }

            set
            {
                cardIssueMethod = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<string> CardDevices
        {
            get
            {
                return cardDevices;
            }

            set
            {
                cardDevices = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<string> CardIssueMethods
        {
            get
            {
                return cardIssueMethods;
            }

            set
            {
                cardIssueMethods = value;
                NotifyPropertyChanged();
            }
        }

        public string ServiceAddress
        {
            get
            {
                return serviceAddress;
            }

            set
            {
                serviceAddress = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsPrinterError
        {
            get
            {
                return _isPrinterError;
            }

            set
            {
                _isPrinterError = value;
                NotifyPropertyChanged();
            }
        }
    }
}
