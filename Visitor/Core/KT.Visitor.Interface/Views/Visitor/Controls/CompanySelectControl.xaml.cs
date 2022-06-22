using KT.Common.Core.Utils;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    /// <summary>
    /// CompanySelectControl.xaml 的交互逻辑
    /// </summary>
    public partial class CompanySelectControl : UserControl
    {
        public CompanySelectControlViewModel ViewModel;
        private CompanyApi _companyApi;
        private CompanyTreeCheckHelper _companyTreeCheckHelper;

        public CompanySelectControl(CompanySelectControlViewModel viewModel,
            CompanyApi companyApi,
            CompanyTreeCheckHelper companyTreeCheckHelper)
        {
            InitializeComponent();

            ViewModel = viewModel;
            _companyApi = companyApi;
            _companyTreeCheckHelper = companyTreeCheckHelper;

            //公司控件操作，隐藏所有无关内容，查询出相应数据再打开
            ClearCompany();

            this.DataContext = ViewModel;

            this.Loaded += CompanySelectControl_Loaded;
        }

        private void CompanySelectControl_Loaded(object sender, RoutedEventArgs e)
        {
            //组件事件
            ViewModel.CompanySearchSelectControl.SearchClick += Btn_Search_Click;
            ViewModel.CompanySearchSelectControl.ListCompanyClick += Btn_ListCompany_Click;
            ViewModel.CompanyShowSelectControl.SelectedClick += Btn_SearchSelected_Click;

            //选择公司完成
            ViewModel.CompanySelectListControl.SelectedCompanyAction = (CompanyViewModel company) =>
            {
                ShowCompanyDetial(company);
            };

            //默认展示公司列表
            Btn_ListCompany_Click(sender, null);
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
            ViewModel.CompanySelectListControl.Visibility = Visibility.Collapsed;
            ViewModel.CompanySelectListControl.Visibility = Visibility.Collapsed;
            ViewModel.CompanyWarnControl.Visibility = Visibility.Collapsed;
            ViewModel.CompanyShowDetailControl.Visibility = Visibility.Collapsed;

            //清除选择的公司
            ViewModel.CompanyShowDetailControl.ViewModel.Company = null;
        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            SearchAsync();
        }

        private async Task SearchAsync()
        {
            //清理所有公司
            ClearCompany();

            var searchText = ViewModel.CompanySearchSelectControl.ViewModel.SearchText;
            //为空清理所有公司数据
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return;
            }

            //根据查询条件查询公司列表数据
            var companys = await _companyApi.GetCompaniesAsync(searchText.IsNullTrim());

            //公司数据绑定
            if (companys == null || companys.Count <= 0)
            {
                //查询不到公司操作错误信息
                ViewModel.CompanyWarnControl.Visibility = Visibility.Visible;
            }
            else if (companys.Count == 1)
            {
                //存在一条数据默认选择
                ShowCompanyDetial(new CompanyViewModel(companys.FirstOrDefault()));
            }
            else
            {
                var list = companys.OrderBy(x => x.Unit).ToList();
                //绑定公司列表选择器，非树型
                var vms = _companyTreeCheckHelper.SetTreeChildren(list, null, false);
                //绑定公司列表选择器
                ViewModel.CompanyShowSelectControl.ViewModel.Companies = vms;
                ViewModel.CompanySelectListControl.Visibility = Visibility.Visible;
            }
        }

        private void Btn_SearchSelected_Click(object sender, RoutedEventArgs e)
        {
            var company = ViewModel.CompanyShowSelectControl.ViewModel.Company;
            if (company != null)
            {
                ShowCompanyDetial(company);
            }
        }

        /// <summary>
        /// 显示选择的公司
        /// </summary>
        /// <param name="company"></param>
        public void ShowCompanyDetial(CompanyViewModel company)
        {
            //清理所有公司信息
            ClearCompany();

            //赋值并显示公司
            ViewModel.CompanyShowDetailControl.ViewModel.Company = company;
            ViewModel.CompanyShowDetailControl.Visibility = Visibility;
        }

        /// <summary>
        /// 显示所有公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ListCompany_Click(object sender, RoutedEventArgs e)
        {
            //清理所有公司信息
            ClearCompany();

            InitCompanyListAsync();
        }

        private async Task InitCompanyListAsync()
        {
            //初始化公司列表数据
            ViewModel.CompanySelectListControl.Visibility = Visibility.Visible;
            await ViewModel.CompanySelectListControl.InitTreeCompanysAsync();
        }
    }
}
