using KT.Common.Core.Utils;
using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Turnstile.Entity.Entities
{
    /// <summary>
    /// 设备权限组
    /// </summary>
    [Table("CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE")]
    public class CardDeviceRightGroupRelationCardDeviceEntity : BaseEntity
    {
        public string CardDeviceRightGroupId { get; set; }
        /// <summary>
        /// 设备权限组
        /// </summary>
        public CardDeviceRightGroupEntity CardDeviceRightGroup { get; set; }

        public string CardDeviceId { get; set; }
        /// <summary>
        /// 设备
        /// </summary>
        public CardDeviceEntity CardDevice { get; set; }

        public CardDeviceRightGroupRelationCardDeviceEntity()
        {
            Id = IdUtil.NewId();
        }

    }
}
