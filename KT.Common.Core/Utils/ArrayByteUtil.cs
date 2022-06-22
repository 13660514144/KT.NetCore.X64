using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Core.Utils
{
    public class ArrayByteUtil
    {
        /// <summary>
        /// 合并数组
        /// </summary>
        /// <param name="first">第一个数组</param>
        /// <param name="second">第二个数组</param>
        /// <returns>合并后的数组(第一个数组+第二个数组，长度为两个数组的长度)</returns>
        private static byte[] BaseMergeArray(byte[] first, byte[] second)
        {
            byte[] result = new byte[first.Length + second.Length];
            first.CopyTo(result, 0);
            second.CopyTo(result, first.Length);
            return result;
        }

        /// <summary>
        /// 合并数组
        /// </summary>
        /// <param name="first">第一个数组</param>
        /// <param name="second">第二个数组</param>
        /// <returns>合并后的数组(第一个数组+第二个数组，长度为两个数组的长度)</returns>
        public static byte[] MergeArray(byte[] first, byte[] second, byte[] extend1 = null, byte[] extend2 = null, byte[] extend3 = null, byte[] extend4 = null, byte[] extend5 = null, byte[] extend6 = null, byte[] extend7 = null, byte[] extend8 = null, byte[] extend9 = null)
        {
            var result = BaseMergeArray(first, second);
            if (extend1 == null || extend1.Length == 0)
            {
                return result;
            }
            result = BaseMergeArray(result, extend1);
            if (extend2 == null || extend2.Length == 0)
            {
                return result;
            }
            result = BaseMergeArray(result, extend2);
            if (extend3 == null || extend3.Length == 0)
            {
                return result;
            }
            result = BaseMergeArray(result, extend3);
            if (extend4 == null || extend4.Length == 0)
            {
                return result;
            }
            result = BaseMergeArray(result, extend4);
            if (extend5 == null || extend5.Length == 0)
            {
                return result;
            }
            result = BaseMergeArray(result, extend5);
            if (extend6 == null || extend6.Length == 0)
            {
                return result;
            }
            result = BaseMergeArray(result, extend6);
            if (extend7 == null || extend7.Length == 0)
            {
                return result;
            }
            result = BaseMergeArray(result, extend7);
            if (extend8 == null || extend8.Length == 0)
            {
                return result;
            }
            result = BaseMergeArray(result, extend8);
            if (extend9 == null || extend9.Length == 0)
            {
                return result;
            }
            result = BaseMergeArray(result, extend9);

            return result;
        }

        /// <summary>
        /// 数组追加
        /// </summary>
        /// <param name="source">原数组</param>
        /// <param name="value">字符串</param>
        /// <returns>合并后的数组(数组+字符串)</returns>
        public static byte[] MergeArray(byte[] source, byte value)
        {
            byte[] result = new byte[source.Length + 1];
            source.CopyTo(result, 0);
            result[source.Length] = value;
            return result;
        }

        /// <summary>
        /// 从数组中截取一部分成新的数组
        /// </summary>
        /// <param name="Source">原数组</param>
        /// <param name="startIndex">原数组的起始位置</param>
        /// <param name="endIndex">原数组的截止位置</param>
        /// <returns></returns>
        public static byte[] SplitArray(byte[] source, int startIndex, int endIndex)
        {
            try
            {
                byte[] result = new byte[endIndex - startIndex + 1];
                for (int i = 0; i <= endIndex - startIndex; i++) result[i] = source[i + startIndex];
                return result;
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 不足长度的前面补空格,超出长度的前面部分去掉
        /// </summary>
        /// <param name="first">要处理的数组</param>
        /// <param name="byteLen">数组长度</param>
        /// <returns></returns>
        public static byte[] MergeArray(byte[] first, int byteLen)
        {
            byte[] result;
            if (first.Length > byteLen)
            {
                result = new byte[byteLen];
                for (int i = 0; i < byteLen; i++) result[i] = first[i + first.Length - byteLen];
                return result;
            }
            else
            {
                result = new byte[byteLen];
                for (int i = 0; i < byteLen; i++)
                {
                    result[i] = 0;
                }

                first.CopyTo(result, byteLen - first.Length);
                return result;
            }
        }
    }
}
