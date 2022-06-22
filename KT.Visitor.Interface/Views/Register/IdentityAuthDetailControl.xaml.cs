using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Views.Register;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Auth.Controls
{
    /// <summary>
    /// IdentityAuthDetailControl.xaml 的交互逻辑
    /// </summary>
    public partial class IdentityAuthDetailControl : UserControl
    {

        public IdentityAuthDetailControlViewModel ViewModel { get; set; }

        public IdentityAuthDetailControl()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<IdentityAuthDetailControlViewModel>();

            this.DataContext = ViewModel;
        }
    }
}
