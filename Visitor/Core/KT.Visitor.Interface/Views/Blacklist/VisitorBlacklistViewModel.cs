using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Views.Common;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;

namespace KT.Visitor.Interface.Views.Blacklist
{
    public class VisitorBlacklistViewModel : BindableBase
    {
        public NavPageControl NavPageControl { get; private set; }

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand<BlacklistModel> RemoveCommand { get; private set; }
        public DelegateCommand<BlacklistModel> EditCommand { get; private set; }

        private BlacklistSearchModel _searchInfo;
        private List<BlacklistModel> _blacklists;

        private BlacklistApi _blacklistApi;
        private MainFrameHelper _mainFrameHelper;
        private IContainerProvider _containerProvider;

        public VisitorBlacklistViewModel(BlacklistApi blacklistApi,
            MainFrameHelper mainFrameHelper,
            IContainerProvider containerProvider,
            NavPageControl navPageControl)
        {
            _blacklistApi = blacklistApi;
            _mainFrameHelper = mainFrameHelper;
            _containerProvider = containerProvider;
            NavPageControl = navPageControl;

            AddCommand = new DelegateCommand(Add);
            SearchCommand = new DelegateCommand(Search);
            RemoveCommand = new DelegateCommand<BlacklistModel>(Remove);
            EditCommand = new DelegateCommand<BlacklistModel>(Edit);

            _searchInfo = new BlacklistSearchModel();

            NavPageControl.InitDataHandler = LoadDataAsync;
        }

        private void Search()
        {
            NavPageControl.RefreshData(1);
        }

        private async Task<BasePageData> LoadDataAsync(int page, int size)
        {
            _searchInfo.Page = page;
            _searchInfo.Size = size;

            var pageData = await _blacklistApi.GetBlacksAsync(_searchInfo);
            Blacklists = pageData?.List;
            return pageData;
        }

        private void Add()
        {
            var addBlacklistPage = _containerProvider.Resolve<AddBlacklistPage>();
            _mainFrameHelper.Link(addBlacklistPage);
        }

        private void Edit(BlacklistModel blacklist)
        {
            var addBlacklistPage = _containerProvider.Resolve<AddBlacklistPage>();
            addBlacklistPage.ViewModel.RefreshEdit(blacklist);
            _mainFrameHelper.Link(addBlacklistPage);
        }

        private void Remove(BlacklistModel blacklist)
        {
            _ = RemoveBlackAsync(blacklist);
        }

        private async Task RemoveBlackAsync(BlacklistModel blacklist)
        {
            var result = MessageWarnBox.Show("移出黑名单后该访客可以正常使用登记预约功能进出大厦，是否确认移出？", "温馨提示");
            if (result == true)
            {
                await _blacklistApi.DeleteAsync(blacklist.Id);

                //刷新当前页数据
                NavPageControl.RefreshData();
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
    }
}
