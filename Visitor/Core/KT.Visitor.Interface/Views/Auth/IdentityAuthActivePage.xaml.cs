using CommonUtils;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Tools.Printer.DocumentRenderer;
using KT.Visitor.Interface.Tools.Printer.Models;
using KT.Visitor.Interface.Tools.Printer.PrintOperator;
using KT.Visitor.Interface.Views.Auth.Controls;
using KT.Visitor.Interface.Views.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Auth
{
    /// <summary>
    /// vistorList.xaml 的交互逻辑
    /// </summary>
    public partial class IdentityAuthActivePage : Page
    {
        private IdentityAuthActivePageViewModel _viewModel;
        private VisitorAuthApi _visitorAuthApi;
        private SmallTicketOperator _smallTicketOperator;
        private MainFrameHelper _mainFrameHelper;
        private FrontBaseApi _frontBaseApi;

        public IdentityAuthActivePage(IdentityAuthActivePageViewModel viewModel,
            VisitorAuthApi visitorAuthApi,
            SmallTicketOperator smallTicketOperator,
           MainFrameHelper mainFrameHelper,
           FrontBaseApi frontBaseApi)
        {
            InitializeComponent();

            _viewModel = viewModel;
            _visitorAuthApi = visitorAuthApi;
            _smallTicketOperator = smallTicketOperator;
            _mainFrameHelper = mainFrameHelper;
            _frontBaseApi = frontBaseApi;

            DataContext = _viewModel;
        }

        public string IdNumber { get; set; }
        public string visName { get; set; }
        public List<VisitorInfoModel> records = new List<VisitorInfoModel>();
        private void VistorList_Loaded(object sender, RoutedEventArgs e)
        {
            StvistorList.Children.Clear();
            if (records.Count > 0)
            {
                foreach (var item in records)
                {
                    var ls = new AppointAuthDetailControl(item);
                    StvistorList.Children.Add(ls);
                }
            }
        }

        private void btn_authAll_Click(object sender, RoutedEventArgs e)
        {
            AuthAllAsync();
        }

        private async Task AuthAllAsync()
        {
            var authTypes = new List<string>();
            foreach (var item in _viewModel.AuthTypes)
            {
                if (item.IsChecked)
                {
                    authTypes.Add(item.Id);
                }
            }

            var _ls = new List<dynamic>();
            foreach (var item in records)
            {
                _ls.Add(new { id = item.Id, faceImg = item.FaceImg });
            }

            var result = _visitorAuthApi.AuthAsync(new { authTypes = authTypes.ToArray(), visitors = _ls });
            //打印，再次判断是否打印二维码
            if (authTypes.Contains("QR"))
            {
                //CommonHelper.PrintQR(lst); 
                foreach (var item in result.Result)
                {
                    //打印二维码
                    var imageInfo = await _frontBaseApi.GetQrAsync(item.Qr);
                    var bitmap = ImageConvert.BytesToBitmap(imageInfo.Bytes);

                    _smallTicketOperator.Init(PrintConfig.VISIT_QR_CODE_DOCUMENT_PATH, VisitQRCodePrintModel.FromVisitorResult(item, bitmap), new VisitQRCodeRenderer());
                    await _smallTicketOperator.StartPrintAsync();
                }
            }

            //成功后跳转访客登记页面  
            _mainFrameHelper.Link<SuccessPage>(false);
        }
    }
}
