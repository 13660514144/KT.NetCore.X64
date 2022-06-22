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

namespace KT.Visitor.Interface.Views.Integrate
{
    /// <summary>
    /// AddAccompanyCardWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddAccompanyCardWindow : WindowX
    {
        public AddAccompanyCardWindowViewModel ViewModel { get; set; }

        public AddAccompanyCardWindow(AddAccompanyCardWindowViewModel viewModel)
        {
            InitializeComponent();

            SetViewModel(viewModel);

            FocusCardNumber();
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
    }
}
