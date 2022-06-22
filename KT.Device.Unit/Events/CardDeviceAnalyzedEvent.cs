using KT.Device.Unit.CardReaders.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Device.Unit.CardReaders.Events
{
    public class CardDeviceAnalyzedEvent : PubSubEvent<CardDeviceAnalyzedModel>
    {

    }

    public class CardDeviceAnalyzedModel
    {
        public CardReceiveModel CardReceive { get; set; }

        public object CardDeviceInfo { get; set; }

    }
}
