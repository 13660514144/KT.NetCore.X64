using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace KT.Visitor.Interface.Views.Setting
{
    /// <summary>
    /// NotifyPage.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyPage : Page
    {

        private NotifyPageViewModel _notifyPageViewModel;
        private ILogger<NotifyPage> _logger;
        private MainFrameHelper _mainFrameHelper;

        private DispatcherTimer timer;

        public NotifyPage(ILogger<NotifyPage> logger, NotifyPageViewModel notifyPageViewModel, MainFrameHelper mainFrameHelper)
        {
            InitializeComponent();

            _logger = logger;
            _notifyPageViewModel = notifyPageViewModel;
            _mainFrameHelper = mainFrameHelper;

            this.DataContext = _notifyPageViewModel;

            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
        }

        private int resendSeconds = 120;
        private void Timer_Tick(object sender, EventArgs e)
        {
            resendSeconds = resendSeconds - 1;
            if (resendSeconds > 0)
            {
                _notifyPageViewModel.SendText = string.Format("{0}s后重发", resendSeconds);
                return;
            }
            _notifyPageViewModel.SendText = "发送";
        }

        private void btn_Send_Click(object sender, RoutedEventArgs e)
        {
            _notifyPageViewModel.SendText = string.Format("{0}s后重发", resendSeconds);
            try
            {

                _notifyPageViewModel.SendAsync();
                timer.Start();
                MessageWarnBox.Show("发送成功！", "提示");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("发送通知出错：{0} ", ex);
                _notifyPageViewModel.SendText = "发送";
            }
            //MainFrameHelper.Instance.LinkVisitorRegister();
        }
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            _mainFrameHelper.LinkVisitorRegister();
        }

        private void PUCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            _notifyPageViewModel.SetMessage();
        }
    }
}
