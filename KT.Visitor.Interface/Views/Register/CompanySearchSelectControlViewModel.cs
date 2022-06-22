using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Views.Helper;
using Panuon.UI.Silver.Core;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Register
{
    public class CompanySearchSelectControlViewModel : PropertyChangedBase
    {
        public ICommand SearchCommand { get; private set; }
        public ICommand HidenSearchCommand { get; private set; }

        private string _searchText;

        private ICompanyApi _companyApi;
        private CompanyTreeCheckHelper _companyTreeCheckHelper;

        public CompanySearchSelectControlViewModel()
        {
            _companyApi = ContainerHelper.Resolve<ICompanyApi>();
            _companyTreeCheckHelper = ContainerHelper.Resolve<CompanyTreeCheckHelper>();

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
