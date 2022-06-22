using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Data.Enums
{
    public class VisitorAuthTypeEnum : BaseEnum
    {
        public VisitorAuthTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        private static object _locker = new object();
        private static List<VisitorAuthTypeEnum> _items;
        public static List<VisitorAuthTypeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<VisitorAuthTypeEnum>() {
                                IDENTITY_AUTH,
                                INVITE_AUTH
                            };
                        }
                    }
                }
                return _items;
            }
        }

        public static VisitorAuthTypeEnum IDENTITY_AUTH { get; } = new VisitorAuthTypeEnum(0, "IDENTITY_AUTH", "身份验证");
        public static VisitorAuthTypeEnum INVITE_AUTH { get; } = new VisitorAuthTypeEnum(1, "INVITE_AUTH", "邀约验证");

    }
}
