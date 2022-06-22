using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Schindler.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Helpers
{
    public interface ISchindlerHandleElevatorSequenceList : IHandleElevatorSequenceList<int, SchindlerHandleElevatorSequenceModel>
    {
    }
}
