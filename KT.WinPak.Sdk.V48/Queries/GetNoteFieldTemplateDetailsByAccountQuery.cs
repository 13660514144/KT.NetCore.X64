using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class GetNoteFieldTemplateDetailsByAccountQuery
    {
        public string bstrAcctName { get; set; }
        public string bstrSubAcctName { get; set; }
        public object vNFTemplates { get; set; }
    }
}
