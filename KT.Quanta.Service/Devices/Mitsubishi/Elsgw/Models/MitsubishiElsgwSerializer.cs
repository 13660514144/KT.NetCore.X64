using DotNetty.Buffers;
using KT.Common.Core.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models
{
    /// <summary>
    /// 自定义序列化
    /// </summary>
    public abstract class MitsubishiElsgwSerializer
    {
        private ILogger<MitsubishiElsgwSerializer> _logger;
        public MitsubishiElsgwSerializer(ILogger<MitsubishiElsgwSerializer> logger)
        {
            _logger = logger;
        }

        protected MitsubishiElsgwSerializer()
        {
            var loggerFactory = LoggerFactory.Create(buliidder =>
            {
                buliidder.AddLog4Net();
            });

            _logger = loggerFactory.CreateLogger<MitsubishiElsgwSerializer>();
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
        public MitsubishiElsgwSerializer ReadFromBytes(byte[] bytes)
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

        protected List<T> ReadList<T>(int count, int length) where T : MitsubishiElsgwSerializer, new()
        {
            var list = new List<T>();
            for (int i = 0; i < count; i++)
            {
                list.Add(ReadObject<T>(length));
            }
            return list;
        }

        protected T ReadObject<T>(int length) where T : MitsubishiElsgwSerializer, new()
        {
            var clz = new T();
            try
            {
                var bytes = ReadBytes(length);
                var temp = (MitsubishiElsgwSerializer)(object)clz;
                temp.ReadFromBytes(bytes);
                return (T)temp;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        protected MitsubishiElsgwSerializer WriteByte(byte value)
        {
            _writeBuffer.WriteByte(value);
            return this;
        }

        /// <summary>
        /// 写入byte[],不会加入byte长度
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected MitsubishiElsgwSerializer WriteBytes(byte[] value)
        {
            _writeBuffer.WriteBytes(value);
            return this;
        }

        protected MitsubishiElsgwSerializer WriteBoolean(bool value)
        {
            _writeBuffer.WriteBoolean(value);
            return this;
        }
        protected MitsubishiElsgwSerializer WriteShort(short value)
        {
            _writeBuffer.WriteShort(value);
            return this;
        }
        protected MitsubishiElsgwSerializer WriteUnsignedShort(ushort value)
        {
            _writeBuffer.WriteUnsignedShort(value);
            return this;
        }
        protected MitsubishiElsgwSerializer WriteInt(int value)
        {
            _writeBuffer.WriteInt(value);
            return this;
        }
        protected MitsubishiElsgwSerializer WriteUnsignedInt(uint value)
        {
            _writeBuffer.WriteInt((int)value);
            return this;
        }

        protected MitsubishiElsgwSerializer WriteLong(long value)
        {
            _writeBuffer.WriteLong(value);
            return this;
        }

        protected MitsubishiElsgwSerializer WriteFloat(float value)
        {
            _writeBuffer.WriteFloat(value);
            return this;
        }

        protected MitsubishiElsgwSerializer WriteDouble(double value)
        {
            _writeBuffer.WriteDouble(value);
            return this;
        }


        protected MitsubishiElsgwSerializer WriteList<T>(List<T> value) where T : MitsubishiElsgwSerializer
        {
            foreach (T item in value)
            {
                WriteObject(item);
            }
            return this;
        }

        protected MitsubishiElsgwSerializer WriteObject<T>(T obj) where T : MitsubishiElsgwSerializer
        {
            obj.WriteToTargetBuffer(_writeBuffer);
            return this;
        }

        protected MitsubishiElsgwSerializer WriteString(string value)
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
            if (realFloors == null)
            {
                return new List<byte>();
            }

            string sourceFloorsBits = string.Empty;
            for (int i = 1; i <= 255; i++)
            {
                if (realFloors.Contains((byte)i))
                {
                    sourceFloorsBits += "1";
                }
                else
                {
                    sourceFloorsBits += "0";
                }
            }
            sourceFloorsBits.TrimEnd('0');

            //不足8位䃼0
            var bitsPre = sourceFloorsBits.Length % 8;
            if (bitsPre != 0)
            {
                var bitsLength = sourceFloorsBits.Length + (8 - bitsPre);
                sourceFloorsBits.PadRight(bitsLength, '0');
            }

            _logger.LogInformation($"sourceFloorsBits:{sourceFloorsBits.ToBitsString()} ");

            var sourceFloorsBytes = new List<byte>();
            var byteLength = sourceFloorsBits.Length / 8;
            for (int i = 0; i < byteLength; i++)
            {
                //获取8bit为1byte
                var byteBit = sourceFloorsBits.Substring(i * 8, 8);
                //楼层倒转
                byteBit = byteBit.Reverse();
                //转换为byte
                var realFloorByte = Convert.ToByte(byteBit, 2);

                sourceFloorsBytes.Add(realFloorByte);
            }

            _logger.LogInformation($"realFloorsBytes:{sourceFloorsBytes.ToCommaPrintString()} ");

            return sourceFloorsBytes;
        }


        /// <summary>
        /// 获取真实楼层数据
        /// </summary>
        /// <param name="sourceFloors"></param>
        /// <returns></returns>
        public List<byte> ToRealFloors(List<byte> sourceFloors)
        {
            var realFloorsBytes = new List<byte>();
            for (int i = 0; i < sourceFloors.Count(); i++)
            {
                var sourceFloor = sourceFloors[i];
                var sourceFloorBits = Convert.ToString(sourceFloor, 2).PadLeft(8, '0');
                var sourceFloorArray = sourceFloorBits.ToArray().ToList().Select(x => x.ToString()).ToList();

                _logger.LogInformation($"index:{i} sourceFloorArray:{sourceFloorArray.ToCommaPrintString()} ");

                //1byte包含8楼层
                for (int j = 1; j <= 8; j++)
                {
                    if (sourceFloorArray[8 - j] == "1")
                    {
                        var realFloorByte = (byte)(j + (i * 8));
                        realFloorsBytes.Add(realFloorByte);
                    }
                }
            }

            _logger.LogInformation($"realFloorsBytes:{realFloorsBytes.ToCommaPrintString()} ");

            return realFloorsBytes;
        }
    }
}