﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class GetAssociatedGroupofReaderQuery
    {
        public string bstrAcclName { get; set; }
        public string bstrReader { get; set; }
        public int lGroupID { get; set; }
    }
}