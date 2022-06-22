using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Entities
{
    /// <summary>
    /// 人员信息表
    /// </summary>
    public class PersonEntity : BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 人员通行权限
        /// </summary>
        public List<PassRightEntity> PassRights { get; set; }

    }
}
