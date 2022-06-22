using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Hikvision.Models
{
    public class HikvisionAddOrUpdatePassRightQuery<T>
    {
        public T Model { get; set; }
        public FaceInfoModel Face { get; set; }
        public T OldModel { get; set; }
    }
}
