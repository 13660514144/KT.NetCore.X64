using KT.Common.WpfApp.Helpers;
using System;
using System.Windows.Controls; 

namespace KT.Elevator.Unit.Secondary.ClientApp.Views
{
    /// <summary>
    /// DestinationPanelControl.xaml 的交互逻辑
    /// </summary>
    public partial class DestinationPanelControl : UserControl
    {
        public DestinationPanelControlViewModel ViewModel { get; private set; }
        public DestinationPanelControl()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<DestinationPanelControlViewModel>();

            this.DataContext = ViewModel;
        } 
    }
}
