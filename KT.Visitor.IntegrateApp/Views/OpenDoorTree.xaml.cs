using CefSharp.Wpf;
using HelperTools;
using KT.Common.WpfApp.Helpers;
using KT.Visitor.Common.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace KT.Visitor.IntegrateApp.Views
{
    /// <summary>
    /// OpenDoorTree.xaml 的交互逻辑
    /// </summary>
    public partial class OpenDoorTree : UserControl
    {
        private ILogger _logger;
        private AppSettings _appSettings;
        public OpenDoorTree()
        {
            InitializeComponent();
            _logger = ContainerHelper.Resolve<ILogger>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            Init();
        }
        private void Init()
        {
            string Head = JsonConvert.SerializeObject(_appSettings.Js);                 
            string url = $"{_appSettings.Uritree}?base64=";
            JObject C = new JObject()
            {
                new JProperty("Token", _appSettings.Js.Token),
                new JProperty("Secret", _appSettings.Js.Secret),
                new JProperty("BaseUrl",_appSettings.BaseUrl)
            };
            string base64 = JsonConvert.SerializeObject(C);
            string str = CompressByte.EncodeBase64("UTF-8",base64);
            url += str;
            _logger.LogInformation($"HEAD==>{url}");
            if (!string.IsNullOrWhiteSpace(url))
            {
                ChromiumWebBrowser viewer = new ChromiumWebBrowser(url);
                OpenDoor.Children.Insert(0, viewer);
            }
        }
    }
}
