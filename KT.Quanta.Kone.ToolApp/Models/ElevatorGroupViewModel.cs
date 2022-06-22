using KT.Common.WpfApp.ViewModels;
using System.Collections.ObjectModel;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class ElevatorGroupViewModel : BindableBase
    {
        private string _id;
        private string _name;
        public string _edificeId;
        private ObservableCollection<ElevatorGroupFloorViewModel> _elevatorGroupfloors;

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

        public string EdificeId
        {
            get => _edificeId;
            set => SetProperty(ref _edificeId, value);
        }

        public ObservableCollection<ElevatorGroupFloorViewModel> ElevatorGroupFloors
        {
            get
            {
                return _elevatorGroupfloors;
            }

            set
            {
                SetProperty(ref _elevatorGroupfloors, value);
            }
        }
    }
}
