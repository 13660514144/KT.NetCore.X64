using KT.Quanta.Service.Devices.Schindler.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler
{
    /// <summary>
    /// Direct Call / Direct Call with NACK
    /// 
    /// The function "direct call" is used if the third-party system knows the automatic destination of a card user.
    /// In the PORT Technology database, a dummy user with the person ID "DIRECTCALL" must be defined.
    /// The dummy user must have access to the corresponding zones.
    /// 
    /// </summary>
    public class SchindlerDispatchDirectCallRequest : SchindlerTelegramHeaderRequest
    {
        /// <summary>
        /// 构造函数初始数据
        /// </summary>
        public SchindlerDispatchDirectCallRequest()
        {
            FunctionType = SchindlerDispatchMessageTypeEnum.DIRECT_CALL_REQUEST.Code;
        }

        /// <summary>
        /// Number of the terminal where the card has been swiped.
        /// Number refers to ONE physical terminal.
        /// Number also used to communicate with the user (ask for a
        /// destination, display the allocated elevator, etc.).
        /// </summary>
        public int TerminalId { get; set; }

        /// <summary>
        /// Floor number which the card holder always travels to.
        /// For floors below zero, use "-" (for example: "-5").
        /// </summary>
        public int DestinationFloor { get; set; }

        /// <summary>
        /// Unique name that identifies the card holder.(optional)
        /// SID seraches for a user with the same ID number and initiates a call to the
        /// destination floor.
        /// 
        /// If this parameter is left empty, the access rights of the dummy user
        /// with person ID "DIRECT CALL" apply.
        /// </summary>
        public long PersonId { get; set; }
        public string Append { get; set; } = string.Empty;

        protected override void Read()
        {
            base.Read();

            TerminalId = ReadIntSubstringNext(2);

            DestinationFloor = ReadInt();
            PersonId = ReadLong();
            Append = ReadString();
        }

        protected override void Write()
        {
            base.Write();

            WriteIntSubstringPadLeft(TerminalId, 2);

            WriteInt(DestinationFloor);
            WriteLongPadLeftZeroEmpty(PersonId, 7);
            WriteString(Append);
        }
    }
}
