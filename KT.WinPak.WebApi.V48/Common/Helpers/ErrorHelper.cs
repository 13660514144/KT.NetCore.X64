using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.WinPak.WebApi.V48.Common.Helpers
{
    public class ErrorHelper
    {
        public static bool IsRpcError(string message)
        {
            //过程过程调用失败重新登录
            if (message.Contains("0x800706BA")
               || message.Contains("远程过程调用失败")
               || message.Contains("0x800706BA")
               || message.Contains("RPC 服务器不可用")
               || message.Contains("0x8004E005")
               || message.Contains("此组件配置为使用同步而且此方法调用将导致发生死锁"))
            {
                return true;
            }
            return false;
        }

        public static bool IsRpcError(Exception ex)
        {
            string message = ex.Message + ex.InnerException?.Message;
            //过程过程调用失败重新登录
            if (message.Contains("0x800706BA")
               || message.Contains("远程过程调用失败")
               || message.Contains("0x800706BA")
               || message.Contains("RPC 服务器不可用")
               || message.Contains("0x8004E005")
               || message.Contains("此组件配置为使用同步而且此方法调用将导致发生死锁"))
            {
                return true;
            }
            return false;
        }
    }
}
