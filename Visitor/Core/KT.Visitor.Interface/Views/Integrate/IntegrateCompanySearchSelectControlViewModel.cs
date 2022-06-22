using KT.Common.Core.Utils;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Helper;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Panuon.UI.Silver.Core;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class IntegrateCompanySearchSelectControlViewModel : PropertyChangedBase
    {
        public ICommand SearchCommand { get; private set; }
        public ICommand HidenSearchCommand { get; private set; }

        private string _searchText;

        private IContainerProvider _containerProvider;
        private CompanyApi _companyApi;
        private CompanyTreeCheckHelper _companyTreeCheckHelper;

        public IntegrateCompanySearchSelectControlViewModel(IContainerProvider containerProvider,
            CompanyApi companyApi,
            CompanyTreeCheckHelper companyTreeCheckHelper)
        {
            _containerProvider = containerProvider;
            _companyApi = companyApi;
            _companyTreeCheckHelper = companyTreeCheckHelper;

            SearchCommand = new DelegateCommand(Search);
            HidenSearchCommand = new DelegateCommand(HidenSearch);
        }

        public Action HidenSearchAction;
        private void HidenSearch()
        {
            HidenSearchAction?.Invoke();
        }

        public Action<string> SearchCompanyAction;
        private void Search()
        {
            SearchCompanyAction?.Invoke(SearchText);
        }

        public string SearchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                _searchText = value;
                NotifyPropertyChanged();
            }
        }
    }
}
