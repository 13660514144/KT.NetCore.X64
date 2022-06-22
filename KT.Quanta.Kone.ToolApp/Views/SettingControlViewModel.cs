using AutoMapper;
using KT.Common.Core.Enums;
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
using System.Linq;
using System.Windows.Input;

namespace KT.Quanta.Kone.ToolApp.Views
{
    public class SettingControlViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive
        {
            get { return true; }
        }

        private ObservableCollection<ElevatorGroupViewModel> _elevatorGroups;
        private ElevatorGroupViewModel _elevatorGroup;
        private List<OpenAccessForDopMessageTypeEnum> _openAccessForDopMessageTypes;
        private List<StandardCallTypeEnum> _standardCallTypes;
        private IEnumerable<AcsBypassCallTypeEnum> _acsBypassCallTypes;
        private KoneSystemConfigViewModel _koneSystemConfig;
        private bool _isSubmiting;

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??= new DelegateCommand(RefreshAsync);

        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ??= new DelegateCommand(SaveAsync);

        private readonly IElevatorGroupApi _elevatorGroupApi;
        private readonly IKoneApi _koneApi;
        private readonly IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;

        public SettingControlViewModel(IElevatorGroupApi elevatorGroupApi,
            IKoneApi koneApi,
            IMapper mapper,
            IEventAggregator eventAggregator)
        {
            _elevatorGroupApi = elevatorGroupApi;
            _koneApi = koneApi;
            _mapper = mapper;
            _eventAggregator = eventAggregator;

            OpenAccessForDopMessageTypes = BaseEnum.GetAll<OpenAccessForDopMessageTypeEnum>().ToList();
            StandardCallTypes = BaseEnum.GetAll<StandardCallTypeEnum>().ToList();
            AcsBypassCallTypes = BaseEnum.GetAll<AcsBypassCallTypeEnum>();

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

            var model = await _koneApi.GetSystemConfigAsync();
            KoneSystemConfig = _mapper.Map<KoneSystemConfigViewModel>(model);
        }

        private async void SaveAsync()
        {
            IsSubmiting = true;

            var model = _mapper.Map<KoneSystemConfigModel>(KoneSystemConfig);
            await _koneApi.SetSystemConfigAsync(model);

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

        public List<OpenAccessForDopMessageTypeEnum> OpenAccessForDopMessageTypes
        {
            get
            {
                return _openAccessForDopMessageTypes;
            }

            set
            {
                SetProperty(ref _openAccessForDopMessageTypes, value);
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

        public List<StandardCallTypeEnum> StandardCallTypes
        {
            get => _standardCallTypes;
            set
            {
                SetProperty(ref _standardCallTypes, value);
            }
        }
        public IEnumerable<AcsBypassCallTypeEnum> AcsBypassCallTypes
        {
            get => _acsBypassCallTypes;
            set
            {
                SetProperty(ref _acsBypassCallTypes, value);
            }
        }

        public KoneSystemConfigViewModel KoneSystemConfig
        {
            get => _koneSystemConfig;
            set
            {
                SetProperty(ref _koneSystemConfig, value);
            }
        }
    }
}
