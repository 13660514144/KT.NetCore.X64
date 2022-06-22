using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Manage.Entity.Entities
{
    /// <summary>
    /// 大厦
    /// </summary>
    [Table("EDIFICE")]
    public class EdificeEntity : BaseEntity
    {
        /// <summary>
        /// 大厦名称
        /// </summary>
        public string Name { get; set; }
    }
}
