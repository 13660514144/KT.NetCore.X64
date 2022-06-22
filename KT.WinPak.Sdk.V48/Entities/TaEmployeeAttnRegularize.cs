using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaEmployeeAttnRegularize
    {
        public int EmpAttnRegularizeId { get; set; }
        public int EmployeeId { get; set; }
        public int AttnRegularizeTypeId { get; set; }
        public string AuthorizedBy { get; set; }
        public DateTime AttnRegularizeDate { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public int StatusId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Comments { get; set; }
        public byte? IsHalfDay { get; set; }
        public string Reason { get; set; }

        public virtual TaAttnRegularizeMaster AttnRegularizeType { get; set; }
        public virtual TaEmployee Employee { get; set; }
        public virtual TaStatusMaster Status { get; set; }
    }
}
