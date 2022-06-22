namespace KT.Elevator.Unit.Entity.Models
{
    /// <summary>
    /// 电梯真实楼层
    /// </summary>
    public class ElevatorFloorModel
    {
        public long Floor { get; set; }

        public bool IsFront { get; set; }

        public bool IsRear { get; set; }

        public static ElevatorFloorModel Set(long floorRealId, bool isFront, bool isRear)
        {
            var result = new ElevatorFloorModel();
            result.Floor = floorRealId;
            result.IsFront = isFront;
            result.IsRear = isRear;

            return result;
        }

        /// <summary>
        /// 楼层权限设置
        /// </summary>
        /// <param name="floorRealId"></param>
        /// <param name="floorIsFront">楼层前门</param>
        /// <param name="floorIsRear">楼层后门</param>
        /// <param name="isFront">权限前门</param>
        /// <param name="isRear">权限后门</param>
        /// <returns></returns>
        public static ElevatorFloorModel Set(long floorRealId, bool floorIsFront, bool floorIsRear, bool isFront, bool isRear)
        {
            var result = new ElevatorFloorModel();
            result.Floor = floorRealId;
            result.IsFront = isFront && floorIsFront;
            result.IsRear = isRear && floorIsRear;

            return result;
        }

        public static ElevatorFloorModel Set(ElevatorFloorModel sourceFloor, bool? isFront, bool? isRear)
        {
            if (isFront == null)
            {
                //方向不变
            }
            else if (sourceFloor.IsFront && isFront.Value)
            {
                sourceFloor.IsFront = true;
            }
            else
            {
                sourceFloor.IsFront = false;
            }

            if (isRear == null)
            {
                //方向不变
            }
            else if (sourceFloor.IsRear && isRear.Value)
            {
                sourceFloor.IsRear = true;
            }
            else
            {
                sourceFloor.IsRear = false;
            }

            return sourceFloor;
        }
    }
}
