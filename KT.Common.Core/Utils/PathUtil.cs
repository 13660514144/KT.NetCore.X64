using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KT.Common.Core.Utils
{
    public class PathUtil
    {
        //要随机的字母
        private static string _letters = "abcdefghijklmnopqrstuvwxyz";

        public static string GetFileRandomFullName(PathEnum basePath, string fileName)
        {
            //获取随机字母
            var randomValue = GetRandomValue();

            //创建文件夹
            var path = Path.Combine(basePath.Value, randomValue.Item1, randomValue.Item2);
            Directory.CreateDirectory(path);

            //返回文件全名
            var fileFullName = Path.Combine(path, fileName);
            return fileFullName;
        }

        private static (string, string) GetRandomValue()
        {
            //随机类
            Random random = new Random();
            //通过索引下标随机 
            var letter1 = _letters[random.Next(25)].ToString();
            var letter2 = _letters[random.Next(25)].ToString();

            return (letter1, letter2);
        }
    }
}
