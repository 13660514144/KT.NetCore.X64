using DotNetty.Common;
using DotNetty.Common.Utilities;
using System;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Network.NettyServer
{
    public class NettyThreadLocalCache : FastThreadLocal<AsciiString>
    {
        protected override AsciiString GetInitialValue()
        {
            var dateTime = DateTime.UtcNow;
            return AsciiString.Cached($"{dateTime.DayOfWeek}, {dateTime:dd MMM yyyy HH:mm:ss z}");
        }
    }
}