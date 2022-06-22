using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.SelfApp
{
    public class SelfAppSettings
    {
        /// <summary>
        /// 英文提示
        /// </summary>
        public string EnglishWarnText { get; set; }

        /// <summary>
        /// 英文提示标题
        /// </summary>
        public string EnglishWarnTitle { get; set; }

        /// <summary>
        /// 是否一部分显示图片
        /// </summary>
        public bool PictureCarouseIsShow { get; set; }

        /// <summary>
        /// 显示图片部分高度
        /// </summary>
        public double PictureCarouselPartHeight { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string PictureCarouselPath { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public decimal PictureCarouselShowSecondTime { get; set; } = 5;

        /// <summary>
        /// 楼层选择每页条数
        /// </summary>
        public int FloorSelectPageSize { get; set; } = 32;

        /// <summary>
        /// 公司选择每页条数
        /// </summary>
        public int CompanySelectPageSize { get; set; } = 10;

        /// <summary>
        /// 开启证件阅读器延迟，等摄像头初始化完成后才能开启，否则阅读证件后无法正常拍照
        /// </summary>
        public double IdReaderStartDelaySecondTime { get; set; } = 1;
    }
}
