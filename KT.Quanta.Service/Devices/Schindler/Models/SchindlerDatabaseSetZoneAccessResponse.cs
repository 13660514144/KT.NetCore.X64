using KT.Quanta.Service.Devices.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    /// <summary>
    /// Setting of Zone Access
    /// 
    /// The server system acknowledges the receipt of this message with a telegram of function type 11. 
    /// The acknowledgement telegram has no parameters.
    /// 
    /// If the setting of zone access has failed, the server system answers with a telegram of function type 12 (set zone access NACK). The NACK
    /// telegram has two parameters, separated by a pipe character(|) (ASCII code 124):
    ///   Error code: Numeric
    ///   Error message: Text message describing the error
    /// </summary>
    public class SchindlerDatabaseSetZoneAccessResponse : SchindlerDatabaseSerializer
    {
        /// <summary>
        /// 构造函数初始数据
        /// </summary>
        public SchindlerDatabaseSetZoneAccessResponse()
        {
            //FunctionType = 1;
        }

        /// <summary>
        /// Number of the terminal where the card has been swiped.
        /// Number refers to ONE physical terminal.
        /// Number also used to communicate with the user (ask for a
        /// destination, display the allocated elevator, etc.).
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Text message describing the error.
        /// </summary>
        public string Message { get; set; }

        protected override void Read()
        {
            Code = ReadInt();

            Message = ReadString();
        }

        protected override void Write()
        {
            WriteIntSubstringPadLeft(Code, 2);

            WriteString(Message);
        }
    }
}
