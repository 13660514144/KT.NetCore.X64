using KT.Common.Core.Enums;
using KT.Common.Core.Utils;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.Data.Enums;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace KT.Visitor.Interface.Views.Visitor
{
    /// <summary>
    /// vistorRecordsList.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorRecordListPage : Page
    {
        private VisitorRecordListPageViewModel _viewModel;
        private VisitorRecordApi _visitorRecordApi;
        private VisitorDetailPage _visitorDetailPage;

        public VisitorRecordListPage(VisitorRecordListPageViewModel viewModel, VisitorRecordApi visitorRecordApi, VisitorDetailPage visitorDetailPage)
        {
            InitializeComponent();

            // unavpage.ControlChange += Unavpage_ControlChange;
            _viewModel = viewModel;
            _visitorRecordApi = visitorRecordApi;
            _visitorDetailPage = visitorDetailPage;

            this.DataContext = _viewModel;

            ctl_Page.InitDataHandler = InitDataAsync;

            PreviewMouseDown += VisitorRecordListPage_PreviewMouseDown;
        }

        //点其它地方关闭时间选择器
        private void VisitorRecordListPage_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //时间选择器
            //点击时间选择文本框不关闭弹出时间选择器
            if (object.ReferenceEquals(tb_start_time, e.Source) || object.ReferenceEquals(tb_end_time, e.Source))
            {
                //关闭其它弹出 
                pop_TreeCompanys.IsOpen = false;
                return;
            }
            else
            {
                //点击时间选择器不关闭  
                if (IsNotClose(grid_Picker, e.Source))
                {
                    return;
                }
                else
                {
                    pop_SelectTime.IsOpen = false;
                }
            }

            //时间选择器
            //点击时间选择文本框不关闭弹出时间选择器
            if (object.ReferenceEquals(btn_TreeCompanys, e.Source))
            {
                //关闭其它弹出 
                pop_SelectTime.IsOpen = false;
                return;
            }
            else
            {
                //点击树型选择器不关闭  
                if (IsNotClose(Grid_CheckTreeCompanys, e.Source))
                {
                    return;
                }
                else
                {
                    pop_TreeCompanys.IsOpen = false;
                }
            }
        }


        /// <summary>
        /// 检查是否关闭窗口
        /// </summary>
        /// <param name="control"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private bool IsNotClose(UIElement control, object source)
        {
            if (object.ReferenceEquals(control, source))
            {
                return true;
            }
            if (control is Panel)
            {
                var panel = control as Panel;
                foreach (UIElement item in panel.Children)
                {
                    if (IsNotClose(item, source))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void Btn_detail_Click(object sender, RoutedEventArgs e)
        {
            _ = DetailAsync();
        }

        private async Task DetailAsync()
        {
            var visitorblack = dtgShow.SelectedItem as VisitorInfoModel;

            var result = await _visitorRecordApi.GetVisitorInfoAsync(visitorblack.Id);
            _visitorDetailPage.Init(result);

            NavigationService.Navigate(_visitorDetailPage, UriKind.RelativeOrAbsolute);
        }

        private void BtnSeach_Click(object sender, RoutedEventArgs e)
        {
            ctl_Page.RefreshData(1);
        }

        private async Task<BasePageData> InitDataAsync(int page, int size)
        {
            VisitorQuery query = new VisitorQuery();

            //搜索条件的组合 
            query.Name = txt_name.Text?.Trim();
            if (!string.IsNullOrEmpty(cbo_sex.SelectedValue?.ToString()))
            {
                query.Gender = GenderEnum.GetByText(cbo_sex.SelectedValue?.ToString())?.Value;
            }
            query.VisitorFrom = VisitorFromEnum.GetValueByValueOrText(cbo_visSource.SelectedValue?.ToString());

            query.IcCard = txt_icnum.Text?.Trim();
            if (!string.IsNullOrEmpty(_viewModel.Status))
            {
                query.VisitorStatus = _viewModel.Status;
            }
            query.StaffName = txt_interName.Text?.Trim();

            DateTime? dtStart = DateTimeUtil.ToDateTime(tb_start_time.Text.Trim());
            DateTime? dtEnd = DateTimeUtil.ToDateTime(tb_end_time.Text.Trim());
            if (dtStart.HasValue && dtEnd.HasValue)
            {
                query.Start = dtStart.Value.ToString("yyyy-MM-dd HH:mm:ss");
                query.End = dtEnd.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            query.CompanyIds = await _viewModel.GetSelectdCommpanyIdsAsync();
            query.Page = page;
            query.Size = size;

            query.IdNumber = txt_idNumber.Text?.Trim();

            var qds = await _visitorRecordApi.GetVistorRecordsAsync(query);
             
            dtgShow.ItemsSource = qds?.List;

            return qds;
        }

        private void btn_TreeCompanys_Click(object sender, RoutedEventArgs e)
        {
            pop_TreeCompanys.IsOpen = !pop_TreeCompanys.IsOpen;
        }

        private void dtp_time_GotFocus(object sender, RoutedEventArgs e)
        {
            pop_SelectTime.IsOpen = true;
        }

        private void dtp_time_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pop_SelectTime.IsOpen = true;
        }

        private void Btn_Clean_Click(object sender, RoutedEventArgs e)
        {
            this._viewModel.StartTime = DateTimeUtil.DayStartMilliString();
            this._viewModel.EndTime = DateTimeUtil.DayEndMilliString();
        }

        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            pop_SelectTime.IsOpen = false;
        }
    }
}
