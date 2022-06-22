using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.IdReader.Common;
using KT.Visitor.SelfApp.Helpers;
using KT.Visitor.SelfApp.Public;
using KT.Visitor.SelfApp.Views.Common;
using KT.Visitor.SelfApp.Views.Register;
using Newtonsoft.Json;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KT.Visitor.SelfApp.Register
{
    /// <summary>
    /// RollSelectCompanyPage.xaml 的交互逻辑
    /// </summary>
    public partial class RollSelectCompanyPage : Page
    {
        private RollSelectCompanyPageViewModel _viewModel;
        private ICompanyApi _companyApi;
        private PrintHandler _printHandler;
        private IVisitorApi _visitorApi;
        private MainFrameHelper _mainFrameHelper;

        public RollSelectCompanyPage()
        {
            InitializeComponent();

            _viewModel = ContainerHelper.Resolve<RollSelectCompanyPageViewModel>();
            _companyApi = ContainerHelper.Resolve<ICompanyApi>();
            _printHandler = ContainerHelper.Resolve<PrintHandler>();
            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();
            _mainFrameHelper = ContainerHelper.Resolve<MainFrameHelper>();


            this.DataContext = _viewModel;

            this.Loaded += WSelectCompany_Loaded;
        }

        private void WSelectCompany_Loaded(object sender, RoutedEventArgs e)
        {
            _ = LoadCompanyAsync();
        }

        private async Task LoadCompanyAsync()
        {
            //加载楼栋 
            wp_build.Children.Clear();
            csb = await _companyApi.GetMapsAsync();
            if (csb != null)
            {
                foreach (var item in csb)
                {
                    Style tb_style = (Style)this.FindResource("buildstyle");
                    var tb = new TextBlock { Text = item.Name, Style = tb_style };
                    tb.MouseLeftButtonDown += Tb_MouseLeftButtonDown;
                    tb.Cursor = Cursors.Hand;
                    wp_build.Children.Add(tb);
                }
                //默认选择第一家公司
                if (wp_build.Children.Count > 0)
                {
                    Tb_MouseLeftButtonDown(wp_build.Children[0], null);
                }
            }
        }
        #region 成员变量 
        private CompanyModel currBuild;
        private CompanyModel currFloor;
        private List<CompanyModel> csb;

        public AppointmentModel Appointor { get; set; }

        #endregion
        private void Tb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tb = sender as TextBlock;
            //设置未选中的大厦样式
            this.currBuild = csb.Where(p => p.Name == tb.Text).FirstOrDefault();
            foreach (var item in wp_build.Children)
            {
                if (item.GetType().Name == "TextBlock")
                {
                    var _tb = item as TextBlock;
                    _tb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E9EC9"));
                }
            }
            //设置选中的字体颜色
            tb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3AF2FF"));

            //加载楼层
            var _floor = csb.Where(p => p.Name == currBuild.Name).FirstOrDefault();
            wp_floor.Children.Clear();
            if (_floor.Nodes == null)
            {
                return;
            }
            //动态加载楼层按钮
            foreach (var item in _floor.Nodes)
            {
                Style btn_style = (Style)this.FindResource("floorstyle");
                var btn = new Button() { Content = item.Name, Style = btn_style };
                btn.Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/icons/@2x/楼层未选中@2x.png"))
                };
                //楼层点击事件
                btn.Click += Btn_Click;
                wp_floor.Children.Add(btn);
            }
            Btn_Click(wp_floor.Children[0], null);
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            this.currFloor = this.currBuild.Nodes.Where(p => p.Name == btn.Content.ToString()).FirstOrDefault();
            //恢复默认的图片 未选中
            foreach (var item in wp_floor.Children)
            {
                if (item.GetType().Name == "Button")
                {
                    var _bt = item as Button;
                    _bt.Background = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/icons/@2x/楼层未选中@2x.png"))
                    };
                }
            }
            //设置选中的图片
            btn.Background = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/icons/@2x/楼层选中@2x.png"))
            };
            dtg_Show.ItemsSource = null;

            if (this.currFloor != null && currFloor.Nodes != null)
            {
                dtg_Show.ItemsSource = currFloor.Nodes;
            }
        }

        private void BtnOperate_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_Show.SelectedIndex != -1)
            {
                var company = dtg_Show.SelectedItem as CompanyModel;
                if (Appointor == null)
                {
                    Appointor = new AppointmentModel();
                }
                Appointor.Once = true;
                Appointor.BeVisitCompanyId = company.Id;
                Appointor.BeVisitCompanyName = company.Name;
                Appointor.BeVisitFloorId = currFloor.Id;
                Appointor.Unit = company.Unit;
                Appointor.AccessDate = 1;
            }

            var page = ContainerHelper.Resolve<RegisterReadIdCardPage>();
            page.Appointor = Appointor;
            _mainFrameHelper.Link(page);
        }

        private void Img_exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var UI = ContainerHelper.Resolve<HomePage>();
            _mainFrameHelper.Link(UI);
        }

        private void dtg_Show_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
