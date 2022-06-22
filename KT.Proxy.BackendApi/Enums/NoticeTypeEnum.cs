using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KT.Proxy.BackendApi.Enums
{
    public class NoticeTypeEnum : BaseEnum
    {
        public static NoticeTypeEnum VISITOR_ACCESS_TODAY => new NoticeTypeEnum(1, "VISITOR_ACCESS_TODAY", "当天访客");
        public static NoticeTypeEnum VISITOR_ON_EDIFICE => new NoticeTypeEnum(2, "VISITOR_ON_EDIFICE", "在楼访客");
        public static NoticeTypeEnum STAFF_ON_EDIFICE => new NoticeTypeEnum(3, "STAFF_ON_EDIFICE", "在楼员工");
        public static NoticeTypeEnum ALL_STAFF => new NoticeTypeEnum(4, "ALL_STAFF", "所有员工");

        public NoticeTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public bool IsChecked { get; set; }

        private static object _locker = new object();
        private static List<NoticeTypeEnum> _items;
        public static List<NoticeTypeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<NoticeTypeEnum>() {
                                VISITOR_ACCESS_TODAY,
                                VISITOR_ON_EDIFICE,
                                STAFF_ON_EDIFICE,
                                ALL_STAFF
                            };
                        }
                    }
                }
                return _items;
            }
        }

    }
}
