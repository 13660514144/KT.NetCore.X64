using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Manage.Service.Entities
{
    /// <summary>
    /// 电梯组
    /// </summary>
    [Table("ELEVATOR_GROUP")]
    public class ElevatorGroupEntity : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string BrandModel { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 所在大厦
        /// </summary>
        public EdificeEntity Edifice { get; set; }

        /// <summary>
        /// 可去楼层
        /// </summary>
        public List<ElevatorGroupRelationFloorEntity> RelationFloors { get; set; }

        /// <summary>
        /// 电梯服务器
        /// </summary>
        public List<ElevatorServerEntity> ElevatorServers { get; set; }

        /// <summary>
        /// 关联电梯映射
        /// </summary>
        public List<ElevatorInfoEntity> ElevatorInfos { get; set; }
    }
}
