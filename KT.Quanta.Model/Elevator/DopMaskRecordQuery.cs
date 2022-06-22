using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Models
{
    public class DopMaskRecordQuery
    {
        public string ElevatorServer { get; set; }
        public string Type { get; set; }
        public string Operate { get; set; }
        public bool? IsSuccess { get; set; }
        public int? Status { get; set; }
    }
}
