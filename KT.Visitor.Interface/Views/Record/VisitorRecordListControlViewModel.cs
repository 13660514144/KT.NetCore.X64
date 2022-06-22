using KT.Common.Core.Enums;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Common.Views.Helper;
using KT.Visitor.Data.Enums;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Models;
using KT.Visitor.Interface.Views.Common;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views
{
    public class VisitorRecordListControlViewModel : BindableBase
    {
        public NavPageControl NavPageControl { get; private set; }

        private ICommand _detailCommand;
        public ICommand DetailCommand => _detailCommand ?? (_detailCommand = new DelegateCommand<VisitorInfoModel>(DetailAsync));

        //导入访客
        private ICommand _linkVisitorImportRecordListCommand;
        public ICommand LinkVisitorImportRecordListCommand => _linkVisitorImportRecordListCommand ??= new DelegateCommand(LinkVisitorImportRecordList);

        private ObservableCollection<VisitStatusEnum> statusItems;
        private ObservableCollection<GenderEnum> _genders;
        private ObservableCollection<VisitorFromEnum> _visitorFroms;
        private CompanyTreeCheckHelper _companyTreeCheckHelper;

        private string _status;
        private string _startTime;
        private string _endTime;

        private IEventAggregator _eventAggregator;

        public VisitorRecordListControlViewModel()
        {
            _companyTreeCheckHelper = ContainerHelper.Resolve<CompanyTreeCheckHelper>();
            NavPageControl = ContainerHelper.Resolve<NavPageControl>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            TreeCompany = ContainerHelper.Resolve<TreeCheckCompanyViewModel>();

            _ = InitAsync();
        }

        private void DetailAsync(VisitorInfoModel item)
        {
            //跳转到黑名单页面
            _eventAggregator.GetEvent<NavLinkEvent>().Publish(NavLinkModel.Create(FontNavEnum.VISITOR_DETAIL, data: item.Id));
        }

        public async Task InitAsync()
        {
            _status = string.Empty;
            StartTime = DateTimeUtil.DayStartMilliString();
            EndTime = DateTimeUtil.DayEndMilliString();

            //初始化对你 
            StatusItems = VisitStatusEnum.GetVMs(true);
            Genders = new ObservableCollection<GenderEnum>(GenderEnum.ViewItems);
            VisitorFroms = new ObservableCollection<VisitorFromEnum>(VisitorFromEnum.ViewItems);

            //初始化数据
            await TreeCompany.InitTreeCheckAsync("请选择大厦楼层");
        }

        private void LinkVisitorImportRecordList()
        {
            _eventAggregator.GetEvent<NavLinkEvent>().Publish(NavLinkModel.Create(FontNavEnum.VISITOR_IMPORT));
        }

        /// <summary>
        /// 获取树型选择的公司ID列表
        /// </summary>
        /// <returns></returns>
        internal async Task<List<long>> GetSelectdCommpanyIdsAsync()
        {
            return await Task.Run(() =>
            {
                return _companyTreeCheckHelper.GetSelectdCommpanyIds(TreeCompany.TreeCompanies);
            });
        }

        private TreeCheckCompanyViewModel _treeCompany;

        public TreeCheckCompanyViewModel TreeCompany
        {
            get
            {
                return _treeCompany;
            }

            set
            {
                SetProperty(ref _treeCompany, value);
            }
        }


        /// <summary>
        /// 状态下拉选择
        /// </summary>
        public ObservableCollection<VisitStatusEnum> StatusItems
        {
            get
            {
                return statusItems;
            }
            set
            {
                SetProperty(ref statusItems, value);
            }
        }

        /// <summary>
        /// 当前选择的状态
        /// </summary>
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                SetProperty(ref _status, value);
            }
        }

        public string StartTime
        {
            get
            {
                return _startTime;
            }

            set
            {
                SetProperty(ref _startTime, value);
            }
        }

        public string EndTime
        {
            get
            {
                return _endTime;
            }

            set
            {
                SetProperty(ref _endTime, value);
            }
        }

        public ObservableCollection<GenderEnum> Genders
        {
            get
            {
                return _genders;
            }
            set
            {
                SetProperty(ref _genders, value);
            }
        }

        public ObservableCollection<VisitorFromEnum> VisitorFroms
        {
            get
            {
                return _visitorFroms;
            }
            set
            {
                SetProperty(ref _visitorFroms, value);
            }
        }
    }
}
