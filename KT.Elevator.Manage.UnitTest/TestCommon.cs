using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.UnitTest
{
    public class TestCommon
    {
        public static ILoggerFactory AppLoggerFactory =
            LoggerFactory.Create(buliidder =>
            {
                buliidder.AddLog4Net();
            });

        public static ILogger Logger = AppLoggerFactory.CreateLogger("TestCommon Log");
    }
}
