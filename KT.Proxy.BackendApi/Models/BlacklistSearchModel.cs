using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Proxy.BackendApi.Models
{
    public class BlacklistSearchModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string IdNumber { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
