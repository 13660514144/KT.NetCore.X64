using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.Core.Exceptions;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.IdReader;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Tools.Printer.DocumentRenderer;
using KT.Visitor.Interface.Tools.Printer.Models;
using KT.Visitor.Interface.Tools.Printer.PrintOperator;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Threading;

namespace KT.Visitor.Interface.Views.Visitor
{
    /// <summary>
    /// RegisterPage.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterPage : Page
    {
        private RegisterPageViewModel _viewModel;
        public RegisterPage(RegisterPageViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            this.DataContext = _viewModel;
        }
    }
}
