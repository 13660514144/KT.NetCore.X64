using KT.Quanta.Service.Devices.Schindler.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler
{
    /// <summary>
    /// Call by ID / Call by ID with NACK
    /// 
    /// A card has been swiped at a third-party card reader(either built into a turnstile or assigned to a Z-Line terminal).
    /// The card holder is searched in the third-party database.
    /// The person ID of this card holder is sent to PORT Technology database.
    /// SID searches for this person ID and tells the corresponding terminal to handle the call request.
    /// With the function type 04 "call by ID" and the function type 14 "call by ID with NACK", the database between PORT Technology
    /// system and the third-party system must be synchronized.For details on the data synchronization
    /// </summary>
    public class SchindlerDispatchCallByPersonRequest : SchindlerTelegramHeaderRequest
    {
        /// <summary>
        /// 构造函数初始数据
        /// </summary>
        public SchindlerDispatchCallByPersonRequest()
        {
            FunctionType = SchindlerDispatchMessageTypeEnum.CALL_BY_ID_REQUEST.Code;
        }

        /// <summary>
        /// Number of the terminal where the card has been swiped.
        /// Number refers to ONE physical terminal.
        /// Number also used to communicate with the user (ask for a destination, display the allocated
        /// elevator, etc.).
        /// </summary>
        public int TerminalId { get; set; }

        /// <summary>
        /// Unique name that identifies the card holder.
        /// SID seraches for a user with the same ID number.
        /// </summary>
        public long PersonId { get; set; }

        protected override void Read()
        {
            base.Read();

            TerminalId = ReadEndSubstringIntNext();

            PersonId = ReadLong();
        }

        protected override void Write()
        {
            base.Write();

            WriteIntSubstring(TerminalId);

            WriteLongPadLeftZeroEmpty(PersonId, 7);

            //要增加分格符
            WriteString(string.Empty);
        }
    }
}
