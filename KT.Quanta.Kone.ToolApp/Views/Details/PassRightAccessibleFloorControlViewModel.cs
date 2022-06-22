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
    public class PassRightAccessibleFloorControlViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
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
        private ObservableCollection<PassRightAccessibleFloorViewModel> _passRightAccessibleFloors;
        private PassRightAccessibleFloorViewModel _passRightAccessibleFloor;

        private ICommand _returnCommand;
        public ICommand ReturnCommand => _returnCommand ??= new DelegateCommand(Return);

        private ICommand _elevatorGroupSelectedCommand;
        public ICommand ElevatorGroupSelectedCommand => _elevatorGroupSelectedCommand ??= new DelegateCommand(ElevatorGroupSelected);

        private ICommand _frontAllCheckedCommand;
        public ICommand FrontAllCheckedCommand => _frontAllCheckedCommand ??= new DelegateCommand(FrontAllChecked);
        private ICommand _rearAllCheckedCommand;
        public ICommand RearAllCheckedCommand => _rearAllCheckedCommand ??= new DelegateCommand(RearAllChecked);

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
        private readonly IPassRightAccessibleFloorApi _passRightAccessibleFloorApi;
        private readonly IPassRightApi _passRightApi;
        private readonly IElevatorGroupApi _elevatorGroupApi;
        private readonly IKoneApi _koneApi;
        private readonly IMapper _mapper;
        private readonly IHandleElevatorDeviceApi _handleElevatorDeviceApi;

        public PassRightAccessibleFloorControlViewModel(IElevatorGroupApi elevatorGroupApi,
            IKoneApi koneApi,
            IMapper mapper,
            IHandleElevatorDeviceApi handleElevatorDeviceApi,
            IEventAggregator eventAggregator,
            IRegionManager regionManager,
            IPassRightApi passRightApi,
            IPassRightAccessibleFloorApi passRightAccessibleFloorApi)
        {
            _elevatorGroupApi = elevatorGroupApi;
            _koneApi = koneApi;
            _mapper = mapper;
            _eventAggregator = eventAggregator;
            _handleElevatorDeviceApi = handleElevatorDeviceApi;
            _regionManager = regionManager;
            _passRightApi = passRightApi;
            _passRightAccessibleFloorApi = passRightAccessibleFloorApi;

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

            SetPassRightAccessibleFloorsAsync();
        }

        private async void SetPassRightAccessibleFloorsAsync()
        {
            if (PassRight == null)
            {
                return;
            }
            if (_elevatorGroups?.FirstOrDefault() == null)
            {
                PassRightAccessibleFloors = new ObservableCollection<PassRightAccessibleFloorViewModel>();
                return;
            }

            var passRightAccessibleFloorModels = await _passRightAccessibleFloorApi.GetWithDetailBySignAsync(PassRight.Sign);
            PassRightAccessibleFloors = new ObservableCollection<PassRightAccessibleFloorViewModel>();
            foreach (var elevatorGroup in _elevatorGroups)
            {
                var passRightAccessibleFloorModel = passRightAccessibleFloorModels?.FirstOrDefault(x => x.ElevatorGroupId == elevatorGroup.Id);

                var passRightAccessibleFloor = new PassRightAccessibleFloorViewModel();
                passRightAccessibleFloor.ElevatorGroup = elevatorGroup;
                passRightAccessibleFloor.ElevatorGroupId = elevatorGroup.Id;
                passRightAccessibleFloor.Sign = PassRight.Sign;
                passRightAccessibleFloor.PassRightAccessibleFloorDetails = new List<PassRightAccessibleFloorDetailViewModel>();
                if (PassRight.Floors?.FirstOrDefault() != null)
                {
                    foreach (var item in PassRight.Floors)
                    {
                        PassRightAccessibleFloorDetailViewModel passRightAccessibleFloorDetailViewModel;
                        var passRightAccessibleFloorDetail = passRightAccessibleFloorModel?.PassRightAccessibleFloorDetails?.FirstOrDefault(x => x.FloorId == item.Id);
                        if(passRightAccessibleFloorDetail!= null)
                        {
                            passRightAccessibleFloorDetailViewModel = _mapper.Map<PassRightAccessibleFloorDetailViewModel>(passRightAccessibleFloorDetail);
                            passRightAccessibleFloorDetailViewModel.Floor = item;
                        }
                        else
                        {
                            passRightAccessibleFloorDetailViewModel = new PassRightAccessibleFloorDetailViewModel();
                            passRightAccessibleFloorDetailViewModel.FloorId = item.Id;
                            passRightAccessibleFloorDetailViewModel.Floor = item;
                        }
                        passRightAccessibleFloor.PassRightAccessibleFloorDetails.Add(passRightAccessibleFloorDetailViewModel);
                    }
                }
                PassRightAccessibleFloors.Add(passRightAccessibleFloor);
            }
        }

        private void ElevatorGroupSelected()
        {

        }

        private void FrontAllChecked()
        {
            foreach (var item in PassRight.Floors)
            {
                item.IsFront = IsFrontAllChecked;
            }
        }

        private void RearAllChecked()
        {
            foreach (var item in PassRight.Floors)
            {
                item.IsRear = IsRearAllChecked;
            }
        }

        private async void SaveAllAsync()
        {
            IsSubmiting = true;

            var models = _mapper.Map<List<PassRightAccessibleFloorModel>>(PassRightAccessibleFloors);
            await _passRightAccessibleFloorApi.AddOrEditsAsync(models);

            IsSubmiting = false;
        }

        private async void SaveCurrentAsync()
        {
            IsSubmiting = true;

            var model = _mapper.Map<PassRightAccessibleFloorModel>(PassRightAccessibleFloor);
            await _passRightAccessibleFloorApi.AddOrEditsAsync(new List<PassRightAccessibleFloorModel>() { model });

            IsSubmiting = false;
        }

        private async void DeleteAllAsync()
        {
            IsSubmiting = true;

            var model = _mapper.Map<PassRightModel>(PassRight);
            await _passRightAccessibleFloorApi.DeleteBySignAsync(model);

            IsSubmiting = false;
        }

        private async void DeleteCurrentAsync()
        {
            IsSubmiting = true;

            var model = _mapper.Map<PassRightAccessibleFloorModel>(PassRightAccessibleFloor);
            await _passRightAccessibleFloorApi.DeleteBySignAndElevatorGroupId(model);

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

            SetPassRightAccessibleFloorsAsync();
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

        public ObservableCollection<PassRightAccessibleFloorViewModel> PassRightAccessibleFloors
        {
            get => _passRightAccessibleFloors;
            set
            {
                SetProperty(ref _passRightAccessibleFloors, value);
            }
        }

        public PassRightAccessibleFloorViewModel PassRightAccessibleFloor
        {
            get => _passRightAccessibleFloor;
            set
            {
                SetProperty(ref _passRightAccessibleFloor, value);
            }
        }
    }
}
