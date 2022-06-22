using CommonUtils;
using KT.Visitor.Interface.Views.Common;
using Prism.Ioc;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    /// <summary>
    /// TakePictureControl.xaml 的交互逻辑
    /// </summary>
    public partial class TakePictureControl : UserControl
    {
        public TakePictureControlViewModel ViewModel;

        private IContainerProvider _containerProvider;

        public TakePictureControl(TakePictureControlViewModel viewModel,
            IContainerProvider containerProvider)
        {
            InitializeComponent();

            ViewModel = viewModel;
            _containerProvider = containerProvider;

            this.DataContext = ViewModel;
        }

        public void Init()
        {

        }

        /// <summary>
        /// 拍照,拍照后销毁界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_TakePicture_Click(object sender, RoutedEventArgs e)
        {
            var cameraTakePhoto = _containerProvider.Resolve<CameraTakePhotoWindow>();
            var result = cameraTakePhoto.ShowDialog();
            if (result.HasValue && result.Value && cameraTakePhoto.CaptureImage != null)
            {
                //显示已拍的图片
                ViewModel.Picture = cameraTakePhoto.CaptureImage.Image;
                ViewModel.ImageUrl = cameraTakePhoto.CaptureImage.ImageUrl;
                ViewModel.IsTakedPicture = true;

                image_TackPicture.Source = ImageConvert.ImageToBitmapImage(cameraTakePhoto.CaptureImage.Image, ImageFormat.Png);
                btn_TakePicture.Visibility = Visibility.Collapsed;
            }
        }
    }
}
