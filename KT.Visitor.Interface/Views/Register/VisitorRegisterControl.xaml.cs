using Prism.Regions;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Register
{
    /// <summary>
    /// VisitorRegisterControl.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorRegisterControl : UserControl
    {
        private IRegionManager _regionManager;

        public VisitorRegisterControl(IRegionManager regionManager)
        {
            InitializeComponent();

            _regionManager = regionManager;
            RegionManager.SetRegionManager(this, _regionManager);

            var viewModel = (VisitorRegisterControlViewModel)this.DataContext;
            viewModel.ViewLoadedInit(_regionManager);
        } 
    }
}
