using KT.Common.Core.Enums;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Kone.ToolApp.Enums
{
    public class ConnectedStateEnum : PublicBaseEnum
    {
        public ConnectedStateEnum()
        {
        }

        public ConnectedStateEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static ConnectedStateEnum DisConnected => new ConnectedStateEnum(1, "DISCONNECTED", "断开");
        public static ConnectedStateEnum Connected => new ConnectedStateEnum(2, "CONNECTED", "连接");
        public static ConnectedStateEnum DisconnectedAndConnectedMaskState => new ConnectedStateEnum(3, "DISCONNECTED_AND_CONNECTED", "断开与连接");

        public static List<ConnectedStateEnum> GetAllWithNull()
        {
            var results = PublicBaseEnum.GetAll<ConnectedStateEnum>().ToList();
            results.Insert(0, null);

            return results;
        }
    }
}
