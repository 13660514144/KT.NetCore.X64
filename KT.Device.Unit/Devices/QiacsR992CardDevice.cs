using KT.Device.Unit.CardReaders.Datas;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Device.Unit.Devices
{
    public class QiacsR992CardDevice : CardDeviceBase, IQiacsR992CardDevice
    {
        public QiacsR992CardDevice(ILogger logger,
            IEventAggregator eventAggregator,
            IQiacsR992CardDeviceAnalyze dataAnalyze)
            : base(logger, eventAggregator, dataAnalyze)
        {
        }
    }
}
