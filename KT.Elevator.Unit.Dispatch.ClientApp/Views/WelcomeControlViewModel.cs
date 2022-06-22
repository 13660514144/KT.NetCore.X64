using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Elevator.Unit.Dispatch.ClientApp.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Views
{
    public class WelcomeControlViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private AppSettings _appSettings;
        private string _logoPath;
        private string _systemName;
        private string _systemSecondName;

        public WelcomeControlViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();

            if (_appSettings.LogoPath.Contains(":"))
            {
                LogoPath = _appSettings.LogoPath;
            }
            else
            {
                LogoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _appSettings.LogoPath);
            }
            SystemName = _appSettings.SystemName;
            SystemSecondName = _appSettings.SystemSecondName;
        }

        public string LogoPath
        {
            get
            {
                return _logoPath;
            }

            set
            {
                SetProperty(ref _logoPath, value);
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
