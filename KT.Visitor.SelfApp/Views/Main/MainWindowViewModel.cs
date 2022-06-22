using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Visitor.SelfApp.Views.Setting;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.SelfApp.Views
{
    public class MainWindowViewModel : BindableBase
    {
        private double _pictureCarouselPartHeight;
        private bool _pictureCarouseIsShow;
        private PictureCarouselControl _pictureCarousel;

        private readonly SelfAppSettings _selfAppSettings;

        public MainWindowViewModel()
        {
            _selfAppSettings = ContainerHelper.Resolve<SelfAppSettings>();

            PictureCarousel = ContainerHelper.Resolve<PictureCarouselControl>();

            PictureCarouselPartHeight = _selfAppSettings.PictureCarouselPartHeight;
            PictureCarouseIsShow = _selfAppSettings.PictureCarouseIsShow;
        }

        public double PictureCarouselPartHeight
        {
            get
            {
                return _pictureCarouselPartHeight;
            }

            set
            {
                SetProperty(ref _pictureCarouselPartHeight, value);
            }
        }

        public bool PictureCarouseIsShow
        {
            get
            {
                return _pictureCarouseIsShow;
            }

            set
            {
                SetProperty(ref _pictureCarouseIsShow, value);
            }
        }

        public PictureCarouselControl PictureCarousel
        {
            get
            {
                return _pictureCarousel;
            }

            set
            {
                _pictureCarousel = value;
            }
        }
    }
}
