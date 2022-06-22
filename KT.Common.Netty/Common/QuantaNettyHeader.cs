using KT.Common.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Netty.Common
{
    public class QuantaNettyHeader : QuantaSerializer
    {
        public QuantaNettyHeader()
        {
            Identifier = IdUtil.NewId();
        }

        /// <summary>
        /// 表头(32bytes)
        /// </summary>
        public string Header { get; set; }

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
        /// 状态码
        /// </summary>
        public int Status { get; set; }

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
            Header = ReadString(QuantaNettyHelper.DataHeaderLength);
            Identifier = ReadString(QuantaNettyHelper.DataIdentifierLength);
            Module = ReadInt();
            Command = ReadInt();
            Status = ReadInt();
            DataLength = ReadInt();
            Datas = ReadBytes(DataLength);
        }

        protected override void Write()
        {
            WriteStringFixed(Header, QuantaNettyHelper.DataHeaderLength);
            WriteStringFixed(Identifier, QuantaNettyHelper.DataIdentifierLength);
            WriteInt(Module);
            WriteInt(Command);
            WriteInt(Status);
            WriteInt(DataLength);
            WriteBytesNotLength(Datas);
        }
    }
}
