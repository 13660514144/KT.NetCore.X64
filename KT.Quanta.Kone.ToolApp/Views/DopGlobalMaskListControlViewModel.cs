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
    public class DopGlobalMaskListControlViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive
        {
            get { return true; }
        }

        private List<DopGlobalDefaultAccessMaskViewModel> _dopGlobalDefaultAccessMasks;

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??= new DelegateCommand(RefreshAsync);

        private ICommand _addCommand;
        public ICommand AddCommand => _addCommand ??= new DelegateCommand(AddAsync);

        private ICommand _operationCommand;
        public ICommand OperationCommand => _operationCommand ??= new DelegateCommand<DopGlobalDefaultAccessMaskViewModel>(OperationAsync);

        private readonly IKoneApi _koneApi;
        private readonly IRegionManager _regionManager;
        private readonly IMapper _mapper;

        public DopGlobalMaskListControlViewModel(IKoneApi koneApi,
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
            var models = await _koneApi.GetAllDopGlobalMasksWithElevatorGroupAsync();
            models = models?.OrderBy(x => x.ConnectedState).OrderBy(x => x.ElevatorGroup.Name).ToList();
            DopGlobalDefaultAccessMasks = _mapper.Map<List<DopGlobalDefaultAccessMaskViewModel>>(models);
        }

        private void AddAsync()
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(EditDopGlobalMaskControl));
        }

        private void OperationAsync(DopGlobalDefaultAccessMaskViewModel model)
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", model?.Id);

            _regionManager.RequestNavigate("ContentRegion", nameof(EditDopGlobalMaskControl), parameters);
        }

        public List<DopGlobalDefaultAccessMaskViewModel> DopGlobalDefaultAccessMasks
        {
            get => _dopGlobalDefaultAccessMasks;
            set
            {
                SetProperty(ref _dopGlobalDefaultAccessMasks, value);
            }
        }
    }
}
