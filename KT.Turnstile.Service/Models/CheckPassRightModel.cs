using KT.Turnstile.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Model.Models
{
    public class CheckPassRightModel
    {
        public bool HasRight { get; set; }
        public long PassTime { get; set; }

        public PassRecordTypeEnum Type { get; set; }

        public PassRightModel PassRight { get; set; }
    }
}
