using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secodary.Entity.Enums
{
    public class UnitSystemConfigEnum : BaseEnum
    {
        public static UnitSystemConfigEnum SERVER_IP { get; } = new UnitSystemConfigEnum(2, "SERVER_IP", "服务器IP");
        public static UnitSystemConfigEnum SERVER_PORT { get; } = new UnitSystemConfigEnum(3, "SERVER_PORT", "服务器端口");
        public static UnitSystemConfigEnum PUSH_ADDRESS { get; } = new UnitSystemConfigEnum(4, "PUSH_ADDRESS", "服务器事件上传地址");
        public static UnitSystemConfigEnum LAST_SYNC_TIME { get; } = new UnitSystemConfigEnum(5, "LAST_SYNC_TIME", "最后同步时间");

        public static UnitSystemConfigEnum HANDLE_ELEVATOR_DEVICE_ID { get; } = new UnitSystemConfigEnum(6, "HANDLE_ELEVATOR_DEVICE_ID", "派梯设备id");
        public static UnitSystemConfigEnum FACE_APP_ID { get; } = new UnitSystemConfigEnum(7, "FACE_APP_ID", "人脸AppID");
        public static UnitSystemConfigEnum FACE_SDK_KEY { get; } = new UnitSystemConfigEnum(8, "FACE_SDK_KEY", "人脸SDK KEY");
        public static UnitSystemConfigEnum FACE_ACTIVATE_CODE { get; } = new UnitSystemConfigEnum(9, "FACE_ACTIVATE_CODE", "人脸激活码");
        public static UnitSystemConfigEnum ELEVATOR_GROUP_ID { get; } = new UnitSystemConfigEnum(10, "ELEVATOR_GROUP_ID", "关联电梯组Id");
        public static UnitSystemConfigEnum DEVICE_FLOOR_ID { get; } = new UnitSystemConfigEnum(7, "DEVICE_FLOOR_ID", "所在楼层");

        private UnitSystemConfigEnum(int code, string value, string text) : base(code, value, text)
        {

        }
    }
}
