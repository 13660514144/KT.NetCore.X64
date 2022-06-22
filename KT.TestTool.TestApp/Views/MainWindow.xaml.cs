using KT.Common.Core.Helpers;
using KT.Common.WpfApp.Attributes;
using KT.Common.WpfApp.Dependency;
using KT.TestTool.TestApp.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace KT.TestTool.TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Identity
        private IDictionary<string, Type> _partialViewDic;
        #endregion

        #region Property
        private MainWindowViewModel _mainWindowViewModel;
        #endregion

        #region Constructor

        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            this.Title = "内部测试工具-" + VersionSetting.Text;

            //获取所有菜单
            _partialViewDic = new Dictionary<string, Type>();
            foreach (var item in DependencyService.Services)
            {
                var attributes = item.ServiceType.GetCustomAttributes(false);
                foreach (var attribute in attributes)
                {
                    if (attribute is NavigationItemAttribute)
                    {
                        _partialViewDic.Add(item.ServiceType.Name, item.ServiceType);
                    }
                }
            }

            _mainWindowViewModel = mainWindowViewModel;

            this.DataContext = _mainWindowViewModel;
        }
        #endregion

        private void WindowX_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Lb_Navigate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
            {
                return;
            }

            var listBox = sender as ListBox;
            var itemViewModel = listBox?.SelectedItem as TreeViewItemViewModel;

            if (string.IsNullOrEmpty(itemViewModel?.Tag))
            {
                return;
            }

            if (_partialViewDic.ContainsKey(itemViewModel.Tag))
            {
                var control = DependencyService.ServiceProvider.GetRequiredService(_partialViewDic[itemViewModel.Tag]);
                ContentControl.Content = control;
                //ContentControl.Content = Activator.CreateInstance(_partialViewDic[tag]);
            }
            else
            {
                ContentControl.Content = null;
            }
        }
    }
}
