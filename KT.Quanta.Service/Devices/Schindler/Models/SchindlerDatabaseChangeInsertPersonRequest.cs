using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    /// <summary>
    /// Changing/Inserting of Person/Badge
    ///
    /// For backward compatibility, the "change/insert a person" function can be sent with function type 02 (old) or function type 06
    /// (recommended). The server system acknowledges the receipt of this message with a telegram of function type 03. The acknowledgement
    /// telegram has no parameters.
    ///  
    /// The data listed below is transmitted for each person.
    /// When a badge for a person should be deleted, the corresponding record is sent without badge number.
    /// If the badges should not be changed, the text "ignore" should be sent for badge number 1. Badge number 2 and badge number 3 should be empty. 
    /// 
    /// 001020000001|Zhang|San|Company||HR|Employeemanager|00000000BB1CFEF5|||||
    /// 019060014408|Zhao|Liu||||TO22F|0000000054AA2597||||| 
    /// 129060015941|Fao|Se||||TO20F|000000003A2BE2D9|||2020-12-10 10:00:00|2022-08-01 00:00:00|
    /// 133060015940|Dalao|Ce shi||||TO20F|000000003A2BE2D8|||||
    /// 134100015940|17,18,19,20|Always||None|
    /// 
    /// 
    /// 019060015940|Dalao|Ce shi||||TO20F|000000003A2BE2D8|||||
    /// 020100015940|20|Always|17,18,19,20|Always|
    /// 
    /// </summary>
    public class SchindlerDatabaseChangeInsertPersonRequest : SchindlerTelegramHeaderRequest
    {
        public SchindlerDatabaseChangeInsertPersonRequest()
        {
            FunctionType = SchindlerDatabaseMessageTypeEnum.CHANGE_INSERT_PERSON_REQUEST_NACK.Code;

            //EntryDate = DateTime.Now.AddMinutes(-10);
            //ExitDate = DateTime.Now.AddYears(100);
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

        /// <summary>
        /// Not empty
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Defines the master group the person will be inserted to.
        /// A master group with the same name must exist within PORT Technology.
        /// </summary>
        public string Company { get; set; } = string.Empty;

        /// <summary>
        /// Not used by PORT Technology.
        /// </summary>
        public string Enterprise { get; set; } = string.Empty;

        /// <summary> 
        /// Optional
        /// Can be empty.
        /// </summary>
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// Defines the user profile to be assigned to the person.
        /// A user profile with the same name must exist within PORT Technology.
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// 2- to 16-digit number
        /// 8-digit serial number Hi followed by 6-digit serial number Lo by default.
        /// Interface configuration can be set up independently to 1-digit to 8-digit numbers for Lo and Hi if required.
        ///  All numbers are hexadecimal.
        ///  The number and the number of digits must match with the number and the number of digits that the PORT card reader reads.
        /// </summary>
        public long BadgeNumber1 { get; set; }

        /// <summary>
        /// 2- to 16-digit number
        /// </summary>
        public long? BadgeNumber2 { get; set; }

        /// <summary>
        /// 2- to 16-digit number
        /// </summary>
        public long? BadgeNumber3 { get; set; }

        /// <summary>
        /// 19-digit string yyyy-mm-dd hh:mm:ss
        /// </summary>
        public DateTime? EntryDate { get; set; }

        /// <summary>
        /// 19-digit string yyyy-mm-dd hh:mm:ss
        /// </summary>
        public DateTime? ExitDate { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        public string PhoneNumber { get; set; }

        protected override void Read()
        {
            base.Read();

            PersonId = ReadEndSubstringLongNext();
            FamilyName = ReadString();
            FirstName = ReadString();
            Company = ReadString();
            Enterprise = ReadString();
            Department = ReadString();
            ProfileName = ReadString();
            BadgeNumber1 = ReadHexToLong();
            BadgeNumber2 = ReadHexToLong();
            BadgeNumber3 = ReadHexToLong();
            EntryDate = ReadStringDateTime();
            ExitDate = ReadStringDateTime();
            PhoneNumber = ReadString();
        }

        protected override void Write()
        {
            base.Write();

            WriteLongSubstringPadLeft(PersonId, 7);
            WriteString(FamilyName);
            WriteString(FirstName);
            WriteString(Company);
            WriteString(Enterprise);
            WriteString(Department);
            WriteString(ProfileName);
            WriteHexFromLong(BadgeNumber1, 16);
            WriteHexFromLong(BadgeNumber2, 16);
            WriteHexFromLong(BadgeNumber3, 16);
            WriteStringDateTime(EntryDate);
            WriteStringDateTime(ExitDate);
            WriteString(PhoneNumber);
        }
    }
}
