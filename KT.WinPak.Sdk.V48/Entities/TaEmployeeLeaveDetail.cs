using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaEmployeeLeaveDetail
    {
        public int LeaveDetailsId { get; set; }
        public int EmpLeaveId { get; set; }
        public DateTime LeaveDate { get; set; }
        public int StatusId { get; set; }
        public byte? IsHalfDay { get; set; }
        public string Comments { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual TaEmployeeLeave EmpLeave { get; set; }
    }
}
