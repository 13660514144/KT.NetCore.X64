﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class GetAccessTreeByNameQuery
    {
        public string bstrAcclName { get; set; }
        public string pbstrAccessTree { get; set; }
    }
}