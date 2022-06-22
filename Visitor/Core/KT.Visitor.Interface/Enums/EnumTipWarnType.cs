using KT.Common.Core.Enums;
using System.Collections.Generic;
using System.Windows.Media;

namespace KT.Visitor.Data.Enums
{

    public class EnumTipWarnType : BaseEnum
    {
        public static EnumTipWarnType ERROR { get; } = new EnumTipWarnType(1, "ERROR", "错误");
        public static EnumTipWarnType SUCCESS { get; } = new EnumTipWarnType(2, "SUCCESS", "成功");
        public static EnumTipWarnType WARNING { get; } = new EnumTipWarnType(3, "WARNING", "提示");

        public EnumTipWarnType(int code, string value, string text) : base(code, value, text)
        {
        }

        private static object _locker = new object();
        private static List<EnumTipWarnType> _items;
        public static List<EnumTipWarnType> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<EnumTipWarnType>() {
                                ERROR ,
                                SUCCESS,
                                WARNING
                            };
                        }
                    }
                }
                return _items;
            }
        }

    }
}
