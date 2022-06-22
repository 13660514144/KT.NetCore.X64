using System;
using System.Collections.Generic;
using System.Text;

namespace KT.TestTool.ConsoleApp
{
    /// <summary>
    /// ELI         Elevator Locking Interface
    /// RCGIF       Remote Call Giving Interface
    /// GCAC        Group Controller Access Control
    /// DOP         Destination Operating Panel
    /// COP         Car Operating Panel
    /// TSDOP       Touch Screen Destination Operating Panel
    /// ACS         Access Control System
    /// KCEGC       Kone Control & Electrification Group Controller
    /// CT          Certification Test
    /// Royal-GC    Group Controllers(KCEGC) with group control functionality enabled
    /// </summary>
    public class KoneSettings
    {
        public KoneSettings()
        {
            Rcgif = new KoneRcgifSettings();
            Eli = new KoneEliSettings();
        }

        /// <summary>
        /// Remote Call Giving Interface
        /// </summary>
        public KoneRcgifSettings Rcgif { get; set; }

        /// <summary>
        /// Elevator Locking Interface
        /// </summary>
        public KoneEliSettings Eli { get; set; }
    }

    /// <summary>
    /// Remote Call Giving Interface
    /// </summary>
    public class KoneRcgifSettings
    {
        /// <summary>
        /// 事件循环组个数
        /// </summary>
        public int EventLoopGroupCount { get; set; } = 2;

        /// <summary>
        /// 接收最大长度，超过则忽略掉接收的内容
        /// </summary>
        public int DiscardMinimumLength { get; set; } = 1024;
    }

    /// <summary>
    /// Elevator Locking Interface
    /// </summary>
    public class KoneEliSettings
    {
        public KoneEliSettings()
        {
            
        }

        /// <summary>
        /// 事件循环组个数
        /// </summary>
        public int EventLoopGroupCount { get; set; } = 2;

        /// <summary>
        /// 接收最大长度，超过则忽略掉接收的内容
        /// </summary>
        public int DiscardMinimumLength { get; set; } = 1024;

        /// <summary>
        /// Dop open access for dop message type
        /// 1:normal
        /// 2:with call type
        /// </summary>
        public int OpenAccessForDopMessageType { get; set; } = 1;
    }
}