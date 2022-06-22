using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Elevator.Unit.Dispatch.ClientApp.Device.Hitachi;
using KT.Elevator.Unit.Dispatch.ClientApp.Events;
using KT.Elevator.Unit.Dispatch.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Dispatch.ClientApp.Service.IServices;
using KT.Elevator.Unit.Dispatch.Entity.Models;
using KT.Quanta.Common.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Views
{
    public class SuccessControlViewModel : BindableBase
    {
        private string _floorName;
        private string _elevatorName;

        private IEventAggregator _eventAggregator;

        public SuccessControlViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<HandledElevatorEvent>().Subscribe(HandledElevatorAsync);
            _eventAggregator.GetEvent<HandleElevatorReceiveEvent>().Subscribe(HandleElevatorReceive);
        }

        private async void HandledElevatorAsync(UnitDispatchHandleElevatorModel handleElevator)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                FloorName = handleElevator.SendData.DestinationFloorName;
            });

            await Task.CompletedTask;
        }

        private void HandleElevatorReceive(HitachiReceiveModel data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ElevatorName = data.ElevatorName;
            });
        }

        public string FloorName
        {
            get
            {
                return _floorName;
            }

            set
            {
                SetProperty(ref _floorName, value);
            }
        }

        public string ElevatorName
        {
            get
            {
                return _elevatorName;
            }

            set
            {
                SetProperty(ref _elevatorName, value);
            }
        }
    }
}
