using DotNetty.Buffers;
using KT.Common.Core.Utils;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Models
{
    /// <summary>
    /// 自定义序列化
    /// </summary>
    public abstract class MitsubishiElipSerializer
    {
        private ILogger<MitsubishiElipSerializer> _logger;
        public MitsubishiElipSerializer(ILogger<MitsubishiElipSerializer> logger)
        {
            _logger = logger;
        }

        protected MitsubishiElipSerializer()
        {
            var loggerFactory = LoggerFactory.Create(buliidder =>
            {
                buliidder.AddLog4Net();
            });

            _logger = loggerFactory.CreateLogger<MitsubishiElipSerializer>();
        }

        public Encoding ENCODING = Encoding.UTF8;

        protected IByteBuffer _writeBuffer;

        protected IByteBuffer _readBuffer;

        /// <summary>
        /// 反序列化具体实现
        /// </summary>
        protected abstract void Read();

        /// <summary>
        /// 序列化具体实现
        /// </summary>
        protected abstract void Write();

        /// <summary>
        /// 从byte数组获取数据
        /// </summary>
        /// <param name="bytes">读取的数组</param>
        /// <returns></returns>
        public MitsubishiElipSerializer ReadFromBytes(byte[] bytes)
        {
            _readBuffer = Unpooled.WrappedBuffer(bytes);
            Read();
            _readBuffer.Clear();
            //释放buffer 
            _readBuffer = null;
            return this;
        }

        /// <summary>
        /// 从buffer获取数据 
        /// </summary>
        /// <param name="readBuffer">readBuffer</param>
        public void ReadFromBuffer(IByteBuffer readBuffer)
        {
            _readBuffer = readBuffer;
            Read();
        }

        /// <summary>
        /// 写入本地buffer
        /// </summary>
        /// <returns></returns>
        public IByteBuffer WriteToLocalBuffer()
        {
            _writeBuffer = Unpooled.Buffer();
            Write();
            return _writeBuffer;
        }

        /// <summary>
        /// 写入目标buffer
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public IByteBuffer WriteToTargetBuffer(IByteBuffer buffer)
        {
            _writeBuffer = buffer;
            Write();
            return _writeBuffer;
        }

        /// <summary>
        /// 返回buffer数组
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            WriteToLocalBuffer();
            byte[] bytes;
            if (_writeBuffer.WriterIndex == 0)
            {
                bytes = new byte[0];
            }
            else
            {
                bytes = new byte[_writeBuffer.WriterIndex];
                _writeBuffer.ReadBytes(bytes);
            }
            _writeBuffer.Clear();
            //释放buffer 
            _writeBuffer = null;
            return bytes;
        }

        protected byte ReadByte()
        {
            return _readBuffer.ReadByte();
        }

        /// <summary>
        /// 读取bytes,从buffer中读取Short长度
        /// </summary>
        /// <returns>读取到的bytes</returns>
        protected byte[] ReadBytes(int length)
        {
            byte[] bytes = new byte[length];
            _readBuffer.ReadBytes(bytes);

            return bytes;
        }

        /// <summary>
        /// 读取剩下所有bytes
        /// </summary>
        /// <returns>读取到的bytes</returns>
        protected byte[] ReadEndBytes()
        {
            //读取乘下所有bytes
            byte[] bytes = new byte[_readBuffer.ReadableBytes];
            _readBuffer.ReadBytes(bytes);

            return bytes;
        }

        /// <summary>
        /// 读取1byte转char(0x31=>'1')
        /// </summary>
        /// <returns></returns>
        protected char ReadChar()
        {
            var @byte = _readBuffer.ReadByte();
            return Convert.ToChar(@byte);
        }

        /// <summary>
        /// 读取1byte转char转换成string值的byte(0x31=>'1'=>"1"=>1)
        /// </summary>
        /// <returns></returns>
        protected byte ReadCharStringByte()
        {
            var @char = ReadChar();
            return Convert.ToByte(@char.ToString());
        }

        /// <summary>
        /// 读取byte[]转char[]转换成string
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        protected uint ReadCharStringUnsignedInt(int length)
        {
            var @string = string.Empty;
            for (int i = 0; i < length; i++)
            {
                @string += ReadChar();
            }
            return Convert.ToUInt32(@string);
        }

        protected bool ReadBoolean()
        {
            return _readBuffer.ReadBoolean();
        }

        protected short ReadShort()
        {
            return _readBuffer.ReadShort();
        }

        protected ushort ReadUnsignedShort()
        {
            return _readBuffer.ReadUnsignedShort();
        }
        protected int ReadInt()
        {
            return _readBuffer.ReadInt();
        }
        protected uint ReadUnsignedInt()
        {
            return _readBuffer.ReadUnsignedInt();
        }
        protected long ReadLong()
        {
            return _readBuffer.ReadLong();
        }

        protected float ReadFloat()
        {
            return _readBuffer.ReadFloat();
        }

        protected double ReadDouble()
        {
            return _readBuffer.ReadDouble();
        }

        protected string ReadString()
        {
            int lenth = _readBuffer.ReadShort();
            if (lenth <= 0)
            {
                return string.Empty;
            }

            byte[] bytes = new byte[lenth];
            _readBuffer.ReadBytes(bytes);

            return ENCODING.GetString(bytes);
        }

        protected List<T> ReadList<T>(int count, int length) where T : MitsubishiElipSerializer, new()
        {
            var list = new List<T>();
            for (int i = 0; i < count; i++)
            {
                list.Add(ReadObject<T>(length));
            }
            return list;
        }

        protected T ReadObject<T>(int length) where T : MitsubishiElipSerializer, new()
        {
            var clz = new T();
            try
            {
                var bytes = ReadBytes(length);
                var temp = (MitsubishiElipSerializer)(object)clz;
                temp.ReadFromBytes(bytes);
                return (T)temp;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected MitsubishiElipSerializer WriteByte(byte value)
        {
            _writeBuffer.WriteByte(value);
            return this;
        }

        /// <summary>
        /// 写入byte[],不会加入byte长度
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected MitsubishiElipSerializer WriteBytes(byte[] value)
        {
            _writeBuffer.WriteBytes(value);
            return this;
        }

        protected MitsubishiElipSerializer WriteChar(char value)
        {
            //'1'=>0x31
            return WirteAsciiStringOne(value.ToString());
        }


        /// <summary>
        /// 读取1byte转char转换成string值的byte(0x31=>'1'=>"1"=>1)
        /// </summary>
        /// <returns></returns>
        protected MitsubishiElipSerializer WriteByteStringChar(byte value)
        {
            //"1"=>0x31
            return WirteAsciiStringOne(value.ToString());
        }

        /// <summary>
        /// 写入1byte的Ascii字符
        /// </summary>
        /// <returns></returns>
        protected MitsubishiElipSerializer WirteAsciiStringOne(string value)
        {
            //"1"=>0x31
            var bytes = Encoding.ASCII.GetBytes(value.ToString());
            if (bytes.Count() > 1)
            {
                throw new Exception($"char转byte失败：转换为多个字节，当前不支持！");
            }
            var @byte = bytes.FirstOrDefault();
            _writeBuffer.WriteByte(@byte);
            return this;
        }

        /// <summary>
        /// 读取byte[]转char[]转换成string
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        protected MitsubishiElipSerializer WriteUnsignedIntStringChar(uint value, int length = 4)
        {
            var @string = value.ToString().PadLeft(length, '0');
            var chars = @string.ToArray();
            foreach (var item in chars)
            {
                WriteChar(item);
            }
            return this;
        }


        protected MitsubishiElipSerializer WriteBoolean(bool value)
        {
            _writeBuffer.WriteBoolean(value);
            return this;
        }
        protected MitsubishiElipSerializer WriteShort(short value)
        {
            _writeBuffer.WriteShort(value);
            return this;
        }
        protected MitsubishiElipSerializer WriteUnsignedShort(ushort value)
        {
            _writeBuffer.WriteUnsignedShort(value);
            return this;
        }
        protected MitsubishiElipSerializer WriteInt(int value)
        {
            _writeBuffer.WriteInt(value);
            return this;
        }
        protected MitsubishiElipSerializer WriteUnsignedInt(uint value)
        {
            _writeBuffer.WriteInt((int)value);
            return this;
        }

        protected MitsubishiElipSerializer WriteLong(long value)
        {
            _writeBuffer.WriteLong(value);
            return this;
        }

        protected MitsubishiElipSerializer WriteFloat(float value)
        {
            _writeBuffer.WriteFloat(value);
            return this;
        }

        protected MitsubishiElipSerializer WriteDouble(double value)
        {
            _writeBuffer.WriteDouble(value);
            return this;
        }

        protected MitsubishiElipSerializer WriteList<T>(List<T> value) where T : MitsubishiElipSerializer
        {
            foreach (T item in value)
            {
                WriteObject(item);
            }
            return this;
        }

        protected MitsubishiElipSerializer WriteObject<T>(T obj) where T : MitsubishiElipSerializer
        {
            obj.WriteToTargetBuffer(_writeBuffer);
            return this;
        }

        protected MitsubishiElipSerializer WriteString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                WriteShort(0);
                return this;
            }

            byte[] data = ENCODING.GetBytes(value);
            short len = (short)data.Length;
            _writeBuffer.WriteShort(len);
            _writeBuffer.WriteBytes(data);
            return this;
        }


        /// <summary>
        /// 获取真实楼层数据
        /// </summary>
        /// <param name="realFloors"></param>
        /// <returns></returns>
        public List<byte> ToSourceFloors(List<byte> realFloors)
        {
            return ByteBitUtil.ToBitValues(realFloors, 255);
        }


        /// <summary>
        /// 获取真实楼层数据
        /// </summary>
        /// <param name="sourceFloors"></param>
        /// <returns></returns>
        public List<byte> ToRealFloors(List<byte> sourceFloors)
        {
            return ByteBitUtil.ToByteValues(sourceFloors);
        }
    }
}