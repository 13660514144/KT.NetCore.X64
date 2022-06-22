using KT.Device.Unit.CardReaders.Datas;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Device.Unit.Devices
{
    public class QscsBx5nCardDevice : CardDeviceBase, IQscsBx5nCardDevice
    {
        public QscsBx5nCardDevice(ILogger logger,
            IEventAggregator eventAggregator,
            IQscsBx5nCardDeviceAnalyze dataAnalyze)
            : base(logger, eventAggregator, dataAnalyze)
        {
        }
    }
}
