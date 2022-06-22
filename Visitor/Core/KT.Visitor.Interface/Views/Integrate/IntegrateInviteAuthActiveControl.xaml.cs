using CommonUtils;
using KT.Common.Core.Exceptions;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.Data.Enums;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Tools.Printer.DocumentRenderer;
using KT.Visitor.Interface.Tools.Printer.Models;
using KT.Visitor.Interface.Tools.Printer.PrintOperator;
using KT.Visitor.Interface.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Auth
{
    /// <summary>
    /// InviteAuthActivePage.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateInviteAuthActiveControl : UserControl
    {
        private InviteAuthActivePageViewModel ViewModel;
        private VisitorAuthApi _visitorAuthApi;
        private UploadImgApi _uploadImgApi;
        private MainFrameHelper _mainFrameHelper;
        private SmallTicketOperator _smallTicketOperator;
        private FrontBaseApi _frontBaseApi;

        public IntegrateInviteAuthActiveControl(InviteAuthActivePageViewModel viewModel,
            VisitorAuthApi visitorAuthApi,
            UploadImgApi uploadImgApi,
            MainFrameHelper mainFrameHelper,
            SmallTicketOperator smallTicketOperator,
            FrontBaseApi frontBaseApi)
        {
            InitializeComponent();

            ViewModel = viewModel;
            _visitorAuthApi = visitorAuthApi;
            _uploadImgApi = uploadImgApi;
            _mainFrameHelper = mainFrameHelper;
            _smallTicketOperator = smallTicketOperator;
            _frontBaseApi = frontBaseApi;

            DataContext = ViewModel;
            Loaded += Page_Loaded;
        }
        public VisitorInfoModel Record { get; set; }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Record != null)
            {
                Record.BeVisitStaffName = "被访问人:" + Record.BeVisitStaffName;
                Record.BeVisitCompanyName = "公司:" + Record.BeVisitCompanyName;
                Record.BeVisitCompanyLocation = "来访地点:" + Record.BeVisitCompanyLocation;
                Record.Name = "访客姓名:" + Record.Name;
                Record.Phone = "手机号:" + Record.Phone;
                Record.IcCard = "IC卡:" + Record.IcCard;
                Record.VisitDate = "来访时间:" + Record.VisitDate;

                ViewModel.Record = Record;
            }
        }


        private void btn_auth_Click(object sender, RoutedEventArgs e)
        {
            _ = AuthAsync();
        }

        private async Task AuthAsync()
        {
            var authTypes = ViewModel.AuthTypes.Where(x => x.IsChecked).Select(x => x.Id).ToArray();

            string faceImage = string.Empty;
            //设置人脸头像
            var takePictureVM = ViewModel.TakePictureControl.ViewModel;
            faceImage = takePictureVM.ImageUrl;
            bool isFace = authTypes.Contains(AuthModelEnum.FACE.Value);
            if (isFace)
            {
                //人脸授权需拍照
                if (string.IsNullOrEmpty(faceImage))
                {
                    throw CustomException.Run("抱歉，人脸授权请先拍照！");
                }
            }
            //else
            //{
            //    //非人脸授权自动拍照
            //    var tp = ctl_TakePicture.TakePicture();
            //    faceImage = tp.ImageUrl;
            //}

            var _ls = new List<dynamic>();
            _ls.Add(new { id = Record.Id, ic = txt_vis_ic.Text, faceImg = faceImage });
            var results = await _visitorAuthApi.AuthAsync(new { authTypes = authTypes.ToArray(), visitors = _ls });
            //打印 在次判断是否打印二维码
            if (authTypes.Contains(AuthModelEnum.QR.Value))
            {
                //CommonHelper.PrintQR(lst); 
                foreach (var item in results)
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

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            _mainFrameHelper.LinkVisitorRegister();
        }
    }
}
