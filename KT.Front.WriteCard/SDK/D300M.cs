using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace KT.Front.WriteCard.SDK
{
    public class D300M
    {
        // 是否连接
        private static bool _isConnect { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="reConnect">是否重连：true:已连接断开连接后重连 false:已连接不操作直接返回</param>
        /// <returns></returns>
        public static Task ConnectAsync(bool reConnect = true)
        {
            int st = -1;
            if (_isConnect)
            {
                if (!reConnect)
                {
                    return Task.CompletedTask;
                }
                st = D300MSdk.USBHidExitCommunicate();
                if (st == 0)
                {
                    _isConnect = false;
                }
            }
            else
            {
                st = D300MSdk.USBHidInitCommunicate();
                if (st == 0)
                {
                    _isConnect = true;
                }
            }

            return Task.CompletedTask;
        }
    }
}
