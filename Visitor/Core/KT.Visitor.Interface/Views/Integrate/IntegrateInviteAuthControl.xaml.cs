using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Views.Integrate;
using Prism.Ioc;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Auth
{
    /// <summary>
    /// IdentityAuth.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateInviteAuthControl : UserControl
    {
        public IntegrateInviteAuthControlViewModel ViewModel { get; set; }

        public IntegrateInviteAuthControl(IntegrateInviteAuthControlViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            this.DataContext = ViewModel;
        }
    }
}
