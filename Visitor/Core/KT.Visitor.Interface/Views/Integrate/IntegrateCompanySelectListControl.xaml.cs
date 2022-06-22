using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Visitor.Controls;
using System;
using System.Collections.Generic;
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

namespace KT.Visitor.Interface.Views.Integrate
{
    /// <summary>
    /// IntegrateCompanySelectListControl.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateCompanySelectListControl : UserControl
    {

        public IntegrateCompanySelectListControlViewModel ViewModel;

        public IntegrateCompanySelectListControl(IntegrateCompanySelectListControlViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

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
