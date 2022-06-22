using AutoMapper;
using KT.Common.Core.Utils;
using KT.Common.Data.Models;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.QuantaApi.Apis;
using KT.Quanta.Kone.ToolApp.Events;
using KT.Quanta.Kone.ToolApp.Models;
using KT.Quanta.Kone.ToolApp.Views.Details;
using KT.Quanta.Model.Kone;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace KT.Quanta.Kone.ToolApp.Views
{
    public class PassRightListControlViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive
        {
            get { return true; }
        }
        private PageDataViewModel<PassRightViewModel> _pageData;
        private string _personName;
        private string _cardNumber;
        private bool _isSubmiting;

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??= new DelegateCommand(RefreshAsync);

        private ICommand _setAccessibleFloorCommand;
        public ICommand SetAccessibleFloorCommand => _setAccessibleFloorCommand ??= new DelegateCommand<PassRightViewModel>(SetAccessibleFloorAsync);

        private ICommand _setDestinationFloorCommand;
        public ICommand SetDestinationFloorCommand => _setDestinationFloorCommand ??= new DelegateCommand<PassRightViewModel>(SetDestinationFloorAsync);

        private ICommand _setEliMessageTypeCommand;
        public ICommand SetEliMessageTypeCommand => _setEliMessageTypeCommand ??= new DelegateCommand<PassRightViewModel>(SetEliMessageTypeAsync);

        private ICommand _setEliCallTypeCommand;
        public ICommand SetEliCallTypeCommand => _setEliCallTypeCommand ??= new DelegateCommand<PassRightViewModel>(SetEliCallTypeAsync);

        private ICommand _setRcgifCallTypeCommand;
        public ICommand SetRcgifCallTypeCommand => _setRcgifCallTypeCommand ??= new DelegateCommand<PassRightViewModel>(SetRcgifCallTypeAsync);


        private readonly IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPassRightApi _passRightApi;
        private readonly IRegionManager _regionManager;

        public PassRightListControlViewModel(IMapper mapper,
            IEventAggregator eventAggregator,
            IPassRightApi passRightApi,
            IRegionManager regionManager)
        {
            _mapper = mapper;
            _eventAggregator = eventAggregator;
            _passRightApi = passRightApi;
            _regionManager = regionManager;

            _eventAggregator.GetEvent<ExceptionEvent>().Subscribe(Error);

            PageData = new PageDataViewModel<PassRightViewModel>
            {
                Page = 1,
                Size = 20
            };

            RefreshAsync();
        }

        private void Error(Exception ex)
        {
            IsSubmiting = false;
        }

        private async void RefreshAsync()
        {
            var pageDataModel = await _passRightApi.GetAllPageAsync(PageData.Page, PageData.Size, PersonName, CardNumber);
            PageData = _mapper.Map<PageDataViewModel<PassRightViewModel>>(pageDataModel);
        }

        private void SetAccessibleFloorAsync(PassRightViewModel passRight)
        {
            var parameters = new NavigationParameters();
            parameters.Add("passRight", passRight);

            _regionManager.RequestNavigate("ContentRegion", nameof(PassRightAccessibleFloorControl), parameters);
        }

        private void SetDestinationFloorAsync(PassRightViewModel passRight)
        {
            var parameters = new NavigationParameters();
            parameters.Add("passRight", passRight);

            _regionManager.RequestNavigate("ContentRegion", nameof(PassRightDestinationFloorControl), parameters);
        }

        private void SetEliMessageTypeAsync(PassRightViewModel passRight)
        {
            var parameters = new NavigationParameters();
            parameters.Add("passRight", passRight);

            _regionManager.RequestNavigate("ContentRegion", nameof(EditEliMessageTypeControl), parameters);
        }

        private void SetEliCallTypeAsync(PassRightViewModel passRight)
        {
            var parameters = new NavigationParameters();
            parameters.Add("passRight", passRight);

            _regionManager.RequestNavigate("ContentRegion", nameof(EditEliCallTypeControl), parameters);
        }

        private void SetRcgifCallTypeAsync(PassRightViewModel passRight)
        {
            var parameters = new NavigationParameters();
            parameters.Add("passRight", passRight);

            _regionManager.RequestNavigate("ContentRegion", nameof(EditRcgifCallTypeControl), parameters);
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

        public string PersonName
        {
            get => _personName;
            set
            {
                SetProperty(ref _personName, value);
            }
        }

        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                SetProperty(ref _cardNumber, value);
            }
        }

        public PageDataViewModel<PassRightViewModel> PageData
        {
            get => _pageData;
            set
            {
                SetProperty(ref _pageData, value);
            }
        }
    }
}
