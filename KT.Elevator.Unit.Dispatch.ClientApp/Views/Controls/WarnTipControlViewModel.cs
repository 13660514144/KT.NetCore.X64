using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Elevator.Unit.Dispatch.ClientApp.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Views.Controls
{
    public class WarnTipControlViewModel : BindableBase
    {
        private bool _isShow = false;
        private string _warnText;

        private Timer _runTimer;
        private IEventAggregator _eventAggregator;

        public WarnTipControlViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<WarnTipEvent>().Subscribe(ShowWarnTip);
        }

        private void ShowWarnTip(string message)
        {
            IsShow = true;
            WarnText = message;

            if (_runTimer != null)
            {
                _runTimer.Dispose();
            }
            _runTimer = new Timer(TipsTimeCallback, null, 5000, 5000);
        }

        private void TipsTimeCallback(object state)
        {
            IsShow = false;
            WarnText = string.Empty;
        }

        public bool IsShow
        {
            get
            {
                return _isShow;
            }

            set
            {
                SetProperty(ref _isShow, value);
            }
        }

        public string WarnText
        {
            get
            {
                return _warnText;
            }

            set
            {
                SetProperty(ref _warnText, value);
            }
        }

    }
}
