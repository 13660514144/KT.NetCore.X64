using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Unit.Secodary.Entity.Entities
{
    /// <summary>
    /// 通行权限
    /// </summary>
    [Table("PASS_RIGHT")]
    public class UnitPassRightEntity : BaseEntity
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string AccessType { get; set; }

        /// <summary>
        /// 人脸特性,不同服务器上生成特性不一样，不能远程传值
        /// </summary>
        public byte[] Feature { get; set; }

        /// <summary>
        /// 人脸特性
        /// </summary>
        public int FeatureSize { get; set; }

        /// <summary>
        /// 起始时间，UTC毫秒
        /// </summary>
        public long TimeStart { get; set; }

        /// <summary>
        /// 过期时间，UTC毫秒
        /// </summary>
        public long TimeEnd { get; set; }

        /// <summary>
        /// 权限组Id
        /// </summary> 
        public List<UnitPassRightDetailEntity> PassRightDetails { get; set; }

        public UnitPassRightEntity()
        {
            PassRightDetails = new List<UnitPassRightDetailEntity>();
        }

        public void SetValues(UnitPassRightEntity entity)
        {
            Sign = entity.Sign;
            AccessType = entity.AccessType;
            Feature = entity.Feature;
            FeatureSize = entity.FeatureSize;
            TimeStart = entity.TimeStart;
            TimeEnd = entity.TimeEnd;

            PassRightDetails = entity.PassRightDetails;
        }
    }
}
