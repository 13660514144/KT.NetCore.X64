using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaDesignationMaster
    {
        public TaDesignationMaster()
        {
            TaEmployees = new HashSet<TaEmployee>();
        }

        public int DesignationId { get; set; }
        public string DesignationCode { get; set; }
        public string DesignationName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<TaEmployee> TaEmployees { get; set; }
    }
}
