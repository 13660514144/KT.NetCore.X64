using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Settings
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Winpak刷新SDK错误重试提交次数
        /// 0为不刷新SDK
        /// 1为刷新，一次不成功后不再刷新
        /// 2为刷新，一次不成功后再重新刷新一次
        /// 。。。。。
        /// </summary>
        public int ReloginTimes { get; set; } = 2;

        /// <summary>
        /// Winpak错误重试提交次数
        /// 0为出错直接返回错误结果
        /// 1为出错误再重试一次
        /// 2为出错误再重试二次
        /// 。。。。。
        /// </summary>
        public int RetryTimes { get; set; } = 2;

        /// <summary>
        /// 账户名
        /// </summary>
        public string AccountName { get; set; } = "admin";

        /// <summary>
        /// 账户名
        /// </summary>
        public string SubAccountName { get; set; } = "admin";

        /// <summary>
        /// 账户名
        /// </summary>
        public List<string> SubAccountNames { get; set; } = new List<string> { "admin" };

        /// <summary>
        /// 账户名
        /// </summary>
        public List<string> PushStatus { get; set; } = new List<string>() { "有效卡", "有效卡，未使用门", "有效卡，使用门", "主机授权，已下载卡" };

        /// <summary>                                                      
        /// 数据库名称                                                    
        /// </summary>
        public string DBName { get; set; } = "WIN-PAK PRO";
    }
}
