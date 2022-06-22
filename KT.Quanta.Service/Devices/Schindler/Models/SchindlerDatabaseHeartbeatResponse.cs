using KT.Quanta.Service.Devices.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    /// <summary>
    /// "I am Alive" Acknowledgement Message  
    /// I am alive acknowledgement message (function type 01):
    /// </summary>
    public class SchindlerDatabaseHeartbeatResponse : SchindlerDatabaseSerializer
    {
        /// <summary>
        /// 构造函数初始数据
        /// </summary>
        public SchindlerDatabaseHeartbeatResponse()
        {
            // FunctionType = 1;
        }

        protected override void Read()
        {

        }

        protected override void Write()
        {

        }
    }
}
