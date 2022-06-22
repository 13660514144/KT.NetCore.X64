using KT.Common.Data.Models;
using KT.Quanta.Service.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Quanta.Entity.Kone
{
    [Table("DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK")]
    public class DopGlobalDefaultAccessFloorMaskEntity : BaseEntity
    {
        public string AccessMaskId { get; set; }
        public DopGlobalDefaultAccessMaskEntity AccessMask { get; set; }
        public string FloorId { get; set; }
        public FloorEntity Floor { get; set; }
        public bool IsDestinationFront { get; set; }
        public bool IsDestinationRear { get; set; }
        public bool IsSourceFront { get; set; }
        public bool IsSourceRear { get; set; }
    }
}
