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

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    /// <summary>
    /// CompanySearchSelectControl.xaml 的交互逻辑
    /// </summary>
    public partial class CompanySearchSelectControl : UserControl
    {
        public CompanySearchSelectControlViewModel ViewModel;
        public CompanySearchSelectControl(CompanySearchSelectControlViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
        }
        public RoutedEventHandler SearchClick;
        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            SearchClick?.Invoke(this, e);
        }


        public RoutedEventHandler ListCompanyClick;
        private void Btn_ListCompany_Click(object sender, RoutedEventArgs e)
        {
            ListCompanyClick?.Invoke(this, e);
        }
    }
}
