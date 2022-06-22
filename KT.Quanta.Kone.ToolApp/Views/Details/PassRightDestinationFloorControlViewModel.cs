using AutoMapper;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.QuantaApi.Apis;
using KT.Quanta.Common.Enums;
using KT.Quanta.Kone.ToolApp.Events;
using KT.Quanta.Kone.ToolApp.Models;
using KT.Quanta.Model.Elevator.Dtos;
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
    public class PassRightDestinationFloorControlViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        public bool KeepAlive
        {
            get { return false; }
        }

        private PassRightViewModel _passRight;
        private bool _isFrontAllChecked;
        private bool _isRearAllChecked;
        private bool _isSubmiting;
        private ObservableCollection<ElevatorGroupViewModel> _elevatorGroups;
        private ObservableCollection<PassRightDestinationFloorViewModel> _passRightDestinationFloors;
        private PassRightDestinationFloorViewModel _passRightDestinationFloor;

        private ICommand _returnCommand;
        public ICommand ReturnCommand => _returnCommand ??= new DelegateCommand(Return);

        private ICommand _elevatorGroupSelectedCommand;
        public ICommand ElevatorGroupSelectedCommand => _elevatorGroupSelectedCommand ??= new DelegateCommand(ElevatorGroupSelected);

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
        private readonly IPassRightDestinationFloorApi _passRightDestinationFloorApi;
        private readonly IPassRightApi _passRightApi;
        private readonly IElevatorGroupApi _elevatorGroupApi;
        private readonly IKoneApi _koneApi;
        private readonly IMapper _mapper;
        private readonly IHandleElevatorDeviceApi _handleElevatorDeviceApi;

        public PassRightDestinationFloorControlViewModel(IElevatorGroupApi elevatorGroupApi,
            IKoneApi koneApi,
            IMapper mapper,
            IHandleElevatorDeviceApi handleElevatorDeviceApi,
            IEventAggregator eventAggregator,
            IRegionManager regionManager,
            IPassRightApi passRightApi,
            IPassRightDestinationFloorApi passRightDestinationFloorApi)
        {
            _elevatorGroupApi = elevatorGroupApi;
            _koneApi = koneApi;
            _mapper = mapper;
            _eventAggregator = eventAggregator;
            _handleElevatorDeviceApi = handleElevatorDeviceApi;
            _regionManager = regionManager;
            _passRightApi = passRightApi;
            _passRightDestinationFloorApi = passRightDestinationFloorApi;

            _eventAggregator.GetEvent<ExceptionEvent>().Subscribe(Error);

            RefreshAsync();
        }

        private void Error(Exception ex)
        {
            IsSubmiting = false;
        }

        private async void RefreshAsync()
        {
            var elevatorGroups = await _elevatorGroupApi.GetAllAsync();
            _elevatorGroups = _mapper.Map<ObservableCollection<ElevatorGroupViewModel>>(elevatorGroups);

            SetPassRightDestinationFloorsAsync();
        }

        private async void SetPassRightDestinationFloorsAsync()
        {
            if (PassRight == null)
            {
                return;
            }
            if (_elevatorGroups?.FirstOrDefault() == null)
            {
                PassRightDestinationFloors = new ObservableCollection<PassRightDestinationFloorViewModel>();
                return;
            }

            var passRightDestinationFloorModels = await _passRightDestinationFloorApi.GetWithDetailBySignAsync(PassRight.Sign);
            PassRightDestinationFloors = new ObservableCollection<PassRightDestinationFloorViewModel>();
            foreach (var elevatorGroup in _elevatorGroups)
            {
                var passRightDestinationFloorModel = passRightDestinationFloorModels?.FirstOrDefault(x => x.ElevatorGroupId == elevatorGroup.Id);

                var passRightDestinationFloor = _mapper.Map<PassRightDestinationFloorViewModel>(passRightDestinationFloorModel);
                if(passRightDestinationFloor == null)
                {
                    passRightDestinationFloor = new PassRightDestinationFloorViewModel();
                }

                passRightDestinationFloor.ElevatorGroup = elevatorGroup;
                passRightDestinationFloor.ElevatorGroupId = elevatorGroup.Id;
                passRightDestinationFloor.Sign = PassRight.Sign;

                if (PassRight.Floors?.FirstOrDefault() != null)
                {
                    var floor = PassRight.Floors?.FirstOrDefault(x => x.Id == passRightDestinationFloor.FloorId);
                    if (floor != null)
                    {
                        passRightDestinationFloor.Floor = floor;
                    }
                }

                PassRightDestinationFloors.Add(passRightDestinationFloor);
            }
        }

        private void ElevatorGroupSelected()
        {

        }

        private async void SaveAllAsync()
        {
            IsSubmiting = true;

            var models = _mapper.Map<List<PassRightDestinationFloorModel>>(PassRightDestinationFloors);
            await _passRightDestinationFloorApi.AddOrEditsAsync(models);

            IsSubmiting = false;
        }

        private async void SaveCurrentAsync()
        {
            IsSubmiting = true;

            var model = _mapper.Map<PassRightDestinationFloorModel>(PassRightDestinationFloor);
            await _passRightDestinationFloorApi.AddOrEditsAsync(new List<PassRightDestinationFloorModel>() { model });

            IsSubmiting = false;
        }

        private async void DeleteAllAsync()
        {
            IsSubmiting = true;

            var model = _mapper.Map<PassRightModel>(PassRight);
            await _passRightDestinationFloorApi.DeleteBySignAsync(model);

            IsSubmiting = false;
        }

        private async void DeleteCurrentAsync()
        {
            IsSubmiting = true;

            var model = _mapper.Map<PassRightDestinationFloorModel>(PassRightDestinationFloor);
            await _passRightDestinationFloorApi.DeleteBySignAndElevatorGroupId(model);

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

            SetPassRightDestinationFloorsAsync();
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
        public bool IsFrontAllChecked
        {
            get => _isFrontAllChecked;
            set
            {
                SetProperty(ref _isFrontAllChecked, value);
            }
        }

        public bool IsRearAllChecked
        {
            get => _isRearAllChecked;
            set
            {
                SetProperty(ref _isRearAllChecked, value);
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

        public ObservableCollection<PassRightDestinationFloorViewModel> PassRightDestinationFloors
        {
            get => _passRightDestinationFloors;
            set
            {
                SetProperty(ref _passRightDestinationFloors, value);
            }
        }

        public PassRightDestinationFloorViewModel PassRightDestinationFloor
        {
            get => _passRightDestinationFloor;
            set
            {
                SetProperty(ref _passRightDestinationFloor, value);
            }
        }
    }
}
