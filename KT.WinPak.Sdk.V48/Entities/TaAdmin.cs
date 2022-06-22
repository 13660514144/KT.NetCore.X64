using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaAdmin
    {
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string LoginPassword { get; set; }
        public string Role { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? EntryCount { get; set; }
        public string AdminMailId { get; set; }
        public int? RoleId { get; set; }
        public int LogInAttempt { get; set; }
        public DateTime? LastLogOn { get; set; }
    }
}
