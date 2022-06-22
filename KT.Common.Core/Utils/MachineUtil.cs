using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace KT.Common.Core.Utils
{
    /// <summary>
    /// 获取硬盘号和CPU号
    /// </summary>
    public class MachineUtil
    {
        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns></returns>
        public static List<string> GetIp()
        {
            var networks = NetworkInterface.GetAllNetworkInterfaces();
            var network = networks.Select(p => p.GetIPProperties())
                  .SelectMany(p => p.UnicastAddresses)
                  .Where(p => p.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                        && !System.Net.IPAddress.IsLoopback(p.Address));

            var ips = new List<string>();
            if (network != null)
            {
                foreach (var item in network)
                {
                    ips.Add(item.Address.ToString());
                }
            }
            return ips;
        }

        public static string GetIp(List<string> startsWith)
        {
            return GetIp().FirstOrDefault(x => startsWith.Any(y => x.StartsWith(y)));
        }
        public static string GetIp(string startsWith)
        {
            return GetIp().FirstOrDefault(x => x.StartsWith(startsWith));
        }
    }
}