using System;

namespace KT.Common.Core.Helpers
{
    /// <summary>
    /// 版本设置
    /// </summary>
    public class VersionSetting
    {
        public static string BranchAbbreviationName => "MR";
        public static string BranchName => "MR";

        public static string Value => "1.7.106";

        public static string Text
        {
            get
            {
                return DebugHelper.DebugRun(() =>
                {
                    return $"{BranchAbbreviationName}-{Value}-Debug";
                }, () =>
                {
                    return $"{BranchAbbreviationName}-{Value}";
                });

            }
        }

        public static long Code
        {
            get
            {
                var val = 0;
                var splits = Value.Split(".");
                val += Convert.ToInt32(splits[0]) * 1000000;
                val += Convert.ToInt32(splits[1]) * 1000;
                val += Convert.ToInt32(splits[2]) * 1;
                return val;
            }
        }
    }
}
