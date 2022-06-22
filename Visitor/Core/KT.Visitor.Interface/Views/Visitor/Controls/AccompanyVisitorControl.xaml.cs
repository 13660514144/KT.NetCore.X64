using System;
using System.Windows;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    /// <summary>
    /// AccompanyVisitorControl.xaml 的交互逻辑
    /// </summary>
    public partial class AccompanyVisitorControl : UserControl
    {
        public AccompanyVisitorControlViewModel ViewModel { get; set; }

        public AccompanyVisitorControl(AccompanyVisitorControlViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            this.DataContext = ViewModel;
        }

        //控件移除事件
        public event EventHandler ControlRemove;
        private void Btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            ControlRemove?.Invoke(this, e);
        }

        private void PUTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.IsFocus = true;
        }

        private void PUTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.IsFocus = false;
        }
    }
}
