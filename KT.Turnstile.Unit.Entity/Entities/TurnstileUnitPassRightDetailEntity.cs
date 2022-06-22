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
    /// 通行权限关联通行权限组明细
    /// </summary>
    [Table("PASS_RIGHT_DETAIL")]
    public class TurnstileUnitPassRightDetailEntity : BaseEntity
    {
        /// <summary>
        /// 通行权限Id
        /// </summary>
        public string PassRightId { get; set; }

        /// <summary>
        /// 通行权限
        /// </summary>
        public TurnstileUnitPassRightEntity PassRight { get; set; }

        /// <summary>
        /// 权限组Id
        /// </summary>
        public string RightGroupId { get; set; }

        /// <summary>
        /// 权限组
        /// </summary>
        public TurnstileUnitRightGroupEntity RightGroup { get; set; }
    }
}
