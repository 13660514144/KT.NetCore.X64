using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class GetNoteFieldTemplateDetailsByAccountQuery
    {
        public string bstrAcctName { get; set; }
        public object vNFTemplates { get; set; }
    }
}
