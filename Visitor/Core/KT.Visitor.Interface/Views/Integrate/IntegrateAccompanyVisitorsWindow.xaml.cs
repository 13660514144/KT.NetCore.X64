using CommonUtils;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.IdReader;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KT.Visitor.Interface.Views.Integrate
{
    /// <summary>
    /// IntegrateAccompanyVisitorsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateAccompanyVisitorsWindow : WindowX
    {
        public IntegrateAccompanyVisitorsWindowViewModel ViewModel;

        private ILogger<IntegrateAccompanyVisitorsWindow> _logger;
        private IContainerProvider _containerProvider;

        public IntegrateAccompanyVisitorsWindow(IntegrateAccompanyVisitorsWindowViewModel viewModel,
            ILogger<IntegrateAccompanyVisitorsWindow> logger,
            IContainerProvider containerProvider)
        {
            InitializeComponent();


            _logger = logger;
            _containerProvider = containerProvider;

            SetViewModel(viewModel);
        }

        internal void SetViewModel(IntegrateAccompanyVisitorsWindowViewModel viewModel)
        {
            ViewModel = viewModel;
            this.DataContext = ViewModel;
        }
    }
}