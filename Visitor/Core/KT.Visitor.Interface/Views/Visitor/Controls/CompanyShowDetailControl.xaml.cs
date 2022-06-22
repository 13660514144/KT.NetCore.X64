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
    /// CompanyShowDetailControl.xaml 的交互逻辑
    /// </summary>
    public partial class CompanyShowDetailControl : UserControl
    {
        public CompanyShowDetailControlViewModel ViewModel;

        public CompanyShowDetailControl()
        {
            InitializeComponent();

            this.ViewModel = new CompanyShowDetailControlViewModel();
            this.DataContext = ViewModel;
        }
    }
}
