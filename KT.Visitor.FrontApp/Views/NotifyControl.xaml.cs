using KT.Common.Core.Exceptions;
using KT.Common.WpfApp.Helpers;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Models;
using Microsoft.Extensions.Logging;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace KT.Visitor.FrontApp.Views
{
    /// <summary>
    /// NotifyControl.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyControl : UserControl
    {
        private DispatcherTimer timer;

        public NotifyControlViewModel ViewModel;
        private ILogger _logger;
        private IEventAggregator _eventAggregator;
        private DialogHelper _dialogHelper;

        public NotifyControl()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<NotifyControlViewModel>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();

            this.DataContext = ViewModel;

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
                ViewModel.SendText = string.Format("{0}s后重发", resendSeconds);
                return;
            }
            ViewModel.SendText = "发送";
        }

        private void btn_Send_Click(object sender, RoutedEventArgs e)
        {
            _ = SendAsync();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            _eventAggregator.GetEvent<VisitorRegisteEvent>().Publish();
        }

        private void PUCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            ViewModel.SetMessage();
        }

        private async Task SendAsync()
        {
            resendSeconds = 120;
            ViewModel.SendText = string.Format("{0}s后重发", resendSeconds);
            try
            {
                await ViewModel.SendAsync();
                timer.Start();
                ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("发送成功！", "温馨提示");
            }
            catch (CustomException ex)
            {
                ContainerHelper.Resolve<MessageWarnBox>().ShowMessage(ex.Message, "温馨提示");
                ViewModel.SendText = "发送";
            }
            catch (Exception ex)
            {
                _logger.LogInformation("发送通知出错：{0} ", ex);
                ContainerHelper.Resolve<MessageWarnBox>().ShowMessage(ex.Message, "温馨提示");
                ViewModel.SendText = "发送";
            }
        }
    }
}
