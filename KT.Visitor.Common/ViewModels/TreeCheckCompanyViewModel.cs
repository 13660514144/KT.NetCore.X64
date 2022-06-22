using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Views.Helper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Visitor.Common.ViewModels
{
    public class TreeCheckCompanyViewModel : BindableBase
    {
        private bool _isTreeCompanyInited;
        private List<CompanyViewModel> _selectedCompanies;

        private string _selectedNames;
        private ObservableCollection<CompanyViewModel> _treeCompanies;

        private CompanyTreeCheckHelper _companyTreeCheckHelper;
        private ICompanyApi _companyApi;

        private string _defalutValue;

        public TreeCheckCompanyViewModel()
        {
            _companyTreeCheckHelper = ContainerHelper.Resolve<CompanyTreeCheckHelper>();
            _companyApi = ContainerHelper.Resolve<ICompanyApi>();

            _selectedCompanies = new List<CompanyViewModel>();
        }

        public async Task InitTreeCheckAsync(string defalutValue = "全区禁访",
            bool isContainNullCompanyFloor = true)
        {
            _defalutValue = defalutValue;
            _selectedCompanies = new List<CompanyViewModel>();

            TreeCompanies = await _companyTreeCheckHelper.InitTreeAsync(null, CheckedChanged, isContainNullCompanyFloor);
            _isTreeCompanyInited = true;

            RefreshSelectedNames();
        }

        public async Task InitTreeCheckAsync(List<CompanyModel> selectedCompanies,
            bool isContainNullCompanyFloor = true)
        {
            _selectedCompanies = new List<CompanyViewModel>();
            TreeCompanies = await _companyTreeCheckHelper.InitTreeAsync(selectedCompanies, CheckedChanged, isContainNullCompanyFloor);
            _isTreeCompanyInited = true;

            RefreshSelectedNames();
        }

        public void CheckedChanged(CompanyViewModel company)
        {
            if (company.Type != "company")
            {
                return;
            }

            if (company.IsChecked)
            {
                _selectedCompanies.Add(company);
            }
            else
            {
                _selectedCompanies.Remove(company);
            }

            //未初始化完成不操作，防止初始化时选中界面卡死
            if (!_isTreeCompanyInited)
            {
                return;
            }

            RefreshSelectedNames();
        }


        private void RefreshSelectedNames()
        {
            if (_selectedCompanies != null && _selectedCompanies.FirstOrDefault() != null)
            {
                SelectedNames = _selectedCompanies.Select(x => (x.Name + " " + x.FloorName)).ToList().RightDecorates("，");
            }
            else
            {
                SelectedNames = _defalutValue;
            }
        }

        public string SelectedNames
        {
            get
            {
                return _selectedNames;
            }

            set
            {
                SetProperty(ref _selectedNames, value);
            }
        }

        public ObservableCollection<CompanyViewModel> TreeCompanies
        {
            get
            {
                return _treeCompanies;
            }

            set
            {
                SetProperty(ref _treeCompanies, value);
            }
        }
    }
}
