using KT.Quanta.Service.Devices.Hikvision.Sdks;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Hikvision.Models
{
    public class HikvisionPersonCardQuery
    {
        public string CardNo { get; set; }
        public ushort CardRightPlan { get; set; } = 1;
        public uint EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public byte[] DoorRights { get; set; }

        public HikvisionPersonCardQuery()
        {
            DoorRights = new byte[CHCNetSDKCard.MAX_DOOR_NUM_256];
        }
    }
}
