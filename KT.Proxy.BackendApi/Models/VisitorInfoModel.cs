using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.Core.Enums;
using KT.Proxy.BackendApi.Enums;
using KT.Proxy.BackendApi.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace KT.Proxy.BackendApi.Models
{
    /// <summary>
    /// 访客信息
    /// </summary>
    public class VisitorInfoModel
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
                if (!string.IsNullOrEmpty(_snapshotImg))
                {
                    ServerSnapshotImg = StaticInfo.ServerAddress + value;
                }
                else
                {
                    ServerSnapshotImg = string.Empty;
                }
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
                if (!string.IsNullOrEmpty(_faceImg))
                {
                    ServerFaceImg = StaticInfo.ServerAddress + value;
                }
                else
                {
                    ServerFaceImg = string.Empty;
                }
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

        private string _icCard { get; set; }
        /// <summary>
        /// IC卡
        /// </summary>
        public string IcCard
        {
            get => _icCard;
            set
            {
                _icCard = value;
                CardText = string.Empty;
                //二维码IC卡显示名称
                if (!string.IsNullOrEmpty(value))
                {
                    CardText += "IC卡 " + value;
                }
                if (!string.IsNullOrEmpty(QrCode))
                {
                    CardText += "  二维码 " + QrCode;
                }
                CardText = CardText?.Trim();
            }
        }

        private string _qrCode;

        /// <summary>
        /// 二维码
        /// </summary>
        public string QrCode
        {
            get => _qrCode;
            set
            {
                _qrCode = value;
                CardText = string.Empty;
                //二维码IC卡显示名称
                if (!string.IsNullOrEmpty(IcCard))
                {
                    CardText += "IC卡 " + IcCard;
                }
                if (!string.IsNullOrEmpty(value))
                {
                    CardText += "  二维码 " + value;
                }
                CardText = CardText?.Trim();
            }
        }

        /// <summary>
        /// Ic卡显示文本
        /// </summary>
        [JsonIgnore]
        public string CardText { get; set; }

        /// <summary>
        /// 来访时间
        /// </summary>
        public string VisitDate { get; set; }

        /// <summary>
        /// 访客来源
        /// </summary>
        public string VisitorFrom { get; set; }

        /// <summary>
        /// 被访员工
        /// </summary>
        public string BeVisitStaffName { get; set; }

        /// <summary>
        /// 被访公司名称
        /// </summary>
        public string BeVisitCompanyName { get; set; }

        /// <summary>
        /// 被访公司地址
        /// </summary>
        public string BeVisitCompanyLocation { get; set; }

        /// <summary>
        /// 是否团队访问
        /// </summary>
        public bool Team { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// 来访状态
        /// </summary>
        public string VisitorStatus { get; set; }

        /// <summary>
        /// 来访状态描述
        /// </summary>
        public string VisitorStatusName { get; set; }

        /// <summary>
        /// 是否黑名单
        /// </summary>
        public bool BlackList { get; set; }

        /// <summary>
        /// 拉黑原因
        /// </summary>
        public string BlackReason { get; set; }

        /// <summary>
        /// 禁访区域
        /// </summary>
        public List<long> Areas { get; set; }

        /// <summary>
        /// 如果team为true则存在数据，随行人员
        /// </summary>
        public List<VisitorInfoModel> Retinues { get; set; }

        /// <summary>
        /// 状态流
        /// </summary>
        public List<StatusFlowModel> Status { get; set; }

        private string _edificeName;
        /// <summary>
        /// 大厦名称
        /// </summary>
        public string EdificeName
        {
            get
            {
                return _edificeName;
            }
            set
            {
                _edificeName = value;
                EdificeFloorName = value + FloorName;
            }
        }

        private string _floorName;
        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName
        {
            get
            {
                return _floorName;
            }
            set
            {
                _floorName = value;
                EdificeFloorName = EdificeName + value;
            }
        }

        /// <summary>
        /// 大厦楼层名称
        /// </summary>
        public string EdificeFloorName { get; set; }

        /// <summary>
        /// 证件照
        /// </summary>
        [JsonIgnore]
        public BitmapImage IdCardImage { get; set; }

        /// <summary>
        /// 拍照
        /// </summary>
        [JsonIgnore]
        public BitmapImage PhotoImage { get; set; }
    }
}
