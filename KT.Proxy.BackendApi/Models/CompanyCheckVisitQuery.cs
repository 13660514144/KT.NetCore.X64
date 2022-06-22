using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Proxy.BackendApi.Models
{
    public class CompanyCheckVisitQuery
    {
        public long CompanyId { get; set; }
        public long FloorId { get; set; }
    }
}
