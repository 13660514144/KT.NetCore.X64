using KT.Quanta.Common.Models;
using KT.Quanta.Unit.Model.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Unit.ClientApp.Convetors
{
    public class HandleElevatorDisplayConvertor
    {
        public static HandleElevatorDisplayModel ToModel(HandleElevatorDisplayRequest request)
        {
            var model = new HandleElevatorDisplayModel();
            model.DestinationFloorName = request.DestinationFloorName;
            model.ElevatorName = request.ElevatorName;

            return model;
        }
    }
}
