using KT.Common.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class PassRightAccessibleFloorDetailViewModel : BindableBase
    {
        private string _id;
        private string _floorId;
        private FloorViewModel _floor;
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

        public string FloorId
        {
            get => _floorId;
            set
            {
                SetProperty(ref _floorId, value);
            }
        }

        public FloorViewModel Floor
        {
            get => _floor;
            set
            {
                SetProperty(ref _floor, value);
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
