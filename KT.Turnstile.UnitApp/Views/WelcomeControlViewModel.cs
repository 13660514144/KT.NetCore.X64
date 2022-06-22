using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using System;
using System.IO;

namespace KT.Turnstile.Unit.ClientApp.Views
{
    public class WelcomeControlViewModel : BindableBase
    {
        private string _logoUrl;
        private string _systemName;
        private string _systemSecondName;

        private AppSettings _appSettings;

        public WelcomeControlViewModel()
        {
            _appSettings = ContainerHelper.Resolve<AppSettings>();

            Init();
        }

        private void Init()
        {
            if (_appSettings.LogoPath.Contains(":"))
            {
                LogoUrl = _appSettings.LogoPath;
            }
            else
            {
                LogoUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _appSettings.LogoPath);
            }
            SystemName = _appSettings.SystemName;
            SystemSecondName = _appSettings.SystemSecondName;
        }

        public string LogoUrl
        {
            get
            {
                return _logoUrl;
            }

            set
            {
                SetProperty(ref _logoUrl, value);
            }
        }

        public string SystemName
        {
            get
            {
                return _systemName;
            }

            set
            {
                SetProperty(ref _systemName, value);
            }
        }

        public string SystemSecondName
        {
            get
            {
                return _systemSecondName;
            }

            set
            {
                SetProperty(ref _systemSecondName, value);
            }
        }
    }
}
