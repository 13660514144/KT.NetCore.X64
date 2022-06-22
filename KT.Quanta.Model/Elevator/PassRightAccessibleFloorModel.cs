using KT.Quanta.Service.Models;
using System.Collections.Generic;

namespace KT.Quanta.Model.Elevator.Dtos
{
    public class PassRightAccessibleFloorModel : BaseQuantaModel
    {
        public PassRightAccessibleFloorModel()
        {
            PassRightAccessibleFloorDetails = new List<PassRightAccessibleFloorDetailModel>();
        }

        public string Sign { get; set; }

        public string ElevatorGroupId { get; set; }

        public List<PassRightAccessibleFloorDetailModel> PassRightAccessibleFloorDetails { get; set; }
    }
}
