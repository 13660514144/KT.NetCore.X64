using KT.Common.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class HandleElevatorDeviceAuxiliaryViewModel : BindableBase
    {
        private string _id;
        private string _handleElevatorDeviceId;
        private bool _isFront;
        private bool _isRear;

        public string Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public string HandleElevatorDeviceId
        {
            get => _handleElevatorDeviceId;
            set
            {
                SetProperty(ref _handleElevatorDeviceId, value);
            }
        }

        /// <summary>
        /// 是否开启前门
        /// </summary>
        public bool IsFront
        {
            get => _isFront;
            set
            {
                SetProperty(ref _isFront, value);
            }
        }

        /// <summary>
        /// 是否开启前门
        /// </summary>
        public bool IsRear
        {
            get => _isRear;
            set
            {
                SetProperty(ref _isRear, value);
            }
        }

    }
}
