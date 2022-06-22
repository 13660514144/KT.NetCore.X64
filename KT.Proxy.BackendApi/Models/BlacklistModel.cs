using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.Core.Enums;
using KT.Proxy.BackendApi.Enums;
using KT.Proxy.BackendApi.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KT.Proxy.BackendApi.Models
{
    /// <summary>
    /// 访客信息
    /// </summary>
    public class BlacklistModel
    {
        /// <summary>
        /// Id主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        private string _idType;
        /// <summary>
        /// 证件类型
        /// </summary>
        public string IdType
        {
            get
            {
                return _idType;
            }
            set
            {
                _idType = value;
                IdTypeText = CertificateTypeEnum.GetTextByValue(_idType);
            }
        }
        [JsonIgnore]
        public string IdTypeText { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdNumber { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 拉黑原因
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 拉黑时间
        /// </summary>
        public string CreateTime { get; set; }

        private string _snapshotImg;
        /// <summary>
        /// 抓拍照
        /// </summary>
        public string SnapshotImg
        {
            get => _snapshotImg;
            set
            {
                _snapshotImg = value;
                ServerSnapshotImg = StaticInfo.ServerAddress + value;
            }
        }
        /// <summary>
        /// 抓拍照
        /// </summary>
        public string ServerSnapshotImg { get; set; }

        private string _faceImg;
        /// <summary>
        /// 图片
        /// </summary>
        public string FaceImg
        {
            get => _faceImg;
            set
            {
                _faceImg = value;
                ServerFaceImg = StaticInfo.ServerAddress + value;
            }
        }
        [JsonIgnore]
        public string ServerFaceImg { get; set; }

        private string _gender;
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                var g = GenderEnum.GetByValue(value);
                GenderText = g != null ? g.Text : value;
            }
        }
        /// <summary>
        /// 性别显示文本
        /// </summary>
        [JsonIgnore]
        public string GenderText { get; set; }

        /// <summary>
        /// 访客记录Id，从访客记录详情来黑需要传访客记录Id
        /// </summary>
        public long? VisitorLogId { get; set; }

        /// <summary>
        /// 禁访区域
        /// </summary>
        public string BlockArea { get; set; }

        /// <summary>
        /// 禁访区域
        /// </summary>
        public List<CompanyModel> BlockAreas { get; set; }
    }
}
