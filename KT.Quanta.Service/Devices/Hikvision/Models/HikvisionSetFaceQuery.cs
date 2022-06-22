using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Hikvision.Models
{
    public class HikvisionSetFaceQuery
    {
        public string CardNo { get; set; }
        public string FacePath { get; set; }
        public int CardReaderNo { get; set; } = 1;
    }
}
