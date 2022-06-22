using KT.Proxy.WebApi.Backend.Apis;
using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.Interface.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KT.Visitor.Interface.Views.Auth
{
    /// <summary>
    /// IdentityAuth.xaml 的交互逻辑
    /// </summary>
    public partial class IdentityAuthPage : Page
    {
        private MainFrameHelper _mainFrameHelper;
        private IContainerProvider _containerProvider;
        private VisitorAuthApi _visitorAuthApi;

        public IdentityAuthPage(MainFrameHelper mainFrameHelper
            , IContainerProvider containerProvider,
             VisitorAuthApi visitorAuthApi)
        {
            InitializeComponent();

            _mainFrameHelper = mainFrameHelper;
            _containerProvider = containerProvider;
            _visitorAuthApi = visitorAuthApi;

            Init();
        }

        public void Init()
        {
            buttons = new Button[12] { btn_0, btn_1, btn_2, btn_3, btn_4, btn_5, btn_6, btn_7, btn_8, btn_9, btn_Del, btn_OK };
            foreach (var item in buttons)
            {
                item.Click += Item_Click;
            }
        }

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Content.ToString() == "删除")
            {
                if (!string.IsNullOrEmpty(txt_code.Text))
                {
                    txt_code.Text = txt_code.Text.Substring(0, txt_code.Text.Length - 1);

                }

            }
            else if (button.Content.ToString() == "确定")
            {
                wpbtns.Visibility = Visibility.Collapsed;
            }
            else
            {
                txt_code.Text += button.Content.ToString();
            }
        }

        Button[] buttons;
        private int authCategory = 0;//验证的方式 0是身份验证 1是邀约验证 默认是身份验证
        private void Btn_SearchOK_Click(object sender, RoutedEventArgs e)
        {
            SearchAsync();
        }

        private async Task SearchAsync()
        {
            List<VisitorInfoModel> records = new List<VisitorInfoModel>();
            //先进行查询
            if (authCategory == 0)
            {
                var results = await _visitorAuthApi.AuthCheckAsync(new { phone = txt_code.Text.Trim() });
                if (results == null || results.FirstOrDefault() == null)
                {
                    var _UI = _containerProvider.Resolve<AuthMsgPage>();
                    _UI.ErrMsg = "未匹配到访问记录，请核实";
                    _UI.OperateType = authCategory;
                    _mainFrameHelper.Link(_UI, false);
                    return;
                }

            }
            else if (authCategory == 1)
            {
                var results = await _visitorAuthApi.AuthCheckAsync(new { askCode = txt_code.Text.Trim() });
                if (results == null || results.FirstOrDefault() == null)
                {
                    var _UI = _containerProvider.Resolve<AuthMsgPage>();
                    _UI.ErrMsg = "未匹配到邀约记录，请核实";
                    _UI.OperateType = authCategory;
                    _mainFrameHelper.Link(_UI, false);
                    return;
                }
            }
            var UI = _containerProvider.Resolve<ReadIdCardPage>();
            UI.records = records;
            UI.authCategory = this.authCategory;

            _mainFrameHelper.Link(UI, false);
        }

        private void BtnActive_Click(object sender, RoutedEventArgs e)
        {
            ChangebkByClick(sender as Button);
        }

        private void Btn_auth_sf_Click(object sender, RoutedEventArgs e)
        {
            ChangebkByClick(sender as Button);
        }

        private void ChangebkByClick(Button button)
        {
            Color color = (Color)ColorConverter.ConvertFromString("{DynamicResource Scb_Theme08}");
            button.Background = new SolidColorBrush(color);
            button.Foreground = new SolidColorBrush(Colors.White);
            if (button.Name == "btn_auth_sf")
            {
                authCategory = 0;
                titleMag.Text = "请输入访客手机号";
                //txt_code.Watermark = "请输入您的手机号"; TOODO

                btnActive.Background = new SolidColorBrush(Colors.White);
                btnActive.Foreground = new SolidColorBrush(Colors.Black);
            }
            else if (button.Name == "btnActive")
            {
                authCategory = 1;
                titleMag.Text = "请输入访客邀请码";
                //txt_code.Watermark = "请输入您的邀请码";TOODO

                btn_auth_sf.Background = new SolidColorBrush(Colors.White);
                btn_auth_sf.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void Txt_code_GotFocus(object sender, RoutedEventArgs e)
        {
            wpbtns.Visibility = Visibility.Visible;
        }
    }
}
