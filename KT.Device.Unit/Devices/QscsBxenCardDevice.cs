using KT.Device.Unit.CardReaders.Datas;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Device.Unit.Devices
{
    public class QscsBxenCardDevice : CardDeviceBase, IQscsBxenCardDevice
    {
        public QscsBxenCardDevice(ILogger logger,
            IEventAggregator eventAggregator,
            IQscsBxenCardDeviceAnalyze dataAnalyze)
            : base(logger, eventAggregator, dataAnalyze)
        {
        }
    }
}
