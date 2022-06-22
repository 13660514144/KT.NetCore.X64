using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Service.Entities
{
    /// <summary>
    /// 文件管理
    /// </summary>
    [Table("FACE_INFO")]
    public class FaceInfoEntity : BaseEntity
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型,Face
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 原图地址
        /// </summary>
        public string SourceUrl { get; set; }

        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string FaceUrl { get; set; }

        /// <summary>
        /// 人脸特征
        /// </summary>
        public byte[] Feature { get; set; }

        /// <summary>
        /// 特征值大小
        /// </summary>
        public int FeatureSize { get; set; }

    }
}
