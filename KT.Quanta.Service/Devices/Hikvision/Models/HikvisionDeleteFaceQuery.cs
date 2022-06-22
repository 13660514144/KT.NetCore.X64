using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Hikvision.Models
{
    public class HikvisionDeleteFaceQuery
    {
        public string CardNo { get; set; }
        public int CardReaderNo { get; set; } = 1;
    }
}
