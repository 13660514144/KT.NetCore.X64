using KT.Common.WpfApp.Helpers;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Views.Common;
using Prism.Events;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    /// <summary>
    /// ShowTakePictureControl.xaml 的交互逻辑
    /// </summary>
    public partial class ShowTakePictureControl : UserControl
    {
        public ShowTakePictureControlViewModel ViewModel;

        private DialogHelper _dialogHelper;

        private IEventAggregator _eventAggregator;

        public ShowTakePictureControl()
        {
            InitializeComponent();

            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<RegistedSuccessEvent>().Subscribe(EndRegisted);

            //ViewModel初始化
            ViewModel = ContainerHelper.Resolve<ShowTakePictureControlViewModel>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();

            this.DataContext = this.ViewModel;
        }

        /// <summary>
        /// 身份证照片
        /// </summary>
        public Bitmap _idCardImage { get; set; }

        public void Init(Bitmap idCardImage)
        {
            _idCardImage = idCardImage;
        }

        private void EndRegisted()
        {
            image_TackPicture.Source = null;
            btn_TakePicture.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 拍照,拍照后销毁界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_TakePicture_Click(object sender, RoutedEventArgs e)
        {
            var cameraTakePhoto = ContainerHelper.Resolve<CameraTakePhotoWindow>();
            cameraTakePhoto.IdCardImage = _idCardImage;
            var result = _dialogHelper.ShowDialog(cameraTakePhoto);
            if (result.HasValue && result.Value && cameraTakePhoto.CaptureImage != null)
            {
                //显示已拍的图片
                this.ViewModel.CaptureImage = cameraTakePhoto.CaptureImage;
                this.ViewModel.IsTakingPicture = false;
                
                System.Drawing.Image imgSource = System.Drawing.Image.FromFile("photo.jpg");
                Bitmap outBmp = new Bitmap(250, 200);
                Graphics g = Graphics.FromImage(outBmp);
                g.Clear(Color.Transparent);
                // 设置画布的描绘质量   
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgSource, new Rectangle(0, 0, 250, 200 + 1), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
                g.Dispose();
                imgSource.Dispose();
                //outBmp.Save("phothchange.jpg");

                Bitmap bitmapSource = new Bitmap(outBmp.Width, outBmp.Height);
                int i, j;
                for (i = 0; i < outBmp.Width; i++)
                {
                    for (j = 0; j < outBmp.Height; j++)
                    {
                        Color pixelColor = outBmp.GetPixel(i, j);
                        Color newColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
                        bitmapSource.SetPixel(i, j, newColor);
                    }
                }
                outBmp.Dispose();
                MemoryStream ms = new MemoryStream();
                bitmapSource.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(ms.ToArray());
                bitmapImage.EndInit();         
                image_TackPicture.Source = bitmapImage;
                bitmapSource.Dispose();
                //image_TackPicture.Source =cameraTakePhoto.CaptureImage.Image;
                btn_TakePicture.Visibility = Visibility.Collapsed;
                GC.Collect();
            }
        }
    }
}
