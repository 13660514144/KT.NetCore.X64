using KT.Common.Core.Utils;
using KT.Proxy.BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Proxy.BackendApi.Helpers
{
    public static class RequestHelper
    {
        public static string BackendBaseUrl { get; set; }

        public static Dictionary<string, string> GetHeaders()
        {
            var dicsign = SetHeaders();

            var _dic = dicsign.OrderBy(p => p.Key);
            var _serc = string.Empty;
            foreach (var item in _dic)
            {
                if (item.Key == "secret")
                {
                    continue;
                }
                _serc += item.Key + "=" + item.Value + "&";
            }
            _serc += "secret=" + dicsign["secret"];
            dicsign.Add("sign", _serc.Encrypt());

            dicsign.Remove("secret");
            return dicsign;
        }

        private static Dictionary<string, string> SetHeaders()
        {
            var dic = new Dictionary<string, string>();
            dic.Add("timestamp", DateTimeUtil.UtcNowSeconds().ToString());
            dic.Add("nonce", Guid.NewGuid().ToString("P"));
            dic.Add("token", MasterUser.Token);
            dic.Add("platform", "PC");
            dic.Add("version", "1.0.0");
            dic.Add("secret", MasterUser.Secret);
            return dic;
        }

    }

}
