using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Models
{
    public class FaceRequestModel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        public IFormFile File { get; set; }
    }
}
