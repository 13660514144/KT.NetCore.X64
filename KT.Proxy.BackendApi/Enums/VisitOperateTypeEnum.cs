using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Enums
{
    public class VisitOperateTypeEnum : BaseEnum
    {
        public static VisitOperateTypeEnum CANCEL { get; } = new VisitOperateTypeEnum(1, "CANCEL", "取消访问");
        public static VisitOperateTypeEnum FINISH { get; } = new VisitOperateTypeEnum(2, "FINISH", "结束访问");

        private VisitOperateTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        private static object _locker = new object();
        private static List<VisitOperateTypeEnum> _items;
        public static List<VisitOperateTypeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<VisitOperateTypeEnum>() {
                                CANCEL ,
                                FINISH
                            };
                        }
                    }
                }
                return _items;
            }
        }

        //根据Name获取对像
        public static VisitOperateTypeEnum GetByValue(string value)
        {
            return Items.FirstOrDefault(x => x.Value == value);
        }

        //根据Name获取对像
        public static VisitOperateTypeEnum GetByCode(int code)
        {
            return Items.FirstOrDefault(x => x.Code == code);
        }

        //根据Name获取对像
        public static VisitOperateTypeEnum GetByText(int code)
        {
            return Items.FirstOrDefault(x => x.Code == code);
        }
    }
}
