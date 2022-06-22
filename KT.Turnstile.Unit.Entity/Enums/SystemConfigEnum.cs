using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.Entity.Enums
{
    public class SystemConfigEnum : BaseEnum
    {
        public static SystemConfigEnum SERVER_IP { get; } = new SystemConfigEnum(2, "SERVER_IP", "服务器IP");
        public static SystemConfigEnum SERVER_PORT { get; } = new SystemConfigEnum(3, "SERVER_PORT", "服务器端口");
        public static SystemConfigEnum PUSH_ADDRESS { get; } = new SystemConfigEnum(4, "PUSH_ADDRESS", "服务器事件上传地址");
        public static SystemConfigEnum LAST_SYNC_TIME { get; } = new SystemConfigEnum(5, "LAST_SYNC_TIME", "最后同步时间");
        public static SystemConfigEnum CLIENT_IP { get; } = new SystemConfigEnum(6, "CLIENT_IP", "服务器IP");

        private SystemConfigEnum(int code, string value, string text) : base(code, value, text)
        {

        }
    }
}
