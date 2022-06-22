using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Microsoft.Extensions.DependencyInjection;
using Panuon.UI.Silver;
using Prism.Ioc;
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

namespace KT.Visitor.Interface.Views.Integrate
{
    /// <summary>
    /// IntegrateRegisterControl.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateRegisterControl : UserControl
    {
        private IntegrateRegisterControlViewModel _viewModel;
        private IContainerProvider _containerProvider;

        public IntegrateRegisterControl(IntegrateRegisterControlViewModel viewModel,
            IContainerProvider containerProvider)
        {
            InitializeComponent();

            _viewModel = viewModel;
            _containerProvider = containerProvider;

            //初始化要加载数据，先登录再加载
            this.DataContext = _viewModel;
        }
    }
}
