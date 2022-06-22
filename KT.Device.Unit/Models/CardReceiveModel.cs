using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Device.Unit.CardReaders.Models
{
    public class CardReceiveModel
    {
        public string CardNumber { get; set; }
        public bool IsCheckDate { get; set; }
        public long? StartTime { get; set; }
        public long? EndTime { get; set; }

        public string AccessType { get; set; }
        public string Address { get; set; }
    }
}
