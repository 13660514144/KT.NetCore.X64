using AutoMapper;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.QuantaApi.Apis;
using KT.Quanta.Kone.ToolApp.Enums;
using KT.Quanta.Kone.ToolApp.Events;
using KT.Quanta.Kone.ToolApp.Helper;
using KT.Quanta.Kone.ToolApp.Models;
using KT.Quanta.Model.Kone;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KT.Quanta.Kone.ToolApp.Views.Details
{
    public class EditDopSpecificMaskControlViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        public bool KeepAlive
        {
            get { return false; }
        }

        private ObservableCollection<ElevatorGroupViewModel> _elevatorGroups;
        private ObservableCollection<HandleElevatorDeviceViewModel> _handleElevatorDevices;
        private List<ConnectedStateEnum> _connectedStates;
        private bool _isDestinationFront;
        private bool _isDestinationRear;
        private bool _isSubmiting;
        private DopSpecificDefaultAccessMaskViewModel _dopSpecificDefaultAccessMask;
        private List<DopSpecificMaskSelectModel> _dopSpecificMaskSelectList;
        private DopSpecificMaskSelectModel _dopSpecificMaskSelect;

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??= new DelegateCommand(RefreshAsync);

        private ICommand _elevatorGroupSelectedCommand;
        public ICommand ElevatorGroupSelectedCommand => _elevatorGroupSelectedCommand ??= new DelegateCommand(ElevatorGroupSelectedAsync);

        private ICommand _destinationFrontAllCheckedCommand;
        public ICommand DestinationFrontAllCheckedCommand => _destinationFrontAllCheckedCommand ??= new DelegateCommand(DestinationFrontAllSelected);
        private ICommand _destinationRearAllCheckedCommand;
        public ICommand DestinationRearAllCheckedCommand => _destinationRearAllCheckedCommand ??= new DelegateCommand(DestinationRearAllSelected);

        private ICommand _dopSpecificMaskSelectedCommand;
        public ICommand DopSpecificMaskSelectedCommand => _dopSpecificMaskSelectedCommand ??= new DelegateCommand<DopSpecificMaskSelectModel>(DopSpecificMaskSelectedAsync);

        private ICommand _returnCommand;
        public ICommand ReturnCommand => _returnCommand ??= new DelegateCommand(Return);
        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ??= new DelegateCommand(SaveAsync);
        private ICommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ??= new DelegateCommand(DeleteAsync);

        private readonly IElevatorGroupApi _elevatorGroupApi;
        private readonly IKoneApi _koneApi;
        private readonly IMapper _mapper;
        private readonly IHandleElevatorDeviceApi _handleElevatorDeviceApi;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        public EditDopSpecificMaskControlViewModel(IElevatorGroupApi elevatorGroupApi,
            IKoneApi koneApi,
            IMapper mapper,
            IHandleElevatorDeviceApi handleElevatorDeviceApi,
            IEventAggregator eventAggregator,
            IRegionManager regionManager)
        {
            _elevatorGroupApi = elevatorGroupApi;
            _koneApi = koneApi;
            _mapper = mapper;
            _eventAggregator = eventAggregator;
            _handleElevatorDeviceApi = handleElevatorDeviceApi;
            _regionManager = regionManager;

            ConnectedStates = ConnectedStateEnum.GetAllWithNull();

            _eventAggregator.GetEvent<ExceptionEvent>().Subscribe(Error);

            DopSpecificMaskSelectList = DopSpecificMaskSelectHelper.DopSpecificMaskSelectList;

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

            //刷新数据
            if (!string.IsNullOrEmpty(DopSpecificDefaultAccessMask?.Id))
            {
                await RefreshDataAsync(DopSpecificDefaultAccessMask?.Id);
            }

            RefreshRelevance();
        }

        private void RefreshRelevance()
        {
            if (DopSpecificDefaultAccessMask == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(DopSpecificDefaultAccessMask?.ElevatorGroupId)
                && ElevatorGroups?.FirstOrDefault() != null)
            {
                DopSpecificDefaultAccessMask.ElevatorGroup = ElevatorGroups.FirstOrDefault(x => x.Id == DopSpecificDefaultAccessMask.ElevatorGroupId);
            }
            else
            {
                DopSpecificDefaultAccessMask.ElevatorGroup = null;
            }

            if (!string.IsNullOrEmpty(DopSpecificDefaultAccessMask?.HandleElevatorDeviceId)
                && HandleElevatorDevices?.FirstOrDefault() != null)
            {
                DopSpecificDefaultAccessMask.HandleElevatorDevice = HandleElevatorDevices.FirstOrDefault(x => x.Id == DopSpecificDefaultAccessMask.HandleElevatorDeviceId);
            }
            else
            {
                DopSpecificDefaultAccessMask.HandleElevatorDevice = null;
            }
        }

        private async void ElevatorGroupSelectedAsync()
        {
            // 更换电梯组基本上所有楼层都更换了，所以刷新Mask楼层以正确显示已存在的数据
            var model = await _koneApi.GetDopSpecificMaskAsync(DopSpecificDefaultAccessMask.Id);
            if (model != null)
            {
                var newMaskFloors = _mapper.Map<ObservableCollection<DopSpecificDefaultAccessFloorMaskViewModel>>(model.MaskFloors);

                RefreshMaskFloors(newMaskFloors);
            }
            else
            {
                RefreshMaskFloors(new ObservableCollection<DopSpecificDefaultAccessFloorMaskViewModel>());
            }
        }

        private void RefreshMaskFloors(ObservableCollection<DopSpecificDefaultAccessFloorMaskViewModel> newMaskFloors)
        {
            DopSpecificDefaultAccessMask.MaskFloors = new ObservableCollection<DopSpecificDefaultAccessFloorMaskViewModel>();

            foreach (var floor in DopSpecificDefaultAccessMask.ElevatorGroup.ElevatorGroupFloors)
            {
                var maskFloor = newMaskFloors.FirstOrDefault(x => x.FloorId == floor.Id);
                if (maskFloor != null)
                {
                    // 修改maskFloor
                    maskFloor = _mapper.Map(floor, maskFloor);
                }
                else
                {
                    maskFloor = _mapper.Map<DopSpecificDefaultAccessFloorMaskViewModel>(floor);
                }

                DopSpecificDefaultAccessMask.MaskFloors.Add(maskFloor);
            }
        }

        private void Return()
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(DopSpecificMaskListControl));
        }

        private void DestinationFrontAllSelected()
        {
            foreach (var item in DopSpecificDefaultAccessMask.MaskFloors)
            {
                item.IsDestinationFront = IsDestinationFront;
            }
        }

        private void DestinationRearAllSelected()
        {
            foreach (var item in DopSpecificDefaultAccessMask.MaskFloors)
            {
                item.IsDestinationRear = IsDestinationRear;
            }
        }

        private void DopSpecificMaskSelectedAsync(DopSpecificMaskSelectModel dopSpecificMaskSelectModel)
        {
            if (dopSpecificMaskSelectModel == null)
            {
                dopSpecificMaskSelectModel = DopSpecificMaskSelect;
                if (dopSpecificMaskSelectModel == null)
                {
                    return;
                }
            }
            foreach (var item in DopSpecificDefaultAccessMask.MaskFloors)
            {
                var selectedModel = dopSpecificMaskSelectModel.MaskFloors.FirstOrDefault(x => x.Floor.PhysicsFloor == item.Floor.PhysicsFloor);
                if (selectedModel != null)
                {
                    item.IsDestinationFront = selectedModel.IsDestinationFront;
                    item.IsDestinationRear = selectedModel.IsDestinationRear;
                }
            }
        }

        private async void SaveAsync()
        {
            IsSubmiting = true;

            var model = _mapper.Map<DopSpecificDefaultAccessMaskModel>(DopSpecificDefaultAccessMask);
            await _koneApi.SetDopSpecificMaskAsync(model);

            IsSubmiting = false;
        }

        private async void DeleteAsync()
        {
            IsSubmiting = true;

            var model = _mapper.Map<DopSpecificDefaultAccessMaskModel>(DopSpecificDefaultAccessMask);
            await _koneApi.DeleteDopSpecificMaskAsync(model);

            IsSubmiting = false;
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            var id = navigationContext.Parameters["id"] as string;
            await RefreshDataAsync(id);
        }

        private async Task RefreshDataAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var model = await _koneApi.GetDopSpecificMaskAsync(id);
                DopSpecificDefaultAccessMask = _mapper.Map<DopSpecificDefaultAccessMaskViewModel>(model);

                RefreshRelevance();
            }
            else
            {
                DopSpecificDefaultAccessMask = new DopSpecificDefaultAccessMaskViewModel();
                DopSpecificDefaultAccessMask.Id = id;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

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

        public ObservableCollection<HandleElevatorDeviceViewModel> HandleElevatorDevices
        {
            get
            {
                return _handleElevatorDevices;
            }

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

        public bool IsDestinationFront
        {
            get
            {
                return _isDestinationFront;
            }

            set
            {
                SetProperty(ref _isDestinationFront, value);
            }
        }

        public bool IsDestinationRear
        {
            get
            {
                return _isDestinationRear;
            }

            set
            {
                SetProperty(ref _isDestinationRear, value);
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

        public DopSpecificDefaultAccessMaskViewModel DopSpecificDefaultAccessMask
        {
            get => _dopSpecificDefaultAccessMask;
            set
            {
                SetProperty(ref _dopSpecificDefaultAccessMask, value);
            }
        }
        public List<DopSpecificMaskSelectModel> DopSpecificMaskSelectList
        {
            get => _dopSpecificMaskSelectList;
            set
            {
                SetProperty(ref _dopSpecificMaskSelectList, value);
            }
        }

        public DopSpecificMaskSelectModel DopSpecificMaskSelect
        {
            get => _dopSpecificMaskSelect;
            set
            {
                SetProperty(ref _dopSpecificMaskSelect, value);
            }
        }
    }
}
