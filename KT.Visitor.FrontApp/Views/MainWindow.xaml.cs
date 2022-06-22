using KT.Common.Core.Helpers;
using KT.Common.WpfApp.Helpers;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Tools.ArcIdMatch.ArcIdSdk.Helpers;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Helpers;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using Prism.Events;
using Prism.Regions;
using System;
using System.Windows;

namespace KT.Visitor.FrontApp.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WindowX
    {
        private IRegionManager _regionManager;
        private ReaderFactory _readerFactory;
        private ConfigHelper _configHelper;
        private IEventAggregator _eventAggregator;
        private ArcIdSdkHelper _arcIdCardHelper;
        private ILogger _logger;

        public MainWindow()
        {
            InitializeComponent();

            _regionManager = ContainerHelper.Resolve<IRegionManager>();
            RegionManager.SetRegionManager(this, _regionManager);

            _readerFactory = ContainerHelper.Resolve<ReaderFactory>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _arcIdCardHelper = ContainerHelper.Resolve<ArcIdSdkHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();

            this.KeyDown += MainWindow_KeyDown;
            this.Closed += MainWindow_Closed;
            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)this.DataContext;

            viewModel.ViewLoadedInit(_regionManager);

            //加载证件阅读器
            ReaderFactory.Reader = await _readerFactory.StartAsync(_configHelper.LocalConfig?.Reader, SetPersonAsync, false);

            //初始化人证比对
            //_arcIdCardHelper.Init();

        }

        private void SetPersonAsync(Person person)
        {
            _eventAggregator.GetEvent<ReadedPersonEvent>().Publish(person);
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyDownHelper.ShowSeachAction?.Invoke(sender, e);
        }

        private async void MainWindow_Closed(object sender, EventArgs e)
        {
            EnvironmentExitHelper.Start();

            //销毁
            try
            {
                //关闭证件阅读器
                await _readerFactory.DisposeReaderAsync();

                //关闭人证比对
                _arcIdCardHelper.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError($"关闭程序销毁数据失败：ex:{ex} ");
            }
            finally
            {
                Environment.Exit(-1);
            }
        }
    }
}
