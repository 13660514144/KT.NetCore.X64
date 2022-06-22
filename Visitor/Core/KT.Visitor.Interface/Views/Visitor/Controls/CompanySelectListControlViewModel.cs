using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Helper;
using Panuon.UI.Silver.Core;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    public class CompanySelectListControlViewModel : PropertyChangedBase
    {

        private CompanyTreeCheckHelper _companyTreeCheckHelper;

        public CompanySelectListControlViewModel(CompanyTreeCheckHelper companyTreeCheckHelper)
        {
            _companyTreeCheckHelper = companyTreeCheckHelper;
        }

        /// <summary>
        /// 初始化公司树型数据
        /// </summary>
        public async Task InitTreeCompanysAsync()
        {
            //初始化树形公司选择，默认选择第一家公司
            this.TreeCompanys = await _companyTreeCheckHelper.InitTreeAsync(true);
        }

        //选择的公司
        private CompanyViewModel company;
        //公司列表数据
        private CompanyViewModel floor;
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
                return company;
            }

            set
            {
                company = value;
                NotifyPropertyChanged();
            }
        }

        public CompanyViewModel Floor
        {
            get
            {
                return floor;
            }

            set
            {
                floor = value;
                NotifyPropertyChanged();
            }
        }
    }
}
