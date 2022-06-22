using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Data.Enums
{
    public class VisitorFromEnum : BaseEnum
    {
        public static VisitorFromEnum WEIXIN { get; } = new VisitorFromEnum(1, "WEIXIN", "微信");
        public static VisitorFromEnum SELF_HELP { get; } = new VisitorFromEnum(2, "SELF_HELP", "自助机");
        public static VisitorFromEnum FRONT_DESK { get; } = new VisitorFromEnum(3, "FRONT_DESK", "前台预约");

        public VisitorFromEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        private static object _locker = new object();
        private static List<VisitorFromEnum> _items;
        public static List<VisitorFromEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<VisitorFromEnum>() {
                                WEIXIN ,
                                SELF_HELP,
                                FRONT_DESK
                            };
                        }
                    }
                }
                return _items;
            }
        }
        private static List<VisitorFromEnum> _viewItems;
        public static List<VisitorFromEnum> ViewItems
        {
            get
            {
                if (_viewItems == null)
                {
                    _viewItems = new List<VisitorFromEnum>() {
                        new VisitorFromEnum(0, "", "全部"),
                        WEIXIN,
                        SELF_HELP,
                        FRONT_DESK
                    };
                }
                return _viewItems;
            }
        }

        public static VisitorFromEnum GetByValueOrText(string val)
        {
            return Items.FirstOrDefault(x => x.Value == val || x.Text == val);
        }

        public static string GetValueByValueOrText(string val)
        {
            var result = Items.FirstOrDefault(x => x.Value == val || x.Text == val);
            return result?.Value;
        }
    }
}
