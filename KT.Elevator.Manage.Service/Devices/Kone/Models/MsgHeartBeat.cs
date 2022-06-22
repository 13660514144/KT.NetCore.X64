using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Kone.Models
{
    public class MsgHeartBeat : MsgEnity
    {
        public string Timestamp { get; set; }
        public override void Analysis(byte[] buffer)
        {
            base.Analysis(buffer);

            this.Timestamp = buffer.GetHexString(9, 8);
        }

        /* 
          data: 80000000118000800201D4A003E0AE1C80
          Endianess: 0x81 AppID: 0x8000 MsgType: 0x8002 MsgSize: 17
          SUB HEADER 3: 
          Timestamp: 30.12.18 13:52:41:544
         */
    }

    public class MsgDisconnect : MsgEnity
    {
        public long Reason { get; set; }

        public long Version { get; set; }
        public long MsgID { get; set; }
        public long RespID { get; set; }
        public long AppMsgType { get; set; }
        public string Timestamp { get; set; }

        public override void Analysis(byte[] buffer)
        {
            base.Analysis(buffer);

            this.AppMsgType = buffer.toHexInt(9, 2);
            this.Version = buffer.toHexInt(11, 1);
            this.MsgID = buffer.toHexInt(16, 1);
            this.RespID = buffer.toHexInt(17, 1);
            this.Reason = buffer.toHexInt(29, 2);
            this.Timestamp = buffer.GetHexString(21, 8);
        }

        /*
        data: 800000001F-8000-8000-0066-01-00-0000-00-1E-00000000-01D49FFBF3F6C330-0001
        HEADER: Endianess: 0x81 AppID: 0x8000 MsgType: 0x8000 MsgSize: 31
        SUB HEADER 1: 
             AppMsgType: 102 Version: 1.0 msgID: 0x1e RespID: 0 Timestamp: 30.12.18 12:55:57:923
        DISCONNECT: 
        Reason: 1
         */
    }
}
