using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace KT.TestTool.TestApp.Helpers
{
    public class ProwatchPrintHelper
    {
        private ILogger<ProwatchPrintHelper> _logger;
        public ProwatchPrintHelper(ILogger<ProwatchPrintHelper> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo">1000130000L</param>
        /// <returns></returns>
        public string GetCardNoValue(int cardNo)
        {
            byte[] bs1 = new byte[] { 0x61 };

            byte[] bs2 = BitConverter.GetBytes(cardNo);

            bs2 = ArrayByteUtil.MergeArray(new byte[] { 0x00, 0x00, 0x00, 0x00 }, bs2);

            int seconds = (int)(DateTimeUtil.UtcNowSeconds() - 3600);
            byte[] bs3 = BitConverter.GetBytes(seconds);

            int expires = 86400 + 3600 * 8;//3600*24*7;
            byte[] bs4 = BitConverter.GetBytes(expires);

            byte[] bs5 = new byte[] { 0x00 };

            byte[] bs6 = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00 };

            byte[] concat = ArrayByteUtil.MergeArray(bs1, bs2, bs3, bs4, bs5, bs6);

            var bytes = DesUtil.Encrypt3Des(concat, "Radio888ATTeBDsPKmGYM4Ud");

            var encode = Convert.ToBase64String(bytes);

            String qr = "**" + encode;

            return qr;
        }


        //public string GetCardNoValue(int cardNo)
        //{
        //    byte[] bs1 = new byte[] { 0x61 };

        //    byte[] bs2 = BitConverter.GetBytes(cardNo);

        //    bs2 = ArrayByteUtil.MergeArray(new byte[] { 0x00, 0x00, 0x00, 0x00 }, bs2);

        //    int seconds = (int)"2020-04-22 08:00:00".ToUtcSeconds();
        //    byte[] bs3 = BitConverter.GetBytes(seconds);

        //    int expires = 86400 + 3600 * 8;//3600*24*7;
        //    byte[] bs4 = BitConverter.GetBytes(expires);

        //    byte[] bs5 = new byte[] { 0x00 };

        //    byte[] bs6 = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00 };

        //    byte[] concat = ArrayByteUtil.MergeArray(bs1, bs2, bs3, bs4, bs5, bs6);

        //    var bytes = DesUtil.Encrypt3Des(concat, "Radio888ATTeBDsPKmGYM4Ud");

        //    var encode = Convert.ToBase64String(bytes);

        //    String qr = "**" + encode;

        //    return qr;
        //}
    }
}
