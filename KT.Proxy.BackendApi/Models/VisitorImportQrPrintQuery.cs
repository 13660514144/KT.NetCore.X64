using System.Collections.Generic;

namespace KT.Proxy.BackendApi.Models
{
    public class VisitorImportQrPrintQuery
    {
        public string ImportId { get; set; }
        public string Select { get; set; }
        public List<long> DetailIds { get; set; }
    }
}
