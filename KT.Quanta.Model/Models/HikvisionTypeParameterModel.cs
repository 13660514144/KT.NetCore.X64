using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Hikvision.Models
{
    public class HikvisionTypeParameterModel
    {
        public string BrandModel { get; set; }

        public byte DeployLevel { get; set; } = 1;
        public byte DeployType { get; set; } = 1;
    }
}
