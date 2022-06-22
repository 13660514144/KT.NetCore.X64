using KT.Common.Data.Models;
using KT.Quanta.Service.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Quanta.Entity.Kone
{
    [Table("DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK")]
    public class DopSpecificDefaultAccessFloorMaskEntity : BaseEntity
    {
        public string AccessMaskId { get; set; }
        public DopSpecificDefaultAccessMaskEntity AccessMask { get; set; }
        public string FloorId { get; set; }
        public FloorEntity Floor { get; set; }
        public bool IsDestinationFront { get; set; } = true;
        public bool IsDestinationRear { get; set; }
    }
}
