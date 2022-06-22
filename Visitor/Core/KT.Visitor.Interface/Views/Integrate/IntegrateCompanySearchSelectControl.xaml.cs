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
    /// IntegrateCompanySearchSelectControl.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateCompanySearchSelectControl : UserControl
    {
        public IntegrateCompanySearchSelectControlViewModel ViewModel;
        public IntegrateCompanySearchSelectControl(IntegrateCompanySearchSelectControlViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            this.DataContext = ViewModel;
        } 
    }
}
