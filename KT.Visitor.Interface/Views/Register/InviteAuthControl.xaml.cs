using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Views.Register;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using Prism.Regions;
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
    /// InviteAuth.xaml 的交互逻辑
    /// </summary>
    public partial class InviteAuthControl : UserControl
    {
        public InviteAuthControlViewModel ViewModel { get; set; }

        private IRegionManager _regionManager;
        public InviteAuthControl()
        {
            InitializeComponent();

            _regionManager = ContainerHelper.Resolve<IRegionManager>();
            RegionManager.SetRegionManager(this, _regionManager);

            ViewModel = ContainerHelper.Resolve<InviteAuthControlViewModel>();

            this.DataContext = ViewModel;
            ViewModel.ViewLoadedInit(_regionManager);

        }
    }
}
