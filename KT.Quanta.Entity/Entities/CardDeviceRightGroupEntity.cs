using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Service.Entities
{
    /// <summary>
    /// 设备权限组
    /// </summary>
    [Table("CARD_DEVICE_RIGHT_GROUP")]
    public class CardDeviceRightGroupEntity : BaseEntity
    {
        /// <summary>
        /// 设备权限组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 关联读卡器设备
        /// </summary>
        public List<CardDeviceRightGroupRelationCardDeviceEntity> RelationCardDevices { get; set; }

        /// <summary>
        /// 关联权限
        /// </summary>
        public List<PassRightRelationCardDeviceRightGroupEntity> RelationPassRights { get; set; }
    }
}
