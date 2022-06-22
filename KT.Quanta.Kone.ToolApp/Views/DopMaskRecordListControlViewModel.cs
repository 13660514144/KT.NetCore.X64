using AutoMapper;
using KT.Common.Data.Models;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.QuantaApi.Apis;
using KT.Quanta.Kone.ToolApp.Enums;
using KT.Quanta.Kone.ToolApp.Events;
using KT.Quanta.Kone.ToolApp.Models;
using KT.Quanta.Kone.ToolApp.Views.Details;
using KT.Quanta.Service.Models;
using Prism.Events;
using Prism.Regions;
using System;
using System.Windows.Input;

namespace KT.Quanta.Kone.ToolApp.Views
{
    public class DopMaskRecordListControlViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive
        {
            get { return true; }
        }
        private PageQueryDataViewModel<DopMaskRecordViewModel, DopMaskRecordQueryViewModel> _pageData;
        private bool _isSubmiting;

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??= new DelegateCommand(RefreshAsync);

        private ICommand _detailCommand;
        public ICommand DetailCommand => _detailCommand ??= new DelegateCommand<DopMaskRecordViewModel>(DetailAsync);

        private ICommand _pageSelectedCommand;
        public ICommand PageSelectedCommand => _pageSelectedCommand ??= new DelegateCommand<PageDetailViewModel>(PageSelectedAsync);

        private readonly IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDopMaskRecordApi _dopMaskRecordApi;
        private readonly IRegionManager _regionManager;

        public DopMaskRecordListControlViewModel(IMapper mapper,
            IEventAggregator eventAggregator,
            IDopMaskRecordApi dopMaskRecordApi,
            IRegionManager regionManager)
        {
            _mapper = mapper;
            _eventAggregator = eventAggregator;
            _dopMaskRecordApi = dopMaskRecordApi;
            _regionManager = regionManager;

            _eventAggregator.GetEvent<ExceptionEvent>().Subscribe(Error);

            PageData = new PageQueryDataViewModel<DopMaskRecordViewModel, DopMaskRecordQueryViewModel>
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
            var pageQuery = _mapper.Map<PageQuery<DopMaskRecordQuery>>(PageData);
            var pageDataModel = await _dopMaskRecordApi.GetListAsync(pageQuery);
            PageData = _mapper.Map(pageDataModel, PageData);
            PageData.Pages = 200;
            PageData.InitPageDetails();
        }

        private void PageSelectedAsync(PageDetailViewModel pageDetail)
        {
            if (pageDetail != null)
            {
                PageData.Page = pageDetail.Page;
                RefreshAsync();
            }
        }

        private void DetailAsync(DopMaskRecordViewModel dopMaskRecord)
        {
            var parameters = new NavigationParameters();
            parameters.Add("dopMaskRecord", dopMaskRecord);

            if (dopMaskRecord.Type == KoneEliSequenceTypeEnum.DopGlobalDefaultMask.Value)
            {
                _regionManager.RequestNavigate("ContentRegion", nameof(DopGlobalMaskRecordDetailControl), parameters);
            }
            else if (dopMaskRecord.Type == KoneEliSequenceTypeEnum.DopSepcificDefaultMask.Value)
            {
                _regionManager.RequestNavigate("ContentRegion", nameof(DopSpecificMaskRecordDetailControl), parameters);
            }
            else
            {
                throw new Exception($"找不到显示的类型或类型不支持！");
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

        public PageQueryDataViewModel<DopMaskRecordViewModel, DopMaskRecordQueryViewModel> PageData
        {
            get => _pageData;
            set
            {
                SetProperty(ref _pageData, value);
            }
        }
    }
}
