using KT.Quanta.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.WpfApp.ViewModels
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfigViewModel : BindableBase
    {
        private string _title;
        private string _subtitle;
        private string _copyright;
        private string _logoUrl;

        public SystemConfigViewModel(SystemConfigSettings systemConfigSettings)
        {
            Title = systemConfigSettings.Title;
            Subtitle = systemConfigSettings.Subtitle;
            Copyright = systemConfigSettings.Copyright;
            LogoUrl = systemConfigSettings.LogoUrl;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
            }
        }

        /// <summary>
        /// 副标题
        /// </summary>
        public string Subtitle
        {
            get { return _subtitle; }
            set
            {
                SetProperty(ref _subtitle, value);
            }
        }

        /// <summary>
        /// 版权声明
        /// </summary>
        public string Copyright
        {
            get { return _copyright; }
            set
            {
                SetProperty(ref _copyright, value);
            }
        }

        /// <summary>
        /// Logo图标路径
        /// </summary>
        public string LogoUrl
        {
            get { return _logoUrl; }
            set
            {
                SetProperty(ref _logoUrl, value);
            }
        }
    }
}
