using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Helpers
{
    public class MitsubishiElsgwHelper
    {
        /// <summary>
        /// 包头
        /// </summary>
        public readonly static ushort Header = 5936;

        /// <summary>
        /// 数据过大时丢弃
        /// </summary>
        public readonly static ushort DiscardMin = 2048;


        /// <summary>
        /// 获取自增长数字
        /// </summary>
        private static int _sequenceNumber = 0;
        public static byte GetSequenceNumber()
        {
            if (_sequenceNumber >= 255)
            {
                _sequenceNumber = 0;
            }
            Interlocked.Increment(ref _sequenceNumber);
            return (byte)_sequenceNumber;
        }
    }
}
