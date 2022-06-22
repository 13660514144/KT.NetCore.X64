using KT.Common.WpfApp.ViewModels;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class HandleElevatorDeviceViewModel : BindableBase
    {
        private string _id;
        private string _name;
        private string _floorId;
        private string _floorName;
        private string _physicsFloor;
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
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
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
        public string FloorName
        {
            get => _floorName;
            set
            {
                SetProperty(ref _floorName, value);
            }
        }
        public string PhysicsFloor
        {
            get => _physicsFloor;
            set
            {
                SetProperty(ref _physicsFloor, value);
            }
        }
        public bool IsFront
        {
            get => _isFront;
            set
            {
                SetProperty(ref _isFront, value);
            }
        }
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
