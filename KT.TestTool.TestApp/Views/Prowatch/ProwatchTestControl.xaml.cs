using KT.Common.WpfApp.Attributes;
using System;
using System.Windows.Controls;

namespace KT.TestTool.TestApp.Views.Prowatch
{
    /// <summary>
    /// ProwatchTestControl.xaml 的交互逻辑
    /// </summary>
    [NavigationItem]
    public partial class ProwatchTestControl : UserControl
    {
        private ProwatchTestControlViewModel _viewModel;

        private Action<Action> _dispatcherAction;

        public ProwatchTestControl(ProwatchTestControlViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            _dispatcherAction = new Action<Action>((action) =>
            {
                base.Dispatcher.Invoke(action);
            });

            _viewModel.Init(_dispatcherAction);

            this.DataContext = _viewModel;
        }
    }
}
