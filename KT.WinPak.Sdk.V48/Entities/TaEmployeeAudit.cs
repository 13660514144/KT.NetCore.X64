using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaEmployeeAudit
    {
        public int EmployeeAuditId { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string LoginPassword { get; set; }
        public string Role { get; set; }
        public byte? EmpTypeId { get; set; }
        public int? LocationId { get; set; }
        public int? DeptId { get; set; }
        public int? DesignationId { get; set; }
        public string EmpAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNo { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public DateTime? ResignationDate { get; set; }
        public DateTime? JoinDate { get; set; }
        public string AccessNo { get; set; }
        public int? ReporteeId { get; set; }
        public bool? IsShiftRotate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? LogInAttempt { get; set; }
        public DateTime? LastLogOn { get; set; }
        public int? IsFirstEntry { get; set; }
    }
}
