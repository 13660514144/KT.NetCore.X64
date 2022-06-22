using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Interface.Models
{
    public class SubmitParameterModel
    {
        /// <summary>
        /// 人员信息
        /// </summary>
        public RegistVisitorViewModel VisitorInfo { get; set; }

        /// <summary>
        /// 访问原由
        /// </summary>
        public string VisiteReason { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        [JsonIgnore]
        public BitmapImage Image { get; set; }

        /// <summary>
        /// 需要证件陪同访客
        /// </summary>
        public List<VisitorInfoModel> Accompanies { get; set; }

        /// <summary>
        /// 加人数的陪同访客，不用校验
        /// </summary>
        public List<VisitorInfoModel> NumAccompanies { get; set; }

        /// <summary>
        /// 加人脸的陪同访客,不用校验
        /// </summary>
        public List<VisitorInfoModel> CardAccompanies { get; set; }

        /// <summary>
        /// 加人手机号的陪同访客,不用校验
        /// </summary>
        public List<VisitorInfoModel> PhotoAccompanies { get; set; }

        /// <summary>
        /// 授权时限
        /// </summary>
        public AuthorizeTimeLimitViewModel AuthorizeTimeLimit { get; set; }
    }
}
