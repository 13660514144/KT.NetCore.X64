using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Interface.Events;
using Panuon.UI.Silver.Core;
using Prism.Events;
using System;
using System.Drawing;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    public class ShowTakePictureControlViewModel : BindableBase
    {

        private bool _isTakingPicture = false;
        /// <summary>
        /// 抓取的照片
        /// </summary>
        private CaptureImageViewModel _captureImage;

        private IEventAggregator _eventAggregator;

        public ShowTakePictureControlViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<RegistedSuccessEvent>().Subscribe(Clear);
        }

        public void Clear()
        {
            IsTakingPicture = false;
            CaptureImage = null;
        }

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
    }
}
