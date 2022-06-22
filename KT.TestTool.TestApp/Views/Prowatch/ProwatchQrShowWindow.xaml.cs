using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KT.TestTool.TestApp.Views.Prowatch
{
    /// <summary>
    /// ProwatchQrShowWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProwatchQrShowWindow : Window
    {
        public ProwatchQrShowWindowViewModel ViewModel { get; set; }

        public ProwatchQrShowWindow(ProwatchQrShowWindowViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel; 

            this.DataContext = ViewModel;
        }

        
    }
}
