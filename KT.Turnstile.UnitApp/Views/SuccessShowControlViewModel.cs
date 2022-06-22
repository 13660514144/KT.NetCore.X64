using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Elevator.Unit.Entity.Models;
using KT.Quanta.Common.Models;
using KT.Turnstile.Unit.ClientApp.Events;
using KT.Turnstile.Unit.ClientApp.Service.IServices;
using KT.Turnstile.Unit.ClientApp.Views.Controls;
using KT.Turnstile.Unit.ClientApp.Views.Helpers;
using KT.Turnstile.Unit.Entity.Models;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Turnstile.Unit.ClientApp.Views
{
    public class SuccessShowControlViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private IPassRecordService _passRecordService;
        private AppSettings _appSettings;
        private IRegionManager _regionManager;

        public SuccessShowControlViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _passRecordService = ContainerHelper.Resolve<IPassRecordService>();
            _regionManager = ContainerHelper.Resolve<IRegionManager>();

            _regionManager.RegisterViewWithRegion(RegionNameHelper.HandleElevatorSuccessRegion, typeof(HandleElevatorSuccessControl));
            _regionManager.RegisterViewWithRegion(RegionNameHelper.FaceNoRightPassRegion, typeof(FaceNoRightPassControl));
            _regionManager.RegisterViewWithRegion(RegionNameHelper.FaceEnterPassRegion, typeof(FaceEnterPassControl));
            _regionManager.RegisterViewWithRegion(RegionNameHelper.FaceLeavePassRegion, typeof(FaceLeavePassControl));
            _regionManager.RegisterViewWithRegion(RegionNameHelper.DefaultNoRightPassRegion, typeof(DefaultNoRightPassControl));
            _regionManager.RegisterViewWithRegion(RegionNameHelper.DefaultEnterPassRegion, typeof(DefaultEnterPassControl));
            _regionManager.RegisterViewWithRegion(RegionNameHelper.DefaultLeavePassRegion, typeof(DefaultLeavePassControl));
        }

    }
}
