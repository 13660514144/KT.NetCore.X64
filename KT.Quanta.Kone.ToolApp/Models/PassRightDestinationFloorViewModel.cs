using KT.Common.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class PassRightDestinationFloorViewModel : BindableBase
    {
        private string _id;
        private ElevatorGroupViewModel _elevatorGroup;
        private string _sign;
        private string _elevatorGroupId;
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

        public string Sign
        {
            get => _sign;
            set
            {
                SetProperty(ref _sign, value);
            }
        }

        public string ElevatorGroupId
        {
            get => _elevatorGroupId;
            set
            {
                SetProperty(ref _elevatorGroupId, value);
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
         

        public ElevatorGroupViewModel ElevatorGroup
        {
            get => _elevatorGroup;
            set
            {
                SetProperty(ref _elevatorGroup, value);
            }
        }
    }
}
