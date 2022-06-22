using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Data.IServices;
using KT.Visitor.IdReader.Common;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver.Core;
using System.Collections.ObjectModel;
using System.Drawing.Printing;

namespace KT.Visitor.Common.ViewModels
{
    public class ConfigInfoViewModel : PropertyChangedBase
    {
        private ILogger _logger;
        private ISystemConfigDataService _systemConfigDataService;
        private ConfigHelper _configHelper;
        private PrintHandler _printHandler;
        private IFunctionApi _frontBaseApi;

        public ConfigInfoViewModel()
        {
            _logger = ContainerHelper.Resolve<ILogger>();
            _systemConfigDataService = ContainerHelper.Resolve<ISystemConfigDataService>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _printHandler = ContainerHelper.Resolve<PrintHandler>();
            _frontBaseApi = ContainerHelper.Resolve<IFunctionApi>();

            Init();
        }

        public void Init()
        {
            //初始化阅读器
            this.Readers = new ObservableCollection<ReaderTypeEnum>(ReaderTypeEnum.Items);
            this.Printers = new ObservableCollection<string>();
            this.CardDevices = new ObservableCollection<string>();
            this.CardIssueMethods = new ObservableCollection<string>();

            var config = _configHelper.LocalConfig;

            //本地配置赋值
            this.ServerAddress = config.ServerAddress;
            this.Printer = config.Printer;
            this.Reader = config.Reader;
            this.CardDevice = config.CardDevice;
            this.CardIssueMethod = config.CardIssueMethod;

            // 打印机
            InitPrinter();
        }

        /// <summary>
        /// 初始化打印机
        /// </summary>
        private void InitPrinter()
        {
            if (PrinterSettings.InstalledPrinters == null
                || PrinterSettings.InstalledPrinters.Count <= 0)
            {
                return;
            }
            foreach (var item in PrinterSettings.InstalledPrinters)
            {
                Printers.Add(item.ToString());
            }
            Printers.Insert(0, string.Empty);
        }


        private string serviceAddress = "http://192.168.0.219:8080";
        // 正件阅读器
        private string reader;
        private ObservableCollection<ReaderTypeEnum> readers;
        // 打印机
        private string printer;
        private ObservableCollection<string> printers;
        //发卡机
        private string cardDevice;
        private ObservableCollection<string> cardDevices;
        private string cardIssueMethod;
        private ObservableCollection<string> cardIssueMethods;

        private bool _isPrinterError;

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

        public ObservableCollection<ReaderTypeEnum> Readers
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

        public string ServerAddress
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
