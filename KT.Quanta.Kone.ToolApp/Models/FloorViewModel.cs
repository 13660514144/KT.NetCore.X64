using KT.Common.WpfApp.ViewModels;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class FloorViewModel : BindableBase
    {
        private string _id;
        private string _name;
        private int? _physicsFloor;
        private bool _isFront;
        private bool _isRear;

        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public bool IsFront
        {
            get
            {
                return _isFront;
            }

            set
            {
                SetProperty(ref _isFront, value);
            }
        }

        public bool IsRear
        {
            get
            {
                return _isRear;
            }

            set
            {
                SetProperty(ref _isRear, value);
            }
        }

        public int? PhysicsFloor
        {
            get
            {
                return _physicsFloor;
            }

            set
            {
                SetProperty(ref _physicsFloor, value);
            }
        }
    }
}
