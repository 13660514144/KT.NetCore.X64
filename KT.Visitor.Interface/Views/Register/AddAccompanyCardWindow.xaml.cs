using KT.Common.WpfApp.Helpers;
using Panuon.UI.Silver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KT.Visitor.Interface.Views.Register
{
    /// <summary>
    /// AddAccompanyCardWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddAccompanyCardWindow : WindowX
    {
        public AddAccompanyCardWindowViewModel ViewModel { get; set; }

        private bool _isConfirm = false;
        public AddAccompanyCardWindow()
        {
            InitializeComponent();

            var viewModel = ContainerHelper.Resolve<AddAccompanyCardWindowViewModel>();

            SetViewModel(viewModel);

            FocusCardNumber();

            this.Closed += AddAccompanyCardWindow_Closed;
        }

        private void AddAccompanyCardWindow_Closed(object sender, EventArgs e)
        {
            if (!_isConfirm)
            {
                ViewModel.ClearAccompany();
            }
        }

        public void SetViewModel(AddAccompanyCardWindowViewModel viewModel)
        {
            ViewModel = viewModel;
            ViewModel.FocusCardNumber = FocusCardNumber;

            this.DataContext = ViewModel;
        }

        private void FocusCardNumber()
        {
            Tb_CardNumber.Focus();
        }

        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            _isConfirm = true;
            this.Close();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
