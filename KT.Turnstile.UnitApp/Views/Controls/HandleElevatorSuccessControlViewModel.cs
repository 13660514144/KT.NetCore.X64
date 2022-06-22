using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Quanta.Common.Models;
using KT.Turnstile.Unit.ClientApp.Events;
using KT.Turnstile.Unit.ClientApp.Service.Helpers;
using KT.Turnstile.Unit.Entity.Models;
using Prism.Events;
using System.Windows;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace KT.Turnstile.Unit.ClientApp.Views.Controls
{
    public class HandleElevatorSuccessControlViewModel : BindableBase
    {
        private string _floorName;
        private string _elevatorName;
        private bool _show;
        private ILogger _logger;
        private IEventAggregator _eventAggregator;
        private readonly ElevatorDisplayDeviceSettings _elevatorDisplayDeviceSettings;

        public HandleElevatorSuccessControlViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _elevatorDisplayDeviceSettings = ContainerHelper.Resolve<ElevatorDisplayDeviceSettings>();

            _eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Subscribe(HandledElevatorSuccess);
            _eventAggregator.GetEvent<PassDisplayEvent>().Subscribe(PassShow);
            _logger = ContainerHelper.Resolve<ILogger>();
        }

        private void PassShow(PassDisplayModel obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Show = false;
            });
        }

        private void HandledElevatorSuccess(HandleElevatorDisplayModel handledElevatorSuccess)
        {
            _logger.LogInformation($"display==>{JsonConvert.SerializeObject(handledElevatorSuccess)}");
            Application.Current.Dispatcher.Invoke(() =>
            {
                FloorName = handledElevatorSuccess.DestinationFloorName;
                ElevatorName = handledElevatorSuccess.ElevatorName;
                Show = true;
            });
            //输出宝顿派梯屏
            if (_elevatorDisplayDeviceSettings.IsEnable)
            {
                var elevatorDisplayDeviceClient = ContainerHelper.Resolve<ElevatorDisplayDeviceClient>();
                var elevatorDisplay = new ElevatorDisplayModel();
                elevatorDisplay.PhysicsFloor = handledElevatorSuccess.PhysicsFloor;
                elevatorDisplay.ElevatorNumber = handledElevatorSuccess.ElevatorNumber;
                elevatorDisplayDeviceClient.SendAsync(elevatorDisplay);
            }
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
