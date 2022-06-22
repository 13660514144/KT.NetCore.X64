using DotNetty.Buffers;
using KT.Common.Core.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Quanta.Service.Devices.Common
{
    /// <summary>
    /// 自定义序列化
    /// 适应Substring字符与分隔符
    /// </summary>
    public abstract class SchindlerDatabaseSerializer
    {
        public Encoding ENCODING = Encoding.ASCII;

        private ILogger<SchindlerDatabaseSerializer> _logger;
        public SchindlerDatabaseSerializer(ILogger<SchindlerDatabaseSerializer> logger)
        {
            _logger = logger;
        }

        protected SchindlerDatabaseSerializer()
        {
            var loggerFactory = LoggerFactory.Create(buliidder =>
             {
                 buliidder.AddLog4Net();
             });

            _logger = loggerFactory.CreateLogger<SchindlerDatabaseSerializer>();
        }

        /// <summary>
        /// 只在发送数据时使用
        /// </summary>
        protected IByteBuffer _writeBuffer;

        /// <summary>
        /// 只在接收数据时使用
        /// </summary>
        protected IByteBuffer _readBuffer;

        /// <summary>
        /// 数据组合成字符串
        /// </summary>
        protected List<string> _writeStrings;

        /// <summary>
        /// 数据组合成字符串
        /// </summary>
        protected List<string> _readStrings;

        public SchindlerDatabaseSerializer ReadFromBytes(byte[] bytes)
        {
            var value = ENCODING.GetString(bytes);
            _logger.LogInformation($"ReadFromBytes value:{value} ");
            _readStrings = value.Split(Separator).ToList();
            Read();
            _readStrings = null;
            return this;
        }

        public SchindlerDatabaseSerializer ReadFromBuffer(IByteBuffer byteBuffer)
        {
            var value = byteBuffer.ToString(ENCODING);
            _logger.LogInformation($"ReadFromBuffer value:{value} ");
            _readStrings = value.Split(Separator).ToList();
            Read();
            _readStrings = null;
            return this;
        }

        public SchindlerDatabaseSerializer ReadFromValues(List<string> values)
        {
            _readStrings = values;
            Read();
            _readStrings = null;
            return this;
        }

        /// <summary>
        /// 写入本地buffer
        /// </summary>
        /// <returns></returns>
        public IByteBuffer WriteToLocalBuffer()
        {
            _writeBuffer = Unpooled.Buffer();
            Write();
            //组合字符串
            var value = _writeStrings.ToSeparateString(Separator);
            _logger.LogInformation($"WriteToLocalBuffer value:{value} ");
            //获取bytes
            var bytes = ENCODING.GetBytes(value);
            //写入buffer
            _writeBuffer.WriteBytes(bytes);
            return _writeBuffer;
        }

        /// <summary>
        /// 字符截取索引
        /// </summary>
        private int _readCharIndex = 0;

        /// <summary>
        /// 数组读索引
        /// </summary>
        private int _readSplitIndex = 0;

        /// <summary>
        /// 字符截取索引
        /// </summary>
        private int _writeCharIndex = 0;

        /// <summary>
        /// 数组读索引
        /// </summary>
        private int _writeSplitIndex = 0;

        /// <summary>
        /// 分隔符
        /// </summary>
        protected string Separator => "|";

        /// <summary>
        /// 反序列化具体实现
        /// </summary>
        protected abstract void Read();

        /// <summary>
        /// 序列化具体实现
        /// </summary>
        protected abstract void Write();

        /// <summary>
        /// 读取字符串
        /// </summary>
        public string ReadString()
        {
            var result = _readStrings[_readSplitIndex];

            //读到完成跳转到下一个
            _readSplitIndex++;
            _readCharIndex = 0;

            return result;
        }

        /// <summary>
        /// 读取字符串
        /// </summary>
        public long ReadLong()
        {
            var value = ReadString();
            var result = long.Parse(value);

            return result;
        }
        /// <summary>
        /// 读取字符串
        /// </summary>
        public DateTime? ReadStringDateTime()
        {
            var value = ReadString();
            if (!string.IsNullOrEmpty(value))
            {
                var result = Convert.ToDateTime(value);
                return result;
            }
            return null;
        }

        /// <summary>
        /// 读取16进制字符串
        /// </summary>
        public long ReadHexToLong()
        {
            var value = _readStrings[_readSplitIndex];

            //16进制转10进制
            var result = Convert.ToInt64(value, 16);

            //读到完成跳转到下一个
            _readSplitIndex++;
            _readCharIndex = 0;

            return result;
        }

        /// <summary>
        /// Substring读取字符串
        /// </summary>
        /// <param name="length">截取长度</param>
        /// <returns>截取的数据结果</returns>
        public string ReadSubstring(int length)
        {
            var value = _readStrings[_readSplitIndex];

            //获取Substring结果
            var result = value.Substring(_readCharIndex, length);
            _readCharIndex = _readCharIndex + length;

            return result;
        }

        /// <summary>
        /// Substring读取下一对象字符串
        /// </summary>
        /// <param name="length">截取长度</param>
        /// <returns>截取的数据结果</returns>
        public string ReadNextSubstring(int length)
        {
            //转到下一数据
            _readSplitIndex++;
            _readCharIndex = 0;

            var value = _readStrings[_readSplitIndex];

            //获取Substring结果
            var result = value.Substring(_readCharIndex, length);

            return result;
        }

        /// <summary>
        /// Substring读对象字符串，完成后跳转到下一个
        /// </summary>
        /// <param name="length">截取长度</param>
        /// <returns>截取的数据结果</returns>
        public string ReadSubstringNext(int length)
        {
            var value = _readStrings[_readSplitIndex];

            //获取Substring结果
            var result = value.Substring(_readCharIndex, length);

            //转到下一数据
            _readSplitIndex++;
            _readCharIndex = 0;

            return result;
        }

        /// <summary>
        /// Substring读对象字符串，完成后跳转到下一个
        /// </summary> 
        /// <returns>截取的数据结果</returns>
        public string ReadEndSubstringNext()
        {
            var value = _readStrings[_readSplitIndex];

            //获取Substring结果
            var result = value.Substring(_readCharIndex, value.Length - _readCharIndex);

            //转到下一数据
            _readSplitIndex++;
            _readCharIndex = 0;

            return result;
        }

        /// <summary>
        /// Substring读对象字符串，完成后跳转到下一个
        /// </summary> 
        /// <returns>截取的数据结果</returns>
        public int ReadEndSubstringIntNext()
        {
            var value = ReadEndSubstringNext();
            var result = int.Parse(value);
            return result;
        }
        /// <summary>
        /// Substring读对象字符串，完成后跳转到下一个
        /// </summary>
        /// <param name="length">截取长度</param>
        /// <returns>截取的数据结果</returns>
        public long ReadEndSubstringLongNext()
        {
            var value = ReadEndSubstringNext();
            return Convert.ToInt64(value);
        }

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value"></param>
        public void WriteString(string value)
        {
            IsNullWriteCreate();

            _writeStrings.Add(value);
            _writeCharIndex++;
        }

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value"></param>
        public void WriteStringDateTime(DateTime? value, string fromater = "yyyy-MM-dd HH:mm:ss")
        {
            if (value.HasValue)
            {
                var result = value.Value.ToString(fromater);
                WriteString(result);
            }
            else
            {
                WriteString(string.Empty);
            }
        }

        /// <summary>
        /// 写入16进制字符串
        /// </summary>
        /// <param name="value"></param>
        public void WriteHexFromLong(long? value, int length)
        {
            IsNullWriteCreate();

            //十进制转16进制
            if (value.HasValue)
            {
                var result = value.Value.ToString("X");
                result = result.PadLeft(length, '0');
                _writeStrings.Add(result);
            }
            else
            {
                _writeStrings.Add(string.Empty);
            }

            _writeCharIndex++;
        }

        /// <summary>
        /// 拼入字符串
        /// </summary>
        /// <param name="value"></param>
        public void WriteSubstring(string value)
        {
            IsNullWriteCreate();

            if (_writeStrings.Count == _writeSplitIndex)
            {
                //当前可写字符不存在
                _writeStrings.Add(value);
                _writeCharIndex++;
            }
            else
            {
                //拼入当前可写字符不存在
                _writeStrings[_writeSplitIndex] = _writeStrings[_writeSplitIndex] + value;
            }
        }

        public void WriteSubstringPadLeft(string value, int length, char padding = '0')
        {
            //补位
            value = value.PadLeft(length, padding);

            WriteSubstring(value);
        }

        private void IsNullWriteCreate()
        {
            if (_writeStrings == null)
            {
                _writeStrings = new List<string>();
                _writeCharIndex = 0;
            }
        }

        /// <summary>
        /// 读取整型数据
        /// </summary>
        /// <returns></returns>
        public int ReadInt()
        {
            var value = ReadString();
            return int.Parse(value);
        }

        /// <summary>
        /// 读取截取整型数据
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public int ReadIntSubstring(int length)
        {
            var value = ReadSubstring(length);
            return int.Parse(value);
        }

        /// <summary>
        /// 读取截取下一字符整型数据
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public int ReadIntNextSubstring(int length)
        {
            var value = ReadNextSubstring(length);
            return int.Parse(value);
        }

        /// <summary>
        /// 读取截取整形数据，并跳到下一字符
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public int ReadIntSubstringNext(int length)
        {
            var value = ReadSubstringNext(length);
            return int.Parse(value);
        }

        /// <summary>
        /// 读取当前字符剩余数据
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public int ReadEndSubstringNext(int length)
        {
            var value = ReadSubstringNext(length);
            return int.Parse(value);
        }

        /// <summary>
        /// 读取所有剩余数据
        /// </summary>
        /// <returns></returns>
        public List<string> ReadEndStrings()
        {
            var results = new List<string>();
            var firstEndString = ReadEndSubstringNext();
            if (!string.IsNullOrEmpty(firstEndString))
            {
                results.Add(firstEndString);
            }
            for (; _readCharIndex < _readStrings.Count; _readCharIndex++)
            {
                results.Add(_readStrings[_readCharIndex]);
            }
            return results;
        }

        public string GetWriteValue()
        {
            var value = _writeStrings.ToSeparateString(Separator);
            return value;
        }

        public byte[] GetBytes(string value)
        {
            var bytes = ENCODING.GetBytes(value);
            return bytes;
        }

        public void WriteInt(int value)
        {
            WriteString(value.ToString());
        }

        public void WriteIntSubstring(int value)
        {
            WriteSubstring(value.ToString());
        }

        public void WriteIntSubstringPadLeft(int value, int length, char padding = '0')
        {
            WriteSubstringPadLeft(value.ToString(), length, padding);
        }

        public void WriteLongSubstringPadLeft(long value, int length, char padding = '0')
        {
            WriteSubstringPadLeft(value.ToString(), length, padding);
        }
        public void WriteLongPadLeftZeroEmpty(long value, int length, char padding = '0')
        {
            if (value != 0)
            {
                var result = value.ToString().PadLeft(length, padding);
                WriteString(result);
            }
            else
            {
                WriteString(string.Empty);
            }
        }
    }
}
