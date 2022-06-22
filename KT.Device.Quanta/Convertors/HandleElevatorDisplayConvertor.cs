using KT.Quanta.Common.Models;
using KT.Quanta.Unit.Model.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using KT.Common.Core.Utils;

namespace KT.Device.Quanta.Convertors
{
    public class HandleElevatorDisplayConvertor
    {
        public static HandleElevatorDisplayRequest ToRequest(HandleElevatorDisplayModel model)
        {
            var request = new HandleElevatorDisplayRequest();
            request.DestinationFloorName = model.DestinationFloorName;
            request.ElevatorName = model.ElevatorName;

            return request;
        }
    }
}
