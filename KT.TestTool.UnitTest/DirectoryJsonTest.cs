using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Turnstile.Unit.ClientApp.Device.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Unicode;

namespace KT.TestTool.UnitTest
{
    [TestClass]
    public class DirectoryJsonTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var jsonDic = new Dictionary<string, int>();
            jsonDic.Add("HIKVISION_DS_K2210", 0);
            jsonDic.Add("HIKVISION_DS_K1T672MW", 1);
            jsonDic.Add("HIKVISION_DS_K5604Z_ZZH", 1);

            var json = JsonConvert.SerializeObject(jsonDic, JsonUtil.JsonSettings);

            Console.WriteLine(json);
        }
    }
}
