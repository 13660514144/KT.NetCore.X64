using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Enums;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Data.Enums;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Models;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Common;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views
{
    public class VisitorImportDetailListControlViewModel : BindableBase
    {
        private DelegateCommand _printAllCommand;
        public ICommand PrintAllCommand => _printAllCommand ??= new DelegateCommand(PrintAllAsync);

        private DelegateCommand _printPartCommand;
        public ICommand PrintPartCommand => _printPartCommand ??= new DelegateCommand(PrintPartAsync);

        private DelegateCommand<VisitorImportDetailViewModel> _printOneCommand;
        public ICommand PrintOneCommand => _printOneCommand ??= new DelegateCommand<VisitorImportDetailViewModel>(PrintOneAsync);
        private DelegateCommand<VisitorImportDetailViewModel> _showErrorCommand;
        public ICommand ShowErrorCommand => _showErrorCommand ??= new DelegateCommand<VisitorImportDetailViewModel>(ShowError);

        private DelegateCommand _checkedAllCommand;
        public ICommand CheckedAllCommand => _checkedAllCommand ??= new DelegateCommand(CheckedAll);

        private DelegateCommand _uncheckedAllCommand;
        public ICommand UncheckedAllCommand => _uncheckedAllCommand ??= new DelegateCommand(UncheckedAll);

        private DelegateCommand<VisitorImportDetailViewModel> _checkedCommand;
        public ICommand CheckedCommand => _checkedCommand ??= new DelegateCommand<VisitorImportDetailViewModel>(Checked);

        private DelegateCommand<VisitorImportDetailViewModel> _uncheckedCommand;
        public ICommand UncheckedCommand => _uncheckedCommand ??= new DelegateCommand<VisitorImportDetailViewModel>(Unchecked);

        //导入访客
        private ICommand _linkVisitorImportRecordListCommand;
        public ICommand LinkVisitorImportRecordListCommand => _linkVisitorImportRecordListCommand ??= new DelegateCommand(LinkVisitorImportRecordList);

        public NavPageControl NavPageControl { get; private set; }

        private VisitorImportDetailQueryViewModel _visitorImportDetailQuery;
        private VisitorImportViewModel _visitorImport;
        private List<long> _checkedIds;
        private ObservableCollection<VisitorImportDetailViewModel> _visitorImportDetails;

        private bool _isEnablePrint;

        private IEventAggregator _eventAggregator;
        private IFunctionApi _functionApi;
        private IVisitorApi _visitorApi;
        private PrintHandler _printHandler;

        public VisitorImportDetailListControlViewModel()
        {
            NavPageControl = ContainerHelper.Resolve<NavPageControl>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();
            _printHandler = ContainerHelper.Resolve<PrintHandler>();

            VisitorImportDetailQuery = new VisitorImportDetailQueryViewModel();

            Init();
        }

        private async Task PrintAllAsync()
        {
            if (VisitorImportDetails.Count(x => x.Status) == 0)
            {
                ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("没有可以打印的二维码！", "温馨提示");
                return;
            }

            var result = ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("确认打印全部授权成功的二维码吗？");
            if (!result.HasValue || !result.Value)
            {
                return;
            }

            var query = new VisitorImportQrPrintQuery();
            query.ImportId = VisitorImport.Id;
            query.Select = VisitorImportQrPrintTypeEnum.ALL.Value;
            var results = await _visitorApi.GetVisitorImportQrPrintAsync(query);

            //执行打印
            ExceutePrintAsync(results);
        }

        private async void ExceutePrintAsync(List<RegisterResultModel> results)
        {
            Application.Current.Dispatcher?.Invoke(() =>
            {
                IsEnablePrint = false;
            });

            // 异步 打印二维码 
            _printHandler.StartPrintAsync(results, VisitorImport.Once);

            Application.Current.Dispatcher?.Invoke(() =>
            {
                IsEnablePrint = true;
            });

            await Task.CompletedTask;
        }

        private async Task PrintPartAsync()
        {
            if (CheckedIds.Count() == 0)
            {
                ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("请选择需要打印的二维码！", "温馨提示");
                return;
            }

            var query = new VisitorImportQrPrintQuery();
            query.ImportId = VisitorImport.Id;
            query.Select = VisitorImportQrPrintTypeEnum.SOME.Value;
            query.DetailIds = CheckedIds;
            var results = await _visitorApi.GetVisitorImportQrPrintAsync(query);

            //执行打印
            ExceutePrintAsync(results);
        }

        private async Task PrintOneAsync(VisitorImportDetailViewModel detail)
        {
            var query = new VisitorImportQrPrintQuery();
            query.ImportId = VisitorImport.Id;
            query.Select = VisitorImportQrPrintTypeEnum.SOME.Value;
            query.DetailIds = new List<long>() { detail.Id };
            var results = await _visitorApi.GetVisitorImportQrPrintAsync(query);

            //执行打印
            ExceutePrintAsync(results);
        }

        private void ShowError(VisitorImportDetailViewModel detail)
        {
            ContainerHelper.Resolve<MessageInfoBox>().ShowMessage(detail.Failure, "失败原因");
        }

        public void CheckedAll()
        {
            if (VisitorImportDetails?.FirstOrDefault() == null)
            {
                return;
            }
            foreach (var item in VisitorImportDetails)
            {
                item.IsChecked = true;
            }
        }

        public void UncheckedAll()
        {
            if (VisitorImportDetails?.FirstOrDefault() != null)
            {
                foreach (var item in VisitorImportDetails)
                {
                    item.IsChecked = false;
                }
            }

        }

        private void Checked(VisitorImportDetailViewModel detail)
        {
            CheckedIds.Add(detail.Id);
        }

        private void Unchecked(VisitorImportDetailViewModel detail)
        {
            CheckedIds.RemoveAll(x => x == detail.Id);
        }

        public void Init()
        {
            VisitorImportDetailQuery.Status = string.Empty;

            CheckedIds = new List<long>();


            //初始化对你 
            VisitorImportDetailQuery.Statuses = StatusEnum.GetVMs(true);
        }
        private void LinkVisitorImportRecordList()
        {
            _eventAggregator.GetEvent<NavLinkEvent>().Publish(NavLinkModel.Create(FontNavEnum.VISITOR_IMPORT));
        }
        public VisitorImportDetailQueryViewModel VisitorImportDetailQuery
        {
            get => _visitorImportDetailQuery;
            set
            {
                SetProperty(ref _visitorImportDetailQuery, value);
            }
        }

        public VisitorImportViewModel VisitorImport
        {
            get => _visitorImport;
            set
            {
                SetProperty(ref _visitorImport, value);
            }
        }

        public bool IsEnablePrint
        {
            get => _isEnablePrint;
            set
            {
                SetProperty(ref _isEnablePrint, value);
            }
        }

        public List<long> CheckedIds
        {
            get => _checkedIds;
            set => _checkedIds = value;
        }

        public ObservableCollection<VisitorImportDetailViewModel> VisitorImportDetails
        {
            get => _visitorImportDetails;
            set
            {
                SetProperty(ref _visitorImportDetails, value);
            }
        }
    }
}
