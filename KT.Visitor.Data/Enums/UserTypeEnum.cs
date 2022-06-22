using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Data.Enums
{
    public class UserTypeEnum : BaseEnum
    {
        public static UserTypeEnum USER_SYSTEM_INLAY { get; } = new UserTypeEnum(1, "SYSTEM_USER_INLAY", "系统用自带户名");
        public static UserTypeEnum USER_LOGIN { get; } = new UserTypeEnum(2, "USER_LOGIN", "登录用户");

        private UserTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }
        private static object _locker = new object();
        private static List<UserTypeEnum> _items;
        public static List<UserTypeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<UserTypeEnum>() {
                                USER_SYSTEM_INLAY ,
                                USER_LOGIN
                            };
                        }
                    }
                }
                return _items;
            }
        }

        //根据Name获取对像
        public static UserTypeEnum GetByValue(string value)
        {
            return Items.FirstOrDefault(x => x.Value == value);
        }
        //根据Name获取对像
        public static UserTypeEnum GetByCode(int code)
        {
            return Items.FirstOrDefault(x => x.Code == code);
        }
        //根据Name获取对像
        public static UserTypeEnum GetByText(int code)
        {
            return Items.FirstOrDefault(x => x.Code == code);
        }
    }
}
