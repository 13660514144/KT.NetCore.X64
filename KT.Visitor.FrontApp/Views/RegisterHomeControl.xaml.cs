using KT.Common.WpfApp.Helpers;
using Prism.Ioc;
using Prism.Regions;
using System.Windows.Controls;

namespace KT.Visitor.FrontApp.Views.Register
{
    /// <summary>
    /// RegisterHomeControl.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterHomeControl : UserControl
    {
        //private RegisterHomeControlViewModel _viewModel;
        private IRegionManager _regionManager;

        public RegisterHomeControl(IRegionManager regionManager)
        {
            InitializeComponent();

            _regionManager = regionManager;
            RegionManager.SetRegionManager(this, _regionManager);

            //_viewModel = ContainerHelper.Resolve<RegisterHomeControlViewModel>();

            ////初始化要加载数据，先登录再加载
            //this.DataContext = _viewModel;
            this.Loaded += RegisterHomeControl_Loaded;
        }

        private void RegisterHomeControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        { 
            var viewModel = (RegisterHomeControlViewModel)this.DataContext;

            viewModel.ViewLoadedInit(_regionManager);
        }
    }
}
