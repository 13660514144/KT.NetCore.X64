using KT.Common.WpfApp.ViewModels;
using KT.Quanta.Kone.ToolApp.Events;
using KT.Quanta.Kone.ToolApp.Models;
using KT.Quanta.Kone.ToolApp.Views.Details;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Windows.Input;

namespace KT.Quanta.Kone.ToolApp.Views
{
    public class MainWindowViewModel : BindableBase
    {
        private NavigationItemModel _navigationItem;
        private List<NavigationItemModel> _navigationItems;

        private ICommand _navigationSelectedCommand;
        public ICommand NavigationSelectedCommand => _navigationSelectedCommand ??= new DelegateCommand<NavigationItemModel>(NavigationSelected);

        private void NavigationSelected(NavigationItemModel navigationItem)
        {
            if (!string.IsNullOrEmpty(navigationItem?.Uri))
            {
                _regionManager.RequestNavigate("ContentRegion", navigationItem?.Uri);
            }
        }

        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        public MainWindowViewModel(IRegionManager regionManager,
            IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            NavigationItems = new List<NavigationItemModel>();
            NavigationItems.Add(new NavigationItemModel()
            {
                Name = "楼层方向配置",
                Uri = nameof(FloorDirectionControl)
            });
            NavigationItems.Add(new NavigationItemModel()
            {
                Name = "Dop Global Mask",
                Uri = nameof(DopGlobalMaskListControl)
            });
            NavigationItems.Add(new NavigationItemModel()
            {
                Name = "Dop Specific Mask",
                Uri = nameof(DopSpecificMaskListControl)
            });
            NavigationItems.Add(new NavigationItemModel()
            {
                Name = "清除真实Mask（不清除数据库）",
                Uri = nameof(ClearRoyalMaskControl)
            });
            NavigationItems.Add(new NavigationItemModel()
            {
                Name = "派梯设备",
                Uri = nameof(HandleElevatorDeviceControl)
            });
            NavigationItems.Add(new NavigationItemModel()
            {
                Name = "权限管理",
                Uri = nameof(PassRightListControl)
            });
            NavigationItems.Add(new NavigationItemModel()
            {
                Name = "操作",
                Uri = nameof(OperationControl)
            });
            NavigationItems.Add(new NavigationItemModel()
            {
                Name = "设置",
                Uri = nameof(SettingControl)
            });
            NavigationItems.Add(new NavigationItemModel()
            {
                Name = "Mask记录",
                Uri = nameof(DopMaskRecordListControl)
            });
        }


        public NavigationItemModel NavigationItem
        {
            get { return _navigationItem; }
            set { SetProperty(ref _navigationItem, value); }
        }

        public List<NavigationItemModel> NavigationItems
        {
            get { return _navigationItems; }
            set { SetProperty(ref _navigationItems, value); }
        }
    }
}
