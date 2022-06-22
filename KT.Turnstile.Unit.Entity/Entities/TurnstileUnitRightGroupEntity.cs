using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Turnstile.Unit.Entity.Entities
{
    /// <summary>
    /// 通行权限
    /// 通行权限关联通道，通道关联读卡器（多个，分进出）、边缘处理器
    /// </summary>
    [Table("RIGHT_GROUP")]
    public class TurnstileUnitRightGroupEntity : BaseEntity
    {
        public List<TurnstileUnitRightGroupDetailEntity> Details { get; set; }

        public TurnstileUnitRightGroupEntity()
        {
            Details = new List<TurnstileUnitRightGroupDetailEntity>();
        }
    }
}
