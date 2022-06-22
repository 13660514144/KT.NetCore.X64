using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaDepartmentMaster
    {
        public TaDepartmentMaster()
        {
            TaEmployees = new HashSet<TaEmployee>();
        }

        public int DeptId { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<TaEmployee> TaEmployees { get; set; }
    }
}
