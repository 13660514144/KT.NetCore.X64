using KT.Quanta.Service.Models;

namespace KT.Quanta.Model.Elevator
{
    public class ElevatorGroupFloorModel : BaseQuantaModel
    {
        /// <summary>
        /// 电梯组
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 电梯组
        /// </summary>
        public ElevatorGroupModel ElevatorGroup { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public string FloorId { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public FloorModel Floor { get; set; }

        /// <summary>
        /// 梯控楼层Id
        /// </summary>
        public string RealFloorId { get; set; }
    }
}
