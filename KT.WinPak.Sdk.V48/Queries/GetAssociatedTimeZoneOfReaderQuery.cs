﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class GetAssociatedTimeZoneOfReaderQuery
    {
        public string bstrAcclName { get; set; }
        public string bstrAcctName { get; set; }
        public string bstrReader { get; set; }
        public object vTimezone { get; set; }
    }
}
