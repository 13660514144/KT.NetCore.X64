using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace KT.Common.Core.Utils
{
    public static class NumberUtil
    {
        /// <summary>
        /// 根据随机数范围获取一定数量的随机数
        /// </summary>
        /// <param name="minNum">随机数最小值</param>
        /// <param name="maxNum">随机数最大值</param>
        /// <param name="ResultCount">随机结果数量</param> 
        /// <param name="isSame">结果是否重复</param>
        /// <param name="minNum">是否包含最小值</param>
        /// <param name="minNum">是否包含最大值</param>
        /// <returns></returns>
        public static List<int> GetRandom(int minNum, int maxNum, int ResultCount, bool isSame = false, bool isIncludeMinNum = true, bool isIncludeMaxNum = true)
        {
            var rm = new Random();
            List<int> randomList = new List<int>();
            int nValue = 0;

            #region 是否包含最大最小值，默认包含最小值，不包含最大值
            if (!isIncludeMinNum)
            {
                minNum = minNum + 1;
            }
            if (isIncludeMaxNum)
            {
                maxNum = maxNum + 1;
            }
            #endregion

            if (isSame)
            {
                for (int i = 0; randomList.Count < ResultCount; i++)
                {
                    nValue = rm.Next(minNum, maxNum);
                    randomList.Add(nValue);
                }
            }
            else
            {
                for (int i = 0; randomList.Count < ResultCount; i++)
                {
                    nValue = rm.Next(minNum, maxNum);
                    //重复判断
                    if (!randomList.Contains(nValue))
                    {
                        randomList.Add(nValue);
                    }
                }
            }

            return randomList;
        }

        public static int GetPages(this int count, int size)
        {
            if (count % size == 0)
            {
                return count / size;
            }
            else
            {
                return (count / size) + 1;
            }
        }

        public static long ToLong(this object source, long defaultValue = 0)
        {
            if (source == null)
            {
                return defaultValue;
            }
            var isSuccess = Int64.TryParse(source.ToString(), out long result);
            if (isSuccess)
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
