using DotNetty.Buffers;
using KT.Common.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Common.Netty.Common
{
    /// <summary>
    /// 自定义序列化
    /// </summary>
    public abstract class QuantaSerializer
    {
        [JsonIgnore]
        public Encoding ENCODING = QuantaNettyHelper.Encoding;

        [JsonIgnore]
        protected IByteBuffer _writeBuffer;

        [JsonIgnore]
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
        public QuantaSerializer ReadFromBytes(byte[] bytes)
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
        protected byte[] ReadBytes()
        {
            int length = _readBuffer.ReadShort();
            if (length <= 0)
            {
                return new byte[0];
            }

            byte[] bytes = new byte[length];
            _readBuffer.ReadBytes(bytes);

            return bytes;
        }

        /// <summary>
        /// 读取bytes,从buffer中读取Short长度
        /// </summary>
        /// <returns>读取到的bytes</returns>
        protected byte[] ReadBytes(int length)
        {
            if (length <= 0)
            {
                return new byte[0];
            }

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

        protected bool ReadBoolean()
        {
            return _readBuffer.ReadBoolean();
        }

        protected short ReadShort()
        {
            return _readBuffer.ReadShort();
        }

        protected int ReadInt()
        {
            return _readBuffer.ReadInt();
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
            int length = _readBuffer.ReadShort();
            if (length <= 0)
            {
                return string.Empty;
            }

            byte[] bytes = new byte[length];
            _readBuffer.ReadBytes(bytes);

            return ENCODING.GetString(bytes);
        }

        protected string ReadString(int length)
        {
            if (length <= 0)
            {
                return string.Empty;
            }

            byte[] bytes = new byte[length];
            _readBuffer.ReadBytes(bytes);

            return ENCODING.GetString(bytes);
        }

        protected List<T> ReadList<T>() where T : class, new()
        {
            var list = new List<T>();
            int length = _readBuffer.ReadShort();
            for (int i = 0; i < length; i++)
            {
                list.Add(ReadObject<T>());
            }
            return list;
        }

        protected Dictionary<K, V> ReadMap<K, V>() where K : class, new() where V : class, new()
        {
            Dictionary<K, V> map = new Dictionary<K, V>();
            int length = _readBuffer.ReadShort();
            for (int i = 0; i < length; i++)
            {
                K key = ReadObject<K>();
                V value = ReadObject<V>();
                map.Add(key, value);
            }
            return map;
        }

        protected T ReadObject<T>() where T : class, new()
        {
            var clz = new T();

            object value;
            if (clz is int)
            {
                value = ReadInt();
            }
            else if (clz is byte)
            {
                value = ReadByte();
            }
            else if (clz is bool)
            {
                value = ReadBoolean();
            }
            else if (clz is short)
            {
                value = ReadShort();
            }
            else if (clz is long)
            {
                value = ReadLong();
            }
            else if (clz is float)
            {
                value = ReadFloat();
            }
            else if (clz is double)
            {
                value = ReadDouble();
            }
            else if (clz is string)
            {
                value = ReadString();
            }
            else if (clz is QuantaSerializer)
            {
                try
                {
                    bool hasObject = _readBuffer.ReadBoolean();
                    if (hasObject)
                    {
                        QuantaSerializer temp = (QuantaSerializer)(object)clz;
                        temp.ReadFromBuffer(_readBuffer);
                        value = temp;
                    }
                    else
                    {
                        value = null;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                throw new Exception($"不支持类型:{ clz.GetType().FullName}");
            }
            return (T)value;
        }

        protected QuantaSerializer WriteByte(byte value)
        {
            _writeBuffer.WriteByte(value);
            return this;
        }

        /// <summary>
        /// 写入byte[],会加入byte长度
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected QuantaSerializer WriteBytes(byte[] value)
        {
            if (value == null)
            {
                WriteShort(0);
                return this;
            }

            _writeBuffer.WriteShort(value.Length);
            _writeBuffer.WriteBytes(value);
            return this;
        }

        /// <summary>
        /// 写入byte[],会加入byte长度
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected QuantaSerializer WriteBytesNotLength(byte[] value)
        {
            if (value == null)
            {
                return this;
            }

            _writeBuffer.WriteBytes(value);
            return this;
        }

        protected QuantaSerializer WriteBoolean(bool value)
        {
            _writeBuffer.WriteBoolean(value);
            return this;
        }
        protected QuantaSerializer WriteShort(short value)
        {
            _writeBuffer.WriteShort(value);
            return this;
        }

        protected QuantaSerializer WriteInt(int value)
        {
            _writeBuffer.WriteInt(value);
            return this;
        }

        protected QuantaSerializer WriteLong(long value)
        {
            _writeBuffer.WriteLong(value);
            return this;
        }

        protected QuantaSerializer WriteFloat(float value)
        {
            _writeBuffer.WriteFloat(value);
            return this;
        }

        protected QuantaSerializer WriteDouble(double value)
        {
            _writeBuffer.WriteDouble(value);
            return this;
        }

        protected QuantaSerializer WriteList<T>(List<T> value)
        {
            if (value == null)
            {
                _writeBuffer.WriteShort(0);
                return this;
            }
            _writeBuffer.WriteShort(value.Count);
            foreach (T item in value)
            {
                WriteObject(item);
            }
            return this;
        }

        protected QuantaSerializer WriteMap<K, V>(Dictionary<K, V> value)
        {
            if (value == null || value.Count == 0)
            {
                _writeBuffer.WriteShort(0);
                return this;
            }
            _writeBuffer.WriteShort((short)value.Count);
            foreach (var entry in value)
            {
                WriteObject(entry.Key);
                WriteObject(entry.Value);
            }
            return this;
        }

        protected QuantaSerializer WriteString(string value)
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

        protected QuantaSerializer WriteStringFixed(string value, int length)
        {
            byte[] data = ENCODING.GetBytes(value);
            if (data.Length != length)
            {
                throw new ArgumentException($"字符串长度不正确：value:{value} bytes:{data.ToCommaPrintString()} length:{data.Length} netLenght:{length} ");
            }
            _writeBuffer.WriteBytes(data);
            return this;
        }

        protected QuantaSerializer WriteObject(object obj)
        {
            if (obj == null)
            {
                WriteByte(0);
                return this;
            }
            if (obj is bool @bool)
            {
                WriteBoolean(@bool);
                return this;
            }
            if (obj is int @int)
            {
                WriteInt(@int);
                return this;
            }
            if (obj is long @long)
            {
                WriteLong(@long);
                return this;
            }
            if (obj is short @short)
            {
                WriteShort(@short);
                return this;
            }
            if (obj is byte @byte)
            {
                WriteByte(@byte);
                return this;
            }
            if (obj is string @string)
            {
                WriteString(@string);
                return this;
            }
            if (obj is QuantaSerializer serializer)
            {
                WriteBoolean(true);
                serializer.WriteToTargetBuffer(_writeBuffer);
                return this;
            }

            throw new Exception($"不可序列化的类型:{ obj.GetType().FullName}");
        }
    }
}