using KT.Visitor.Interface.Views.Integrate;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Auth
{
    /// <summary>
    /// IdentityAuth.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateInviteAuthSearchControl : UserControl
    {
        public IntegrateInviteAuthSearchControlViewModel ViewModel { get; set; }

        public IntegrateInviteAuthSearchControl(IntegrateInviteAuthSearchControlViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            ViewModel.FocusCardNumber = FocusCardNumber;

            this.DataContext = ViewModel;

            FocusCardNumber();
        }

        private void FocusCardNumber()
        {
            Tb_CardNumber.Focus();
        }
    }
}
