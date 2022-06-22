using KT.Common.WpfApp.Helpers;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using Prism.Ioc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Views.Register
{
    /// <summary>
    /// AccompanyVisitorsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AccompanyVisitorsWindow : WindowX
    {
        public AccompanyVisitorsWindowViewModel ViewModel;

        private bool _isConfirm;

        private ILogger _logger;

        public AccompanyVisitorsWindow()
        {
            InitializeComponent();

            _logger = ContainerHelper.Resolve<ILogger>();

            ViewModel = ContainerHelper.Resolve<AccompanyVisitorsWindowViewModel>();

            this.Closed += AccompanyVisitorsWindow_Closed;
        }

        private void AccompanyVisitorsWindow_Closed(object sender, System.EventArgs e)
        {
            if (!_isConfirm)
            {
                ViewModel.ClearAccompany();
            }
        }

        public void Init()
        {
            if (ViewModel.VisitorControls == null || ViewModel.VisitorControls.FirstOrDefault() == null)
            {
                ViewModel.AddVistor();
            }

            this.DataContext = ViewModel;
        }

        internal void SetViewModel(AccompanyVisitorsWindowViewModel viewModel,
            Action<string, AccompanyVisitorControlViewModel > _scanAndSetPersonAction)
        {
            ViewModel = viewModel;
            ViewModel.ScanAndSetPersonAction = _scanAndSetPersonAction;

            Init();
        }

        private void Btn_Confirm_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _isConfirm = true;
            this.Close();
        }

        private void Btn_Cancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
    }
}