using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Common.Core.Utils
{
    public class ByteBitUtil
    {
        public static List<int> GetOneByteBitValue(byte value)
        {
            var results = new List<int>();
            if (value == 0)
            {
                return results;
            }
            var strValue = Convert.ToString(value, 2);
            var bitValues = strValue.Split();
            for (int i = 1; i <= 8; i++)
            {
                var bitValue = bitValues[i - 1];
                if (bitValue == "1")
                {
                    results.Add(i);
                }
            }
            return results;
        }

        public static byte SetOneByteBitValue(List<int> value)
        {
            var results = new List<int>();
            if (value == null || value.Count == 0)
            {
                return 0;
            }
            var bitValue = string.Empty;
            for (int i = 1; i <= 8; i++)
            {
                if (value.Any(x => x == i))
                {
                    bitValue = $"1{bitValue}";
                }
                else
                {
                    bitValue = $"0{bitValue}";
                }
            }

            return Convert.ToByte(bitValue, 2);
        }

        /// <summary>
        /// 获取真实楼层数据
        /// </summary>
        /// <param name="realFloors"></param>
        /// <returns></returns>
        public static List<byte> ToBitValues(List<byte> realFloors, int length)
        {
            if (realFloors == null)
            {
                return new List<byte>();
            }

            string sourceFloorsBits = string.Empty;
            for (int i = 1; i <= length; i++)
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

            //_logger.LogInformation($"sourceFloorsBits:{sourceFloorsBits.ToBitsString()} ");

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

            //_logger.LogInformation($"realFloorsBytes:{sourceFloorsBytes.ToCommaPrintString()} ");

            return sourceFloorsBytes;
        }


        /// <summary>
        /// 获取真实楼层数据
        /// </summary>
        /// <param name="sourceFloors"></param>
        /// <returns></returns>
        public static List<byte> ToByteValues(List<byte> sourceFloors)
        {
            var realFloorsBytes = new List<byte>();
            for (int i = 0; i < sourceFloors.Count(); i++)
            {
                var sourceFloor = sourceFloors[i];
                var sourceFloorBits = Convert.ToString(sourceFloor, 2).PadLeft(8, '0');
                var sourceFloorArray = sourceFloorBits.ToArray().ToList().Select(x => x.ToString()).ToList();

                //_logger.LogInformation($"index:{i} sourceFloorArray:{sourceFloorArray.ToCommaPrintString()} ");

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

            //_logger.LogInformation($"realFloorsBytes:{realFloorsBytes.ToCommaPrintString()} ");

            return realFloorsBytes;
        }
    }
}
