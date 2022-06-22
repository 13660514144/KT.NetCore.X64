using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secodary.Entity.Entities
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [Table("SYSTEM_CONFIG")]
    public class UnitSystemConfigEntity : BaseEntity
    {
        /// <summary>
        /// Key值
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Value值
        /// </summary>
        public string Value { get; set; }

        public UnitSystemConfigEntity()
        {

        }
    }
}
