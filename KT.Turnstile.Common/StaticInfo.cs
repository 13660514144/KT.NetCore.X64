using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Common
{
    public class StaticInfo
    {
        /// <summary>
        /// 当前边缘处理器
        /// </summary>
        public static string ServerKey { get; set; }
 
        /// <summary>
        /// 是否已初始化数据
        /// </summary>
        public static int Port { get; set; }

    }
}
