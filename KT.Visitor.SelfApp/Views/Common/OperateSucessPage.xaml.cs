using KT.Common.WpfApp.Helpers;
using KT.Visitor.Data.Enums;
using KT.Visitor.SelfApp.Helpers;
using KT.Visitor.SelfApp.Views.Common;
using Prism.Ioc;
using SQLitePCL;
using System;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace KT.Visitor.SelfApp.Public
{
    /// <summary>
    /// OperateSucessPage.xaml 的交互逻辑
    /// </summary>
    public partial class OperateSucessPage : Page
    {
        public OperateSucessPageViewModel ViewModel { get; set; }

        public static bool IsRuning { get; set; }

        //3秒后退回首頁
        DispatcherTimer dispatcher;
        private MainFrameHelper _mainFrameHelper;
        private IContainerProvider _containerProvider;

        public OperateSucessPage(MainFrameHelper mainFrameHelper,
          IContainerProvider containerProvider)
        {
            InitializeComponent();

            _mainFrameHelper = mainFrameHelper;
            _containerProvider = containerProvider;

            ViewModel = ContainerHelper.Resolve<OperateSucessPageViewModel>();
            this.DataContext = ViewModel;

            this.Loaded += WOperateSucess_Loaded;

            IsRuning = true;
        }

        public Action LoadedAction { get; set; }

        private void WOperateSucess_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcher = new DispatcherTimer();
            dispatcher.Interval = TimeSpan.FromSeconds(6);
            dispatcher.Tick += Dispatcher_Tick;
            dispatcher.Start();

            LoadedAction?.Invoke();
        }

        private void Dispatcher_Tick(object sender, EventArgs e)
        {
            IsRuning = false;

            //页面未改变返回主页
            if (!_mainFrameHelper.IsChangePage(this))
            {
                var UI = _containerProvider.Resolve<HomePage>();
                _mainFrameHelper.Link(UI);
            }
            dispatcher.Stop();
        }

        private void Img_exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var UI = _containerProvider.Resolve<HomePage>();
            _mainFrameHelper.Link(UI);
        }
    }
}
