using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Proxy.BackendApi.Models
{
    public class AuthInfoModel
    {
        public List<string> AuthTypes { get; set; }

        public List<AuthVisitorModel> Visitors { get; set; }

        public AuthInfoModel()
        {
            AuthTypes = new List<string>();
            Visitors = new List<AuthVisitorModel>();
        }
    }

    public class AuthVisitorModel
    {
        /// <summary>
        /// Id主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Ic卡号
        /// </summary>
        public string Ic { get; set; }

        /// <summary>
        /// 人脸照片
        /// </summary>
        public string FaceImg { get; set; }

        ///// <summary>
        ///// 访客姓名
        ///// </summary>
        //public string Name { get; set; }

        ///// <summary>
        ///// 性别
        ///// </summary>
        //public string Gender { get; set; }

        ///// <summary>
        ///// 证件类型
        ///// </summary>
        //public string IdType { get; set; }

        ///// <summary>
        ///// 证件号码
        ///// </summary>
        //public string IdNumber { get; set; }
    }
    public class ElevatorAuthModel
    {
        public int Id { get; set; }

        public string FloorName { get; set; }

        public int ElevatorFloorId { get; set; }
    }
}
