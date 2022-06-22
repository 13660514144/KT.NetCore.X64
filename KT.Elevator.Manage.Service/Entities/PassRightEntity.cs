using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Manage.Service.Entities
{
    /// <summary>
    /// 通行权限
    /// </summary>
    [Table("PASS_RIGHT")]
    public class PassRightEntity : BaseEntity
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public string AccessType { get; set; }

        /// <summary>
        /// 当前时间，UTC毫秒
        /// </summary>
        public long TimeNow { get; set; }

        /// <summary>
        /// 过期时间，UTC毫秒
        /// </summary>
        public long TimeOut { get; set; }

        /// <summary>
        /// 所在位置
        /// </summary>
        public FloorEntity Floor { get; set; }

        /// <summary>
        /// 关联人员
        /// </summary>
        public PersonEntity Person { get; set; }

        ///// <summary>
        ///// 父权限，人脸关联卡号
        ///// </summary>
        //public PassRightEntity Parent { get; set; }

        /// <summary>
        /// 关联读卡器权限组
        /// </summary> 
        public List<PassRightRelationFloorEntity> RelationFloors { get; set; }
    }
}
