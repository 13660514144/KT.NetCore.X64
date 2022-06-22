using KT.Common.WpfApp.Helpers;
using KT.Visitor.Interface.Views.Register;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Auth
{
    /// <summary>
    /// InviteAuth.xaml 的交互逻辑
    /// </summary>
    public partial class InviteAuthSearchControl : UserControl
    {
        public InviteAuthSearchControlViewModel ViewModel { get; set; }

        public InviteAuthSearchControl()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<InviteAuthSearchControlViewModel>();
            ViewModel.FocusCardNumber = FocusCardNumber;

            this.DataContext = ViewModel;

            FocusCardNumber();

            this.KeyUp += IdentityAuthSearchControl_KeyUp;
        }

        private void IdentityAuthSearchControl_KeyUp(object sender, KeyEventArgs e)
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
