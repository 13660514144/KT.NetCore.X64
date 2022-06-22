using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Kone.Models
{
    public class MsgEnity
    {
        public string Endianess { get; set; }
        public string AppID { get; set; }
        public string MsgType { get; set; }
        public int MsgSize { get; set; }
        public virtual void Analysis(byte[] buffer)
        {
            this.Endianess = buffer.GetHexString(0, 5);
            this.AppID = buffer.GetHexString(5, 2);
            this.MsgType = buffer.GetHexString(7, 2);
            this.MsgSize = buffer.Length;
        }
    }
}
