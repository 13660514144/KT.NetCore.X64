using KT.WinPak.SDK.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.WinPak.SDK.Entities
{
    public partial class Card
    {
        [NotMapped]
        public List<AccessLevelPlus> AccessLevelPluses { get; set; }

        [NotMapped]
        public AccessLevelPlus AccessLevelPlus { get; set; }
    }
}
