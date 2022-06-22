using KT.Common.WpfApp.Attributes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace KT.TestTool.TestApp.Views.WinPak
{
    /// <summary>
    /// WinPakTestControl.xaml 的交互逻辑
    /// </summary>
    [NavigationItem]
    public partial class WinPakTestControl : UserControl
    {
        private WinPakTestControlViewModel _viewModel;

        private Action<Action> _dispatcherAction;

        public WinPakTestControl(WinPakTestControlViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            this.DataContext = _viewModel;

            _dispatcherAction = new Action<Action>((action) =>
            {
                base.Dispatcher.Invoke(action);
            });
        }

        private void Btn_AddCard_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.StartAddCard(_dispatcherAction);
        }

        private void Btn_AddAndDeleteCard_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddAndDeleteCard(_dispatcherAction);
        }
        private void Btn_DeleteCard_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteCard(_dispatcherAction);
        }

        private void Btn_EditCardAccessLevel_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.EditCardAccessLevel(_dispatcherAction);
        }

        private void Btn_SearchEditCardAccessLevel_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchCardAccessLevel(_dispatcherAction);
        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Search(_dispatcherAction);
        }

        private void Btn_RefrenceSearch_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RefrenceSearch(_dispatcherAction);
        }

    }
}
