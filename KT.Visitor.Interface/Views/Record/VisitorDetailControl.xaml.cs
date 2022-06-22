using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Views.Controls;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Prism.Events;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Models;

namespace KT.Visitor.Interface.Views
{
    /// <summary>
    /// vistorDetail.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorDetailControl : UserControl
    {
        private VisitorDetailControlViewModel _viewModel;

        private IBlacklistApi _blacklistApi;
        private ConfigHelper _configHelper;
        private IEventAggregator _eventAggregator;
        private DialogHelper _dialogHelper;
        private IVisitorApi _visitorApi;

        public VisitorDetailControl()
        {
            InitializeComponent();

            _blacklistApi = ContainerHelper.Resolve<IBlacklistApi>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();

            _viewModel = ContainerHelper.Resolve<VisitorDetailControlViewModel>();

            this.DataContext = _viewModel;

            _viewModel.RefreshAction = InitAsync;
        }

        public async Task InitAsync(long id)
        {
            var visitorInfoModel = await _visitorApi.GetVisitorInfoAsync(id);

            _viewModel.Init(visitorInfoModel);

            uc_status_lst.Children.Clear();
            foreach (var item in visitorInfoModel.Status)
            {
                var ucvm = new VisitorDynamicDetailControl(item.CreateTime, item.StatusInfo);

                uc_status_lst.Children.Add(ucvm);
            }

            sp_AssistantVisitor.Children.Clear();
            //border_Assistant.Visibility = Visibility.Collapsed;
            if (visitorInfoModel.Retinues != null && visitorInfoModel.Retinues.FirstOrDefault() != null)
            {
                foreach (var item in visitorInfoModel.Retinues)
                {
                    var control = ContainerHelper.Resolve<AssistantVisitorControl>();
                    control.Init(item);
                    sp_AssistantVisitor.Children.Add(control);
                }
                //border_Assistant.Visibility = Visibility.Visible;
            }
        }

        private void btn_AppendToBlack_Click(object sender, RoutedEventArgs e)
        {
            _ = _viewModel.AppendToBlackAsync();
        }

        private void But_VisitorRecord_Click(object sender, RoutedEventArgs e)
        {
            _eventAggregator.GetEvent<NavLinkEvent>().Publish(NavLinkModel.Create(FontNavEnum.VISITOR_RECORD, false));
        }

        private void btn_CancelVisit_Click(object sender, RoutedEventArgs e)
        {
            _ = _viewModel.CancelVisitAsync();
        }

        private void btn_EndVisit_Click(object sender, RoutedEventArgs e)
        {
            _ = _viewModel.FinishVisitAsync();
        }
    }
}
