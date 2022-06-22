using KT.Proxy.WebApi.Backend.Apis;
using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Views.Blacklist;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Interface.Views.Visitor
{
    /// <summary>
    /// vistorDetail.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorDetailPage : Page
    {
        private BlacklistApi _blacklistApi;
        private ConfigHelper _configHelper;
        private MainFrameHelper _mainFrameHelper;
        private IContainerProvider _containerProvider;

        public VisitorDetailPage(BlacklistApi blacklistApi,
             ConfigHelper configHelper,
             MainFrameHelper mainFrameHelper,
             IContainerProvider containerProvider)
        {
            InitializeComponent();

            _blacklistApi = blacklistApi;
            _configHelper = configHelper;
            _mainFrameHelper = mainFrameHelper;
            _containerProvider = containerProvider;
        }

        private VisitorInfoModel _visitorInfoModel { get; set; }

        public void Init(VisitorInfoModel visitorInfoModel)
        {
            _visitorInfoModel = visitorInfoModel;

            this.DataContext = _visitorInfoModel;
            foreach (var item in _visitorInfoModel.Status)
            {
                var ucvm = new VisitorDynamicDetailControl(item.CreateTime, item.StatusInfo);

                uc_status_lst.Children.Add(ucvm);
            }
            //if (!string.IsNullOrEmpty(_visitorInfoModel.FaceImg))
            //{
            //    var url = _configHelper.LocalConfig.ServiceAddress + _visitorInfoModel.FaceImg;
            //    img_head.Source = new BitmapImage(new Uri(url));
            //}
            if (!string.IsNullOrEmpty(_visitorInfoModel.SnapshotImg))
            {
                var url = _configHelper.LocalConfig.ServiceAddress + _visitorInfoModel.SnapshotImg;
                img_pz.Source = new BitmapImage(new Uri(url));
            }

            sp_AssistantVisitor.Children.Clear();
            border_Assistant.Visibility = Visibility.Collapsed;
            if (_visitorInfoModel.Retinues != null && _visitorInfoModel.Retinues.FirstOrDefault() != null)
            {
                foreach (var item in _visitorInfoModel.Retinues)
                {
                    var control = _containerProvider.Resolve<AssistantVisitorControl>();
                    control.Init(item);
                    sp_AssistantVisitor.Children.Add(control);
                }
                border_Assistant.Visibility = Visibility.Visible;
            }
        }

        private void btn_AppendToBlack_Click(object sender, RoutedEventArgs e)
        {
            _ = AppendToBlackAsync();
        }

        private async Task AppendToBlackAsync()
        {
            //弹出确认页面操作
            var pullBlackConfirmWindow = new PullBlackConfirmWindow();
            var pullBlackConfirmPageVM = pullBlackConfirmWindow.ViewModel;
            var pullResult = pullBlackConfirmWindow.ShowDialog();

            if (!pullResult.HasValue || !pullResult.Value)
            {
                return;
            }

            var black = new BlacklistModel();
            black.Name = this._visitorInfoModel.Name;
            black.Phone = _visitorInfoModel.Phone;
            black.Company = _visitorInfoModel.Company;
            black.FaceImg = _visitorInfoModel.FaceImg;
            black.SnapshotImg = _visitorInfoModel.SnapshotImg;

            //确认页面修改拉黑原因
            black.Reason = pullBlackConfirmPageVM.PullBlackReason;

            await _blacklistApi.AddAsync(black);

            MessageWarnBox.Show("操作成功！", "提示");
        }

        private void But_VisitorRecord_Click(object sender, RoutedEventArgs e)
        {
            _mainFrameHelper.PreHistory();
        }
    }
}
