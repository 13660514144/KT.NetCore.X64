using KT.Proxy.WebApi.Backend.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Tools.Printer.Models
{
    public class VisitQRCodePrintModel
    {
        /// <summary>
        /// 大厦
        /// </summary>
        public string EdificeName { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public string FloorName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 访问时间
        /// </summary>
        public string DateTime { get; set; }
        /// <summary>
        /// 访客姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 二维码地址
        /// </summary>
        public Bitmap QrImage { get; set; }

        public static VisitQRCodePrintModel FromVisitorResult(RegisterResultModel source, Bitmap qrImage)
        {
            VisitQRCodePrintModel model = new VisitQRCodePrintModel();
            model.EdificeName = source.EdificeName;
            model.FloorName = source.FloorName;
            model.CompanyName = source.CompanyName;
            model.DateTime = source.DateTime;
            model.Name = source.Name;
            model.Phone = source.Phone;

            model.QrImage = qrImage;
            return model;
        }
    }
}
