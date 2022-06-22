using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Service.Entities
{
    /// <summary>
    /// 人员信息表
    /// </summary>
    [Table("PERSON")]
    public class PersonEntity : BaseEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 人员通行权限
        /// </summary>
        public List<PassRightEntity> PassRights { get; set; }

    }
}
