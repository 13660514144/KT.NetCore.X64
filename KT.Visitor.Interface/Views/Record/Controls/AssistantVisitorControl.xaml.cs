using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Interface.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Interface.Views.Controls
{
    /// <summary>
    /// AssistantVisitorControl.xaml 的交互逻辑
    /// </summary>
    public partial class AssistantVisitorControl : UserControl
    {
        private AssistantVisitorControlViewModel ViewModel { get; set; }

        private ConfigHelper _configHelper;

        public AssistantVisitorControl(ConfigHelper configHelper)
        {
            InitializeComponent();

            _configHelper = configHelper;

            ViewModel = ContainerHelper.Resolve<AssistantVisitorControlViewModel>();
        }

        public void Init(VisitorInfoModel visitorInfo)
        {
            ViewModel.VisitorInfo = visitorInfo;

            this.DataContext = ViewModel;
        }
    }
}
