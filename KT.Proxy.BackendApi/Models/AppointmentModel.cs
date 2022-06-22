using KangTa.Visitor.Proxy.ServiceApi.Modes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Models
{
    /// <summary>
    /// 预约记录
    /// </summary>
    public class AppointmentModel
    {
        /// <summary>
        /// 访客姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 访问事由
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string FaceImg { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// IC卡号
        /// </summary>
        public string IcCard { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string IdType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string IdNumber { get; set; }

        /// <summary>
        /// 访问日期
        /// </summary>
        public int AccessDate { get; set; }

        /// <summary>
        /// 是否一进一出
        /// </summary>
        public bool Once { get; set; }
        /// <summary>
        /// 授权类型
        /// </summary>
        public string AuthType { get; set; }
        /// <summary>
        /// 被访公司
        /// </summary>
        public long BeVisitCompanyId { get; set; }

        /// <summary>
        /// 被访公司名称
        /// </summary>
        public string BeVisitCompanyName { get; set; }

        /// <summary>
        /// 被访楼层
        /// </summary>
        public long BeVisitFloorId { get; set; }

        /// <summary>
        /// 单元号
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 陪同访客
        /// </summary>
        public List<VisitorInfoModel> Retinues { get; set; }

        /// <summary>
        /// 访问来源
        /// </summary>
        public string VisitorFrom { get; set; }
    } 
}
