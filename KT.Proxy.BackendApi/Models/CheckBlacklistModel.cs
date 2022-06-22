using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Proxy.BackendApi.Models
{
    public class CheckBlacklistModel
    {
        public string Phone { get; set; }
        public string IdNumber { get; set; }
        public long FloorId { get; set; }
        public long CompanyId { get; set; }
    }
}
