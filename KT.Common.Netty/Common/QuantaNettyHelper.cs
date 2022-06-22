using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Netty.Common
{
    public class QuantaNettyHelper
    {
        public static Encoding Encoding => Encoding.UTF8;
        public static string DataHeader => "c7f89399f9c6431ab37bdb3860fb8e76";
        public static int DataHeaderLength => 32;
        public static int DataIdentifierLength => 32;
        public static int ReceiveMaxLength => 10240;
        public static int HeaderLength => 80;
    }
}
