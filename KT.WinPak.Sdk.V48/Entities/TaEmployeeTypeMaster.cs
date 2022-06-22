using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaEmployeeTypeMaster
    {
        public TaEmployeeTypeMaster()
        {
            TaEmployees = new HashSet<TaEmployee>();
        }

        public int EmpTypeId { get; set; }
        public string EmpTypeCode { get; set; }
        public string EmpTypeDesc { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<TaEmployee> TaEmployees { get; set; }
    }
}
