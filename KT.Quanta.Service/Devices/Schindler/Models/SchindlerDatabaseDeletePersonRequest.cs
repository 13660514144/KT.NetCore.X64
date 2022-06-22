using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    /// <summary>
    /// Deleting of Person/Badge
    /// 
    /// For backward compatibility, the "delete a person" function can be sent with function type 04 (old) or function type 08 (recommended). 
    /// The server system acknowledges the receipt of this message with a telegram of function type 05. The acknowledgement telegram has no parameters. 
    /// </summary>
    public class SchindlerDatabaseDeletePersonRequest : SchindlerTelegramHeaderRequest
    {
        /// <summary>
        /// 构造函数初始数据
        /// </summary>
        public SchindlerDatabaseDeletePersonRequest()
        {
            FunctionType = SchindlerDatabaseMessageTypeEnum.DELETE_PERSON_REQUEST_NACK.Code;
        }
        /// <summary>
        /// Unique ID 
        /// Not empty.
        /// Used as personal number within PORT Technology.
        /// </summary>
        public long PersonId { get; set; }

        /// <summary>
        /// Not empty
        /// </summary>
        public string FamilyName { get; set; }

        protected override void Read()
        {
            base.Read();

            PersonId = ReadEndSubstringLongNext();
            FamilyName = ReadString();
        }

        protected override void Write()
        {
            base.Write();

            WriteLongSubstringPadLeft(PersonId, 7);
            WriteString(FamilyName);
        }
    }
}
