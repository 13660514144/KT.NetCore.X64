using Prism.Ioc;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Auth
{
    /// <summary>
    /// vistorList.xaml 的交互逻辑
    /// </summary>
    public partial class IdentityAuthActiveControl : UserControl
    {
        public IdentityAuthActiveControlViewModel ViewModel { get; private set; }

        public IdentityAuthActiveControl(IContainerProvider containerProvider)
        {
            InitializeComponent();

            ViewModel = containerProvider.Resolve<IdentityAuthActiveControlViewModel>();

            DataContext = this.ViewModel;
        }


    }
}
