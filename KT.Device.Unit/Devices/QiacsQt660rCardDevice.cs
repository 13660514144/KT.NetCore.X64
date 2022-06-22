using KT.Device.Unit.CardReaders.Datas;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Device.Unit.Devices
{
    public class QiacsQt660rCardDevice : CardDeviceBase, IQiacsQt660rCardDevice
    {
        public QiacsQt660rCardDevice(ILogger logger,
            IEventAggregator eventAggregator,
            IQiacsQt660rCardDeviceAnalyze dataAnalyze)
            : base(logger, eventAggregator, dataAnalyze)
        {
        }
    }
}
