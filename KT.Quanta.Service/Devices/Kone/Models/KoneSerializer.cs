using DotNetty.Buffers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Requests
{
    /// <summary>
    /// 自定义序列化
    /// Kone序列化对象函数，默认字符串、对象等不固定长度对象前不增加长度
    /// </summary>
    public abstract class KoneSerializer
    {  
        protected IByteBuffer _writeBuffer;

        protected IByteBuffer _readBuffer;

        private bool _isLittleEndianess = true;

        /// <summary>
        /// 反序列化具体实现
        /// </summary>
        protected abstract void Read(bool isLittleEndianess);

        /// <summary>
        /// 序列化具体实现
        /// </summary>
        protected abstract void Write(bool isLittleEndianess);

        /// <summary>
        /// 从byte数组获取数据
        /// </summary>
        /// <param name="bytes">读取的数组</param>
        /// <returns></returns>
        public KoneSerializer ReadFromBytes(byte[] bytes, bool isLittleEndianess)
        {
            _isLittleEndianess = isLittleEndianess;
            _readBuffer = Unpooled.WrappedBuffer(bytes);
            Read(isLittleEndianess);
            _readBuffer.Clear();
            //释放buffer 
            _readBuffer = null;
            return this;
        }

        /// <summary>
        /// 从buffer获取数据 
        /// </summary>
        /// <param name="readBuffer">readBuffer</param>
        public void ReadFromBuffer(IByteBuffer readBuffer, bool isLittleEndianess)
        {
            _isLittleEndianess = isLittleEndianess;
            _readBuffer = readBuffer;
            Read(isLittleEndianess);
        }

        /// <summary>
        /// 写入本地buffer
        /// </summary>
        /// <returns></returns>
        public IByteBuffer WriteToLocalBuffer(bool isLittleEndianess)
        {
            _isLittleEndianess = isLittleEndianess;
            _writeBuffer = Unpooled.Buffer();
            Write(isLittleEndianess);
            return _writeBuffer;
        }

        /// <summary>
        /// 写入目标buffer
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public IByteBuffer WriteToTargetBuffer(IByteBuffer buffer, bool isLittleEndianess)
        {
            _isLittleEndianess = isLittleEndianess;
            _writeBuffer = buffer;
            Write(isLittleEndianess);
            return _writeBuffer;
        }

        /// <summary>
        /// 返回buffer数组
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes(bool isLittleEndianess)
        {
            WriteToLocalBuffer(isLittleEndianess);
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
        /// 直接读取固定长度的Byte[]
        /// </summary>
        /// <param name="lenght"></param>
        /// <returns></returns>
        protected byte[] ReadBytes(int lenght)
        {
            var bytes = new byte[lenght];
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

        protected short ReadShort()
        {
            if (_isLittleEndianess)
            {
                return _readBuffer.ReadShortLE();
            }
            else
            {
                return _readBuffer.ReadShort();
            }
        }

        protected int ReadInt()
        {
            if (_isLittleEndianess)
            {
                return _readBuffer.ReadIntLE();
            }
            else
            {
                return _readBuffer.ReadInt();
            }
        }

        protected long ReadLong()
        {
            if (_isLittleEndianess)
            {
                return _readBuffer.ReadLongLE();
            }
            else
            {
                return _readBuffer.ReadLong();
            }
        }

        protected float ReadFloat()
        {
            if (_isLittleEndianess)
            {
                return _readBuffer.ReadFloatLE();
            }
            else
            {
                return _readBuffer.ReadFloat();
            }
        }

        protected double ReadDouble()
        {
            if (_isLittleEndianess)
            {
                return _readBuffer.ReadDoubleLE();
            }
            else
            {
                return _readBuffer.ReadDouble();
            }
        }

        protected ushort ReadUnsignedShort()
        {
            if (_isLittleEndianess)
            {
                return _readBuffer.ReadUnsignedShortLE();
            }
            else
            {
                return _readBuffer.ReadUnsignedShort();
            }
        }

        protected uint ReadUnsignedInt()
        {
            if (_isLittleEndianess)
            {
                return _readBuffer.ReadUnsignedIntLE();
            }
            else
            {
                return _readBuffer.ReadUnsignedInt();
            }
        }

        protected ulong ReadUnsignedLong()
        {
            if (_isLittleEndianess)
            {
                return (ulong)_readBuffer.ReadLongLE();
            }
            else
            {
                return (ulong)_readBuffer.ReadLong();
            }
        }

        protected string ReadString(int length)
        {
            if (length <= 0)
            {
                return string.Empty;
            }

            byte[] bytes = new byte[length];
            _readBuffer.ReadBytes(bytes);

            return Encoding.UTF8.GetString(bytes);
        }

        protected List<uint> ReadUnsignedInts(long count)
        {
            var results = new List<uint>();
            for (int i = 0; i < count; i++)
            {
                results.Add(ReadUnsignedInt());
            }
            return results;
        }

        protected List<T> ReadList<T>(int count, bool isLittleEndianess) where T : KoneSerializer, new()
        {
            var results = new List<T>();
            for (int i = 0; i < count; i++)
            {
                results.Add(ReadObject<T>(isLittleEndianess));
            }
            return results;
        }

        protected T ReadObject<T>(bool isLittleEndianess) where T : KoneSerializer, new()
        {
            var result = new T();
            result.Read(isLittleEndianess);
            return result;
        }

        protected KoneSerializer WriteByte(byte value)
        {
            _writeBuffer.WriteByte(value);
            return this;
        }

        /// <summary>
        /// 写入byte[],不会加入bytes长度
        /// </summary>
        /// <param name="value">要写入的bytes值</param>
        /// <returns>当前buffer</returns>
        protected KoneSerializer WriteBytes(byte[] value)
        {
            if (value == null)
            {
                return this;
            }
            if (_writeBuffer == null)
            {
                _writeBuffer = Unpooled.Buffer();
            }
            _writeBuffer.WriteBytes(value);
            return this;
        }

        protected KoneSerializer WriteShort(short value)
        {
            if (_isLittleEndianess)
            {
                _writeBuffer.WriteShortLE(value);
            }
            else
            {
                _writeBuffer.WriteShort(value);
            }
            return this;
        }

        protected KoneSerializer WriteInt(int value)
        {
            if (_isLittleEndianess)
            {
                _writeBuffer.WriteIntLE(value);
            }
            else
            {
                _writeBuffer.WriteInt(value);
            }
            return this;
        }

        protected KoneSerializer WriteLong(long value)
        {
            if (_isLittleEndianess)
            {
                _writeBuffer.WriteLongLE(value);
            }
            else
            {
                _writeBuffer.WriteLong(value);
            }
            return this;
        }

        protected KoneSerializer WriteUnsignedShort(ushort value)
        {
            if (_isLittleEndianess)
            {
                _writeBuffer.WriteUnsignedShortLE(value);
            }
            else
            {
                _writeBuffer.WriteUnsignedShort(value);
            }
            return this;
        }

        protected KoneSerializer WriteUnsignedInt(uint value)
        {
            if (_isLittleEndianess)
            {
                _writeBuffer.WriteIntLE((int)value);
            }
            else
            {
                _writeBuffer.WriteInt((int)value);
            }
            return this;
        }

        protected KoneSerializer WriteUnsignedInts(List<uint> values)
        {
            foreach (var item in values)
            {
                WriteUnsignedInt(item);
            }
            return this;
        }

        protected KoneSerializer WriteUnsignedLong(ulong value)
        {
            if (_isLittleEndianess)
            {
                _writeBuffer.WriteLongLE((long)value);
            }
            else
            {
                _writeBuffer.WriteLong((long)value);
            }
            return this;
        }

        protected KoneSerializer WriteFloat(float value)
        {
            if (_isLittleEndianess)
            {
                _writeBuffer.WriteFloatLE(value);
            }
            else
            {
                _writeBuffer.WriteFloat(value);
            }
            return this;
        }

        protected KoneSerializer WriteDouble(double value)
        {
            if (_isLittleEndianess)
            {
                _writeBuffer.WriteDoubleLE(value);
            }
            else
            {
                _writeBuffer.WriteDouble(value);
            }
            return this;
        }

        protected KoneSerializer WriteList<T>(List<T> values) where T : KoneSerializer
        {
            if (values == null || !values.Any())
            {
                return this;
            }

            foreach (var item in values)
            { 
                var bytes = item.GetBytes(_isLittleEndianess);
                WriteBytes(bytes);
            }
            return this;
        }
        protected KoneSerializer WriteObject<T>(T value) where T : KoneSerializer
        {
            value.WriteToLocalBuffer(_isLittleEndianess);
            var bytes = value.GetBytes(_isLittleEndianess);
            WriteBytes(bytes);

            return this;
        }
    }
}