using KT.Common.Core.Utils;
using KT.Visitor.Interface.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace KT.Visitor.Interface.Views.Common
{
    /// <summary>
    /// SuccessPage.xaml 的交互逻辑
    /// </summary>
    public partial class SuccessPage : Page
    {
        /// <summary>
        /// 10秒后执行事件，如果没有则跳转到主页
        /// </summary>
        public Action AutoEndAction;

        public string PageKey;

        private ILogger<SuccessPage> _logger;
        private MainFrameHelper _mainFrameHelper;

        public SuccessPage(ILogger<SuccessPage> logger, MainFrameHelper mainFrameHelper)
        {
            InitializeComponent();

            _logger = logger;
            _mainFrameHelper = mainFrameHelper;

            _logger.LogInformation("操作成功页面显示！");

            this.PageKey = IdUtil.NewId();
            _mainFrameHelper.PageKey = PageKey;
        }

        DispatcherTimer dTimer = new DispatcherTimer();
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dTimer.Tick += new EventHandler(dTimer_Tick);
            dTimer.Interval = new TimeSpan(0, 0, 10);
            dTimer.Start();
        }

        private void dTimer_Tick(object sender, EventArgs e)
        {
            _logger.LogInformation("自动跳转页面！");
            //页面未更改跳转
            if (!_mainFrameHelper.IsChangePage(PageKey))
            {
                if (AutoEndAction != null)
                {
                    _logger.LogInformation("自动跳转操作页面！");
                    AutoEndAction.Invoke();
                }
                else
                {
                    _logger.LogInformation("自动跳转登记页面！");
                    _mainFrameHelper.LinkVisitorRegister();
                }
            }
            dTimer.Stop();
            _logger.LogInformation("自动跳转页面完成！");
        }

        private void Btn_Home_Click(object sender, RoutedEventArgs e)
        {
            _mainFrameHelper.LinkVisitorRegister();
        }

        private void Btn_Auth_Click(object sender, RoutedEventArgs e)
        {
            _mainFrameHelper.LinkAuthIndex();
        }
    }
}
