﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class GetCardHoldersOnSearchQuery
    {
        public string bstrAcctName { get; set; }
        public object vInputSearchFields { get; set; }
        public object vFieldData { get; set; }
        public object vCompareType { get; set; }
        public object vCardHolders { get; set; }
    }
}
