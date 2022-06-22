using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Interface.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views
{
    /// <summary>
    /// vistorRecordsList.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorImportDetailListControl : UserControl
    {
        private VisitorImportDetailListControlViewModel ViewModel { get; set; }
        private IVisitorApi _visitorApi;

        public VisitorImportDetailListControl(VisitorImportDetailListControlViewModel viewModel,
            IVisitorApi visitorApi)
        {
            InitializeComponent();

            _visitorApi = visitorApi;

            ViewModel = ContainerHelper.Resolve<VisitorImportDetailListControlViewModel>();
            this.DataContext = ViewModel;
            ViewModel.NavPageControl.InitDataHandler = InitDataAsync;

        }

        public async Task InitAsync(VisitorImportModel visitorImport)
        {
            ViewModel.VisitorImport = new VisitorImportViewModel();
            ViewModel.VisitorImport.Id = visitorImport.Id;
            ViewModel.VisitorImport.CompanyStaffName = visitorImport.BeVisitFullName;
            ViewModel.VisitorImport.Once = visitorImport.Once;

            await ViewModel.NavPageControl.RefreshDataAsync(1);
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
            var query = new VisitorImportDetailQuery();
            query.ImportId = ViewModel.VisitorImport.Id;

            //搜索条件的组合 
            query.Name = ViewModel.VisitorImportDetailQuery.Name;
            query.Phone = ViewModel.VisitorImportDetailQuery.Phone;
            query.Status = ViewModel.VisitorImportDetailQuery.Status;
            query.IcCard = ViewModel.VisitorImportDetailQuery.IcCard;

            query.Page = page;
            query.Size = size;

            var qds = await _visitorApi.GetVisitorImportDetailAsync(query);
            if (qds?.List?.FirstOrDefault() == null)
            {
                ViewModel.VisitorImportDetails = new ObservableCollection<VisitorImportDetailViewModel>();
            }
            else
            {
                var results = new ObservableCollection<VisitorImportDetailViewModel>();
                foreach (var item in qds.List)
                {
                    var result = new VisitorImportDetailViewModel();
                    result.Id = item.Id;
                    result.Name = item.Name;
                    result.Phone = item.Phone;
                    result.CardText = item.CardText;
                    result.BeVisitStaffName = item.BeVisitStaffName;
                    result.BeVisitCompanyName = item.BeVisitCompanyName;
                    result.Reason = item.Reason;
                    result.VisitDate = item.VisitDate;
                    result.AuthMsg = item.AuthMsg;
                    result.Status = item.Status;
                    result.StatusText = item.StatusText;
                    result.ServerFaceImg = item.ServerFaceImg;
                    result.Failure = item.Failure;
                    result.FullVisitDate = item.StartVisitDate + "~" + item.EndVisitDate;

                    var isExists = ViewModel.CheckedIds.Any(x => x.Equals(item.Id));
                    if (isExists)
                    {
                        result.IsChecked = true;
                    }

                    results.Add(result);
                }
                ViewModel.VisitorImportDetails = results;
            }

            return qds;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.CheckedAll();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ViewModel.UncheckedAll();
        }
    }
}
