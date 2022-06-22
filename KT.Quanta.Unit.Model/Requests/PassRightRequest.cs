using KT.Common.Netty.Common;
using KT.Quanta.Common.Models;
using System.Collections.Generic;

namespace KT.Quanta.Unit.Model.Requests
{
    public class PassRightRequest : QuantaSerializer
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string AccessType { get; set; }

        /// <summary>
        /// 人脸特性,不同服务器上生成特性不一样，不能远程传值
        /// </summary>
        public byte[] Feature { get; set; }

        /// <summary>
        /// 人脸特性
        /// </summary>
        public int FeatureSize { get; set; }

        /// <summary>
        /// 起始时间，UTC毫秒
        /// </summary>
        public long TimeStart { get; set; }

        /// <summary>
        /// 过期时间，UTC毫秒
        /// </summary>
        public long TimeEnd { get; set; }

        /// <summary>
        /// 权限组Id
        /// </summary> 
        public List<PassRightDetailRequest> PassRightDetails { get; set; }

        protected override void Read()
        {
            Sign = ReadString();
            AccessType = ReadString();
            Feature = ReadBytes();
            FeatureSize = ReadInt();
            TimeStart = ReadLong();
            TimeEnd = ReadLong();
            PassRightDetails = ReadList<PassRightDetailRequest>();
        }

        protected override void Write()
        {
            WriteString(Sign);
            WriteString(AccessType);
            WriteBytes(Feature);
            WriteInt(FeatureSize);
            WriteLong(TimeStart);
            WriteLong(TimeEnd);
            WriteList(PassRightDetails);
        }
    }
}
