using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaEmailTemplate
    {
        public int TemplateId { get; set; }
        public int LanguageId { get; set; }
        public string TemplateDescription { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public virtual TaLanguageSupport Language { get; set; }
    }
}
