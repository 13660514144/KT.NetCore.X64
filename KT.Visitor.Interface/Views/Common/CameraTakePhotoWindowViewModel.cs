using KT.Common.WpfApp.ViewModels;
using KT.Visitor.Common.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Interface.Views.Common
{
    public class CameraTakePhotoWindowViewModel : BindableBase
    {
        private readonly AppSettings _appSettings;
        public CameraTakePhotoWindowViewModel(AppSettings appSettings)
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

    }
}
