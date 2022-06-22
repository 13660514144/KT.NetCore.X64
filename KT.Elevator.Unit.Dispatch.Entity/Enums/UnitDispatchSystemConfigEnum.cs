using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Dispatch.Entity.Enums
{
    public class UnitDispatchSystemConfigEnum : BaseEnum
    {
        public static UnitDispatchSystemConfigEnum SERVER_IP { get; } = new UnitDispatchSystemConfigEnum(2, "SERVER_IP", "服务器IP");
        public static UnitDispatchSystemConfigEnum SERVER_PORT { get; } = new UnitDispatchSystemConfigEnum(3, "SERVER_PORT", "服务器端口");
        public static UnitDispatchSystemConfigEnum PUSH_ADDRESS { get; } = new UnitDispatchSystemConfigEnum(4, "PUSH_ADDRESS", "服务器事件上传地址");
        public static UnitDispatchSystemConfigEnum LAST_SYNC_TIME { get; } = new UnitDispatchSystemConfigEnum(5, "LAST_SYNC_TIME", "最后同步时间");

        public static UnitDispatchSystemConfigEnum HANDLE_ELEVATOR_DEVICE_ID { get; } = new UnitDispatchSystemConfigEnum(6, "HANDLE_ELEVATOR_DEVICE_ID", "派梯设备id");
        public static UnitDispatchSystemConfigEnum FACE_APP_ID { get; } = new UnitDispatchSystemConfigEnum(7, "FACE_APP_ID", "人脸AppID");
        public static UnitDispatchSystemConfigEnum FACE_SDK_KEY { get; } = new UnitDispatchSystemConfigEnum(8, "FACE_SDK_KEY", "人脸SDK KEY");
        public static UnitDispatchSystemConfigEnum FACE_ACTIVATE_CODE { get; } = new UnitDispatchSystemConfigEnum(9, "FACE_ACTIVATE_CODE", "人脸激活码");
        public static UnitDispatchSystemConfigEnum ELEVATOR_GROUP_ID { get; } = new UnitDispatchSystemConfigEnum(10, "ELEVATOR_GROUP_ID", "关联电梯组Id");
        public static UnitDispatchSystemConfigEnum DEVICE_FLOOR_ID { get; } = new UnitDispatchSystemConfigEnum(7, "DEVICE_FLOOR_ID", "所在楼层");
        public static UnitDispatchSystemConfigEnum PORT_NAME { get; } = new UnitDispatchSystemConfigEnum(8, "PORT_NAME", "日力端口");

        private UnitDispatchSystemConfigEnum(int code, string value, string text) : base(code, value, text)
        {

        }
    }
}
