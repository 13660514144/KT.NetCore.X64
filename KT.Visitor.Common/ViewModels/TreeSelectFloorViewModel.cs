using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Views.Helper;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Visitor.Common.ViewModels
{
    public class TreeSelectFloorViewModel : BindableBase
    {
        private bool _isTreeCompanyInited;
        private CompanyViewModel _selectedFloor;
        private string _selectedFloorName = "请选择来访楼层";

        private ObservableCollection<CompanyViewModel> _treeFloors;

        private CompanyTreeCheckHelper _companyTreeCheckHelper;
        private ICompanyApi _companyApi;

        public TreeSelectFloorViewModel()
        {
            _companyTreeCheckHelper = ContainerHelper.Resolve<CompanyTreeCheckHelper>();
            _companyApi = ContainerHelper.Resolve<ICompanyApi>();
        }

        public async Task InitTreeCheckAsync()
        {
            TreeFloors = await _companyTreeCheckHelper.InitTreeFloorAsync(false, SelectedChanged);
            _isTreeCompanyInited = true;
        }

        public void SelectedChanged(CompanyViewModel company)
        {
            if (company.Type == "floor")
            {
                SelectedFloor = company;
                SelectedFloorName = company.Name;

            }
            else
            {
                SelectedFloor = null;
                SelectedFloorName = "请选择来访楼层";
            }

            //未初始化完成不操作，防止初始化时选中界面卡死
            if (!_isTreeCompanyInited)
            {
                return;
            }
        }

        public ObservableCollection<CompanyViewModel> TreeFloors
        {
            get
            {
                return _treeFloors;
            }

            set
            {
                SetProperty(ref _treeFloors, value);
            }
        }

        public CompanyViewModel SelectedFloor
        {
            get => _selectedFloor;
            set
            {
                SetProperty(ref _selectedFloor, value);
            }
        }

        public string SelectedFloorName
        {
            get => _selectedFloorName;
            set
            {
                SetProperty(ref _selectedFloorName, value);
            }
        }
    }
}
