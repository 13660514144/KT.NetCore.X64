using KT.Turnstile.Unit.Entity.Entities;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Unit.ClientApp.Events
{
    public class CardGetMysqlDataEvent : PubSubEvent<TurnstileUnitCardDeviceEntity>
    {
    }
}
