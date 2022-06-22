using KT.Common.Core.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KT.Visitor.Data.Enums
{
    public class StatusEnum : BaseEnum
    {
        public static StatusEnum TRUE { get; } = new StatusEnum(1, "TRUE", "成功");
        public static StatusEnum FALSE { get; } = new StatusEnum(2, "FALSE", "失败");

        public StatusEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        private static object _locker = new object();
        private static List<StatusEnum> _items;
        public static List<StatusEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<StatusEnum>() {
                                TRUE,
                                FALSE,
                            };
                        }
                    }
                }
                return _items;
            }
        }

        public static ObservableCollection<StatusEnum> GetVMs(bool isAddAll = false)
        {
            var results = new ObservableCollection<StatusEnum>(Items);

            results.Insert(0, new StatusEnum(0, string.Empty, "全部"));

            return results;
        }
    }
}
