using KT.Common.WpfApp.Attributes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFMediaKit.DirectShow.Controls;

namespace KT.TestTool.TestApp.Views.Common
{
    /// <summary>
    /// PhotographControl.xaml 的交互逻辑
    /// </summary>
    [NavigationItem]
    public partial class PhotographControl : UserControl
    {
        /// <summary>
        /// 不可用多个照相机
        /// </summary>
        public PhotographControl()
        {
            InitializeComponent();
            this.Loaded += VideoCaptureControl_Loaded;
        }


        private void VideoCaptureControl_Loaded(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// 暂停
        /// </summary>
        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            // captureElement. 怎么抓取高清的原始图像           
            RenderTargetBitmap bmp = new RenderTargetBitmap(
                (int)captureElement.ActualWidth,
                (int)captureElement.ActualHeight,
                96, 96, PixelFormats.Default);

            //为避免抓不全的情况，需要在Render之前调用Measure、Arrange
            //为避免VideoCaptureElement显示不全，需要把
            //VideoCaptureElement的Stretch="Fill"
            //captureElement.Measure(captureElement.RenderSize);
            //captureElement.Arrange(new Rect(captureElement.RenderSize));
            bmp.Render(captureElement);

            //BitmapEncoder encoder = new JpegBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(bmp));
            //encoder.Save(ms);

            captureElement.Pause();
        }

        /// <summary>
        /// 重播
        /// </summary>
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            captureElement.Play();
        }
    }
}

        ///// <summary>
        ///// 暂停
        ///// </summary>
        //private void Pause_Click(object sender, RoutedEventArgs e)
        //{
        //    // captureElement. 怎么抓取高清的原始图像           
        //    RenderTargetBitmap bmp = new RenderTargetBitmap(
        //        (int)captureElement.ActualWidth,
        //        (int)captureElement.ActualHeight,
        //        96, 96, PixelFormats.Default);

        //    //为避免抓不全的情况，需要在Render之前调用Measure、Arrange
        //    //为避免VideoCaptureElement显示不全，需要把
        //    //VideoCaptureElement的Stretch="Fill"
        //    captureElement.Measure(captureElement.RenderSize);
        //    captureElement.Arrange(new Rect(captureElement.RenderSize));
        //    bmp.Render(captureElement);

        //    //BitmapEncoder encoder = new JpegBitmapEncoder();
        //    //encoder.Frames.Add(BitmapFrame.Create(bmp));
        //    //encoder.Save(ms);

        //    captureElement.Pause();
        //}