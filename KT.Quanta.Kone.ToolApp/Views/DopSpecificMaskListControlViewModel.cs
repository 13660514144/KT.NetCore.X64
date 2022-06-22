using AutoMapper;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.QuantaApi.Apis;
using KT.Quanta.Kone.ToolApp.Models;
using KT.Quanta.Kone.ToolApp.Views.Details;
using KT.Quanta.Model.Kone;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace KT.Quanta.Kone.ToolApp.Views
{
    public class DopSpecificMaskListControlViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive
        {
            get { return true; }
        }

        private List<DopSpecificDefaultAccessMaskViewModel> _dopSpecificDefaultAccessMasks;

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??= new DelegateCommand(RefreshAsync);

        private ICommand _addCommand;
        public ICommand AddCommand => _addCommand ??= new DelegateCommand(AddAsync);

        private ICommand _operationCommand;
        public ICommand OperationCommand => _operationCommand ??= new DelegateCommand<DopSpecificDefaultAccessMaskViewModel>(OperationAsync);

        private readonly IKoneApi _koneApi;
        private readonly IRegionManager _regionManager;
        private readonly IMapper _mapper;

        public DopSpecificMaskListControlViewModel(IKoneApi koneApi,
            IRegionManager regionManager,
            IMapper mapper)
        {
            _koneApi = koneApi;
            _regionManager = regionManager;
            _mapper = mapper;

            RefreshAsync();
        }

        private async void RefreshAsync()
        {
            var models = await _koneApi.GetAllDopSpecificMasksWithElevatorGroupAndHandleElevatorDeviceAsync();
            models = models?.OrderBy(x => x.ConnectedState).OrderBy(x => x.HandleElevatorDevice.Name).OrderBy(x => x.ElevatorGroup.Name).ToList();
            DopSpecificDefaultAccessMasks = _mapper.Map<List<DopSpecificDefaultAccessMaskViewModel>>(models);
        }

        private void AddAsync()
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(EditDopSpecificMaskControl));
        }
        private void OperationAsync(DopSpecificDefaultAccessMaskViewModel model)
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", model?.Id);

            _regionManager.RequestNavigate("ContentRegion", nameof(EditDopSpecificMaskControl), parameters);
        }

        public List<DopSpecificDefaultAccessMaskViewModel> DopSpecificDefaultAccessMasks
        {
            get => _dopSpecificDefaultAccessMasks;
            set
            {
                SetProperty(ref _dopSpecificDefaultAccessMasks, value);
            }
        }
    }
}
