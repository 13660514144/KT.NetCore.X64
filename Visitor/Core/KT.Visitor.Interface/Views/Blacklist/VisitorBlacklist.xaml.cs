using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Blacklist
{
    /// <summary>
    /// Pblacklist.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorBlacklist : Page
    {
        private VisitorBlacklistViewModel _viewModel;

        public VisitorBlacklist(VisitorBlacklistViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            this.DataContext = _viewModel;

        }
    }
}
