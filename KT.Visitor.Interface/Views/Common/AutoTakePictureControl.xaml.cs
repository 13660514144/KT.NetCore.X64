using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.Utils;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Settings;
using KT.Visitor.Common.Tools.ArcIdMatch.ArcIdSdk.Helpers;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Views.Register;
using Microsoft.Extensions.Logging;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFMediaKit.DirectShow.Controls;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    /// <summary>
    /// AutoTakePictureControl.xaml 的交互逻辑
    /// </summary>
    public partial class AutoTakePictureControl : UserControl
    {
        public AutoTakePictureControlViewModel ViewModel;

        private DialogHelper _dialogHelper;
        private UploadImgApi _uploadImgApi;
        private AppSettings _appSettings;
        private ILogger _logger;
        private IdCardCheckHelper _idCardCheckHelper;
        private IFunctionApi _functionApi;
        private UploadPhotoHelper _uploadPhotoHelper;
        private ArcIdSdkHelper _arcIdCardHelper;

        public AutoTakePictureControl()
        {
            InitializeComponent();

            //ViewModel初始化
            ViewModel = ContainerHelper.Resolve<AutoTakePictureControlViewModel>();

            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            _uploadImgApi = ContainerHelper.Resolve<UploadImgApi>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _idCardCheckHelper = ContainerHelper.Resolve<IdCardCheckHelper>();
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _uploadPhotoHelper = ContainerHelper.Resolve<UploadPhotoHelper>();
            _arcIdCardHelper = ContainerHelper.Resolve<ArcIdSdkHelper>();

            this.DataContext = this.ViewModel;

            this.Loaded += AutoTakePictureControl_Loaded;
            this.Unloaded += AutoTakePictureControl_Unloaded;
        }

        private void AutoTakePictureControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitCamera();
        }

        private void AutoTakePictureControl_Unloaded(object sender, RoutedEventArgs e)
        {
            CloseCamera();
        }

        public void InitCamera()
        {
            _logger.LogInformation($"正在开启相机：index:{_appSettings.CameraIndex} names:{MultimediaUtil.VideoInputNames.ToList().RightDecorates("，")} ");
            if (MultimediaUtil.VideoInputNames.Length > _appSettings.CameraIndex)
            {
                // 第0个摄像头为默认摄像头
                captureElement.VideoCaptureSource = MultimediaUtil.VideoInputNames[_appSettings.CameraIndex];
                captureElement.Play();
            }
            else
            {
                MessageBox.Show($"获取可用摄像头出错：count:{MultimediaUtil.VideoInputNames.Length} index:{_appSettings.CameraIndex} ");
            }
        }

        public void CloseCamera()
        {
            captureElement.Close();
        }

        private async void btn_TakePicture_Click(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.IsTakingPicture)
            {
                captureElement.Play();
                ViewModel.IsTakingPicture = true;
                return;
            }

            // captureElement. 怎么抓取高清的原始图像           
            RenderTargetBitmap bmp = new RenderTargetBitmap(
                (int)captureElement.ActualWidth,
                (int)captureElement.ActualHeight,
                96, 96, PixelFormats.Default);

            //为避免抓不全的情况，需要在Render之前调用Measure、Arrange
            //为避免VideoCaptureElement显示不全，需要把
            //VideoCaptureElement的Stretch="Fill"
            captureElement.Measure(captureElement.RenderSize);
            captureElement.Arrange(new Rect(captureElement.RenderSize));
            bmp.Render(captureElement);

            //BitmapEncoder encoder = new JpegBitmapEncoder();
            //encoder.Frames.Add();
            //encoder.Save(ms);

            captureElement.Pause();

            await UploadImageAsync(bmp);

            ViewModel.IsTakingPicture = false;
        }

        /// <summary>
        /// 身份证照片
        /// </summary>
        public Bitmap IdCardImage { get; set; }

        private async Task UploadImageAsync(RenderTargetBitmap bmp)
        {
            var imageSource = BitmapFrame.Create(bmp);
            var bitmap = ImageConvert.ImageSourceToBitmap(imageSource);
            if (bitmap == null)
            {
                return;
            }

            //人证比对 
            var visitorConfig = await _functionApi.GetConfigParmsAsync();
            //人证比对
            if (visitorConfig?.OpenVisitorCheck == true)
            {
                var openVisitorCheck = _idCardCheckHelper.IdCardFaceCheck(IdCardImage, bitmap, true);
                if (openVisitorCheck != null)
                {
                    var warnWindow = ContainerHelper.Resolve<IdCardCheckWarnWindow>();
                    warnWindow.ViewModel.IdCardCheck = openVisitorCheck;
                    var warnResult = _dialogHelper.ShowDialog(warnWindow);
                    if (!warnResult.HasValue || !warnResult.Value)
                    {
                        //重新拍照
                        captureElement.Play();
                        return;
                    }
                    if (openVisitorCheck.Face != null)
                    {
                        bitmap = openVisitorCheck.Face;
                    }
                }
            }
            else
            {
                /*var cutFace = _arcIdCardHelper.GetCutFace(bitmap);
                if (cutFace.Face != null)
                {
                    bitmap = cutFace.Face;
                }*/
            }

            //上传图头像服务器
            var imageUrl = await _uploadPhotoHelper.UploadPortraitAsync(bitmap, ViewModel.IsCheckPhoto);

            //返回照片地址
            ViewModel.CaptureImage = new CaptureImageViewModel(imageUrl, bitmap);
        }
    }
}
