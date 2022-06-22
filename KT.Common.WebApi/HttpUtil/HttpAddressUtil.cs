using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Common.WebApi.HttpUtil
{
    /// <summary>
    /// Http地址工具
    /// </summary>
    public static class HttpAddressUtil
    {
        public static string GetIpAddress(this HttpContext context)
        {
            StringValues ips;
            var isKey = context.Request.Headers.TryGetValue("X-Forwarded-For", out ips);
            if (!isKey)
            {
                return string.Empty;
            }
            string ip = ips.FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                return string.Empty;
            }
            return ip;
        }
        public static string GetUserAgent(this HttpContext context)
        {
            StringValues userAgents;
            var isKey = context.Request.Headers.TryGetValue("User-Agent", out userAgents);
            if (!isKey)
            {
                return string.Empty;
            }
            string userAgent = userAgents.FirstOrDefault();
            if (string.IsNullOrEmpty(userAgent))
            {
                return string.Empty;
            }
            return userAgent;
        }
    }
}
