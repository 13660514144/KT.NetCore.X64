using KT.Common.WpfApp.Helpers;
using KT.Visitor.Common.Helpers;
using KT.Visitor.IdReader;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Views.Register;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KT.Visitor.IntegrateApp.Views
{
    /// <summary>
    /// readIDNumber.xaml 的交互逻辑
    /// </summary>
    public partial class ReadIdCardControl : UserControl
    {
        public ReadIdCardControlViewModel ViewModel { get; set; }

        private ConfigHelper _configHelper;
        private ILogger _logger;

        public ReadIdCardControl()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<ReadIdCardControlViewModel>();
            this.DataContext = ViewModel;

            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();
        }
    }
}
