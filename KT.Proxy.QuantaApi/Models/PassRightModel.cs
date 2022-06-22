using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Proxy.QuantaApi.Models
{
    public class PassRightModel
    { 
        public PersonModel Person { get; set; }

        public string Sign { get; set; }

        public FloorModel Floor { get; set; }

        public List<FloorModel> Floors { get; set; }
    }
}
