﻿using AutoMapper;
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
    public class EditDopGlobalMaskControlViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        public bool KeepAlive
        {
            get { return false; }
        }

        private ObservableCollection<ElevatorGroupViewModel> _elevatorGroups;
        private List<ConnectedStateEnum> _connectedStates;
        private bool _isDestinationFront;
        private bool _isDestinationRear;
        private bool _isSourceFront;
        private bool _isSourceRear;
        private bool _isSubmiting;
        private DopGlobalDefaultAccessMaskViewModel _dopGlobalDefaultAccessMask;
        private List<DopGlobalMaskSelectModel> _dopGlobalMaskSelectList;
        private DopGlobalMaskSelectModel _dopGlobalMaskSelect;

        private ICommand _returnCommand;
        public ICommand ReturnCommand => _returnCommand ??= new DelegateCommand(Return);
        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??= new DelegateCommand(RefreshAsync);

        private ICommand _elevatorGroupSelectedCommand;
        public ICommand ElevatorGroupSelectedCommand => _elevatorGroupSelectedCommand ??= new DelegateCommand(ElevatorGroupSelectedAsync);

        private ICommand _destinationFrontAllCheckedCommand;
        public ICommand DestinationFrontAllCheckedCommand => _destinationFrontAllCheckedCommand ??= new DelegateCommand(DestinationFrontAllSelected);
        private ICommand _destinationRearAllCheckedCommand;
        public ICommand DestinationRearAllCheckedCommand => _destinationRearAllCheckedCommand ??= new DelegateCommand(DestinationRearAllSelected);
        private ICommand _sourceFrontAllCheckedCommand;
        public ICommand SourceFrontAllCheckedCommand => _sourceFrontAllCheckedCommand ??= new DelegateCommand(SourceFrontAllSelected);
        private ICommand _sourceRearAllCheckedCommand;
        public ICommand SourceRearAllCheckedCommand => _sourceRearAllCheckedCommand ??= new DelegateCommand(SourceRearAllSelected);

        private ICommand _dopGlobalMaskSelectedCommand;
        public ICommand DopGlobalMaskSelectedCommand => _dopGlobalMaskSelectedCommand ??= new DelegateCommand<DopGlobalMaskSelectModel>(DopGlobalMaskSelectedAsync);

        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ??= new DelegateCommand(SaveAsync);
        private ICommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ??= new DelegateCommand(DeleteAsync);

        private readonly IElevatorGroupApi _elevatorGroupApi;
        private readonly IKoneApi _koneApi;
        private readonly IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        public EditDopGlobalMaskControlViewModel(IElevatorGroupApi elevatorGroupApi,
            IKoneApi koneApi,
            IMapper mapper,
            IEventAggregator eventAggregator,
            IRegionManager regionManager)
        {
            _elevatorGroupApi = elevatorGroupApi;
            _koneApi = koneApi;
            _mapper = mapper;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            ConnectedStates = ConnectedStateEnum.GetAllWithNull();

            _eventAggregator.GetEvent<ExceptionEvent>().Subscribe(Error);

            DopGlobalMaskSelectList = DopGlobalMaskSelectHelper.DopGlobalMaskSelectList;

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

            //刷新数据
            if (!string.IsNullOrEmpty(DopGlobalDefaultAccessMask?.Id))
            {
                await RefreshDataAsync(DopGlobalDefaultAccessMask?.Id);
            }

            RefreshRelevance();
        }

        private void RefreshRelevance()
        {
            if (DopGlobalDefaultAccessMask == null)
            {
                return;
            }
            if (!string.IsNullOrEmpty(DopGlobalDefaultAccessMask?.ElevatorGroupId)
                && ElevatorGroups?.FirstOrDefault() != null)
            {
                DopGlobalDefaultAccessMask.ElevatorGroup = ElevatorGroups.FirstOrDefault(x => x.Id == DopGlobalDefaultAccessMask.ElevatorGroupId);
            }
            else
            {
                DopGlobalDefaultAccessMask.ElevatorGroup = null;
            }
        }

        private async void ElevatorGroupSelectedAsync()
        {
            // 更换电梯组基本上所有楼层都更换了，所以刷新Mask楼层以正确显示已存在的数据
            var model = await _koneApi.GetDopGlobalMaskAsync(DopGlobalDefaultAccessMask.Id);
            if (model != null)
            {
                var newMaskFloors = _mapper.Map<ObservableCollection<DopGlobalDefaultAccessFloorMaskViewModel>>(model.MaskFloors);

                RefreshMaskFloors(newMaskFloors);
            }
            else
            {
                RefreshMaskFloors(new ObservableCollection<DopGlobalDefaultAccessFloorMaskViewModel>());
            }
        }

        private void RefreshMaskFloors(ObservableCollection<DopGlobalDefaultAccessFloorMaskViewModel> newMaskFloors)
        {
            DopGlobalDefaultAccessMask.MaskFloors = new ObservableCollection<DopGlobalDefaultAccessFloorMaskViewModel>();

            foreach (var floor in DopGlobalDefaultAccessMask.ElevatorGroup.ElevatorGroupFloors)
            {
                var maskFloor = newMaskFloors.FirstOrDefault(x => x.FloorId == floor.Id);
                if (maskFloor != null)
                {
                    // 修改maskFloor
                    maskFloor = _mapper.Map(floor, maskFloor);
                }
                else
                {
                    maskFloor = _mapper.Map<DopGlobalDefaultAccessFloorMaskViewModel>(floor);
                }

                DopGlobalDefaultAccessMask.MaskFloors.Add(maskFloor);
            }
        }

        private void Return()
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(DopGlobalMaskListControl));
        }

        private void DestinationFrontAllSelected()
        {
            foreach (var item in DopGlobalDefaultAccessMask.MaskFloors)
            {
                item.IsDestinationFront = IsDestinationFront;
            }
        }

        private void DestinationRearAllSelected()
        {
            foreach (var item in DopGlobalDefaultAccessMask.MaskFloors)
            {
                item.IsDestinationRear = IsDestinationRear;
            }
        }

        private void SourceFrontAllSelected()
        {
            foreach (var item in DopGlobalDefaultAccessMask.MaskFloors)
            {
                item.IsSourceFront = IsSourceFront;
            }
        }
        private void SourceRearAllSelected()
        {
            foreach (var item in DopGlobalDefaultAccessMask.MaskFloors)
            {
                item.IsSourceRear = IsSourceRear;
            }
        }

        private void DopGlobalMaskSelectedAsync(DopGlobalMaskSelectModel dopGlobalMaskSelectModel)
        {
            if (dopGlobalMaskSelectModel == null)
            {
                dopGlobalMaskSelectModel = DopGlobalMaskSelect;
                if (dopGlobalMaskSelectModel == null)
                {
                    return;
                }
            }
            foreach (var item in DopGlobalDefaultAccessMask.MaskFloors)
            {
                var selectedModel = dopGlobalMaskSelectModel.MaskFloors.FirstOrDefault(x => x.Floor.PhysicsFloor == item.Floor.PhysicsFloor);
                if (selectedModel != null)
                {
                    item.IsSourceFront = selectedModel.IsSourceFront;
                    item.IsSourceRear = selectedModel.IsSourceRear;
                    item.IsDestinationFront = selectedModel.IsDestinationFront;
                    item.IsDestinationRear = selectedModel.IsDestinationRear;
                }
            }
        }

        private async void SaveAsync()
        {
            IsSubmiting = true;

            var model = _mapper.Map<DopGlobalDefaultAccessMaskModel>(DopGlobalDefaultAccessMask);
            await _koneApi.SetDopGlobalMaskAsync(model);

            IsSubmiting = false;
        }

        private async void DeleteAsync()
        {
            IsSubmiting = true;

            var model = _mapper.Map<DopGlobalDefaultAccessMaskModel>(DopGlobalDefaultAccessMask);
            await _koneApi.DeleteDopGlobalMaskAsync(model);

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
                var model = await _koneApi.GetDopGlobalMaskAsync(id);
                DopGlobalDefaultAccessMask = _mapper.Map<DopGlobalDefaultAccessMaskViewModel>(model);

                RefreshRelevance();
            }
            else
            {
                DopGlobalDefaultAccessMask = new DopGlobalDefaultAccessMaskViewModel();
                DopGlobalDefaultAccessMask.Id = id;
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

        public bool IsSourceFront
        {
            get
            {
                return _isSourceFront;
            }

            set
            {
                SetProperty(ref _isSourceFront, value);
            }
        }

        public bool IsSourceRear
        {
            get
            {
                return _isSourceRear;
            }

            set
            {
                SetProperty(ref _isSourceRear, value);
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

        public DopGlobalDefaultAccessMaskViewModel DopGlobalDefaultAccessMask
        {
            get => _dopGlobalDefaultAccessMask;
            set
            {
                SetProperty(ref _dopGlobalDefaultAccessMask, value);
            }
        }

        public List<DopGlobalMaskSelectModel> DopGlobalMaskSelectList
        {
            get => _dopGlobalMaskSelectList;
            set
            {
                SetProperty(ref _dopGlobalMaskSelectList, value);
            }
        }

        public DopGlobalMaskSelectModel DopGlobalMaskSelect
        {
            get => _dopGlobalMaskSelect;
            set
            {
                SetProperty(ref _dopGlobalMaskSelect, value);
            }
        }
    }
}