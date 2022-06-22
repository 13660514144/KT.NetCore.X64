using KT.Quanta.Service.Devices.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    /// <summary>
    /// Setting of Zone Access
    /// 
    /// The function type 10 "set zone access" sets the access of the specified zones to the specified values.
    /// Currently, following values are supported(not case-sensitive) :
    /// Always(give a person always access to the specified zones)
    /// None(delete the access to the specified zones).
    /// This pair of zones and their access can be repeated multiple times.
    /// Example
    ///   00010369|1..5,10|Always|12,15,17|None|
    ///     Person ID: 00010369
    ///     Give access to zones 1, 2, 3, 4, 5 and 10
    ///     Delete the access from zones 12, 15 and 17
    ///     All other zones remain unchanged.
    /// </summary>
    public class SchindlerDatabaseSetZoneAccessRequest : SchindlerTelegramHeaderRequest
    {
        /// <summary>
        /// 构造函数初始数据
        /// </summary>
        public SchindlerDatabaseSetZoneAccessRequest()
        {
            FunctionType = SchindlerDatabaseMessageTypeEnum.SET_ZONE_ACCESS_REQUEST.Code;
        }

        /// <summary>
        /// Unique ID
        /// </summary>
        public long PersonId { get; set; }

        /// <summary>
        /// Setting representation, 0-255
        /// Example: 1..7, 20
        /// </summary>
        public string SetZones1 { get; set; }

        /// <summary>
        /// Always
        /// None.
        /// </summary>
        public string Access1 { get; set; }

        /// <summary>
        /// Setting representation, 0-255
        /// Example: 1..7, 20
        /// </summary>
        public string SetZones2 { get; set; }

        /// <summary>
        /// Always
        /// None.
        /// </summary>
        public string Access2 { get; set; }

        /// <summary>
        /// </summary>
        public string Append { get; set; } = string.Empty;

        protected override void Read()
        {
            base.Read();

            PersonId = ReadEndSubstringLongNext();
            SetZones1 = ReadString();
            Access1 = ReadString();
            SetZones2 = ReadString();
            Access2 = ReadString();
            Append = ReadString();
        }

        protected override void Write()
        {
            base.Write();

            WriteLongSubstringPadLeft(PersonId, 7);
            WriteString(SetZones1);
            WriteString(Access1);
            WriteString(SetZones2);
            WriteString(Access2);
            WriteString(Append);
        }
    }
}
