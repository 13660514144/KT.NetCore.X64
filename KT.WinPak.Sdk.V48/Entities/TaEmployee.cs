using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaEmployee
    {
        public TaEmployee()
        {
            TaEmpLeaveAvailabilities = new HashSet<TaEmpLeaveAvailability>();
            TaEmpReportScheduleDetails = new HashSet<TaEmpReportScheduleDetail>();
            TaEmployeeAttnRegularizes = new HashSet<TaEmployeeAttnRegularize>();
            TaEmployeeOutDuties = new HashSet<TaEmployeeOutDuty>();
            TaEmployeeShifts = new HashSet<TaEmployeeShift>();
            TaReportQueues = new HashSet<TaReportQueue>();
            TaUpdateLeaveDetails = new HashSet<TaUpdateLeaveDetail>();
            TaUserReportScheduleDetails = new HashSet<TaUserReportScheduleDetail>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MailId { get; set; }
        public string UserName { get; set; }
        public string LoginPassword { get; set; }
        public string Role { get; set; }
        public int EmpTypeId { get; set; }
        public int LocationId { get; set; }
        public int DeptId { get; set; }
        public int DesignationId { get; set; }
        public string EmpAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNo { get; set; }
        public string Gender { get; set; }
        public string Dob { get; set; }
        public string BloodGroup { get; set; }
        public string ResignationDate { get; set; }
        public DateTime JoinDate { get; set; }
        public string AccessNo { get; set; }
        public int SupervisorId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? LogInAttempt { get; set; }
        public DateTime? LastLogOn { get; set; }
        public int? Entrycount { get; set; }
        public int? RecordId { get; set; }
        public int? RoleId { get; set; }
        public int? GenderId { get; set; }

        public virtual TaDepartmentMaster Dept { get; set; }
        public virtual TaDesignationMaster Designation { get; set; }
        public virtual TaEmployeeTypeMaster EmpType { get; set; }
        public virtual TaLocationMaster Location { get; set; }
        public virtual ICollection<TaEmpLeaveAvailability> TaEmpLeaveAvailabilities { get; set; }
        public virtual ICollection<TaEmpReportScheduleDetail> TaEmpReportScheduleDetails { get; set; }
        public virtual ICollection<TaEmployeeAttnRegularize> TaEmployeeAttnRegularizes { get; set; }
        public virtual ICollection<TaEmployeeOutDuty> TaEmployeeOutDuties { get; set; }
        public virtual ICollection<TaEmployeeShift> TaEmployeeShifts { get; set; }
        public virtual ICollection<TaReportQueue> TaReportQueues { get; set; }
        public virtual ICollection<TaUpdateLeaveDetail> TaUpdateLeaveDetails { get; set; }
        public virtual ICollection<TaUserReportScheduleDetail> TaUserReportScheduleDetails { get; set; }
    }
}
