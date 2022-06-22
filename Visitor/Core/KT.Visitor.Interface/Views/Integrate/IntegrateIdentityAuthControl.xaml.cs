using KT.Proxy.WebApi.Backend.Apis;
using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Views.Integrate;
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
    public partial class IntegrateIdentityAuthControl : UserControl
    {
        public IntegrateIdentityAuthControlViewModel ViewModel { get; set; }

        public IntegrateIdentityAuthControl(IntegrateIdentityAuthControlViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            this.DataContext = ViewModel;
        }
    }
}
