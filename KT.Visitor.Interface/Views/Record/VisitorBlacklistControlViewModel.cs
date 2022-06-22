using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Models;
using KT.Visitor.Interface.Views.Common;
using Prism.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Views
{
    public class VisitorBlacklistControlViewModel : BindableBase
    {
        public NavPageControl NavPageControl { get; private set; }

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand<BlacklistModel> RemoveCommand { get; private set; }
        public DelegateCommand<BlacklistModel> EditCommand { get; private set; }

        private BlacklistSearchModel _searchInfo;
        private List<BlacklistModel> _blacklists;
        private BlacklistQueryViewModel _query;

        private IBlacklistApi _blacklistApi;
        private IEventAggregator _eventAggregator;
        private DialogHelper _dialogHelper;

        public VisitorBlacklistControlViewModel()
        {
            _blacklistApi = ContainerHelper.Resolve<IBlacklistApi>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            NavPageControl = ContainerHelper.Resolve<NavPageControl>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();

            AddCommand = new DelegateCommand(AddAsync);
            SearchCommand = new DelegateCommand(SearchAsync);
            RemoveCommand = new DelegateCommand<BlacklistModel>(RemoveBlackAsync);
            EditCommand = new DelegateCommand<BlacklistModel>(EditAsync);

            _searchInfo = new BlacklistSearchModel();
            Query = new BlacklistQueryViewModel();

            NavPageControl.InitDataHandler = LoadDataAsync;
        }

        private async Task SearchAsync()
        {
            await NavPageControl.RefreshDataAsync(1);
        }

        private async Task<BasePageData> LoadDataAsync(int page, int size)
        {
            _searchInfo.Page = page;
            _searchInfo.Size = size;

            _searchInfo.Name = Query.Name;
            _searchInfo.Phone = Query.Phone;
            _searchInfo.IdNumber = Query.IdNumber;

            var pageData = await _blacklistApi.GetBlacksAsync(_searchInfo);
            Blacklists = pageData?.List;
            return pageData;
        }

        private Task AddAsync()
        {
            _eventAggregator.GetEvent<AddBlacklistEvent>().Publish();
            //var addBlacklistPage = ContainerHelper.Resolve<AddBlacklistControl>();
            //await addBlacklistPage.ViewModel.InitAsync();
            //_eventAggregator.Link(addBlacklistPage);

            return Task.CompletedTask;
        }

        private Task EditAsync(BlacklistModel blacklist)
        {
            _eventAggregator.GetEvent<NavLinkEvent>().Publish(NavLinkModel.Create(FontNavEnum.EDIT_BLACKLIST, true, blacklist.Id,blacklist.ServerFaceImg));


            //var addBlacklistPage = ContainerHelper.Resolve<AddBlacklistControl>();
            //await addBlacklistPage.ViewModel.RefreshEditAsync(blacklist.Id);
            //_eventAggregator.Link(addBlacklistPage);

            return Task.CompletedTask;
        }


        private async Task RemoveBlackAsync(BlacklistModel blacklist)
        {
            var result = ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("移出黑名单后该访客可以正常使用登记预约功能进出大厦，是否确认移出？", "温馨提示", "确定", "取消");
            if (result == true)
            {
                await _blacklistApi.DeleteAsync(blacklist.Id);

                //刷新当前页数据
                await NavPageControl.RefreshDataAsync();
            }
        }

        public BlacklistSearchModel SearchModel
        {
            get
            {
                return _searchInfo;
            }

            set
            {
                SetProperty(ref _searchInfo, value);
            }
        }

        public List<BlacklistModel> Blacklists
        {
            get
            {
                return _blacklists;
            }

            set
            {
                SetProperty(ref _blacklists, value);
            }
        }

        public BlacklistQueryViewModel Query
        {
            get
            {
                return _query;
            }

            set
            {
                SetProperty(ref _query, value);
            }
        }
    }
}
