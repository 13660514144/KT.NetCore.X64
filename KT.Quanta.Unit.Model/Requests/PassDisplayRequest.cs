using KT.Common.Netty.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Unit.Model.Requests
{
    public class PassDisplayRequest : QuantaSerializer
    {
        /// <summary>
        /// 显示类型
        /// <see cref="KT.Quanta.Common.Enums.PassDisplayTypeEnum"/>
        /// </summary>
        public string DisplayType { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// 通行方式
        /// </summary>
        public string AccessType { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        protected override void Read()
        {
            DisplayType = ReadString();
            Time = ReadLong();
            AccessType = ReadString();
            Sign = ReadString();
            ImageUrl = ReadString();
        }

        protected override void Write()
        {
            WriteString(DisplayType);
            WriteLong(Time);
            WriteString(AccessType);
            WriteString(Sign);
            WriteString(ImageUrl);
        }
    }
}
