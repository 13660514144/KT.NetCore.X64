using KT.Visitor.Data.Entity;
using KT.Visitor.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Data.Models
{
    public class SystemConfigModel
    {
        /// <summary>
        /// 证件阅读器
        /// </summary>
        public string Reader { get; set; }

        /// <summary>
        /// 打印机
        /// </summary>
        public string Printer { get; set; }

        /// <summary>
        /// 发卡机
        /// </summary>
        public string CardDevice { get; set; }

        /// <summary>
        /// 发卡机
        /// </summary>
        public string CardIssueMethod { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string ServerAddress { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// 系统LogoUrl地址
        /// </summary>
        public string SystemLogoUrl { get; set; }

        /// <summary>
        /// 上传文件最大值（b)
        /// </summary>
        public double UploadFileSize { get; set; }
 
        public SystemConfigModel()
        {
            UploadFileSize = 900 * 1024; 
        }
    }
}
