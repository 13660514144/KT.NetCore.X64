using KT.Common.Core.Enums;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Data.Enums;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace KT.Visitor.Interface.Views
{
    /// <summary>
    /// vistorRecordsList.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorRecordListControl : UserControl
    {
        private VisitorRecordListControlViewModel ViewModel { get; set; }
        private IVisitorApi _visitorRecordApi;

        public VisitorRecordListControl(VisitorRecordListControlViewModel viewModel,
            IVisitorApi visitorRecordApi)
        {
            InitializeComponent();

            _visitorRecordApi = visitorRecordApi;
        }

        public void InitViewModel()
        {
            ViewModel = ContainerHelper.Resolve<VisitorRecordListControlViewModel>();
            this.DataContext = ViewModel;
            ViewModel.NavPageControl.InitDataHandler = InitDataAsync;
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

        private async void BtnSeach_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.NavPageControl.RefreshDataAsync(1);
        }

        private async Task<BasePageData> InitDataAsync(int page, int size)
        {
            VisitorQuery query = new VisitorQuery();

            //搜索条件的组合 
            query.Name = txt_name.Text?.Trim();
            query.Gender = cbo_sex.SelectedValue?.ToString();
            query.VisitorFrom = cbo_visSource.SelectedValue?.ToString();
            query.IcCard = txt_icnum.Text?.Trim();
            if (!string.IsNullOrEmpty(ViewModel.Status))
            {
                query.VisitorStatus = ViewModel.Status;
            }
            query.StaffName = txt_interName.Text?.Trim();
            DateTime? dtStart = DateTimeUtil.ToDateTime(tb_start_time.Text.Trim());
            DateTime? dtEnd = DateTimeUtil.ToDateTime(tb_end_time.Text.Trim());
            if (dtStart.HasValue && dtEnd.HasValue)
            {
                query.Start = dtStart.Value.ToString("yyyy-MM-dd HH:mm:ss");
                query.End = dtEnd.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            query.CompanyIds = await ViewModel.GetSelectdCommpanyIdsAsync();
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
            this.ViewModel.StartTime = DateTimeUtil.DayStartMilliString();
            this.ViewModel.EndTime = DateTimeUtil.DayEndMilliString();
        }

        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            pop_SelectTime.IsOpen = false;
        }
    }
}
