using KT.Proxy.WebApi.Backend.Models;
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
using System.Windows.Shapes;

namespace KT.Tools.WpfStyle
{
    /// <summary>
    /// DataGridPropertyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataGridPropertyWindow : Window
    {
        public DataGridPropertyWindow()
        {
            InitializeComponent();

            var list = new List<BlacklistModel>();
            list.Add(new BlacklistModel() { Name = "张三", GenderText = "男", Company = "康塔" });
            list.Add(new BlacklistModel() { Name = "张4", GenderText = "男", Company = "康塔" });
            list.Add(new BlacklistModel() { Name = "张5", GenderText = "男", Company = "康塔" });
            list.Add(new BlacklistModel() { Name = "张6", GenderText = "男", Company = "康塔" });
            list.Add(new BlacklistModel() { Name = "张7", GenderText = "男", Company = "康塔" });
            list.Add(new BlacklistModel() { Name = "张8", GenderText = "男", Company = "康塔" });

            dg.ItemsSource = list;
        }
    }
}
