using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Service.Entities
{
    /// <summary>
    /// 电梯映射
    /// </summary>
    [Table("ELEVATOR_INFO")]
    public class ElevatorInfoEntity : BaseEntity
    {
        /// <summary>
        /// 电梯id
        /// </summary>
        public string RealId { get; set; }

        /// <summary>
        /// 电梯名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电梯组Id
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 电梯组
        /// </summary>
        public ElevatorGroupEntity ElevatorGroup { get; set; }
    }
}
