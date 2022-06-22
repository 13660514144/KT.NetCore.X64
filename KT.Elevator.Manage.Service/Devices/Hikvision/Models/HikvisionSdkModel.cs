using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Devices.Hikvision.Models
{
    public class HikvisionPersonCard
    {
        public string CardNo { get; set; }
        public ushort CardRightPlan { get; set; } = 1;
        public uint EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
    }
}
