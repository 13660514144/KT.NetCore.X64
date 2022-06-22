using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Common.Views.Helper;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Models;
using Prism.Events;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views
{
    public class AddBlacklistControlViewModel : BindableBase
    {
        public ICommand LinkBacklistCommand { get; private set; }

        private BlacklistViewModel _blacklist;
        private bool _isEdit = false;

        private CompanyTreeCheckHelper _companyTreeCheckHelper;
        private IEventAggregator _eventAggregator;
        private IBlacklistApi _blacklistApi;
        private DialogHelper _dialogHelper;

        public AddBlacklistControlViewModel()
        {
            _companyTreeCheckHelper = ContainerHelper.Resolve<CompanyTreeCheckHelper>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _blacklistApi = ContainerHelper.Resolve<IBlacklistApi>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();

            TreeCompany = ContainerHelper.Resolve<TreeCheckCompanyViewModel>();

            LinkBacklistCommand = new DelegateCommand(LinkBacklist);
        }

        private void LinkBacklist()
        {
            //跳转到黑名单页面
            _eventAggregator.GetEvent<NavLinkEvent>().Publish(NavLinkModel.Create(FontNavEnum.BLACKLIST));
        }

        public async Task InitAsync()
        {
            _isEdit = false;
            Blacklist = new BlacklistViewModel();
            await TreeCompany.InitTreeCheckAsync(isContainNullCompanyFloor: false);
        }

        public async Task RefreshEditAsync(long id,string Img="")
        {
            _isEdit = true;
            var model = await _blacklistApi.GetByIdAsync(id);
            
            Blacklist = new BlacklistViewModel();
            Blacklist.SetValue(model);
            //Blacklist.FaceImg = Img;
        
            await TreeCompany.InitTreeCheckAsync(model.BlockAreas, false);
        }

        internal async Task SubmitAsync()
        {
            if (string.IsNullOrEmpty(_blacklist.Name))
            {
                ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("姓名不可以为空!");
                return;
            }

            Blacklist.BlockAreas = await GetSelectdCommpaniesAsync();
            var model = Blacklist.GetValue();
            if (_isEdit)
            {
                await _blacklistApi.EditAsync(model);
            }
            else
            {
                await _blacklistApi.AddAsync(model);
            }

            //跳转到黑名单页面
            _eventAggregator.GetEvent<NavLinkEvent>().Publish(NavLinkModel.Create(FontNavEnum.BLACKLIST));
        }

        /// <summary>
        /// 获取树型选择的公司ID列表
        /// </summary>
        /// <returns></returns>
        internal async Task<List<CompanyModel>> GetSelectdCommpaniesAsync()
        {
            return await Task.Run(() =>
            {
                return _companyTreeCheckHelper.GetSelectdCommpany(TreeCompany.TreeCompanies);
            });
        }

        private TreeCheckCompanyViewModel _treeCompany;

        public TreeCheckCompanyViewModel TreeCompany
        {
            get
            {
                return _treeCompany;
            }

            set
            {
                SetProperty(ref _treeCompany, value);
            }
        }

        public BlacklistViewModel Blacklist
        {
            get
            {
                return _blacklist;
            }

            set
            {
                SetProperty(ref _blacklist, value);
            }
        }
    }
}
