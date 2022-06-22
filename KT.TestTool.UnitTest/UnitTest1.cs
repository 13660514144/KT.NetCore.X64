using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Turnstile.Unit.ClientApp.Device.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Text.Unicode;

namespace KT.TestTool.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var bytes = new byte[] { 121, 155, 229, 235, 249, 255 };
            var hex = BitConverter.ToString(bytes, 0);
            var context = Encoding.ASCII.GetString(bytes);
            var ut8 = Encoding.UTF8.GetString(bytes);

            var str = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(bytes);

            var stringBuilder = new StringBuilder();
            foreach (byte b in bytes)
            {
                stringBuilder.Append(b.ToString("X2"));
            }
            var result = stringBuilder.ToString();

            var longResult = Convert.ToInt64(result, 16);

            Console.WriteLine(context);
        }

        [TestMethod]
        public void Test2()
        {
            string key = "%QUANTA@DATA$COM";
            //string hexData = "4DF568BC83AC51372A0A4C21EE4D6F8601E1AD01E532DAAFC342C42B86ED693A0FA1568FE5A81C7B9EF5AB608EC8858C";
            //string hexData = "3A15B7DF922CE697098580B5BC0DE43C34A0D1303FAF062EF654E3093B84EC450FA1568FE5A81C7B9EF5AB608EC8858C";
            string hexData = "F74790405FE98F8B3B43AE4EB0303EBF6680DCFB35F3C0835A04BCF6B71F8D340FA1568FE5A81C7B9EF5AB608EC8858C";

            //解密
            var data = hexData.AesDecryptHex(key);
            // 解密

            var values = data.Split("#");


            var result = new CardReceiveModel();
            if (values.Length >= 4)
            {
                result.CardNumber = values[0];
                result.IsCheckDate = values[1] == "1";
                result.StartTime = ConvertUtil.ToLong(values[2]);
                result.EndTime = ConvertUtil.ToLong(values[3]);
            }
            else
            {
                result.CardNumber = values[0];
                result.IsCheckDate = false;
            }

            //较验二维码时间
            if (result.IsCheckDate)
            {
                var now = DateTimeUtil.UtcNowSeconds();
                if (result.StartTime.HasValue && result.StartTime > now)
                {
                    throw CustomException.Run("二维码起用时间未到：qrDate:{0} now:{1} ", result.StartTime, now);
                }
                if (result.EndTime.HasValue && result.EndTime < now)
                {
                    throw CustomException.Run("二维码过期：qrEndDate:{0} now:{1} ", result.EndTime, now);
                }

            }
        }
    }
}
