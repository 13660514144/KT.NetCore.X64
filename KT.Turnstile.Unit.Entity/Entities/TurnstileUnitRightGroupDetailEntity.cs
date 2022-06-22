using KT.Common.Core.Utils;
using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Turnstile.Unit.Entity.Entities
{
    /// <summary>
    /// 通行权限组关联读卡器明细
    /// </summary>
    [Table("RIGHT_GROUP_DETAIL")]
    public class TurnstileUnitRightGroupDetailEntity : BaseEntity
    {
        public string RightGroupId { get; set; }

        /// <summary>
        /// 通行权限组
        /// </summary>
        public TurnstileUnitRightGroupEntity RightGroup { get; set; }

        public string CardDeviceId { get; set; }

        /// <summary>
        /// 读卡器
        /// </summary>
        public TurnstileUnitCardDeviceEntity CardDevice { get; set; }
    }
}
