using KT.Common.Core.Enums;
using System.Collections.Generic;
using System.Linq;

namespace KT.Common.Core.Enums
{
    public class GenderEnum : BaseEnum
    {
        public static GenderEnum MALE { get; } = new GenderEnum(1, "MALE", "男");
        public static GenderEnum FEMALE { get; } = new GenderEnum(2, "FEMALE", "女");

        public GenderEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        private static List<GenderEnum> _items;
        public static List<GenderEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<GenderEnum>() {
                        MALE,
                        FEMALE
                    };
                }
                return _items;
            }
        }

        private static List<GenderEnum> _viewItems;
        public static List<GenderEnum> ViewItems
        {
            get
            {
                if (_viewItems == null)
                {
                    _viewItems = new List<GenderEnum>() {
                        new GenderEnum(0, "", "全部"),
                        MALE,
                        FEMALE
                    };
                }
                return _viewItems;
            }
        }

        public static GenderEnum GetByValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            var item = Items.FirstOrDefault(x => x.Value == value);
            return item;
        }

        public static GenderEnum GetByText(string text)
        {
            return Items.FirstOrDefault(x => x.Text == text);
        }
    }
}
