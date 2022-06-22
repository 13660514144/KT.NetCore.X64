using KT.Quanta.Service.Devices.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler
{
    /// <summary>
    /// Change/insert NACK 
    /// 
    /// If the import of a person has failed:
    ///   Person inserted with function type 02 (old): The server system does not answer at all.
    ///   Person inserted with function type 06 (recommended): The server system answers with a telegram of function type 07 (change/insert
    ///   NACK). The NACK telegram has two parameters, separated by a pipe character(|) (ASCII code 124):
    ///        Error code: Numeric
    ///        Error message: Text message describing the error.
    /// </summary>
    public class SchindlerDatabaseChangeInsertPersonReponse : SchindlerDatabaseSerializer
    {
        /// <summary>
        /// 构造函数初始数据
        /// </summary>
        public SchindlerDatabaseChangeInsertPersonReponse()
        {
            //FunctionType = 07;
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
