using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.TestTool.TestApp.Models
{
    public class ProwatchAccessTypeEnum   : BaseEnum
    {
        public static ProwatchAccessTypeEnum WEB_API { get; } = new ProwatchAccessTypeEnum(1, "WEB_API", "WebApi访问");
        public static ProwatchAccessTypeEnum SDK_API { get; } = new ProwatchAccessTypeEnum(2, "SDK_API", "SdkApi访问");

        public ProwatchAccessTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        private static List<ProwatchAccessTypeEnum> _items;
        public static List<ProwatchAccessTypeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<ProwatchAccessTypeEnum>() {
                        WEB_API,
                        SDK_API
                    };
                }
                return _items;
            }
        }
    }
}