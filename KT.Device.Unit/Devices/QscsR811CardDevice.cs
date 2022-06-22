using KT.Device.Unit.CardReaders.Datas;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Device.Unit.Devices
{
    public class QscsR811CardDevice : CardDeviceBase, IQscsR811CardDevice
    {
        public QscsR811CardDevice(ILogger logger,
            IEventAggregator eventAggregator,
            IQscsR811CardDeviceAnalyze dataAnalyze)
            : base(logger, eventAggregator, dataAnalyze)
        {
        }
    }
}
