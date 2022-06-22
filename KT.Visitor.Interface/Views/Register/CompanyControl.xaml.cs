using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Common.Views.Helper;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using Prism.Ioc;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Register
{
    /// <summary>
    /// CompanyControl.xaml 的交互逻辑
    /// </summary>
    public partial class CompanyControl : System.Windows.Controls.UserControl
    {
        public CompanyControlViewModel ViewModel;
        private ICompanyApi _companyApi;
        private CompanyTreeCheckHelper _companyTreeCheckHelper;
        private DialogHelper _dialogHelper;

        public CompanyControl()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<CompanyControlViewModel>();
            _companyApi = ContainerHelper.Resolve<ICompanyApi>();
            _companyTreeCheckHelper = ContainerHelper.Resolve<CompanyTreeCheckHelper>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();

            this.DataContext = ViewModel;

            KeyDownHelper.ShowSeachAction = ShowSeachKeyDown;

            this.Loaded += CompanySelectControl_Loaded;
            this.Unloaded += CompanyControl_Unloaded;

            this.IsVisibleChanged += CompanyControl_IsVisibleChanged;
        }

        private void CompanyControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Collapsed)
            {
                KeyDownHelper.ShowSeachAction = null;
            }
            else
            {
                KeyDownHelper.ShowSeachAction = ShowSeachKeyDown;
            }
        }

        private void CompanyControl_Unloaded(object sender, RoutedEventArgs e)
        {
            KeyDownHelper.ShowSeachAction = null;
        }

        public void ShowSeachKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F && (Keyboard.Modifiers & (ModifierKeys.Control)) == (ModifierKeys.Control))
            {
                ViewModel.ShowSearch();
            }
        }

        private void CompanySelectControl_Loaded(object sender, RoutedEventArgs e)
        {
            //组件事件  
            ViewModel.CompanySearchSelectControl.ViewModel.SearchCompanyAction = SearchCompany;
            ViewModel.CompanySearchSelectControl.ViewModel.HidenSearchAction = HidenSearch;

            //默认展示公司列表
            Btn_RefreshSelectCompany_Click(sender, null);

            Btn_Search.Focus();
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
                ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("未查询到该公司，请检查确认", "温馨提示");
                return;
            }
            var list = companys.OrderBy(x => x.Unit).ToList();
            var vms = _companyTreeCheckHelper.SetTreeParentCompany(list, true);
            ViewModel.CompanySelectListControl.ViewModel.TreeCompanys = vms;
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
            //ViewModel.CompanyShowDetailControl.Visibility = Visibility.Collapsed;

            ////清除选择的公司
            //ViewModel.CompanyShowDetailControl.ViewModel.Company = null;
        }

        public RoutedEventHandler SelectedCompanyClick;

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
