using KT.Device.Unit.CardReaders.Datas;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Device.Unit.Devices
{
    public class QscsBx5xCardDevice : CardDeviceBase, IQscsBx5xCardDevice
    {
        public QscsBx5xCardDevice(ILogger logger,
            IEventAggregator eventAggregator,
            IQscsBx5xCardDeviceAnalyze dataAnalyze)
            : base(logger, eventAggregator, dataAnalyze)
        {
        }
    }
}
