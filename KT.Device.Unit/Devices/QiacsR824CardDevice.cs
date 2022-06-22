using KT.Device.Unit.CardReaders.Datas;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Device.Unit.Devices
{
    public class QiacsR824CardDevice : CardDeviceBase, IQiacsR824CardDevice
    {
        public QiacsR824CardDevice(ILogger logger,
            IEventAggregator eventAggregator,
            IQiacsR824CardDeviceAnalyze dataAnalyze)
            : base(logger, eventAggregator, dataAnalyze)
        {
        }
    }
}
