using KT.Common.WpfApp.Helpers;
using KT.Visitor.SelfApp.Helpers;
using KT.Visitor.SelfApp.Views.Common;
using Prism.Ioc;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KT.Visitor.SelfApp.Public
{
    /// <summary>
    /// MultiRegisterWarnPage.xaml 的交互逻辑
    /// </summary>
    public partial class MultiRegisterWarnPage : Page
    {
        public static bool IsRuning { get; set; }

        public MultiRegisterWarnPageViewModel ViewModel;

        private MainFrameHelper _mainFrameHelper;
        private Timer _timer;

        public MultiRegisterWarnPage()
        {

            InitializeComponent();

            _mainFrameHelper = ContainerHelper.Resolve<MainFrameHelper>();

            this.ViewModel = new MultiRegisterWarnPageViewModel();
            this.DataContext = this.ViewModel;

            this.Loaded += MultiRegisterWarnPage_Loaded;
        }

        private void MultiRegisterWarnPage_Loaded(object sender, RoutedEventArgs e)
        {
            IsRuning = true;
            _timer = new Timer(EndRuning, null, 20000, 20000);
        }

        private void EndRuning(object state)
        {
            IsRuning = false;
            _timer.DisposeAsync();
        }

        private void Img_exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var page = ContainerHelper.Resolve<HomePage>();
            _mainFrameHelper.Link(page);
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrc_Click(object sender, RoutedEventArgs e)
        {
            var page = ContainerHelper.Resolve<HomePage>();
            _mainFrameHelper.Link(page);
        }

        public void ShowMessage(string mesg, string title = "温馨提示")
        {
            ViewModel.ErrorMsg = mesg;
            ViewModel.Title = title;

            _mainFrameHelper.Link(this, false);
        }
        /// <summary>
        /// 继续登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_next_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ResubmitAppoint();
            IsRuning = false;
        }
    }
}
