using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Network.NettyServer
{
    public class NettyHttpHelper
    {
        public static readonly NettyThreadLocalCache Cache = new NettyThreadLocalCache();
        public static readonly AsciiString TypePlain = AsciiString.Cached("text/plain");
        public static readonly AsciiString TypeJson = AsciiString.Cached("application/json");
        public static readonly AsciiString ServerName = AsciiString.Cached("Quanta Elevator App");
        public static readonly AsciiString ContentTypeEntity = HttpHeaderNames.ContentType;
        public static readonly AsciiString DateEntity = HttpHeaderNames.Date;
        public static readonly AsciiString ContentLengthEntity = HttpHeaderNames.ContentLength;
        public static readonly AsciiString ServerEntity = HttpHeaderNames.Server;

    }
}
