using KT.Common.WpfApp.Helpers;
using System;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views
{
    /// <summary>
    /// Pblacklist.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorBlacklistControl : UserControl
    {
        private VisitorBlacklistControlViewModel _viewModel;

        public VisitorBlacklistControl()
        {
            InitializeComponent();

        }

        public void InitViewModel()
        {
            _viewModel = ContainerHelper.Resolve<VisitorBlacklistControlViewModel>();

            this.DataContext = _viewModel;
        }
    }
}
