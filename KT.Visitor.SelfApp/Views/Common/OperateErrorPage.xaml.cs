using KT.Common.WpfApp.Helpers;
using KT.Visitor.SelfApp.Helpers;
using KT.Visitor.SelfApp.Views.Common;
using Prism.Ioc;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KT.Visitor.SelfApp.Public
{
    /// <summary>
    /// OperateErrorPage.xaml 的交互逻辑
    /// </summary>
    public partial class OperateErrorPage : Page
    {
        public OperateErrorPageViewModel ViewModel;

        private MainFrameHelper _mainFrameHelper;
        private bool _isPreHistory;

        public OperateErrorPage()
        {
            InitializeComponent();

            _mainFrameHelper = ContainerHelper.Resolve<MainFrameHelper>();

            this.ViewModel = new OperateErrorPageViewModel();
            this.DataContext = this.ViewModel;
        }

        private void Img_exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_isPreHistory)
            {
                _mainFrameHelper.PreHistory(this);
            }
            else
            {
                var page = ContainerHelper.Resolve<HomePage>();
                _mainFrameHelper.Link(page);
            }
        }

        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="mesg">内容</param>
        /// <param name="title">标题</param>
        /// <param name="preHistory">退出键是否返回上一页，是：返回到上一页，否：返回到主页</param>
        public void ShowMessage(string mesg, string title = "温馨提示", bool isPreHistory = false)
        {
            ViewModel.ErrorMsg = mesg;
            ViewModel.Title = title;
            _isPreHistory = isPreHistory;

            _mainFrameHelper.Link(this, isPreHistory);
        }
    }
}
