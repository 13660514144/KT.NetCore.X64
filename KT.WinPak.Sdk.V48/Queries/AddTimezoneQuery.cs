﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class AddTimezoneQuery
    {
        public NCIHelperLib.TimeZone pTz { get; set; }
        public int pTZID { get; set; }
        public int pStatus { get; set; }
    }
}