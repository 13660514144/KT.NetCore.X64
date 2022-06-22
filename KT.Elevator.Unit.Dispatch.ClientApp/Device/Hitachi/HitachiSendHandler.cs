using KT.Device.Unit.Devices;
using KT.Elevator.Unit.Dispatch.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Device.Hitachi
{
    public class HitachiSendHandler
    {
        private readonly HitachiDeviceClient _hitachiDeviceClient;
        private readonly HitachiDataAnalyze _hitachiDataAnalyze;
        public HitachiSendHandler(HitachiDeviceClient hitachiDeviceClient,
            HitachiDataAnalyze hitachiDataAnalyze)
        {
            _hitachiDeviceClient = hitachiDeviceClient;
            _hitachiDataAnalyze = hitachiDataAnalyze;
        }

        public void DispatchSendAsync(UnitDispatchSendHandleElevatorModel obj)
        {
            var bytes = _hitachiDataAnalyze.SendAnalyze(obj);
            _hitachiDeviceClient.SendAsync(bytes);
        }

        internal void ConfirmSendAsync(HitachiReceiveModel obj)
        { 
            var bytes = _hitachiDataAnalyze.ConfirmAnalyze(obj);
            _hitachiDeviceClient.SendAsync(bytes);
        }
    }
}
