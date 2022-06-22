using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Interface.Models
{
    /// <summary>
    /// 导航链接通知参数
    /// </summary>
    public class NavLinkModel
    {
        /// <summary>
        /// 页面名称
        /// </summary>
        public FontNavEnum View { get; set; }

        /// <summary>
        /// 是否创建新界面
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// 其它参数
        /// </summary>
        public object Data { get; set; }

        public string ImgServer { get; set; } = "";
        /// <summary>
        /// 创建新导航链接通知参数
        /// </summary>
        /// <param name="view">目标页面参数</param>
        /// <param name="isNew">是否创建新页面</param>
        /// <param name="data">其它参数</param>
        /// <returns>导航链接通知参数对象</returns>
        public static NavLinkModel Create(FontNavEnum view, bool isNew = true, object data = null,string Img="")
        {
            var result = new NavLinkModel();
            result.View = view;
            result.IsNew = isNew;
            result.Data = data;
            if (!string.IsNullOrEmpty(Img))
            {
                result.ImgServer = Img;
            }
            return result;
        }
    }
}
