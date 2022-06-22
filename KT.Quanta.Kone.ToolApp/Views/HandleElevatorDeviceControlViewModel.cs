using AutoMapper;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.QuantaApi.Apis;
using KT.Quanta.Common.Enums;
using KT.Quanta.Kone.ToolApp.Events;
using KT.Quanta.Kone.ToolApp.Models;
using KT.Quanta.Model.Elevator.Dtos;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace KT.Quanta.Kone.ToolApp.Views
{
    public class HandleElevatorDeviceControlViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive
        {
            get { return false; }
        }

        private ObservableCollection<HandleElevatorDeviceViewModel> _handleElevatorDevices;
        private bool _isFrontAllChecked;
        private bool _isRearAllChecked;
        private bool _isSubmiting;

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??= new DelegateCommand(RefreshAsync);

        private ICommand _frontAllCheckedCommand;
        public ICommand FrontAllCheckedCommand => _frontAllCheckedCommand ??= new DelegateCommand(FrontAllChecked);
        private ICommand _rearAllCheckedCommand;
        public ICommand RearAllCheckedCommand => _rearAllCheckedCommand ??= new DelegateCommand(RearAllChecked);

        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ??= new DelegateCommand(SaveAsync);

        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IHandleElevatorDeviceAuxiliaryApi _handleElevatorDeviceAuxiliaryApi;
        private readonly IHandleElevatorDeviceApi _handleElevatorDeviceApi;
        private readonly IMapper _mapper;

        public HandleElevatorDeviceControlViewModel(IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IHandleElevatorDeviceAuxiliaryApi handleElevatorDeviceAuxiliaryApi,
            IHandleElevatorDeviceApi handleElevatorDeviceApi,
            IMapper mapper)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _handleElevatorDeviceAuxiliaryApi = handleElevatorDeviceAuxiliaryApi;
            _handleElevatorDeviceApi = handleElevatorDeviceApi;
            _mapper = mapper;

            _eventAggregator.GetEvent<ExceptionEvent>().Subscribe(Error);

            RefreshAsync();
        }

        private void Error(Exception ex)
        {
            IsSubmiting = false;
        }

        private async void RefreshAsync()
        {
            var handleElevatorDevices = await _handleElevatorDeviceApi.GetAllAsync();
            handleElevatorDevices = handleElevatorDevices?.OrderBy(x => x.Name).ToList();
            HandleElevatorDevices = _mapper.Map<ObservableCollection<HandleElevatorDeviceViewModel>>(handleElevatorDevices);

            var handleElevatorDeviceAuxiliaries = await _handleElevatorDeviceAuxiliaryApi.GetAllAsync();

            if (HandleElevatorDevices?.FirstOrDefault() != null)
            {
                foreach (var item in HandleElevatorDevices)
                {
                    var handleElevatorDeviceAuxiliary = handleElevatorDeviceAuxiliaries?.FirstOrDefault(x => x.HandleElevatorDeviceId == item.Id);
                    if (handleElevatorDeviceAuxiliary == null)
                    {
                        item.IsFront = true;
                    }
                    else
                    {
                        item.IsFront = handleElevatorDeviceAuxiliary.IsFront;
                        item.IsRear = handleElevatorDeviceAuxiliary.IsRear;
                    }
                }
            }
        }


        private void FrontAllChecked()
        {
            foreach (var item in HandleElevatorDevices)
            {
                item.IsFront = IsFrontAllChecked;
            }
        }

        private void RearAllChecked()
        {
            foreach (var item in HandleElevatorDevices)
            {
                item.IsRear = IsRearAllChecked;
            }
        }
        private async void SaveAsync()
        {
            IsSubmiting = true;

            var floorDirections = new List<HandleElevatorDeviceAuxiliaryModel>();
            foreach (var item in HandleElevatorDevices)
            {
                var floorDirection = new HandleElevatorDeviceAuxiliaryModel();
                floorDirection.HandleElevatorDeviceId = item.Id;
                floorDirection.IsFront = item.IsFront;
                floorDirection.IsRear = item.IsRear;

                floorDirections.Add(floorDirection);
            }
            await _handleElevatorDeviceAuxiliaryApi.AddOrEditsAsync(floorDirections);

            IsSubmiting = false;
        }
         

        public bool IsFrontAllChecked
        {
            get
            {
                return _isFrontAllChecked;
            }

            set
            {
                SetProperty(ref _isFrontAllChecked, value);
            }
        }

        public bool IsRearAllChecked
        {
            get
            {
                return _isRearAllChecked;
            }

            set
            {
                SetProperty(ref _isRearAllChecked, value);
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
    }
}
