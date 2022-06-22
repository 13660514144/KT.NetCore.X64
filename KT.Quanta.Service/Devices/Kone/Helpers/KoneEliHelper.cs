using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KT.Quanta.Service.Devices.Kone.Helpers
{
    public class KoneEliHelper
    {
        /// <summary>
        /// 表头长度
        /// </summary>
        public static int HeaderLength => 9;

        /// <summary>
        /// 获取自增长数字
        /// </summary>
        private static int _messageId = 0;
        public static uint GetMessageId()
        {
            if (_messageId >= 255)
            {
                _messageId = 0;
            }
            Interlocked.Increment(ref _messageId);
            return (uint)_messageId;
        }
    }
}
