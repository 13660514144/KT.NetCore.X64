using KT.Proxy.WebApi.Backend.Apis;
using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Helper;
using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Views.Blacklist
{
    public class AddBlacklistPageViewModel : PropertyChangedBase
    {
        private BlacklistViewModel _blacklist;
        private bool _isEdit = false;

        private CompanyTreeCheckHelper _companyTreeCheckHelper;
        private MainFrameHelper _mainFrameHelper;
        private BlacklistApi _blacklistApi;

        public AddBlacklistPageViewModel(CompanyTreeCheckHelper companyTreeCheckHelper,
            MainFrameHelper mainFrameHelper,
            BlacklistApi blacklistApi)
        {
            _companyTreeCheckHelper = companyTreeCheckHelper;
            _mainFrameHelper = mainFrameHelper;
            _blacklistApi = blacklistApi;

            _ = InitAsync();
        }

        public async Task InitAsync()
        {
            Blacklist = new BlacklistViewModel();
            this.TreeCompanies = await _companyTreeCheckHelper.InitTreeAsync(false);
        }

        public void RefreshEdit(BlacklistModel model)
        {
            _isEdit = true;
            Blacklist = new BlacklistViewModel();
            Blacklist.SetValue(model);
        }

        internal async Task SubmitAsync()
        {
            if (string.IsNullOrEmpty(_blacklist.Name))
            {
                MessageWarnBox.Show("姓名不可以为空!");
                return;
            }

            Blacklist.Areas = await GetSelectdCommpanyIdsAsync();
            var model = Blacklist.GetValue();
            if (_isEdit)
            {
                await _blacklistApi.EditAsync(model);
            }
            else
            {
                await _blacklistApi.AddAsync(model);
            }

            _mainFrameHelper.Link<VisitorBlacklist>();
        }

        /// <summary>
        /// 获取树型选择的公司ID列表
        /// </summary>
        /// <returns></returns>
        internal async Task<List<CompanyModel>> GetSelectdCommpanyIdsAsync()
        {
            return await Task.Run(() =>
            {
                return _companyTreeCheckHelper.GetSelectdCommpany(this.TreeCompanies);
            });
        }

        private ObservableCollection<CompanyViewModel> treeCompanys;

        public ObservableCollection<CompanyViewModel> TreeCompanies
        {
            get
            {
                return treeCompanys;
            }

            set
            {
                treeCompanys = value;
                NotifyPropertyChanged();
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
                _blacklist = value;
                NotifyPropertyChanged();
            }
        }
    }
}
