using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.Utils;

using KT.Proxy.BackendApi.Apis;

using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Settings;
//using KT.Visitor.Common.Tools.ArcIdMatch.ArcIdSdk.Helpers; //X86 FACE
//using KT.Visitor.Common.Tools.FacePro;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Views.Register;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFMediaKit.DirectShow.Controls;

namespace KT.Visitor.Interface.Views.Common
{
    /// <summary>
    /// CameraTakePhotoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CameraTakePhotoWindow : WindowX
    {
        private CameraTakePhotoWindowViewModel _viewModel { get; set; }

        public bool IsCheckPhoto { get; set; }

        private IUploadImgApi _uploadImgApi;
        private ConfigHelper _configHelper;
        private AppSettings _appSettings;
        private ILogger _logger;
        private IFunctionApi _functionApi;
        private IdCardCheckHelper _idCardCheckHelper;
        private DialogHelper _dialogHelper;
        private UploadPhotoHelper _uploadPhotoHelper;
        //private ArcIdSdkHelper _arcIdCardHelper; //x86
        //X64
        //private FaceProvider _faceProvider;
        //X64
        public CameraTakePhotoWindow()
        {
            InitializeComponent();

            _uploadImgApi = ContainerHelper.Resolve<IUploadImgApi>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _idCardCheckHelper = ContainerHelper.Resolve<IdCardCheckHelper>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            _uploadPhotoHelper = ContainerHelper.Resolve<UploadPhotoHelper>();
            //_arcIdCardHelper = ContainerHelper.Resolve<ArcIdSdkHelper>(); //x86
            //x64
            //_faceProvider = ContainerHelper.Resolve<FaceProvider>();
            //x64
            _viewModel = ContainerHelper.Resolve<CameraTakePhotoWindowViewModel>();
            this.DataContext = _viewModel;

            this.Loaded += Window_Loaded;
            this.Unloaded += Window_Unloaded;
            this.Closed += Window_Closed;
        }

        /// <summary>
        /// 抓取的照片
        /// </summary>
        public CaptureImageViewModel CaptureImage { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowHelper.ReleaseFullWindow(this, _appSettings.IsFullScreen);

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

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            captureElement.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            captureElement.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        /// <summary>
        /// 进入拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnPZ_Click(object sender, RoutedEventArgs e)
        {
            // captureElement. 怎么抓取高清的原始图像           
            RenderTargetBitmap bmp = new RenderTargetBitmap(
                (int)(captureElement.ActualWidth*1.35),
                (int)(captureElement.ActualHeight*1.15),
                128, 128, PixelFormats.Default);

            //为避免抓不全的情况，需要在Render之前调用Measure、Arrange
            //为避免VideoCaptureElement显示不全，需要把
            //VideoCaptureElement的Stretch="Fill"
            captureElement.Measure(captureElement.RenderSize);
            captureElement.Arrange(new Rect(captureElement.RenderSize));
            bmp.Render(captureElement);
            
            // 临时保存图片
            PngBitmapEncoder encode = new PngBitmapEncoder();
            encode.Frames.Add(BitmapFrame.Create(bmp));
            using (var file = File.OpenWrite("photo.jpg"))
            {
                encode.Save(file);
                file.Dispose();
            }
            
            //人脸检测
       
            //Image p2 = _faceProvider.CropImage("photo.jpg");//x64
            //var p3= _faceProvider.GetFaceFeature("face-demo.jpg");//x64
            //p2.Save("photo1.jpg");
            //p2.Dispose();
            GC.Collect();//释放资源
            //
            captureElement.Pause();

            await UploadImageAsync(bmp);
        }


        /// <summary>
        /// 身份证照片  
        /// </summary>
        public Bitmap IdCardImage { get; set; }

        /// <summary>
        /// 拍照之后做人证对比
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        private async Task UploadImageAsync(RenderTargetBitmap bmp)
        {
            var imageSource = BitmapFrame.Create(bmp);
            var bitmap = ImageConvert.ImageSourceToBitmap(imageSource);
            if (bitmap == null)
            {
                captureElement.Play();
                return;
            }
            /* tmp test*/
            //IdCardImage = Image.FromFile("photo.jpg") as Bitmap;            
            /* tmp test*/
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
            var imageUrl = await _uploadPhotoHelper.UploadPortraitAsync(bitmap, IsCheckPhoto);

            if (!this.IsActive)
            {
                _logger.LogError($"拍照窗口已关闭！");
                return;
            }

            //返回照片地址
            CaptureImage = new CaptureImageViewModel(imageUrl, bitmap);

            this.DialogResult = true;
        }

    }
}
