using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.Utils;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Settings;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using System;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFMediaKit.DirectShow.Controls;

namespace KT.Visitor.Interface.Views.Register
{
    /// <summary>
    /// AddAccompanyPhotoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddAccompanyPhotoWindow : WindowX
    {
        public AddAccompanyPhotoWindowViewModel ViewModel { get; set; }

        private bool _isConfirm = false;

        private IUploadImgApi _uploadImgApi;
        private AppSettings _appSettings;
        private ILogger _logger;
        private UploadPhotoHelper _uploadPhotoHelper;

        public AddAccompanyPhotoWindow()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<AddAccompanyPhotoWindowViewModel>();
            _uploadImgApi = ContainerHelper.Resolve<IUploadImgApi>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _uploadPhotoHelper = ContainerHelper.Resolve<UploadPhotoHelper>();

            ViewModel.FocusCardNumberAction = FocusCardNumber;
            ViewModel.TakePhotoAction = TakePhoto;
            this.DataContext = ViewModel;

            this.Loaded += AddAccompanyPhotoWindow_Loaded;

            this.Closed += AddAccompanyCardWindow_Closed;
        }

        private void AddAccompanyCardWindow_Closed(object sender, EventArgs e)
        {
            if (!_isConfirm)
            {
                ViewModel.ClearAccompany();
            }
        }

        public void SetViewModel(AddAccompanyPhotoWindowViewModel viewModel)
        {
            ViewModel = viewModel;
            ViewModel.FocusCardNumberAction = FocusCardNumber;
            ViewModel.TakePhotoAction = TakePhoto;

            this.DataContext = ViewModel;
        }

        public void FocusCardNumber()
        {
            Tb_CardNumber.Focus();
        }

        private void AddAccompanyPhotoWindow_Loaded(object sender, RoutedEventArgs e)
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

        private void TakePhoto()
        {
            BtnPZ_Click(this, null);
        }

        private async void BtnPZ_Click(object sender, RoutedEventArgs e)
        {
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

            //captureElement.Pause();

            await UploadImageAsync(bmp);
        }

        private async Task UploadImageAsync(RenderTargetBitmap bmp)
        {
            var imageSource = BitmapFrame.Create(bmp);
            var bitmap = ImageConvert.ImageSourceToBitmap(imageSource);

            //上传图头像服务器
            var imageUrl = await _uploadPhotoHelper.UploadPortraitAsync(bitmap, ViewModel.IsCheckPhoto);

            //返回照片地址
            var bitmapImage = ImageConvert.ImageToBitmapImage(bitmap, ImageFormat.Png);
            ViewModel.AddIcAccompany(imageUrl, bitmapImage);
        }

        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            _isConfirm = true;
            this.Close();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
