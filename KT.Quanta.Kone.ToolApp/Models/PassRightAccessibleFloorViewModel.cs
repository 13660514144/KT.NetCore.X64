using KT.Common.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class PassRightAccessibleFloorViewModel : BindableBase
    {
        private string _id;
        private ElevatorGroupViewModel _elevatorGroup;
        private string _sign;
        private string _elevatorGroupId;
        private List<PassRightAccessibleFloorDetailViewModel> _passRightAccessibleFloorDetails;

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

        public List<PassRightAccessibleFloorDetailViewModel> PassRightAccessibleFloorDetails
        {
            get => _passRightAccessibleFloorDetails;
            set
            {
                SetProperty(ref _passRightAccessibleFloorDetails, value);
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
