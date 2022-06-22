using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using Prism.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Common
{
    /// <summary>
    /// NavPageControl.xaml 的交互逻辑
    /// </summary>
    public partial class NavPageControl : UserControl
    {
        private Style pageStyleSelected => (Style)Application.Current.FindResource("Btn_PageSelected");
        private Style pageStyleNoSelected => (Style)Application.Current.FindResource("Btn_PageNoSelected");

        /// <summary>
        /// 加载数据
        /// </summary> 
        public Func<int, int, Task<BasePageData>> InitDataHandler;

        public NavPageControlViewModel ViewModel { get; set; }

        private DialogHelper _dialogHelper;

        public NavPageControl()
        {

            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            ViewModel = ContainerHelper.Resolve<NavPageControlViewModel>();

            this.DataContext = ViewModel;

            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Page_Loaded);
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="page">传入页码，不传则刷新当前页</param>
        public async Task RefreshDataAsync(int? page = null)
        {
            await InitDataAsync(page == null ? ViewModel.Page : page.Value, ViewModel.Size);
        }

        /// <summary>
        /// 控件加载完成后就开始加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await InitDataAsync(1, ViewModel.Size);
        }

        /// <summary>
        /// 跳转页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_PageNum_Click(object sender, RoutedEventArgs e)
        {
            int pageNumber = Convert.ToInt32(((Button)sender).Tag.ToString());
            await InitDataAsync(pageNumber, ViewModel.Size);
        }

        /// <summary>
        /// 设置页面样式
        /// </summary>
        /// <param name="num"></param>
        /// <param name="pageIndex"></param>
        private void SetPage(int num, int pageIndex)
        {
            Button btn = new Button();
            if (pageIndex == num)
            {
                btn.Style = pageStyleSelected;
            }
            else
            {
                btn.Style = pageStyleNoSelected;
            }
            btn.Content = num.ToString();
            btn.Tag = num;
            btn.Click += btn_PageNum_Click;
            wp_PageNum.Children.Add(btn);
        }

        /// <summary>
        /// 初始化数据
        /// 1、委托方法查找数据
        /// 2、定义显示的页面码
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        private async Task InitDataAsync(int pageIndex, int pageSize)
        {
            if (InitDataHandler == null)
            {
                return;
            }
            var pageInfo = await InitDataHandler.Invoke(pageIndex, pageSize);
            //设置页面数值
            this.ViewModel.SetData(pageInfo);

            //数据为空设置界面不可用与显示 
            if (pageInfo == null || pageInfo.Totals == 0)
            {
                wp_PageNum.Children.Clear();
                btn_PrePage.IsEnabled = false;
                btn_NextPage.IsEnabled = false;
                if (txt_JumpPage != null)
                {
                    txt_JumpPage.IsEnabled = false;
                }
                return;
            }
            else
            {
                btn_NextPage.IsEnabled = false;
                btn_PrePage.IsEnabled = false;
                //第一页 上一页、页面跳转 不可用
                btn_PrePage.IsEnabled = pageIndex > 1;
                //最后一页下一页不可用
                btn_NextPage.IsEnabled = pageIndex != pageInfo.Pages;
                //页数大于一页才能跳转
                txt_JumpPage.IsEnabled = pageInfo.Pages > 1;
            }

            wp_PageNum.Children.Clear();
            if (pageInfo.Pages <= 7)
            {
                for (int i = 1; i <= pageInfo.Pages; i++)
                {
                    SetPage(i, pageIndex);
                }
            }
            else if (pageInfo.Pages > 7)
            {
                if (pageIndex <= 4)
                {
                    for (int i = 1; i <= 6; i++)
                    {
                        SetPage(i, pageIndex);
                    }
                    //最后一页
                    SetPage(pageInfo.Pages, pageIndex);
                }
                else if (pageIndex >= pageInfo.Pages - 3)
                {
                    //第一页
                    SetPage(1, pageIndex);
                    for (int i = pageInfo.Pages - 5; i <= pageInfo.Pages; i++)
                    {
                        SetPage(i, pageIndex);
                    }
                }
                else
                {
                    //第一页
                    SetPage(1, pageIndex);
                    for (int i = pageIndex - 2; i <= pageIndex + 2; i++)
                    {
                        SetPage(i, pageIndex);
                    }
                    //最后一页
                    SetPage(pageInfo.Pages, pageIndex);
                }
            }
            //上一页
            if (pageIndex == 1 || pageInfo.Pages == 0)
            {
                btn_PrePage.IsEnabled = false;
                btn_PrePage.Tag = 1;
            }
            else
            {
                btn_PrePage.IsEnabled = true;
                btn_PrePage.Tag = pageIndex - 1;
            }
            //下一页
            if (pageIndex == pageInfo.Pages || pageInfo.Pages == 0)
            {
                btn_NextPage.IsEnabled = false;
                btn_NextPage.Tag = pageInfo.Pages;
            }
            else
            {
                btn_NextPage.IsEnabled = true;
                btn_NextPage.Tag = pageIndex + 1;
            }
        }

        private void cb_Page_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel.Size > 0)
            {
                _ = InitDataAsync(1, ViewModel.Size);
            }
        }

        private void txt_JumpPage_LostFocus(object sender, RoutedEventArgs e)
        {
            int? page = ConvertUtil.ToInt32(txt_JumpPage.Text.Trim());
            if (page.HasValue && page.Value > 0 && page.Value != ViewModel.Page)
            {
                if (page.Value > ViewModel.Pages)
                {
                    ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("跳转的页面已超出范围！");
                    return;
                }
                _ = InitDataAsync(page.Value, ViewModel.Size);
            }
        }

        private void txt_JumpPage_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txt_JumpPage_LostFocus(sender, null);
            }
        }
    }
}