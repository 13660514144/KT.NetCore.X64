using KT.Tests.PrismApp.IServices;
using KT.Tests.PrismApp.Views;
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

namespace KT.Tests.PrismApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(WarnWindow warnWindow, IUserService userService)
        {
            InitializeComponent();
            userService.Login("aaa","bbb");
            warnWindow.ShowDialog();
        }
    }
}
