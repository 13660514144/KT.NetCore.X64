using KT.Device.Unit.CardReaders.Datas;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Device.Unit.Devices
{
    public class NlsFm25rCardDevice : CardDeviceBase, INlsFm25rCardDevice
    {
        public NlsFm25rCardDevice(ILogger logger,
            IEventAggregator eventAggregator,
            INlsFm25rCardDeviceAnalyze dataAnalyze)
            : base(logger, eventAggregator, dataAnalyze)
        {
        }
    }
}
