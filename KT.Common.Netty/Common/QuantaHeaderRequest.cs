using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Netty.Common
{
    public class QuantaHeaderRequest : QuantaSerializer
    {
        /// <summary>
        /// 表头(32bytes)
        /// </summary>
        public string Header { get; set; } = "c7f89399f9c6431ab37bdb3860fb8e76";

        /// <summary>
        /// 唯一标识，发送什么返回什么(32bytes)
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// 模块号
        /// </summary>
        public int Module { get; set; }

        /// <summary>
        /// 命令号
        /// </summary>
        public int Command { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public byte[] Datas { get; set; }


        protected override void Read()
        {
            Header = ReadString(32);
            Identifier = ReadString(32);
            Module = ReadInt();
            Command = ReadInt();
            //Status = ReadInt();
            DataLength = ReadInt();
            Datas = ReadBytes(DataLength);
        }

        protected override void Write()
        {
            WriteStringFixed(Header);
            WriteStringFixed(Identifier);
            WriteInt(Module);
            WriteInt(Command);
            //WriteInt(Status);
            WriteInt(DataLength);
            WriteBytesNotLength(Datas);
        }
    }
}
