using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;

namespace KT.Quanta.Model.Kone
{
    public class DopGlobalDefaultAccessFloorMaskModel
    {
        /// <summary>
        /// Id主键
        /// </summary>
        public string Id { get; set; }
        public string FloorId { get; set; }
        public FloorModel Floor { get; set; }
        public bool IsDestinationFront { get; set; }
        public bool IsDestinationRear { get; set; }
        public bool IsSourceFront { get; set; }
        public bool IsSourceRear { get; set; }
    }
}
