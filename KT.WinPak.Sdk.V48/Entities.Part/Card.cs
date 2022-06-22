using KT.WinPak.SDK.V48.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class Card
    {
        [NotMapped]
        public List<AccessLevelPlu> AccessLevelPluses { get; set; }

        [NotMapped]
        public AccessLevelPlu AccessLevelPlus { get; set; }
    }
}
