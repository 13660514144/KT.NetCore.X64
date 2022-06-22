using AutoMapper;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.QuantaApi.Apis;
using KT.Quanta.Kone.ToolApp.Enums;
using KT.Quanta.Kone.ToolApp.Events;
using KT.Quanta.Kone.ToolApp.Models;
using KT.Quanta.Model.Kone;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KT.Quanta.Kone.ToolApp.Views
{
    public class ClearRoyalMaskControlViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive
        {
            get { return true; }
        }

        private ObservableCollection<ElevatorGroupViewModel> _elevatorGroups;
        private ElevatorGroupViewModel _elevatorGroup;
        private ObservableCollection<HandleElevatorDeviceViewModel> _handleElevatorDevices;
        private bool _isSubmiting;
        private List<ConnectedStateEnum> _connectedStates;
        private DopSpecificDefaultAccessMaskViewModel _dopSpecificDefaultAccessMask;
        private DopGlobalDefaultAccessMaskViewModel _dopGlobalDefaultAccessMask;

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??= new DelegateCommand(RefreshAsync);

        private ICommand _deleteRoyalDopGlobalMaskCommand;

        public ICommand DeleteRoyalDopGlobalMaskCommand => _deleteRoyalDopGlobalMaskCommand ??= new DelegateCommand(DeleteRoyalDopGlobalMaskAsync);

        private ICommand _deleteRoyalDopSpecificMaskCommand;

        public ICommand DeleteRoyalDopSpecificMaskCommand => _deleteRoyalDopSpecificMaskCommand ??= new DelegateCommand(DeleteRoyalDopSpecificMaskAsync);

        private readonly IElevatorGroupApi _elevatorGroupApi;
        private readonly IKoneApi _koneApi;
        private readonly IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;
        private readonly IHandleElevatorDeviceApi _handleElevatorDeviceApi;

        public ClearRoyalMaskControlViewModel(IElevatorGroupApi elevatorGroupApi,
            IKoneApi koneApi,
            IMapper mapper,
            IEventAggregator eventAggregator,
            IHandleElevatorDeviceApi handleElevatorDeviceApi)
        {
            _elevatorGroupApi = elevatorGroupApi;
            _koneApi = koneApi;
            _mapper = mapper;
            _eventAggregator = eventAggregator;
            _handleElevatorDeviceApi = handleElevatorDeviceApi;

            ConnectedStates = ConnectedStateEnum.GetAllWithNull();
            DopGlobalDefaultAccessMask = new DopGlobalDefaultAccessMaskViewModel();
            DopSpecificDefaultAccessMask = new DopSpecificDefaultAccessMaskViewModel();

            _eventAggregator.GetEvent<ExceptionEvent>().Subscribe(Error);

            RefreshAsync();
        }

        private void Error(Exception ex)
        {
            IsSubmiting = false;
        }

        private async void RefreshAsync()
        {
            var elevatorGroupModels = await _elevatorGroupApi.GetAllWithFloorAsync();
            ElevatorGroups = _mapper.Map<ObservableCollection<ElevatorGroupViewModel>>(elevatorGroupModels);

            var handleElevatorDevices = await _handleElevatorDeviceApi.GetAllAsync();
            HandleElevatorDevices = _mapper.Map<ObservableCollection<HandleElevatorDeviceViewModel>>(handleElevatorDevices);

            //增加第一项空
            HandleElevatorDevices.Insert(0, null);
        }

        private async void DeleteRoyalDopGlobalMaskAsync()
        {
            IsSubmiting = true;

            var model = _mapper.Map<DopGlobalDefaultAccessMaskModel>(DopGlobalDefaultAccessMask);
            await _koneApi.DeleteRoyalDopGlobalMaskAsync(model);

            IsSubmiting = false;
        }

        private async void DeleteRoyalDopSpecificMaskAsync()
        {
            IsSubmiting = true;

            var model = _mapper.Map<DopSpecificDefaultAccessMaskModel>(DopSpecificDefaultAccessMask);
            await _koneApi.DeleteRoyalDopSpecificMaskAsync(model);

            IsSubmiting = false;
        }

        public ObservableCollection<ElevatorGroupViewModel> ElevatorGroups
        {
            get
            {
                return _elevatorGroups;
            }

            set
            {
                SetProperty(ref _elevatorGroups, value);
            }
        }

        public ElevatorGroupViewModel ElevatorGroup
        {
            get
            {
                return _elevatorGroup;
            }

            set
            {
                SetProperty(ref _elevatorGroup, value);
            }
        }

        public bool IsSubmiting
        {
            get
            {
                return _isSubmiting;
            }

            set
            {
                SetProperty(ref _isSubmiting, value);
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

        public List<ConnectedStateEnum> ConnectedStates
        {
            get
            {
                return _connectedStates;
            }

            set
            {
                SetProperty(ref _connectedStates, value);
            }
        }

        public DopGlobalDefaultAccessMaskViewModel DopGlobalDefaultAccessMask
        {
            get => _dopGlobalDefaultAccessMask;
            set
            {
                SetProperty(ref _dopGlobalDefaultAccessMask, value);
            }
        }

        public DopSpecificDefaultAccessMaskViewModel DopSpecificDefaultAccessMask
        {
            get => _dopSpecificDefaultAccessMask;
            set
            {
                SetProperty(ref _dopSpecificDefaultAccessMask, value);
            }
        }
    }
}
