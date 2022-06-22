using KT.Visitor.Interface.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Auth
{
    /// <summary>
    /// pAuthMsg.xaml 的交互逻辑
    /// </summary>
    public partial class AuthMsgPage : Page
    {
        private MainFrameHelper _mainFrameHelper;

        public AuthMsgPage(MainFrameHelper mainFrameHelper)
        {
            InitializeComponent();

            _mainFrameHelper = mainFrameHelper;
        }
        public string ErrMsg { get; set; }
        //操作类型 0是身份验证 1是邀约验证 默认是身份验证
        public int OperateType { get; set; }

        private void Btn_goback_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.GoBack();
            _mainFrameHelper.PreHistory();
        }

        private void Btn_register_Click(object sender, RoutedEventArgs e)
        {
            _mainFrameHelper.LinkVisitorRegister();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (OperateType == 0)
            {
                txb_title.Text = "身份验证";
                if (this.ErrMsg != null)
                {
                    txb_msg.Text = ErrMsg;
                }
                else
                {
                    txb_msg.Text = "身份证姓名与预约访客姓名不一致";
                }

                txb_warning.Visibility = Visibility.Hidden;
            }
            else if (OperateType == 1)
            {
                txb_title.Text = "邀约验证";
                if (this.ErrMsg != null)
                {
                    txb_msg.Text = ErrMsg;
                }
                else
                {
                    txb_msg.Text = "未匹配到邀约记录，请核实";
                }

                txb_warning.Visibility = Visibility.Visible;
            }
        }
    }
}
