using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Quanta.Common.Enums;
using KT.Quanta.Common.Models;
using KT.Turnstile.Unit.ClientApp.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace KT.Turnstile.Unit.ClientApp.Views.Controls
{
    public class FaceNoRightPassControlViewModel : BindableBase
    {
        private bool _show;

        private IEventAggregator _eventAggregator;

        public FaceNoRightPassControlViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Subscribe(HandledElevatorSuccess);
            _eventAggregator.GetEvent<PassDisplayEvent>().Subscribe(PassShow);
        }

        private void PassShow(PassDisplayModel passShow)
        {
            if (passShow.DisplayType == PassDisplayTypeEnum.NoRight.Value
                && passShow.AccessType == AccessTypeEnum.FACE.Value)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Show = true;
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Show = false;
                });
            }
        }

        private void HandledElevatorSuccess(HandleElevatorDisplayModel handledElevatorSuccess)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Show = false;
            });
        }

        public bool Show
        {
            get
            {
                return _show;
            }

            set
            {
                SetProperty(ref _show, value);
            }
        }
    }
}
