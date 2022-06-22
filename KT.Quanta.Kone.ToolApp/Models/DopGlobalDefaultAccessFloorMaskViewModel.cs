using KT.Common.WpfApp.ViewModels;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class DopGlobalDefaultAccessFloorMaskViewModel : BindableBase
    {
        private string _id;
        private string _floorId;
        private FloorViewModel _floor;
        private bool _isDestinationFront = true;
        private bool _isDestinationRear;
        private bool _isSourceFront;
        private bool _isSourceRear;

        /// <summary>
        /// Id主键
        /// </summary>
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

        public bool IsDestinationFront
        {
            get => _isDestinationFront;
            set
            {
                SetProperty(ref _isDestinationFront, value);
            }
        }

        public bool IsDestinationRear
        {
            get => _isDestinationRear;
            set
            {
                SetProperty(ref _isDestinationRear, value);
            }
        }

        public bool IsSourceFront
        {
            get => _isSourceFront;
            set
            {
                SetProperty(ref _isSourceFront, value);
            }
        }

        public bool IsSourceRear
        {
            get => _isSourceRear;
            set
            {
                SetProperty(ref _isSourceRear, value);
            }
        }
    }
}
