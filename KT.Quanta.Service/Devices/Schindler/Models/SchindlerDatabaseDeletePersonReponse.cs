using KT.Quanta.Service.Devices.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler
{
    /// <summary>
    /// Deleting of Person/Badge
    ///
    /// If the deletion of a person has failed:
    ///   Person deleted with function type 04 (old): T
    ///     he server system does not answer at all.
    ///   Person deleted with function type 08 (recommended): 
    ///     The server system answers with a telegram of function type 09 (delete persion with NACK). 
    ///     The NACK telegram has two parameters, separated by a pipe character(|) (ASCII code 124):
    ///       Error code: Numeric
    ///       Error message: Text message describing the error
    /// </summary>
    public class SchindlerDatabaseDeletePersonReponse : SchindlerDatabaseSerializer
    {
        /// <summary>
        /// 构造函数初始数据
        /// </summary>
        public SchindlerDatabaseDeletePersonReponse()
        {
            //FunctionType = 9;
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
