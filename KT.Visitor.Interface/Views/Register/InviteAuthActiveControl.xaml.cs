using KT.Common.WpfApp.Helpers;
using Prism.Ioc;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Auth
{
    /// <summary>
    /// vistorList.xaml 的交互逻辑
    /// </summary>
    public partial class InviteAuthActiveControl : UserControl
    {
        public InviteAuthActiveControlViewModel ViewModel { get; private set; }

        public InviteAuthActiveControl()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<InviteAuthActiveControlViewModel>();

            DataContext = this.ViewModel;
        }
    }
}
