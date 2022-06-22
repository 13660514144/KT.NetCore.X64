using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Views.Register;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Auth.Controls
{
    /// <summary>
    /// InviteAuthDetailControl.xaml 的交互逻辑
    /// </summary>
    public partial class InviteAuthDetailControl : UserControl
    {

        public InviteAuthDetailControlViewModel ViewModel { get; set; }

        public InviteAuthDetailControl()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<InviteAuthDetailControlViewModel>();

            this.DataContext = ViewModel;
        }
    }
}
