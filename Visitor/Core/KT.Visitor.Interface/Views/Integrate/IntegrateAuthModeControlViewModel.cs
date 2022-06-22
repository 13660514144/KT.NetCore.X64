using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.Data.Enums;
using KT.Visitor.Data.IServices;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Helper;
using Microsoft.Extensions.DependencyInjection;
using Panuon.UI.Silver.Core;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class IntegrateAuthModeControlViewModel : PropertyChangedBase
    {
        private ObservableCollection<ItemsCheckViewModel> _authTypes;
        private AuthorizeTimeLimitViewModel _timeLimitVM;
        private bool _isRememberAuthModel;

        private VistitorConfigApi _vistitorConfigApi;
        private VistitorConfigHelper _vistitorConfigHelper;
        private IContainerProvider _containerProvider;
        private ConfigHelper _configHelper;
        private ISystemConfigDataService _systemConfigDataService;

        public IntegrateAuthModeControlViewModel(VistitorConfigApi vistitorConfigApi,
            VistitorConfigHelper vistitorConfigHelper,
            IContainerProvider containerProvider,
            ConfigHelper configHelper,
            ISystemConfigDataService systemConfigDataService)
        {
            _vistitorConfigApi = vistitorConfigApi;
            _vistitorConfigHelper = vistitorConfigHelper;
            _containerProvider = containerProvider;
            _configHelper = configHelper;
            _systemConfigDataService = systemConfigDataService;

            Init();

            _ = RefreshDataAsync();
        }

        internal void RememberAuthType()
        {
            var authType = AuthTypes.FirstOrDefault(x => x.IsChecked)?.Id;
            if (_configHelper.LocalConfig.AuthModel != authType)
            {
                _systemConfigDataService.AddOrUpdateAsync(SystemConfigEnum.IS_REMEMBER_AUTH_MODEL, authType);
                _configHelper.LocalConfig.AuthModel = authType;
            }
        }

        internal void Init()
        {
            IsRememberAuthModel = _configHelper.LocalConfig.IsRememberAuthModel;
            TimeLimitVM = _containerProvider.Resolve<AuthorizeTimeLimitViewModel>();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public async Task RefreshDataAsync()
        {
            //初始化数据
            var visitorConfig = await _vistitorConfigApi.GetConfigParmsAsync();
            var authTypeVms = _vistitorConfigHelper.SetAuthTypes(visitorConfig?.AuthTypes, _configHelper.LocalConfig.AuthModel);
            AuthTypes = authTypeVms;
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
                //SetProperty(ref _timeLimitVM, value);
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<ItemsCheckViewModel> AuthTypes
        {
            get
            {
                return _authTypes;
            }

            set
            {
                _authTypes = value;
                //SetProperty(ref authTypes, value);
                NotifyPropertyChanged();
            }
        }

        public bool IsRememberAuthModel
        {
            get
            {
                return _isRememberAuthModel;
            }

            set
            {
                _isRememberAuthModel = value;
                //更新本地数据库
                _systemConfigDataService.AddOrUpdateAsync(SystemConfigEnum.IS_REMEMBER_AUTH_MODEL, value);
                _configHelper.LocalConfig.IsRememberAuthModel = value;
                //SetProperty(ref _isRememberAuthModel, value);
                NotifyPropertyChanged();

            }
        }
    }
}
