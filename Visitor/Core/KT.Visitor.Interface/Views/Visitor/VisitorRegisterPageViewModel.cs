using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Helper;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Microsoft.Extensions.DependencyInjection;
using Panuon.UI.Silver.Core;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Views.Visitor
{
    public class VisitorRegisterPageViewModel : PropertyChangedBase
    {
        // 公司选择界面 
        public CompanySelectControl CompanySelectControl { get; set; }
        // 访客选择界面 
        public VisitorsControl VisitorsControl { get; set; }

        //授权时限 
        private AuthorizeTimeLimitViewModel _timeLimitVM;
        //访问原因
        private ObservableCollection<ItemsCheckViewModel> visitReasons;
        //授权类型
        private ObservableCollection<ItemsCheckViewModel> authTypes;
        //查询出的公司列表，用于选择要访问的公司
        private ObservableCollection<CompanyViewModel> companys;
        //证件类型
        private ObservableCollection<CertificateTypeEnum> certificateTypes;

        //证件类型，默认身份证
        private string cardType;

        private VistitorConfigApi _vistitorConfigApi;
        private VistitorConfigHelper _vistitorConfigHelper;
        private IContainerProvider _containerProvider;
        private ConfigHelper _configHelper;

        public VisitorRegisterPageViewModel(VistitorConfigApi vistitorConfigApi,
            VistitorConfigHelper vistitorConfigHelper,
            IContainerProvider containerProvider,
            ConfigHelper configHelper)
        {
            _vistitorConfigApi = vistitorConfigApi;
            _vistitorConfigHelper = vistitorConfigHelper;
            _containerProvider = containerProvider;
            _configHelper = configHelper;

            Init();

            _ = RefreshDataAsync();
        }
        internal void Init()
        {
            CompanySelectControl = _containerProvider.Resolve<CompanySelectControl>();
            TimeLimitVM = _containerProvider.Resolve<AuthorizeTimeLimitViewModel>();
            VisitorsControl = _containerProvider.Resolve<VisitorsControl>();

            CertificateTypes = new ObservableCollection<CertificateTypeEnum>(CertificateTypeEnum.Items);
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public async Task RefreshDataAsync()
        {
            //初始化数据
            var visitorConfig = await _vistitorConfigApi.GetConfigParmsAsync();
            this.AuthTypes = _vistitorConfigHelper.SetAuthTypes(visitorConfig?.AuthTypes, _configHelper.LocalConfig.AuthModel);
            this.VisitReasons = _vistitorConfigHelper.SetVisitReasons(visitorConfig?.AccessReasons);
        }

        public ObservableCollection<ItemsCheckViewModel> AuthTypes
        {
            get
            {
                return authTypes;
            }

            set
            {
                authTypes = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<CompanyViewModel> Companys
        {
            get
            {
                return companys;
            }

            set
            {
                companys = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<CertificateTypeEnum> CertificateTypes
        {
            get => certificateTypes;
            set
            {
                certificateTypes = value;
                NotifyPropertyChanged();
            }
        }

        public string CardType
        {
            get => cardType;
            set
            {
                cardType = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<ItemsCheckViewModel> VisitReasons
        {
            get
            {
                return visitReasons;
            }

            set
            {
                visitReasons = value;
                NotifyPropertyChanged();
            }
        }

        public AuthorizeTimeLimitViewModel TimeLimitVM
        {
            get
            {
                return _timeLimitVM;
            }

            set
            {
                _timeLimitVM = value;
                NotifyPropertyChanged();
            }
        }
    }
}
