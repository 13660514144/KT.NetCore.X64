using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Interface.Controls.BaseWindows;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views
{
    /// <summary>
    /// vistorRecordsList.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorImportRecordListControl : UserControl
    {
        private VisitorImportRecordListControlViewModel ViewModel { get; set; }
        private IVisitorApi _visitorApi;

        public VisitorImportRecordListControl(VisitorImportRecordListControlViewModel viewModel,
            IVisitorApi visitorApi)
        {
            InitializeComponent();

            _visitorApi = visitorApi;
        }

        public void InitViewModel()
        {
            ViewModel = ContainerHelper.Resolve<VisitorImportRecordListControlViewModel>();
            this.DataContext = ViewModel;
            ViewModel.NavPageControl.InitDataHandler = InitDataAsync;

            this.ViewModel.VisitorImport.StartVisitDate = DateTimeUtil.DayStartMilliString();
            this.ViewModel.VisitorImport.EndVisitDate = DateTimeUtil.DayEndMilliString();
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
            var query = new PageQuery();

            query.Page = page;
            query.Size = size;
            var qds = await _visitorApi.GetVisitorImportLogAsync(query);

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
            this.ViewModel.VisitorImport.StartVisitDate = DateTimeUtil.DayStartMilliString();
            this.ViewModel.VisitorImport.EndVisitDate = DateTimeUtil.DayEndMilliString();
        }

        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            pop_SelectTime.IsOpen = false;
        }

        private async void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            if (!(ViewModel.VisitorImport.TreeSelectFloor.SelectedFloor?.Id).HasValue)
            {
                throw new Exception("来访楼层不能为空！");
            }
            if (ViewModel.VisitorImport.CompanyStaff == null)
            {
                throw new Exception("来访对象不能为空！");
            }
            var model = new VisitorImportModel();
            model.Id = ViewModel.VisitorImport.Id;
            model.Reason = ViewModel.VisitorImport.Reason;
            model.Xls = ViewModel.VisitorImport.Xls;
            model.Zip = ViewModel.VisitorImport.Zip;
            model.EndVisitDate = ViewModel.VisitorImport.EndVisitDate;
            model.StartVisitDate = ViewModel.VisitorImport.StartVisitDate;
            model.Status = ViewModel.VisitorImport.Status;
            model.Results = ViewModel.VisitorImport.Results;
            model.CreateTime = ViewModel.VisitorImport.CreateTime;
            model.UpdateTime = ViewModel.VisitorImport.UpdateTime;
            //model.BeVisitFloorId = ViewModel.VisitorImport.BeVisitFloorId;
            model.BeVisitFloorId = ViewModel.VisitorImport.TreeSelectFloor.SelectedFloor.Id;
            model.Once = ViewModel.VisitorImport.Once;
            //model.BeVisitCompanyId = ViewModel.VisitorImport.BeVisitCompanyId;
            //model.BeVisitCompanyName = ViewModel.VisitorImport.BeVisitCompanyName;
            //model.BeVisitStaffId = ViewModel.VisitorImport.BeVisitStaffId;
            //model.BeVisitStaffName = ViewModel.VisitorImport.BeVisitStaffName;

            model.BeVisitCompanyId = ViewModel.VisitorImport.CompanyStaff.CompanyId;
            model.BeVisitCompanyName = ViewModel.VisitorImport.CompanyStaff.CompanyName;
            model.BeVisitStaffId = ViewModel.VisitorImport.CompanyStaff.StaffId;
            model.BeVisitStaffName = ViewModel.VisitorImport.CompanyStaff.StaffName;

            if (string.IsNullOrEmpty(model.Reason))
            {
                throw new Exception("来访事由不能为空！");
            }
            if (string.IsNullOrEmpty(model.Xls))
            {
                throw new Exception("请选择要上传的访客信息文件！");
            }

            await _visitorApi.AddVisitorImportAsync(model);

            //var integrateSuccessWindow = ContainerHelper.Resolve<SuccessWindow>();
            //integrateSuccessWindow.ViewModel.Init(registerResults);
            //_dialogHelper.ShowDialog(integrateSuccessWindow);

            ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("上传成功！");
        }

        private void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.IsCompanyStaffDropDownOpen = true;
        }
    }
}
