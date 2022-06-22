using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KT.Elevator.Manage.Service.Devices.Kone
{
    public class KoneHelper
    {
        private static int _num = 0;
        public static uint GetNewMessageId()
        {
            Interlocked.Increment(ref _num);
            return (uint)_num;
        }
    }
}
