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
    public class EditRcgifCallTypeControlViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
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
        private List<AcsBypassCallTypeEnum> _acsBypassCallTypes;
        private List<RcgifPassRightHandleElevatorDeviceCallTypeViewModel> _rcgifPassRightHandleElevatorDeviceCallTypes;
        private RcgifPassRightHandleElevatorDeviceCallTypeViewModel _rcgifPassRightHandleElevatorDeviceCallType;

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

        public EditRcgifCallTypeControlViewModel(IElevatorGroupApi elevatorGroupApi,
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

            AcsBypassCallTypes = BaseEnum.GetAll<AcsBypassCallTypeEnum>().ToList();

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

            var rcgifPassRightHandleElevatorDeviceCallTypes = await _koneApi.GetRcgifPassRightHandleElevatorDeviceCallTypesByPassRightSign(PassRight.Sign);
            _rcgifPassRightHandleElevatorDeviceCallTypes = _mapper.Map<List<RcgifPassRightHandleElevatorDeviceCallTypeViewModel>>(rcgifPassRightHandleElevatorDeviceCallTypes);
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
                RcgifPassRightHandleElevatorDeviceCallType = null;
                return;
            }

            RcgifPassRightHandleElevatorDeviceCallType = _rcgifPassRightHandleElevatorDeviceCallTypes.FirstOrDefault(x => x.HandleElevatorDeviceId == HandleElevatorDevice.Id);
            if (RcgifPassRightHandleElevatorDeviceCallType == null)
            {
                RcgifPassRightHandleElevatorDeviceCallType = new RcgifPassRightHandleElevatorDeviceCallTypeViewModel();
                RcgifPassRightHandleElevatorDeviceCallType.HandleElevatorDeviceId = HandleElevatorDevice.Id;
                RcgifPassRightHandleElevatorDeviceCallType.PassRightSign = PassRight.Sign;
                _rcgifPassRightHandleElevatorDeviceCallTypes.Add(RcgifPassRightHandleElevatorDeviceCallType);
            }
        }

        private async void SaveAllAsync()
        {
            IsSubmiting = true;

            var rcgifPassRightHandleElevatorDeviceCallTypes = _mapper.Map<List<RcgifPassRightHandleElevatorDeviceCallTypeModel>>(_rcgifPassRightHandleElevatorDeviceCallTypes);
            await _koneApi.AddOrEditRcgifPassRightHandleElevatorDeviceCallTypes(rcgifPassRightHandleElevatorDeviceCallTypes);

            IsSubmiting = false;
        }

        private async void SaveCurrentAsync()
        {
            IsSubmiting = true;

            var rcgifPassRightHandleElevatorDeviceCallType = _mapper.Map<RcgifPassRightHandleElevatorDeviceCallTypeModel>(RcgifPassRightHandleElevatorDeviceCallType);
            await _koneApi.AddOrEditRcgifPassRightHandleElevatorDeviceCallType(rcgifPassRightHandleElevatorDeviceCallType);

            IsSubmiting = false;
        }

        private async void DeleteAllAsync()
        {
            IsSubmiting = true;

            var rcgifPassRightHandleElevatorDeviceCallTypes = _mapper.Map<List<RcgifPassRightHandleElevatorDeviceCallTypeModel>>(_rcgifPassRightHandleElevatorDeviceCallTypes);
            await _koneApi.DeleteRcgifPassRightHandleElevatorDeviceCallTypes(rcgifPassRightHandleElevatorDeviceCallTypes);

            IsSubmiting = false;
        }

        private async void DeleteCurrentAsync()
        {
            IsSubmiting = true;

            var rcgifPassRightHandleElevatorDeviceCallType = _mapper.Map<RcgifPassRightHandleElevatorDeviceCallTypeModel>(RcgifPassRightHandleElevatorDeviceCallType);
            await _koneApi.DeleteRcgifPassRightHandleElevatorDeviceCallType(rcgifPassRightHandleElevatorDeviceCallType);

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

        public List<AcsBypassCallTypeEnum> AcsBypassCallTypes
        {
            get => _acsBypassCallTypes;
            set
            {
                SetProperty(ref _acsBypassCallTypes, value);
            }
        }

        public RcgifPassRightHandleElevatorDeviceCallTypeViewModel RcgifPassRightHandleElevatorDeviceCallType
        {
            get => _rcgifPassRightHandleElevatorDeviceCallType;
            set
            {
                SetProperty(ref _rcgifPassRightHandleElevatorDeviceCallType, value);
            }
        }
    }
}
