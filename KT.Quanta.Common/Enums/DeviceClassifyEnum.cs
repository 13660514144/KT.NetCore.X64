using KT.Common.Core.Enums;
using System.Collections.Generic;

namespace KT.Quanta.Common.Enums
{
    /// <summary>
    /// 设备型号
    /// </summary>
    public class DeviceClassifyEnum : BaseEnum
    {
        public DeviceClassifyEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static DeviceClassifyEnum SYNC => new DeviceClassifyEnum(1, "SYNC", "迅达电梯同步数据");
        public static DeviceClassifyEnum HANDLE => new DeviceClassifyEnum(2, "HANDLE", "迅达电梯派梯");
        public static DeviceClassifyEnum RECORD => new DeviceClassifyEnum(3, "RECORD", "迅达电梯获取记录");

        private static readonly object _locker = new object();
        private static List<DeviceClassifyEnum> _items;
        public static List<DeviceClassifyEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<DeviceClassifyEnum>()
                            {
                                SYNC ,
                                HANDLE ,
                                RECORD
                            };
                        }
                    }
                }
                return _items;
            }
        }
    }
}
