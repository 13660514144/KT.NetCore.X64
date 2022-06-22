using KT.Quanta.Service.Devices.Schindler.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler
{
    /// <summary>
    /// Call by Profile / Call by Profile with NACK
    /// 
    /// A card has been swiped at a third-party card reader(either built into a turnstile or assigned to a Z-Line terminal).
    /// The card refers to a user which is assigned to a certain template/profile.
    /// The profile contains:
    /// The assigned zones for the user
    /// Validity
    /// Call parameters such as space requirements, assigned elevators, etc.
    /// The profile is identified by a unique name.A template with exactly the same name must have been created and edited within PORT
    /// Technology.
    /// With the function type 02 "call by profile" and the function type 12 "call by profile with NACK", the PORT Technology can be
    /// used from a third-party system without the need of a synchronized database.
    /// 
    /// demo:
    /// 0060217|IBM|123456|
    /// Message number: 006
    /// Function type: 02
    /// Terminal ID: 17
    /// Profile name: IBM
    /// User number: 123456
    /// </summary>
    public class SchindlerDispatchCallByProfileRequest : SchindlerTelegramHeaderRequest
    {
        /// <summary>
        /// 构造函数初始数据
        /// </summary>
        public SchindlerDispatchCallByProfileRequest()
        {
            FunctionType = SchindlerDispatchMessageTypeEnum.CALL_BY_PROFILE_REQUEST.Code;
        }

        /// <summary>
        /// Terminal ID Numeric Number of the terminal where the card has been swiped.
        /// Number refers to ONE physical terminal.
        /// Number also used to communicate with the user (ask for a destination, display the allocated
        /// elevator, etc.).
        /// </summary>
        public int TerminalId { get; set; }

        /// <summary>
        /// Profile name Alphanumeric Unique name that identifies the template assigned to the user.
        /// A template with exactly the same name must exist within SID.
        /// The profile name is case-sensitive.
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Number that identifies the user.
        /// If the user swipes the same card again, the same number will be transmitted to
        /// SID. “Double-swipers” can be detected and filtered out.
        /// </summary>
        public int UserNumber { get; set; }

        protected override void Read()
        {
            base.Read();

            TerminalId = ReadIntSubstringNext(2);

            ProfileName = ReadString();
            UserNumber = ReadInt();
        }

        protected override void Write()
        {
            base.Write();

            WriteIntSubstringPadLeft(TerminalId, 2);

            WriteString(ProfileName);
            WriteInt(UserNumber);
        }
    }
}
