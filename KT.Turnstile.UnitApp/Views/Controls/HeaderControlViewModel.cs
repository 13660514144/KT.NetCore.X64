using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Quanta.Common.Models;
using KT.Turnstile.Unit.ClientApp.Events;
using Prism.Events;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace KT.Turnstile.Unit.ClientApp.Views.Controls
{
    public class HeaderControlViewModel : BindableBase
    {
        private Timer _returnTimer;

        //链接到首页
        private ICommand _linkToHomeCommand;
        public ICommand LinkToHomeCommand => _linkToHomeCommand ??= new DelegateCommand(LinkToHome);

        private int _surplusTime;

        private IEventAggregator _eventAggregator;
        private AppSettings _appSettings;

        public HeaderControlViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();

            _eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Subscribe(HandledElevator);
            _eventAggregator.GetEvent<PassDisplayEvent>().Subscribe(PassShow);
            _eventAggregator.GetEvent<LinkToHomeEvent>().Subscribe(ReturnTimeEnd);

            Init();
        }

        private void PassShow(PassDisplayModel obj)
        {
            ReturnTimeStart();
        }

        private void HandledElevator(HandleElevatorDisplayModel obj)
        {
            ReturnTimeStart();
        }

        private void LinkToHome()
        {
            _eventAggregator.GetEvent<LinkToHomeEvent>().Publish();
        }

        private void ReturnTimeStart()
        {
            SurplusTime = _appSettings.AutoReturnSecondTime;
            if (_returnTimer != null)
            {
                _returnTimer.Dispose();
            }
            _returnTimer = new Timer(TipsTimeCallback, null, 1000, 1000);
        }

        private void ReturnTimeEnd()
        {
            SurplusTime = _appSettings.AutoReturnSecondTime;
            if (_returnTimer != null)
            {
                _returnTimer.Dispose();
            }
        }

        private void TipsTimeCallback(object state)
        {
            if (SurplusTime <= 1)
            {
                LinkToHome();
                return;
            }

            if (Application.Current?.Dispatcher == null)
            {
                return;
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                SurplusTime--;
            });
        }


        public int SurplusTime
        {
            get
            {
                return _surplusTime;
            }

            set
            {
                SetProperty(ref _surplusTime, value);
            }
        }


        private string _logoUrl;
        private string _systemName;

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


    }
}
