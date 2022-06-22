using KT.Common.WpfApp.ViewModels;
using KT.Quanta.Kone.ToolApp.Enums;
using System.Collections.ObjectModel;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class DopGlobalDefaultAccessMaskViewModel : BindableBase
    {
        private string _id;
        private ConnectedStateEnum _connectedState;
        private string _elevatorGroupId;
        private ObservableCollection<DopGlobalDefaultAccessFloorMaskViewModel> _maskFloors;

        private ElevatorGroupViewModel _elevatorGroup;
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

        /// <summary>
        /// 1:断开
        /// 2:连接
        /// 3:所有
        /// </summary>
        public ConnectedStateEnum ConnectedState
        {
            get => _connectedState;
            set
            {
                SetProperty(ref _connectedState, value);
            }
        }
        /// <summary>
        /// 电梯组Id
        /// </summary>
        public string ElevatorGroupId
        {
            get => _elevatorGroupId;
            set
            {
                SetProperty(ref _elevatorGroupId, value);
            }
        }

        /// <summary>
        /// Dop Specific Mask可去楼层
        /// </summary>
        public ObservableCollection<DopGlobalDefaultAccessFloorMaskViewModel> MaskFloors
        {
            get => _maskFloors;
            set
            {
                SetProperty(ref _maskFloors, value);
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
