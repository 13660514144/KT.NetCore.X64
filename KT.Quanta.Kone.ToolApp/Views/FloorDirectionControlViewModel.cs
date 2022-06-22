using AutoMapper;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.QuantaApi.Apis;
using KT.Quanta.Kone.ToolApp.Events;
using KT.Quanta.Kone.ToolApp.Models;
using KT.Quanta.Service.Models;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace KT.Quanta.Kone.ToolApp.Views
{
    public class FloorDirectionControlViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive
        {
            get { return true; }
        }

        private ObservableCollection<EdificeViewModel> _edifices;
        private EdificeViewModel _edifice;
        private bool _isSubmiting;

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??= new DelegateCommand(RefreshAsync);
        private ICommand _frontAllCheckedCommand;
        public ICommand FrontAllCheckedCommand => _frontAllCheckedCommand ??= new DelegateCommand(FrontAllChecked);
        private ICommand _rearAllCheckedCommand;
        public ICommand RearAllCheckedCommand => _rearAllCheckedCommand ??= new DelegateCommand(RearAllChecked);
        private ICommand _saveCurrentCommand;
        public ICommand SaveCurrentCommand => _saveCurrentCommand ??= new DelegateCommand(SaveCurrentAsync);
        private ICommand _saveAllCommand;
        public ICommand SaveAllCommand => _saveAllCommand ??= new DelegateCommand(SaveAllAsync);

        private readonly IEdificeApi _edificeApi;
        private readonly IFloorApi _floorApi;
        private readonly IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;

        public FloorDirectionControlViewModel(IEdificeApi edificeApi,
            IFloorApi floorApi,
            IMapper mapper,
            IEventAggregator eventAggregator)
        {
            _edificeApi = edificeApi;
            _floorApi = floorApi;
            _mapper = mapper;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<ExceptionEvent>().Subscribe(Error);

            RefreshAsync();
        }

        private void Error(Exception ex)
        {
            IsSubmiting = false;
        }

        private async void RefreshAsync()
        {
            var edificeModels = await _edificeApi.GetAllWithFloorWhereHasRealFloorIdAsync();
            Edifices = _mapper.Map<ObservableCollection<EdificeViewModel>>(edificeModels);

            if (Edifice != null)
            {
                Edifice = Edifices.FirstOrDefault(x => x.Id == Edifice.Id);
            }
        }

        private void FrontAllChecked()
        {
            foreach (var item in Edifice.Floors)
            {
                item.IsFront = Edifice.IsFrontAllChecked;
            }
        }

        private void RearAllChecked()
        {
            foreach (var item in Edifice.Floors)
            {
                item.IsRear = Edifice.IsRearAllChecked;
            }
        }

        private async void SaveCurrentAsync()
        {
            IsSubmiting = true;

            var floors = _mapper.Map<List<FloorModel>>(Edifice.Floors);
            await _floorApi.EditDirectionsAsync(floors);

            //floors = await _floorApi.EditDirectionsAsync(floors);
            //Edifice.Floors = _mapper.Map<ObservableCollection<FloorViewModel>>(floors);

            IsSubmiting = false;
        }

        private async void SaveAllAsync()
        {
            IsSubmiting = true;

            var edifices = _mapper.Map<List<EdificeModel>>(Edifices);
            await _edificeApi.EditDirectionsAsync(edifices);

            //edifices = await _edificeApi.EditDirectionsAsync(edifices);
            //Edifices = _mapper.Map<ObservableCollection<EdificeViewModel>>(edifices);

            IsSubmiting = false;
        }

        public ObservableCollection<EdificeViewModel> Edifices
        {
            get
            {
                return _edifices;
            }

            set
            {
                SetProperty(ref _edifices, value);
            }
        }

        public EdificeViewModel Edifice
        {
            get
            {
                return _edifice;
            }

            set
            {
                SetProperty(ref _edifice, value);
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
    }
}
