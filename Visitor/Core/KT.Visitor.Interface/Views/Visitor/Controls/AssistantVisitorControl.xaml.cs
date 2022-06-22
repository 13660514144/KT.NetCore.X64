using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Visitor.Interface.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    /// <summary>
    /// AssistantVisitorControl.xaml 的交互逻辑
    /// </summary>
    public partial class AssistantVisitorControl : UserControl
    {
        private ConfigHelper _configHelper;

        public AssistantVisitorControl(ConfigHelper configHelper)
        {
            InitializeComponent();

            _configHelper = configHelper;
        }

        public void Init(VisitorTeamModel visitorTeam)
        {
            this.DataContext = visitorTeam;

            if (!string.IsNullOrEmpty(visitorTeam.FaceImg))
            {
                var url = _configHelper.LocalConfig.ServiceAddress + visitorTeam.FaceImg;
                img_head.Source = new BitmapImage(new Uri(url));
            }
            if (!string.IsNullOrEmpty(visitorTeam.SnapshotImg))
            {
                var url = _configHelper.LocalConfig.ServiceAddress + visitorTeam.SnapshotImg;
                img_pz.ImageSource = new BitmapImage(new Uri(url));
            }
        }
    }
}
