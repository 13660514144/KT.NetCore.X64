using KT.Common.WpfApp.Helpers;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Register
{
    /// <summary>
    /// CompanySelectListControl.xaml 的交互逻辑
    /// </summary>
    public partial class CompanySelectListControl : UserControl
    {

        public CompanySelectListControlViewModel ViewModel;

        public CompanySelectListControl()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<CompanySelectListControlViewModel>();

            this.DataContext = ViewModel;
        }

        /// <summary>
        /// 初始化公司树型选择器
        /// </summary>
        public async Task InitTreeCompanysAsync()
        {
            await this.ViewModel.InitTreeCompanysAsync();
        }
    }
}
