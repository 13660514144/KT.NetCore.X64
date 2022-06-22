using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaAttnRegularizeMaster
    {
        public TaAttnRegularizeMaster()
        {
            TaEmployeeAttnRegularizes = new HashSet<TaEmployeeAttnRegularize>();
        }

        public int AttnRegularizeTypeId { get; set; }
        public string AttnRegularizeCode { get; set; }
        public string AttnRegularizeDesc { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public virtual ICollection<TaEmployeeAttnRegularize> TaEmployeeAttnRegularizes { get; set; }
    }
}
