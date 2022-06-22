using CommonUtils;
using KT.Proxy.WebApi.Backend.Apis;
using Panuon.UI.Silver;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFMediaKit.DirectShow.Controls;

namespace KT.Visitor.Interface.Views.Integrate
{
    /// <summary>
    /// AddAccompanyPhotoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddAccompanyPhotoWindow : WindowX
    {
        public AddAccompanyPhotoWindowViewModel ViewModel { get; set; }

        private UploadImgApi _uploadImgApi;
        public AddAccompanyPhotoWindow(AddAccompanyPhotoWindowViewModel viewModel,
            UploadImgApi uploadImgApi)
        {
            InitializeComponent();

            ViewModel = viewModel;
            _uploadImgApi = uploadImgApi;

            this.DataContext = ViewModel;

            this.Loaded += AddAccompanyPhotoWindow_Loaded;
        }

        public void SetViewModel(AddAccompanyPhotoWindowViewModel viewModel)
        {
            ViewModel = viewModel;
            this.DataContext = ViewModel;
        }

        private void AddAccompanyPhotoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (MultimediaUtil.VideoInputNames.Length > 0)
            {
                // 第0个摄像头为默认摄像头
                captureElement.VideoCaptureSource = MultimediaUtil.VideoInputNames[0];
                captureElement.Play();
            }
            else
            {
                MessageBox.Show("电脑没有安装任何可用摄像头");
            }
        }

        private void BtnPZ_Click(object sender, RoutedEventArgs e)
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

            _ = UploadImageAsync(bmp);
        }

        private async Task UploadImageAsync(RenderTargetBitmap bmp)
        {
            var imageSource = BitmapFrame.Create(bmp);
            var image = ImageConvert.ImageSourceToBitmap(imageSource);
            if (image != null)
            {
                //上传图片到服务器 
                string imageUrl = await _uploadImgApi.UploadPortraitAsync(ImageConvert.ImageToBytes(image));

                //返回照片地址
                var bitmapImage = ImageConvert.ImageToBitmapImage(image, ImageFormat.Png);
                ViewModel.AddIcAccompany(imageUrl, bitmapImage);
            }
        }
    }
}
