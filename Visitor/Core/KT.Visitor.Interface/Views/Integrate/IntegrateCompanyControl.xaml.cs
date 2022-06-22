using KT.Common.Core.Utils;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Helper;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Integrate
{
    /// <summary>
    /// IntegrateCompanyControl.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateCompanyControl : UserControl
    {
        public IntegrateCompanyControlViewModel ViewModel;
        private CompanyApi _companyApi;
        private CompanyTreeCheckHelper _companyTreeCheckHelper;

        public IntegrateCompanyControl(IntegrateCompanyControlViewModel viewModel,
            CompanyApi companyApi,
            CompanyTreeCheckHelper companyTreeCheckHelper)
        {
            InitializeComponent();

            ViewModel = viewModel;
            _companyApi = companyApi;
            _companyTreeCheckHelper = companyTreeCheckHelper;

            this.DataContext = ViewModel;

            this.Loaded += CompanySelectControl_Loaded;
        }

        private void CompanySelectControl_Loaded(object sender, RoutedEventArgs e)
        {
            //组件事件  
            ViewModel.CompanySearchSelectControl.ViewModel.SearchCompanyAction += SearchCompany;
            ViewModel.CompanySearchSelectControl.ViewModel.HidenSearchAction += HidenSearch;

            //选择公司完成
            ViewModel.CompanySelectListControl.ViewModel.SelectedCompanyAction = (CompanyViewModel company) =>
            {
                ShowCompanyDetial(company);
            };

            //默认展示公司列表
            Btn_RefreshSelectCompany_Click(sender, null);
        }

        private void HidenSearch()
        {
            ViewModel.CompanySearchSelectControl.Visibility = Visibility.Collapsed;
        }

        private void SearchCompany(string text)
        {
            _ = SearchCompanyAsync(text);
        }
        private async Task SearchCompanyAsync(string text)
        {
            //为空清理所有公司数据
            if (string.IsNullOrWhiteSpace(text))
            {
                await ViewModel.CompanySelectListControl.InitTreeCompanysAsync();
                return;
            }

            //根据查询条件查询公司列表数据
            var companys = await _companyApi.GetCompaniesAsync(text.IsNullTrim());

            //公司数据绑定
            if (companys == null || companys.Count <= 0)
            {
                //查询不到公司操作错误信息
                MessageWarnBox.Show("未查询到该公司，请检查确认", "温馨提示");
                return;
            }
            var list = companys.OrderBy(x => x.Unit).ToList();
            var vms = _companyTreeCheckHelper.SetTreeParentCompany(list, true);
            ViewModel.CompanySelectListControl.ViewModel.TreeCompanys = vms;
            ////绑定公司列表选择器，非树型
            //var vms = _companyTreeCheckHelper.SetTreeChildren(list, null, false);
            ////绑定公司列表选择器
            //ViewModel.CompanySelectListControl.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void ResetClear()
        {
            ClearCompany();

            ViewModel.CompanySearchSelectControl.ViewModel.SearchText = string.Empty;
        }

        /// <summary>
        /// 清理公司选择
        /// </summary>
        private void ClearCompany()
        {
            //公司控件操作，隐藏所有无关内容，查询出相应数据再打开
            ViewModel.CompanySelectListControl.Visibility = Visibility.Visible;
            ViewModel.CompanySearchSelectControl.Visibility = Visibility.Collapsed;
            ViewModel.CompanyShowDetailControl.Visibility = Visibility.Collapsed;

            //清除选择的公司
            ViewModel.CompanyShowDetailControl.ViewModel.Company = null;
        }

        public RoutedEventHandler SelectedCompanyClick;

        /// <summary>
        /// 显示选择的公司
        /// </summary>
        /// <param name="company"></param>
        public void ShowCompanyDetial(CompanyViewModel company)
        {
            //赋值并显示公司
            ViewModel.CompanyShowDetailControl.ViewModel.Company = company;
            if (company.Opening)
            {
                ViewModel.CompanyShowDetailControl.Visibility = Visibility;
            }
            else
            {
                //跳转到下一步
                SelectedCompanyClick?.Invoke(company, null);
            }
        }

        /// <summary>
        /// 显示所有公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_RefreshSelectCompany_Click(object sender, RoutedEventArgs e)
        {
            //清理所有公司信息
            ClearCompany();

            _ = InitCompanyListAsync();
        }

        private async Task InitCompanyListAsync()
        {
            //初始化公司列表数据 
            await ViewModel.CompanySelectListControl.InitTreeCompanysAsync();
        }
    }
}
