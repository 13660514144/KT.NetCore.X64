using KT.Common.WpfApp.Helpers;
using KT.Visitor.Interface.Views.Register;
using Prism.Regions;
using System.Windows.Controls;
using System.Windows.Forms;

namespace KT.Visitor.Interface.Views.Register
{
    /// <summary>
    /// IdentityAuth.xaml 的交互逻辑
    /// </summary>
    public partial class IdentityAuthSearchControl : System.Windows.Controls.UserControl
    {
        public IdentityAuthSearchControlViewModel ViewModel { get; set; }

        public IdentityAuthSearchControl()
        {
            InitializeComponent();


            ViewModel = ContainerHelper.Resolve<IdentityAuthSearchControlViewModel>();
            ViewModel.FocusCardNumber = FocusCardNumber;

            this.DataContext = ViewModel;

            FocusCardNumber();

            this.KeyUp += IdentityAuthSearchControl_KeyUp;

            //this.Loaded += IdentityAuthSearchControl_Loaded;
        }

        //private void IdentityAuthSearchControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    var viewModel = (IdentityAuthSearchControlViewModel)this.DataContext;
        //    if (viewModel != null)
        //    {
        //        viewModel.FocusCardNumber = FocusCardNumber;
        //    }
        //}

        private void IdentityAuthSearchControl_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                ViewModel.EnterInput();
            }
        }

        private void FocusCardNumber()
        {
            Tb_CardNumber.Focus();
        }
    }
}
