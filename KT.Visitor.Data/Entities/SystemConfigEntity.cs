using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Data.Entity
{
    public class SystemConfigEntity : BaseEntity
    {
        /// <summary>
        /// Key值
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Value值
        /// </summary>
        public string Value { get; set; }

        public SystemConfigEntity()
        {

        }

    }
}
