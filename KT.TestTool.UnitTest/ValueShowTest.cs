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
    public class ValueShowTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var a = int.MaxValue;
            var b = long.MaxValue;
            var c = short.MaxValue;

            Console.WriteLine($"{a},{b},{c}");

            Console.ReadKey();
        } 
    }
}
