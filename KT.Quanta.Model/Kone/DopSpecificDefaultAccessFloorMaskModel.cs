using KT.Quanta.Service.Models;

namespace KT.Quanta.Model.Kone
{
    public class DopSpecificDefaultAccessFloorMaskModel
    {
        /// <summary>
        /// Id主键
        /// </summary>
        public string Id { get; set; }
        public string FloorId { get; set; }
        public FloorModel Floor { get; set; }
        public bool IsDestinationFront { get; set; } = true;
        public bool IsDestinationRear { get; set; }
    }
}
