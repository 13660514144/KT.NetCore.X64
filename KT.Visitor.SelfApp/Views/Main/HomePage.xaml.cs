using KT.Visitor.Common.Helpers;
using KT.Visitor.SelfApp.Views.Auth;
using KT.Visitor.SelfApp.Helpers;
using KT.Visitor.SelfApp.Register;
using KT.Visitor.SelfApp.Views.Setting;
using Prism.Ioc;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using KT.Common.WpfApp.Helpers;
using KT.Visitor.Common.Settings;

namespace KT.Visitor.SelfApp
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePageViewModel ViewModel { get; set; }

        private ConfigHelper _configHelper;
        private MainFrameHelper _mainFrameHelper;
        private IContainerProvider _containerProvider;
        private PageShow _PageShow;
        public HomePage(ConfigHelper configHelper,
            MainFrameHelper mainFrameHelper,
            IContainerProvider containerProvider, PageShow _pageShow)
        {
            InitializeComponent();

            _configHelper = configHelper;
            _mainFrameHelper = mainFrameHelper;
            _containerProvider = containerProvider;
            _PageShow = _pageShow;
            ViewModel = ContainerHelper.Resolve<HomePageViewModel>();

            this.DataContext = ViewModel;

            this.btn_vistorRegister.IsEnabled = (_PageShow.visitor==true?true:false);
            this.Vfk.Text = (_PageShow.visitor == true ? "我是访客" : "暂未开通使用");
            this.btn_AuthRole.IsEnabled = (_PageShow.identity == true ? true : false);
            this.Vwx.Text = (_PageShow.identity == true ? "我已在微信预约" : "暂未开通使用");
            this.btn_invite.IsEnabled = (_PageShow.invitation == true ? true : false);
            this.Vyq.Text = (_PageShow.invitation == true ? "我有邀请码" : "暂未开通使用");
        }
        #region 成员变量
        //无操作检测
        private DispatcherTimer timer_noOperater;
        //时间显示
        private DispatcherTimer timer_clock;

        Timer pressTimer;//按下操作
        #endregion
        private void Btn_vistorRegister_Click(object sender, RoutedEventArgs e)
        {
            
            var UI = _containerProvider.Resolve<SelectCompanyPage>();
            _mainFrameHelper.Link(UI);
        }

        private void Btn_AuthRole_Click(object sender, RoutedEventArgs e)
        {
            if (_PageShow.visitor == false) return;
            var UI = _containerProvider.Resolve<IdentityAuthPage>();
            _mainFrameHelper.Link(UI);
        }
        private void Btn_invite_Click(object sender, RoutedEventArgs e)
        {
            var UI = _containerProvider.Resolve<InviteAuthPage>();
            _mainFrameHelper.Link(UI);
        }
        private void Btn_vistorRegister_Loaded(object sender, RoutedEventArgs e)
        {
            timer_noOperater = new System.Windows.Threading.DispatcherTimer();
            timer_noOperater.Interval = new TimeSpan(0, 0, 1);
            timer_noOperater.Tick += new EventHandler(timer_noOperater_Tick);

            timer_clock = new System.Windows.Threading.DispatcherTimer();
            timer_clock.Interval = TimeSpan.FromMinutes(0.5);
            timer_clock.Tick += Timer_clock_Tick;
            timer_clock.Start();
            tb_time.Text = DateTime.Now.ToString("yyyy年MM月dd日") + "   " + DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("zh-cn")) + "   " + DateTime.Now.ToString("HH:mm");
        }
        private void Timer_clock_Tick(object sender, EventArgs e)
        {
            tb_time.Text = DateTime.Now.ToString("yyyy年MM月dd日") + "   " + DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("zh-cn")) + "   " + DateTime.Now.ToString("HH:mm");
        }
        private void timer_noOperater_Tick(object sender, EventArgs e)
        {
            if (OperateTimeOutHelper.GetIdleTick() / 1000 > 30)
            {
                var UI = _containerProvider.Resolve<HomePage>();
                _mainFrameHelper.Link(UI);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            pressTimer = new Timer();
            pressTimer.Interval = 1000;
            pressTimer.Tick += PressTimer_Tick;

            timer_noOperater = new System.Windows.Threading.DispatcherTimer();
            timer_noOperater.Interval = new TimeSpan(0, 0, 1);
            timer_noOperater.Tick += new EventHandler(timer_noOperater_Tick);
            // timer_noOperater.Start();
            timer_clock = new System.Windows.Threading.DispatcherTimer();
            timer_clock.Interval = TimeSpan.FromSeconds(1);
            timer_clock.Tick += Timer_clock_Tick;
            timer_clock.Start();
            tb_time.Text = DateTime.Now.ToString("yyyy年MM月dd日") + "   " + DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("zh-cn")) + "   " + DateTime.Now.ToString("HH:mm");
        }

        private void PressTimer_Tick(object sender, EventArgs e)
        {
            pressTimer.Stop();
            Dispatcher.Invoke(() =>
            {
                var UI = _containerProvider.Resolve<SettingPage>();
                _mainFrameHelper.Link(UI, false);
            });
        }

        private void Image_TouchDown(object sender, MouseButtonEventArgs e)
        {
            pressTimer.Start();
        }

        private void Image_TouchUp(object sender, MouseButtonEventArgs e)
        {
            pressTimer.Stop();
        }
    }
}
