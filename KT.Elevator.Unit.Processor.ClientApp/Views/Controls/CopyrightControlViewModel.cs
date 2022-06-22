using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Elevator.Unit.Processor.ClientApp.Events;
using KT.Elevator.Unit.Processor.ClientApp.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Processor.ClientApp.Views.Controls
{
    public class CopyrightControlViewModel : BindableBase
    {
        private AppSettings _appSettings;
        private string _systemCopyright;
        private string _nowYear;
        private StateViewModel _state;

        private IEventAggregator _eventAggregator;

        public CopyrightControlViewModel()
        {
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            Init();

            _eventAggregator.GetEvent<IsOnlineEvent>().Subscribe(IsOnline);
            _eventAggregator.GetEvent<IsLoadingDataEvent>().Subscribe(IsLoadingData);
        }

        private void IsOnline(bool isOnline)
        {
            State.IsOnline = isOnline;
        }
        private void IsLoadingData(bool isLoadingData)
        {
            State.IsLoadingData = isLoadingData;
        }

        private void Init()
        {
            SystemCopyright = _appSettings.SystemCopyright;
            NowYear = DateTime.Now.ToString("yyyy");

            State = ContainerHelper.Resolve<StateViewModel>();
        }

        public string NowYear
        {
            get
            {
                return _nowYear;
            }

            set
            {
                SetProperty(ref _nowYear, value);
            }
        }
        public string SystemCopyright
        {
            get
            {
                return _systemCopyright;
            }

            set
            {
                SetProperty(ref _systemCopyright, value);
            }
        }

        public StateViewModel State
        {
            get => _state;
            set
            {
                SetProperty(ref _state, value);
            }
        }
    }
}
