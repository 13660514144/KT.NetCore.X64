using KT.Common.Core.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KT.Visitor.Data.Enums
{
    public class VisitStatusEnum : BaseEnum
    {
        public static VisitStatusEnum CANCEL { get; } = new VisitStatusEnum(1, "CANCEL", "已取消");
        public static VisitStatusEnum WAITING_AUDIT { get; } = new VisitStatusEnum(2, "WAITING_AUDIT", "待审核");
        public static VisitStatusEnum AUDIT_FAIL { get; } = new VisitStatusEnum(3, "AUDIT_FAIL", "未过审");
        public static VisitStatusEnum WAITING_VERIFY { get; } = new VisitStatusEnum(4, "WAITING_VERIFY", "待验证");
        public static VisitStatusEnum WAITING_VISIT { get; } = new VisitStatusEnum(5, "WAITING_VISIT", "待来访");
        public static VisitStatusEnum VISITING { get; } = new VisitStatusEnum(6, "VISITING", "访问中");
        public static VisitStatusEnum FINISH { get; } = new VisitStatusEnum(7, "FINISH", "已完成");
        public static VisitStatusEnum UNVISIT { get; } = new VisitStatusEnum(8, "UNVISIT", "过期未访");
        public static VisitStatusEnum OUTTIME_VISIT { get; } = new VisitStatusEnum(9, "OUTTIME_VISIT", "超期逗留");
        public static VisitStatusEnum OUTTIME_AUDIT { get; } = new VisitStatusEnum(10, "OUTTIME_AUDIT", "超时未审核");

        public VisitStatusEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        private static object _locker = new object();
        private static List<VisitStatusEnum> _items;
        public static List<VisitStatusEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<VisitStatusEnum>() {
                                CANCEL,
                                WAITING_AUDIT,
                                AUDIT_FAIL,
                                WAITING_VERIFY,
                                WAITING_VISIT,
                                VISITING ,
                                FINISH ,
                                UNVISIT ,
                                OUTTIME_VISIT,
                                OUTTIME_AUDIT
                            };
                        }
                    }
                }
                return _items;
            }
        }

        public static ObservableCollection<VisitStatusEnum> GetVMs(bool isAddAll = false)
        {
            var results = new ObservableCollection<VisitStatusEnum>(Items);

            results.Insert(0, new VisitStatusEnum(0, string.Empty, "全部"));

            return results;
        }
    }
}
