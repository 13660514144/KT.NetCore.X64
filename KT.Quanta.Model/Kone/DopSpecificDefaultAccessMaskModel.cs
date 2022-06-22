using KT.Quanta.Service.Models;
using System.Collections.Generic;

namespace KT.Quanta.Model.Kone
{
    public class DopSpecificDefaultAccessMaskModel
    {
        public DopSpecificDefaultAccessMaskModel()
        {
            MaskFloors = new List<DopSpecificDefaultAccessFloorMaskModel>();
        }

        /// <summary>
        /// Id主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 0:连接
        /// 1:断开
        /// </summary>
        public int ConnectedState { get; set; } = -1;

        /// <summary>
        /// 电梯组id
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 电梯组
        /// </summary>
        public ElevatorGroupModel ElevatorGroup { get; set; }

        /// <summary>
        /// 派梯设备id
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }

        /// <summary>
        /// 派梯设备
        /// </summary>
        public HandleElevatorDeviceModel HandleElevatorDevice { get; set; }

        /// <summary>
        /// Dop Specific Mask可去楼层
        /// </summary>
        public List<DopSpecificDefaultAccessFloorMaskModel> MaskFloors { get; set; }
    }
}
