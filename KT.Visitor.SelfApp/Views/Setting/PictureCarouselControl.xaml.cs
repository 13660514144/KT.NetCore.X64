using KT.Common.WpfApp.Helpers;
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

namespace KT.Visitor.SelfApp.Views.Setting
{
    /// <summary>
    /// PictureCarouselControl.xaml 的交互逻辑
    /// </summary>
    public partial class PictureCarouselControl : UserControl
    {
        private PictureCarouselControlViewModel _viewModel;
        private SelfAppSettings _selfAppSettings;

        public PictureCarouselControl()
        {
            InitializeComponent();

            _viewModel = ContainerHelper.Resolve<PictureCarouselControlViewModel>();
            _selfAppSettings = ContainerHelper.Resolve<SelfAppSettings>();

            this.DataContext = _viewModel;
 
            _viewModel.Init();
        }
    }
}
