﻿using KT.Front.WriteCard.Entity;
using System.Windows;

namespace KT.Front.ToolApp.Views
{
    /// <summary>
    /// ConfirnToInitWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfirmToInitWindow : Window
    {
        private WriteRule currentRule;
        public ConfirmToInitWindow(WriteRule _currentRule)
        {
            currentRule = _currentRule;
            InitializeComponent();
        }

        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            bool c = await currentRule.InitCardAsync();
            if (c == true)
            {
                MessageBox.Show("清除成功！");
                this.Close();
            }
            else
            {
                MessageBox.Show("清除失败，请检查设备连接");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
