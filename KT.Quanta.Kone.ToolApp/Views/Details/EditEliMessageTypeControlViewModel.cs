using AutoMapper;
using KT.Common.Core.Enums;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.QuantaApi.Apis;
using KT.Quanta.Common.Enums;
using KT.Quanta.Kone.ToolApp.Enums;
using KT.Quanta.Kone.ToolApp.Events;
using KT.Quanta.Kone.ToolApp.Models;
using KT.Quanta.Model.Elevator.Dtos;
using KT.Quanta.Model.Kone;
using KT.Quanta.Service.Models;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace KT.Quanta.Kone.ToolApp.Views.Details
{
    public class EditEliMessageTypeControlViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        public bool KeepAlive
        {
            get { return false; }
        }

        private PassRightViewModel _passRight;
        private bool _isSubmiting;
        private ObservableCollection<ElevatorGroupViewModel> _elevatorGroups;
        private ElevatorGroupViewModel _elevatorGroup;
        private ObservableCollection<HandleElevatorDeviceViewModel> _handleElevatorDevices;
        private HandleElevatorDeviceViewModel _handleElevatorDevice;
        private List<OpenAccessForDopMessageTypeEnum> _openAccessForDopMessageTypes;
        private List<EliOpenAccessForDopMessageTypeViewModel> _eliOpenAccessForDopMessageTypes;
        private EliOpenAccessForDopMessageTypeViewModel _eliOpenAccessForDopMessageType;

        private ICommand _returnCommand;
        public ICommand ReturnCommand => _returnCommand ??= new DelegateCommand(Return);

        private ICommand _elevatorGroupSelectedCommand;
        public ICommand ElevatorGroupSelectedCommand => _elevatorGroupSelectedCommand ??= new DelegateCommand(ElevatorGroupSelectedAsync);

        private ICommand _handleElevatorDeviceSelectedCommand;
        public ICommand HandleElevatorDeviceSelectedCommand => _handleElevatorDeviceSelectedCommand ??= new DelegateCommand(HandleElevatorDeviceSelected);

        private ICommand _saveAllCommand;
        public ICommand SaveAllCommand => _saveAllCommand ??= new DelegateCommand(SaveAllAsync);

        private ICommand _saveCurrentCommand;
        public ICommand SaveCurrentCommand => _saveCurrentCommand ??= new DelegateCommand(SaveCurrentAsync);

        private ICommand _deleteAllCommand;
        public ICommand DeleteAllCommand => _deleteAllCommand ??= new DelegateCommand(DeleteAllAsync);

        private ICommand _deleteCurrentCommand;
        public ICommand DeleteCurrentCommand => _deleteCurrentCommand ??= new DelegateCommand(DeleteCurrentAsync);

        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPassRightApi _passRightApi;
        private readonly IElevatorGroupApi _elevatorGroupApi;
        private readonly IKoneApi _koneApi;
        private readonly IMapper _mapper;
        private readonly IHandleElevatorDeviceApi _handleElevatorDeviceApi;

        public EditEliMessageTypeControlViewModel(IElevatorGroupApi elevatorGroupApi,
            IKoneApi koneApi,
            IMapper mapper,
            IHandleElevatorDeviceApi handleElevatorDeviceApi,
            IEventAggregator eventAggregator,
            IRegionManager regionManager,
            IPassRightApi passRightApi)
        {
            _elevatorGroupApi = elevatorGroupApi;
            _koneApi = koneApi;
            _mapper = mapper;
            _eventAggregator = eventAggregator;
            _handleElevatorDeviceApi = handleElevatorDeviceApi;
            _regionManager = regionManager;
            _passRightApi = passRightApi;

            _eventAggregator.GetEvent<ExceptionEvent>().Subscribe(Error);

            OpenAccessForDopMessageTypes = BaseEnum.GetAll<OpenAccessForDopMessageTypeEnum>().ToList();

            RefreshAsync();
        }

        private void Error(Exception ex)
        {
            IsSubmiting = false;
        }

        private async void RefreshAsync()
        {
            var elevatorGroups = await _elevatorGroupApi.GetAllAsync();
            ElevatorGroups = _mapper.Map<ObservableCollection<ElevatorGroupViewModel>>(elevatorGroups);

            var eliOpenAccessForDopMessageTypes = await _koneApi.GetEliOpenAccessForDopMessageTypesByPassRightSign(PassRight.Sign);
            _eliOpenAccessForDopMessageTypes = _mapper.Map<List<EliOpenAccessForDopMessageTypeViewModel>>(eliOpenAccessForDopMessageTypes);
        }

        private async void ElevatorGroupSelectedAsync()
        {
            var handleElevatorDevices = await _handleElevatorDeviceApi.GetByElevatorGroupIdAsync(ElevatorGroup.Id);
            HandleElevatorDevices = _mapper.Map<ObservableCollection<HandleElevatorDeviceViewModel>>(handleElevatorDevices);
        }

        private void HandleElevatorDeviceSelected()
        {
            if (HandleElevatorDevice == null)
            {
                EliOpenAccessForDopMessageType = null;
                return;
            }

            EliOpenAccessForDopMessageType = _eliOpenAccessForDopMessageTypes.FirstOrDefault(x => x.HandleElevatorDeviceId == HandleElevatorDevice.Id);
            if (EliOpenAccessForDopMessageType == null)
            {
                EliOpenAccessForDopMessageType = new EliOpenAccessForDopMessageTypeViewModel();
                EliOpenAccessForDopMessageType.HandleElevatorDeviceId = HandleElevatorDevice.Id;
                EliOpenAccessForDopMessageType.PassRightSign = PassRight.Sign;
                _eliOpenAccessForDopMessageTypes.Add(EliOpenAccessForDopMessageType);
            }
        }

        private async void SaveAllAsync()
        {
            IsSubmiting = true;

            var eliOpenAccessForDopMessageTypes = _mapper.Map<List<EliOpenAccessForDopMessageTypeModel>>(_eliOpenAccessForDopMessageTypes);
            await _koneApi.AddOrEditEliOpenAccessForDopMessageTypes(eliOpenAccessForDopMessageTypes);

            IsSubmiting = false;
        }

        private async void SaveCurrentAsync()
        {
            IsSubmiting = true;

            var eliOpenAccessForDopMessageType = _mapper.Map<EliOpenAccessForDopMessageTypeModel>(EliOpenAccessForDopMessageType);
            await _koneApi.AddOrEditEliOpenAccessForDopMessageType(eliOpenAccessForDopMessageType);

            IsSubmiting = false;
        }

        private async void DeleteAllAsync()
        {
            IsSubmiting = true;

            var eliOpenAccessForDopMessageTypes = _mapper.Map<List<EliOpenAccessForDopMessageTypeModel>>(_eliOpenAccessForDopMessageTypes);
            await _koneApi.DeleteEliOpenAccessForDopMessageTypes(eliOpenAccessForDopMessageTypes);

            IsSubmiting = false;
        }

        private async void DeleteCurrentAsync()
        {
            IsSubmiting = true;

            var eliOpenAccessForDopMessageType = _mapper.Map<EliOpenAccessForDopMessageTypeModel>(EliOpenAccessForDopMessageType);
            await _koneApi.DeleteEliOpenAccessForDopMessageType(eliOpenAccessForDopMessageType);

            IsSubmiting = false;
        }

        private void Return()
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(PassRightListControl));
        }

        private string GetDirectionValue(bool isFront, bool isRear)
        {
            if (isFront && !isRear)
            {
                return FloorDirectionEnum.Front.Value;
            }
            else if (!isFront && isRear)
            {
                return FloorDirectionEnum.Rear.Value;
            }
            else
            {
                return string.Empty;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var passRight = navigationContext.Parameters["passRight"] as PassRightViewModel;
            if (passRight != null)
            {
                PassRight = passRight;
            }

            RefreshAsync();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public PassRightViewModel PassRight
        {
            get => _passRight;
            set
            {
                SetProperty(ref _passRight, value);
            }
        }

        public bool IsSubmiting
        {
            get => _isSubmiting;
            set
            {
                SetProperty(ref _isSubmiting, value);
            }
        }

        public ObservableCollection<ElevatorGroupViewModel> ElevatorGroups
        {
            get => _elevatorGroups;
            set
            {
                SetProperty(ref _elevatorGroups, value);
            }
        }

        public HandleElevatorDeviceViewModel HandleElevatorDevice
        {
            get => _handleElevatorDevice;
            set
            {
                SetProperty(ref _handleElevatorDevice, value);
            }
        }

        public ElevatorGroupViewModel ElevatorGroup
        {
            get => _elevatorGroup;
            set
            {
                SetProperty(ref _elevatorGroup, value);
            }
        }

        public ObservableCollection<HandleElevatorDeviceViewModel> HandleElevatorDevices
        {
            get => _handleElevatorDevices;
            set
            {
                SetProperty(ref _handleElevatorDevices, value);
            }
        }

        public List<OpenAccessForDopMessageTypeEnum> OpenAccessForDopMessageTypes
        {
            get => _openAccessForDopMessageTypes;
            set
            {
                SetProperty(ref _openAccessForDopMessageTypes, value);
            }
        }

        public EliOpenAccessForDopMessageTypeViewModel EliOpenAccessForDopMessageType
        {
            get => _eliOpenAccessForDopMessageType;
            set
            {
                SetProperty(ref _eliOpenAccessForDopMessageType, value);
            }
        }
    }
}
