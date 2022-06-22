using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using HelperIpHlpApi;
namespace KT.Quanta.WebApi.Common.Helper
{
    public class ElevatorMessageKey
    {
        public MessageKey MsgInfo=new MessageKey();
        public List<MessageKey> ListKey = new List<MessageKey>();
 
        public class MessageKey
        {
            public string Key { get; set; }
            public string ElevatorName { get; set; }
            public string FloorName { get; set; }
            public string ReturnKey { get; set; } = string.Empty;
            public long UtcTime { get; set; }
        }
      
    }
}
