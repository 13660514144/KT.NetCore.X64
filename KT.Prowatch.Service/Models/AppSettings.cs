using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Prowatch.Service.Models
{
    public class AppSettings
    {
        /// <summary>
        /// 重复下载卡次数
        /// </summary>
        public int RedownloadCardStateTimes { get; set; } = 4;

        /// <summary>
        /// 重复下载卡时间（秒）
        /// </summary>
        public decimal RedownloadCardStateSecondTime { get; set; } = 60M;

        /// <summary>
        /// 重复下载卡每张卡间隔时间（秒）
        /// </summary>
        public decimal RedownloadCardStateIntervalSecondTime { get; set; } = 0.3M;

        /// <summary>
        /// 重复下载卡空闲等待时间（秒）
        /// </summary>
        public decimal RedownloadCardStateFreeWaitSecondTime { get; set; } = 10M;

        /// <summary>
        /// 数据库下载触发卡次数
        /// </summary>
        public int RedownloadCardStateByDbCommandTimes { get; set; } = 3;

        /// <summary>
        /// 数据库下载触发卡间隔时间
        /// </summary>
        public decimal RedownloadCardStateByDbCommandSecondTime { get; set; } = 0.3M;

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DBName { get; set; } = "PWNT";
    }
}
