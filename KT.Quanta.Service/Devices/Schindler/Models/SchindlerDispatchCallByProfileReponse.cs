using KT.Quanta.Service.Devices.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler
{
    /// <summary>
    /// Call by Profile / Call by Profile with NACK
    /// 
    /// For backward compatibility, the "call by profile" function can be sent with function type 02 (old) or function type 12 (recommended). The
    /// server system acknowledges the receipt of this message with a telegram of function type 03. The acknowledgement telegram has no
    /// parameters.
    /// 
    /// If the call request could not be sent to the required terminal:
    /// Call request sent with function type 02 (old): The server system does not answer at all.
    /// Call request sent with function type 12 (recommended): The server system answers with a telegram of function type 13 (call by
    /// profile NACK). The NACK telegram has two parameters, separated by a pipe character(|) (ASCII code 124):
    /// Error code: Numeric
    /// Error message: Text message describing the error. 
    /// 
    /// 00 Message format incorrect
    /// 01 Supplied terminal ID not existing
    /// 02 Supplied profile name not existing
    /// 03 Supplied person ID not existing
    /// 04 Supplied destination floor not numeric
    /// </summary>
    public class SchindlerDispatchCallByProfileReponse : SchindlerDatabaseSerializer
    {
        /// <summary>
        /// 构造函数初始数据
        /// </summary>
        public SchindlerDispatchCallByProfileReponse()
        {
            //FunctionType = 13;
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
