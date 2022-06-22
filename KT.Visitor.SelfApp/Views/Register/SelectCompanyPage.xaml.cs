using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.Data.Models;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.IdReader.Common;
using KT.Visitor.SelfApp.Helpers;
using KT.Visitor.SelfApp.Public;
using KT.Visitor.SelfApp.Views.Common;
using KT.Visitor.SelfApp.Views.Register;
using Microsoft.Extensions.Logging;
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
    /// SelectCompanyPage.xaml 的交互逻辑
    /// </summary>
    public partial class SelectCompanyPage : Page
    {
        private SelectCompanyPageViewModel _viewModel;
        private ICompanyApi _companyApi;
        private PrintHandler _printHandler;
        private IVisitorApi _visitorApi;
        private MainFrameHelper _mainFrameHelper;
        private SelfAppSettings _selfAppSettings;
        private ILogger _logger;

        public SelectCompanyPage()
        {
            InitializeComponent();

            _viewModel = ContainerHelper.Resolve<SelectCompanyPageViewModel>();
            _companyApi = ContainerHelper.Resolve<ICompanyApi>();
            _printHandler = ContainerHelper.Resolve<PrintHandler>();
            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();
            _mainFrameHelper = ContainerHelper.Resolve<MainFrameHelper>();
            _selfAppSettings = ContainerHelper.Resolve<SelfAppSettings>();
            _logger = ContainerHelper.Resolve<ILogger>();

            this.DataContext = _viewModel;

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
                    tb.MouseLeftButtonDown += TbBuilding_Click;
                    tb.Cursor = Cursors.Hand;
                    wp_build.Children.Add(tb);
                }
                //默认选择第一家公司
                if (wp_build.Children.Count > 0)
                {
                    TbBuilding_Click(wp_build.Children[0], null);
                }
            }
        }
        #region 成员变量 
        private CompanyModel currBuild;
        private CompanyModel currFloor;
        private List<CompanyModel> csb;

        public AppointmentModel Appointor { get; set; }

        #endregion
        private void TbBuilding_Click(object sender, MouseButtonEventArgs e)
        {
            var tb = sender as TextBlock;
            this.currBuild = csb.Where(p => p.Name == tb.Text).FirstOrDefault();
            //设置未选中的大厦样式
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
            var building = csb.Where(p => p.Name == tb.Text).FirstOrDefault();
            _viewModel.FloorPageData = new PageDataViewModel<CompanyViewModel>();
            if (building.Nodes?.FirstOrDefault() == null)
            {
                return;
            }

            _viewModel.FloorPageData.Size = _selfAppSettings.FloorSelectPageSize;
            _viewModel.FloorPageData.Totals = currBuild.Nodes.Count;
            _viewModel.FloorPageData.Pages = _viewModel.FloorPageData.Totals.GetPages(_viewModel.FloorPageData.Size);

            //选择第一页
            SetFloorPageData(1, _selfAppSettings.FloorSelectPageSize);
        }

        private void SetFloorPageData(int page, int size)
        {
            if (page <= 0)
            {
                return;
            }
            if (((page - 1) * size) >= currBuild.Nodes.Count)
            {
                return;
            }

            var floors = currBuild.Nodes.Skip((page - 1) * size).Take(size).ToList();
            //动态加载楼层按钮
            wp_floor.Children.Clear();
            foreach (var item in floors)
            {
                Style btn_style = (Style)this.FindResource("floorstyle");
                var btn = new Button() { Content = item.Name, Style = btn_style };
                btn.Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/icons/@2x/楼层未选中@2x.png"))
                };
                //楼层点击事件
                btn.Click += BtnFloor_Click;
                wp_floor.Children.Add(btn);
            }

            _viewModel.FloorPageData.Page = page;

            //默认选择第一个楼层
            BtnFloor_Click(wp_floor.Children[0], null);
        }

        private void BtnFloor_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            this.currFloor = this.currBuild.Nodes.Where(p => p.Name == btn.Content.ToString()).FirstOrDefault();

            _viewModel.CompanyPageData = new PageDataViewModel<CompanyViewModel>();
            if (currFloor?.Nodes?.FirstOrDefault() != null)
            {
                _viewModel.CompanyPageData.Size = _selfAppSettings.CompanySelectPageSize;
                _viewModel.CompanyPageData.Totals = currFloor.Nodes.Count;
                _viewModel.CompanyPageData.Pages = _viewModel.CompanyPageData.Totals.GetPages(_viewModel.CompanyPageData.Size);
            }

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

            SetCompanyPageData(1, _selfAppSettings.CompanySelectPageSize);
        }

        private void SetCompanyPageData(int page, int size)
        {
            if (page <= 0)
            {
                return;
            }
            if (((page - 1) * size) >= currFloor.Nodes.Count)
            {
                return;
            }

            var companies = currFloor.Nodes.Skip((page - 1) * size).Take(size).ToList();
            if (companies?.FirstOrDefault() != null)
            {
                _viewModel.CompanyPageData.Page = page;
                dtg_Show.ItemsSource = companies;
            }
        }

        private async void BtnOperate_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_Show.SelectedIndex != -1)
            {
                var company = dtg_Show.SelectedItem as CompanyModel;
                if (company.Opening)
                {
                    throw CustomException.Run("该公司不支持自助机登记，请移步前台登记，感谢您的配合～");
                }

                //判断公司能否访问指定楼层
                var query = new CompanyCheckVisitQuery();
                query.CompanyId = company.Id;
                query.FloorId = currFloor.Id;
                var isFloor = await _companyApi.CheckVisitAsync(query);
                if (isFloor.Code != 200)
                {
                    //$"该公司指定接待楼层为{company.FloorName}，其他楼层暂不支持来访，请访问接待楼层"
                    ContainerHelper.Resolve<OperateErrorPage>().ShowMessage(isFloor.Message, "温馨提示", true);
                    return;
                }

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
            else
            {
                _logger.LogError($"公司未选择正确的行：selectedIndex:{dtg_Show.SelectedIndex} ");
                return;
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

        private void BtnFloorPre_Click(object sender, RoutedEventArgs e)
        {
            SetFloorPageData(_viewModel.FloorPageData.Page - 1, _selfAppSettings.FloorSelectPageSize);
        }

        private void BtnFloorNext_Click(object sender, RoutedEventArgs e)
        {
            SetFloorPageData(_viewModel.FloorPageData.Page + 1, _selfAppSettings.FloorSelectPageSize);
        }

        private void BtnCompanyPre_Click(object sender, RoutedEventArgs e)
        {
            SetCompanyPageData(_viewModel.CompanyPageData.Page - 1, _selfAppSettings.CompanySelectPageSize);
        }

        private void BtnCompanyNext_Click(object sender, RoutedEventArgs e)
        {
            SetCompanyPageData(_viewModel.CompanyPageData.Page + 1, _selfAppSettings.CompanySelectPageSize);
        }
    }
}
