using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Models
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class CompanyModel
    {
        private string unit;
        private string name;

        /// <summary>
        /// 公司Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                UnitNameText = $"{Unit} {value}";
            }
        }

        /// <summary>
        /// 公司单元
        /// </summary>
        public string Unit
        {
            get
            {
                return unit;
            }
            set
            {
                unit = value;
                UnitNameText = $"{value} {name}";
            }
        }

        /// <summary>
        /// 公司单元号与名称
        /// </summary>
        public string UnitNameText { get; set; }

        /// <summary>
        /// 大厦Id
        /// </summary>
        public long EdificeId { get; set; }

        /// <summary>
        /// 大厦名称
        /// </summary>
        public string EdificeName { get; set; }

        /// <summary>
        /// 楼层Id
        /// </summary>
        public long FloorId { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 预约审核人
        /// </summary>
        public string AuditorName { get; set; }

        /// <summary>
        /// 预约审核人电话
        /// </summary>
        public string AuditorPhone { get; set; }

        /// <summary>
        /// 是否开启预约审核功能
        /// </summary>
        public bool Opening { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<CompanyModel> Nodes { get; set; }
    }
}



