﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class GetAvailableTimezonesOfPanelQuery
    {
        public int lAcctID { get; set; }
        public int lPanelID { get; set; }
        public object vTimezones { get; set; }
    }
}