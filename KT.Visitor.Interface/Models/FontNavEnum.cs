using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Interface.Models
{
    /// <summary>
    /// 前台导航菜单
    /// </summary>
    public class FontNavEnum : BaseEnum
    {
        public FontNavEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static FontNavEnum VISITOR_REGISTE => new FontNavEnum(1, "VISITOR_REGISTE", "访客登记");
        public static FontNavEnum VISITOR_RECORD => new FontNavEnum(2, "VISITOR_RECORD", "访客记录");
        public static FontNavEnum BLACKLIST => new FontNavEnum(3, "BLACKLIST", "黑名单");
        public static FontNavEnum EDIT_BLACKLIST => new FontNavEnum(5, "EDIT_BLACKLIST", "修改黑名单");
        public static FontNavEnum VISITOR_DETAIL => new FontNavEnum(6, "VISITOR_DETAIL", "访客详情");
        public static FontNavEnum VISITOR_IMPORT_DETAIL => new FontNavEnum(7, "VISITOR_IMPORT_DETAIL", "访客导入详情");
        public static FontNavEnum VISITOR_IMPORT => new FontNavEnum(7, "VISITOR_IMPORT", "访客导入");
        public static FontNavEnum VISITOR_GATE => new FontNavEnum(8, "VISITOR_GATE", "闸机开关");
        public static FontNavEnum CONTROL_DOOR => new FontNavEnum(8, "CONTROL_DOOR", "开关门控制");
        public static FontNavEnum MESSAGE => new FontNavEnum(8, "MESSAGE", "通知");
    }
}
