using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class AddUpdateALQuery
    {
        public int dwAcceslevelID { get; set; }
        public object sName { get; set; }
        public object sDesc { get; set; }
        public int lAccountID { get; set; }
        public object anReaderIDs { get; set; }
        public object anReaderTimeZones { get; set; }
        public object anReaderGroups { get; set; }
    }
}
