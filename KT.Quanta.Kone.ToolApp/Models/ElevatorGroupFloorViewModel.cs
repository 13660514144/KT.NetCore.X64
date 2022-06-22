using KT.Common.WpfApp.ViewModels;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class ElevatorGroupFloorViewModel : BindableBase
    {
        private string _id;
        private int? _realFloorId;
        private FloorViewModel _floor;

        public string Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public int? RealFloorId
        {
            get => _realFloorId;
            set
            {
                SetProperty(ref _realFloorId, value);
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
    }
}
