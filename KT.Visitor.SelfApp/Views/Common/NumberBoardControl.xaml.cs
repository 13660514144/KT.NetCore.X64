using KT.Common.Core.Utils;
using Panuon.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KT.Visitor.SelfApp
{
    /// <summary>
    /// NumberBoardControl.xaml 的交互逻辑
    /// </summary>
    public partial class NumberBoardControl : UserControl
    {
        public static readonly DependencyProperty TxtNumberProperty = DependencyProperty.Register("TxtNumber", typeof(string), typeof(NumberBoardControl), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 提醒输入水印
        /// </summary>
        public string TxtNumber
        {
            get { return (string)GetValue(TxtNumberProperty); }
            set { SetValue(TxtNumberProperty, value); }
        }

        public NumberBoardControl()
        {
            InitializeComponent();

            pUButtons = new Button[12] { btn_0, btn_1, btn_2, btn_3, btn_4, btn_5, btn_6, btn_7, btn_8, btn_9, btn_Del, btn_OK  };
            foreach (var item in pUButtons)
            {
                item.Click += Item_Click;
            }
        }

        public void ShowPoint()
        { 
            foreach (var item in pUButtons)
            {
                item.Height = 48;
            }
        }

        public Action EndAction;

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Content.ToString() == "删除")
            {
                if (!string.IsNullOrEmpty(TxtNumber))
                {
                    TxtNumber = TxtNumber.Substring(0, TxtNumber.Length - 1);

                }
            }
            else if (button.Content.ToString() == "确定")
            {
                EndAction?.Invoke();
            }
            else
            {
                TxtNumber += button.Content.ToString();
            }
        }

        Button[] pUButtons;
    }
}
