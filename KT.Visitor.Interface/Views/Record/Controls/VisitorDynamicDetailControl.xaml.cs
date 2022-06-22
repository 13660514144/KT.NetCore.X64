using KT.Common.WpfApp.Helpers;
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

namespace KT.Visitor.Interface.Views.Controls
{
    /// <summary>
    /// VisitorDynamicDetailControl.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorDynamicDetailControl : UserControl
    { 
        public VisitorDynamicDetailControl(string time, string msg)
        {
            InitializeComponent();
             
            tb_activeTime.Text = time == null ? "" : time;
            tb_activeMsg.Text = msg == null ? "" : msg;
        }
        public string ActiveTime { get; set; }
        public string ActiveMsg { get; set; }


    }
}
