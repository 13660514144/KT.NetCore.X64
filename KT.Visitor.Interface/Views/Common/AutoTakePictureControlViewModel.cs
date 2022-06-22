using KT.Common.WpfApp.ViewModels;
using KT.Visitor.Common.Settings;
using KT.Visitor.Common.ViewModels;
using Panuon.UI.Silver.Core;
using System.Drawing;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    public class AutoTakePictureControlViewModel : BindableBase
    {
        private bool _isTakingPicture = true;
        private bool _isCheckPhoto;
        private readonly AppSettings _appSettings;

        public AutoTakePictureControlViewModel(AppSettings appSettings)
        {
            _appSettings = appSettings;

            CameraTransformAngle = _appSettings.CameraTransformAngle;
        }

        private double _cameraTransformAngle;

        public double CameraTransformAngle
        {
            get => _cameraTransformAngle;
            set
            {
                SetProperty(ref _cameraTransformAngle, value);
            }
        }
        /// <summary>
        /// 抓取的照片
        /// </summary>
        private CaptureImageViewModel _captureImage;

        /// <summary>
        /// 是否已经拍照完成
        /// </summary>
        public bool IsTakingPicture
        {
            get
            {
                return _isTakingPicture;
            }

            set
            {
                SetProperty(ref _isTakingPicture, value);
            }
        }

        public CaptureImageViewModel CaptureImage
        {
            get
            {
                return _captureImage;
            }

            set
            {
                SetProperty(ref _captureImage, value);
            }
        }

        public bool IsCheckPhoto
        {
            get
            {
                return _isCheckPhoto;
            }

            set
            {
                SetProperty(ref _isCheckPhoto, value);
            }
        }
    }
}
