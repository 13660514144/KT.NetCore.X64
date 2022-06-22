using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Turnstile.Entity.Entities
{
    /// <summary>
    /// 通行权限关联通道
    /// </summary>
    [Table("PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP")]
    public class PassRightRelationCardDeviceRightGroupEntity : BaseEntity
    {
        public string PassRightId { get; set; }
        /// <summary>
        /// 通行权限
        /// </summary>
        public PassRightEntity PassRight { get; set; }

        public string CardDeviceRightGroupId { get; set; }
        /// <summary>
        /// 权限组
        /// </summary>
        public CardDeviceRightGroupEntity CardDeviceRightGroup { get; set; }
    }
}
