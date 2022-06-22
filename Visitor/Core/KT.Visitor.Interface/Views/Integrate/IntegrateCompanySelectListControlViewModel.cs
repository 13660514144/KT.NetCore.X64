using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Helper;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Panuon.UI.Silver.Core;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class IntegrateCompanySelectListControlViewModel : PropertyChangedBase
    {
        /// <summary>
        /// 选择公司事件
        /// </summary>
        public Action<CompanyViewModel> SelectedCompanyAction;

        public ICommand SelectedCompanyCommand { get; private set; }
        public CompanyWarnControl CompanyWarnControl { get; private set; }

        private CompanyTreeCheckHelper _companyTreeCheckHelper;

        public IntegrateCompanySelectListControlViewModel(CompanyTreeCheckHelper companyTreeCheckHelper,
            CompanyWarnControl companyWarnControl)
        {
            _companyTreeCheckHelper = companyTreeCheckHelper;
            CompanyWarnControl = companyWarnControl;

            SelectedCompanyCommand = new DelegateCommand(SelectedCompany);
        }

        private void SelectedCompany()
        {
            SelectedCompanyAction?.Invoke(Company);
        }

        /// <summary>
        /// 初始化公司树型数据
        /// </summary>
        public async Task InitTreeCompanysAsync()
        {
            //初始化树形公司选择，默认选择第一家公司
            this.TreeCompanys = await _companyTreeCheckHelper.InitTreeAsync(true);
        }

        //选择的大厦
        private CompanyViewModel _buliding;
        //选择的楼层
        private CompanyViewModel _floor;
        //选择的公司
        private CompanyViewModel _company;
        //公司树形列表数据
        private ObservableCollection<CompanyViewModel> treeCompanys;

        public ObservableCollection<CompanyViewModel> TreeCompanys
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

        public CompanyViewModel Company
        {
            get
            {
                return _company;
            }

            set
            {
                _company = value;
                NotifyPropertyChanged();
            }
        }

        public CompanyViewModel Floor
        {
            get
            {
                return _floor;
            }

            set
            {
                _floor = value;
                NotifyPropertyChanged();
            }
        }

        public CompanyViewModel Buliding
        {
            get
            {
                return _buliding;
            }

            set
            {
                _buliding = value;
                NotifyPropertyChanged();
            }
        }
    }
}
