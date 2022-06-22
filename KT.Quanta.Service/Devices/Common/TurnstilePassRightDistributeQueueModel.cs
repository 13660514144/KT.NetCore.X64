using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Common
{
    public class TurnstilePassRightDistributeQueueModel
    {
        public string DistributeType { get; set; }

        public List<string> Ids { get; set; }
        public TurnstilePassRightModel PassRight { get; set; }
        public FaceInfoModel Face { get; set; }
    }
}
