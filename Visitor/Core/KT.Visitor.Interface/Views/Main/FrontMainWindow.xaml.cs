using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Views.Setting;
using KT.Proxy.WebApi.Backend.Models;
using Microsoft.Extensions.DependencyInjection;
using Panuon.UI.Silver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Prism.Ioc;

namespace KT.Visitor.Interface
{
    /// <summary>
    /// FrontMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FrontMainWindow : WindowX
    {
        private MainWindowViewModel _viewModel;
        private ConfigHelper _configHelper;
        private MainFrameHelper _mainFrameHelper;
        private VisitorTotalApi _visitorTotalApi;
        private IContainerProvider _containerProvider;

        public FrontMainWindow(ConfigHelper configHelper,
            MainFrameHelper mainFrameHelper,
            VisitorTotalApi visitorTotalApi,
            IContainerProvider containerProvider,
            MainWindowViewModel viewModel)
        {
            InitializeComponent();
            _configHelper = configHelper;
            _mainFrameHelper = mainFrameHelper;
            _visitorTotalApi = visitorTotalApi;
            _containerProvider = containerProvider;

            this.Closed += MainWindow_Closed;

            //初始化ViewModel
            this._viewModel = viewModel;

            this.DataContext = _viewModel;

            //初始化首页导航
            _mainFrameHelper.Init(frame);
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            //彻底退出
            Environment.Exit(0);
        }

        DispatcherTimer tm_totalvistor;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tm_totalvistor = new DispatcherTimer();
            tm_totalvistor.Interval = TimeSpan.FromMinutes(1);
            tm_totalvistor.Tick += Tm_totalvistor_Tick;
            tm_totalvistor.Start();
            Tm_totalvistor_Tick(sender, null);
            tb_loginName.Text = "您好," + MasterUser.LoginName;

            //Uri(“图片路径“)
            string fileUrl = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Files/Images/logoWhite.png");
            if (File.Exists(fileUrl))
            {
                var imagesouce = new BitmapImage(new Uri(fileUrl));
                logo.Source = imagesouce.Clone();
            }
        }

        private void Tm_totalvistor_Tick(object sender, EventArgs e)
        {
            _ = TotalVisitorAsync();
        }
        private async Task TotalVisitorAsync()
        {
            var result = await _visitorTotalApi.TotalVostorTotalAsync();
            if (result == null)
            {
                return;
            }
            txt_TodayVistorNum.Text = result.VisitorNum.ToString();
            CurrVisitor.Text = result.TotalVisitorNum.ToString();
        }

        private void Btn_vis_auth_Click(object sender, RoutedEventArgs e)
        {
            ActiveItem((sender as Button).Name);
            _mainFrameHelper.LinkAuthIndex(); ;
        }

        private void Btn_vis_register_Click(object sender, RoutedEventArgs e)
        {
            ActiveItem((sender as Button).Name);
            _mainFrameHelper.LinkVisitorRegister();
        }

        private void Btn_vis_record_Click(object sender, RoutedEventArgs e)
        {
            ActiveItem((sender as Button).Name);
            _mainFrameHelper.LinkVistorRecord();
        }

        private void Btn_vis_black_Click(object sender, RoutedEventArgs e)
        {
            ActiveItem((sender as Button).Name);
            _mainFrameHelper.LinkBlacklist();
        }


        public void ActiveItem(string btnName)
        {
            List<Image> images = new List<Image>();
            images.Add(img_reg);
            //images.Add(img_auth);
            images.Add(img_inv);
            images.Add(img_black);

            var currImg = string.Empty;

            switch (btnName)
            {
                case "btn_vis_register":
                    currImg = img_reg.Name;
                    break;
                //case "btn_vis_auth":
                //    currImg = img_auth.Name;
                //    break;
                case "btn_vis_record":
                    currImg = img_inv.Name;
                    break;
                case "btn_vis_black":
                    currImg = img_black.Name;
                    break;
            }

            foreach (var item in images)
            {
                if (item.Name == currImg)
                {
                    item.Visibility = Visibility.Visible;
                }
                else
                {
                    item.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Btn_config_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = _containerProvider.Resolve<ConfigSetting>();
        }

        private void btn_notify_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = _containerProvider.Resolve<NotifyPage>();
        }

        /// <summary>
        /// 动态加载网页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        //public static T GetPage<T>() where T : class, new()
        //{
        //    Type tp = typeof(T);
        //    string key = tp.Name + ".xaml";
        //    T UI;
        //    if (CommondHelper.dct_Pages.ContainsKey(key))
        //    {
        //        UI = CommondHelper.dct_Pages[key] as T;
        //    }
        //    else
        //    {
        //        UI = new T();

        //        CommondHelper.dct_Pages.Add(key, UI);
        //    }
        //    return UI;
        //}
    }
}
