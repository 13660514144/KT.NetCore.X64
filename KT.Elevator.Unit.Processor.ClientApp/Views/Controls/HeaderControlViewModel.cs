using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Elevator.Unit.Processor.ClientApp.Events;
using KT.Quanta.Common.Models;
using Prism.Events;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace KT.Elevator.Unit.Processor.ClientApp.Views.Controls
{
    public class HeaderControlViewModel : BindableBase
    {
        private Timer _returnTimer;

        //链接到首页
        private ICommand _linkToHomeCommand;
        public ICommand LinkToHomeCommand => _linkToHomeCommand ??= new DelegateCommand(LinkToHome);

        private decimal _surplusTime;
        private string _logoUrl;
        private string _systemName;
        private bool _isCanExit;

        private IEventAggregator _eventAggregator;
        private AppSettings _appSettings;

        public HeaderControlViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();

            _eventAggregator.GetEvent<ShowSuccessPanelEvent>().Subscribe(ShowSuccessPanel);
            _eventAggregator.GetEvent<LinkToHomeEvent>().Subscribe(LinkToHomeSubscribe);
            //_eventAggregator.GetEvent<HandledElevatorEvent>().Subscribe(HandledElevatorAsync);

            _eventAggregator.GetEvent<NotRightEvent>().Subscribe(NotRight);
            _eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Subscribe(HandledElevatorSuccess);
            _eventAggregator.GetEvent<HandledElevatorErrorEvent>().Subscribe(HandledElevatorError);

            Init();
        }

        private void HandledElevatorError(HandleElevatorErrorModel obj)
        {
            IsCanExit = true;
            ReturnTimeStart(_appSettings.SuccessReturnSecondTime);
        }

        private void HandledElevatorSuccess(HandleElevatorDisplayModel obj)
        {
            IsCanExit = true;
            ReturnTimeStart(_appSettings.DestinationPanelReturnSecondTime);
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
        }

        private void ShowSuccessPanel()
        {
            IsCanExit = true;
            ReturnTimeStart(_appSettings.SuccessReturnSecondTime);
        }

        //private void HandledElevatorAsync(UnitHandleElevatorModel obj)
        //{
        //    IsCanExit = true;
        //    ReturnTimeStart(_appSettings.SuccessReturnSecondTime);
        //}

        private void LinkToDestinationPanel()
        {
            IsCanExit = true;
            ReturnTimeStart(_appSettings.DestinationPanelReturnSecondTime);
        }

        private void NotRight()
        {
            IsCanExit = true;
            ReturnTimeStart(_appSettings.NotRightReturnSecondTime);
        }

        private void LinkToHomeSubscribe()
        {
            ReturnTimeEnd();
            IsCanExit = false;
        }

        private void LinkToHome()
        {
            _eventAggregator.GetEvent<LinkToHomeEvent>().Publish();
        }

        private void ReturnTimeStart(decimal time)
        {
            SurplusTime = time;
            if (_returnTimer != null)
            {
                _returnTimer.Dispose();
            }
            _returnTimer = new Timer(TipsTimeCallback, null, 1000, 1000);
        }

        private void ReturnTimeEnd()
        {
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

        public decimal SurplusTime
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

        public bool IsCanExit
        {
            get
            {
                return _isCanExit;
            }

            set
            {
                SetProperty(ref _isCanExit, value);
            }
        }
    }
}
