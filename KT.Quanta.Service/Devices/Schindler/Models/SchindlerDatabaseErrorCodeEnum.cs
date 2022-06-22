using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    public class SchindlerDatabaseErrorCodeEnum : BaseEnum
    {
        public SchindlerDatabaseErrorCodeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static SchindlerDatabaseErrorCodeEnum Error01 => new SchindlerDatabaseErrorCodeEnum(01, "01", "System occupied");
        public static SchindlerDatabaseErrorCodeEnum Error02 => new SchindlerDatabaseErrorCodeEnum(02, "02", "Database timeout");
        public static SchindlerDatabaseErrorCodeEnum Error03 => new SchindlerDatabaseErrorCodeEnum(03, "03", "Unknown command");
        public static SchindlerDatabaseErrorCodeEnum Error04 => new SchindlerDatabaseErrorCodeEnum(04, "04", "Main database not present");
        public static SchindlerDatabaseErrorCodeEnum Error05 => new SchindlerDatabaseErrorCodeEnum(05, "05", "Unexpected answer from database");
        public static SchindlerDatabaseErrorCodeEnum Error06 => new SchindlerDatabaseErrorCodeEnum(06, "06", "Card already assigned to another user");
        public static SchindlerDatabaseErrorCodeEnum Error10 => new SchindlerDatabaseErrorCodeEnum(10, "10", "Date in record invalid");
        public static SchindlerDatabaseErrorCodeEnum Error11 => new SchindlerDatabaseErrorCodeEnum(11, "11", "Wrong length for badge number");
        public static SchindlerDatabaseErrorCodeEnum Error12 => new SchindlerDatabaseErrorCodeEnum(12, "12", "Wrong format for badge number");
        public static SchindlerDatabaseErrorCodeEnum Error13 => new SchindlerDatabaseErrorCodeEnum(13, "13", "Exit date is before entry date");
        public static SchindlerDatabaseErrorCodeEnum Error14 => new SchindlerDatabaseErrorCodeEnum(14, "14", "User not found");
        public static SchindlerDatabaseErrorCodeEnum Error15 => new SchindlerDatabaseErrorCodeEnum(15, "15", "Invalid access specified (must be 'Always' or 'None')");


        public static List<SchindlerDatabaseErrorCodeEnum> List => new List<SchindlerDatabaseErrorCodeEnum>
        {
            Error01,
            Error02,
            Error03,
            Error04,
            Error05,
            Error06,
            Error10,
            Error11,
            Error12,
            Error13,
            Error14,
            Error15,
        };

        public static SchindlerDatabaseErrorCodeEnum GetByCode(int code)
        {
            return List.FirstOrDefault(x => x.Code == code);
        }
    }
}