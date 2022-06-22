using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Device.Unit.CardReaders.Events;
using KT.Device.Unit.CardReaders.Models;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Processor.ClientApp.Events;
using KT.Elevator.Unit.Processor.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Processor.ClientApp.Service.IServices;
using KT.Elevator.Unit.Processor.ClientApp.Service.Network.NettyServer;
using KT.Elevator.Unit.Processor.ClientApp.ViewModels;
using KT.Elevator.Unit.Processor.ClientApp.Views.Controls;
using KT.Quanta.Common.Enums;
using KT.Quanta.Common.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KT.Elevator.Unit.Processor.ClientApp.Views
{
    public class MainWindowViewModel : BindableBase
    {
        private UserControl _showControl;
        private WarnTipControl _warnTipControl;

        private string _cardNumber;

        private UserControl _homeControl;
        private SuccessControl _successControl;

        private HubHelper _hubHelper;
        private ConfigHelper _configHelper;
        private INettyServerHost _nettyServerHost;

        private ILogger _logger;
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        private AppSettings _appSettings;

        public MainWindowViewModel()
        {
            WarnTipControl = ContainerHelper.Resolve<WarnTipControl>();

            _successControl = ContainerHelper.Resolve<SuccessControl>();

            _regionManager = ContainerHelper.Resolve<IRegionManager>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();

            _hubHelper = ContainerHelper.Resolve<HubHelper>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            _homeControl = ContainerHelper.Resolve<WelcomeControl>();

            ShowControl = _homeControl;

            _eventAggregator.GetEvent<LinkToHomeEvent>().Subscribe(LinkToHome);
            _eventAggregator.GetEvent<OneComeEvent>().Subscribe(OneCome);
            _eventAggregator.GetEvent<CardInputedEvent>().Subscribe(CardInputed);
            _eventAggregator.GetEvent<FaceNoPassEvent>().Subscribe(FaceNoPass);
            _eventAggregator.GetEvent<ShowSuccessPanelEvent>().Subscribe(ShowSuccessPanel);
            _eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Subscribe(HandledElevatorSuccess);
            _eventAggregator.GetEvent<HandledElevatorErrorEvent>().Subscribe(HandledElevatorError);
        }

        private void HandledElevatorError(HandleElevatorErrorModel obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ShowControl = _successControl;
            });
        }

        private void HandledElevatorSuccess(HandleElevatorDisplayModel obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ShowControl = _successControl;
            });
        }

        //private void HandledElevatorAsync(UnitHandleElevatorModel obj)
        //{
        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        ShowControl = _successControl;
        //    });
        //}

        private void ShowSuccessPanel()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ShowControl = _successControl;
            });
        }

        private void FaceNoPass(UnitHandleElevatorModel handleElevator)
        {
            FaceNoPassAsync(handleElevator);
        }

        private void CardInputed(UnitHandleElevatorModel handleElevator)
        {
            CardInputedAsync(handleElevator);
        }

        private async void FaceNoPassAsync(UnitHandleElevatorModel handleElevator)
        {
            //一体机可去楼层
            var floorService = ContainerHelper.Resolve<IFloorService>();
            var floorModels = await floorService.GetAllByHandleElevatorDeviceIdAsync(handleElevator.HandleElevatorDeviceId);
            var floorTuple = FloorViewModel.ToModels(floorModels);

            _eventAggregator.GetEvent<ShowSuccessPanelEvent>().Publish();

            ////无权限
            //if (floorTuple.PassableFloors.FirstOrDefault() == null)
            //{
            //    _eventAggregator.GetEvent<WarnTipEvent>().Publish("抱歉，您无可去楼层权限！");
            //    _eventAggregator.GetEvent<NotRightEvent>().Publish();
            //}

            //多楼层派梯，公共楼层
            handleElevator.DestinationFloorIds = floorTuple.PassableFloors.Select(x => x.Id).ToList();
            //handleElevator.SourceFloorId = _configHelper.LocalConfig.DeviceFloorId;
            handleElevator.AccessType = AccessTypeEnum.FACE.Value;
            handleElevator.DeviceType = CardDeviceTypeEnum.FACE_CAMERA.Value;

            _eventAggregator.GetEvent<HandledElevatorEvent>().Publish(handleElevator);

            await _hubHelper.ManualHandleElevatorAsync(handleElevator);
        }

        /// <summary>
        /// 人脸授权成功
        /// </summary>
        /// <param name="faceId">人脸Id</param>
        private async void CardInputedAsync(UnitHandleElevatorModel handleElevator)
        {
            _logger.LogInformation($"刷卡通行：handleElevator:{JsonConvert.SerializeObject(handleElevator, JsonUtil.JsonPrintSettings)} ");

            //判断是否有权限 
            var passRightService = ContainerHelper.Resolve<IPassRightService>();
            var passRight = await passRightService.GetBySignAsync(handleElevator.HandleElevatorRight.PassRightSign, handleElevator.AccessType);
            if (passRight != null)
            {
                _logger.LogInformation($"刷卡通行权限：passRights:{JsonConvert.SerializeObject(passRight, JsonUtil.JsonPrintSettings)} ");
            }

            //一体机可去楼层
            var floorService = ContainerHelper.Resolve<IFloorService>();
            var floorModels = await floorService.GetAllByHandleElevatorDeviceIdAsync(handleElevator.HandleElevatorDeviceId);
            var floorTuple = FloorViewModel.ToModels(floorModels, passRight);

            _eventAggregator.GetEvent<ShowSuccessPanelEvent>().Publish();

            ////无权限
            //if (floorTuple.PassableFloors.FirstOrDefault() == null)
            //{
            //    _eventAggregator.GetEvent<WarnTipEvent>().Publish("抱歉，您无可去楼层权限！");
            //    _eventAggregator.GetEvent<NotRightEvent>().Publish();
            //}

            //直接派梯，只直接派有权限的楼层，不直接派公共楼层
            if (_appSettings.IsEnableAutoHandleElevator && floorTuple.PassableFloors.Count == 1 && floorTuple.RightFloors.FirstOrDefault() != null)
            {
                var floor = floorTuple.RightFloors.FirstOrDefault();

                handleElevator.DestinationFloorId = floor.Id;
                //handleElevator.SourceFloorId = _configHelper.LocalConfig.DeviceFloorId;

                handleElevator.DestinationFloorName = floor.Name;
                _eventAggregator.GetEvent<HandledElevatorEvent>().Publish(handleElevator);

                await _hubHelper.HandleElevatorAsync(handleElevator);
            }
            //目的层选择器
            else if (_appSettings.IsEnableManualHandleElevator)
            {
                //多楼层派梯
                handleElevator.DestinationFloorIds = floorTuple.PassableFloors.Select(x => x.Id).ToList();
                //handleElevator.SourceFloorId = _configHelper.LocalConfig.DeviceFloorId;

                _eventAggregator.GetEvent<HandledElevatorEvent>().Publish(handleElevator);

                await _hubHelper.ManualHandleElevatorAsync(handleElevator);
            }
            else
            {
                _logger.LogError($"派梯类型配置错误：IsEnableAutoHandleElevator:{_appSettings.IsEnableAutoHandleElevator},IsEnableManualHandleElevator:{_appSettings.IsEnableManualHandleElevator}");
            }
        }

        private void OneCome()
        {
            _homeControl.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 回到首页
        /// </summary>
        private void LinkToHome()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ShowControl = _homeControl;

                Task.Run(async () =>
                {
                    await Task.Delay((int)(_appSettings.FaceRestartSecondTime * 1000));

                    _eventAggregator.GetEvent<CameraInitializeStateEvent>().Publish(true);
                });
            });
        }

        /// <summary>
        /// 使用卡号开始派梯
        /// </summary>
        /// <param name="cardNumber">卡号，用作参数，防止异步操作提前删除</param>
        /// <returns></returns>
        internal async void CardNumberPassAsync(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                return;
            }

            var inputs = cardNumber.Split(",");

            var cardReceive = new CardReceiveModel();
            cardReceive.CardNumber = inputs[0];
            cardReceive.IsCheckDate = false;

            cardReceive.AccessType = "IC_CARD";
            if (inputs.Length > 1)
            {
                if (inputs[1] == 2.ToString())
                {
                    cardReceive.AccessType = "QR_CODE";
                }
            }

            var cardDeviceService = ContainerHelper.Resolve<ICardDeviceService>();
            var cardDevices = await cardDeviceService.GetAllAsync();
            var cardDevice = cardDevices.FirstOrDefault();
            if (inputs.Length > 2)
            {
                var cardDeviceId = inputs[2];
                cardDevice = cardDevices.FirstOrDefault(x => x.Id == cardDeviceId);
            }

            var data = new CardDeviceAnalyzedModel();
            data.CardDeviceInfo = cardDevice;
            data.CardReceive = cardReceive;

            _eventAggregator.GetEvent<CardDeviceAnalyzedEvent>().Publish(data);
        }

        /// <summary>
        /// 键盘输入，读卡器为USB读卡器，监听键盘输入，获取USB读卡器的值
        /// </summary>
        /// <param name="e"></param>
        internal void AddCardNumberKey(System.Windows.Forms.KeyEventArgs e)
        {
            _logger.LogInformation($"输入字符：{e.KeyCode} ");
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                //异步 通行
                _logger.LogInformation($"输入卡号：{_cardNumber} ");

                CardNumberPassAsync(_cardNumber);

                _cardNumber = string.Empty;
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.Back)
            {
                if (_cardNumber == null || _cardNumber.Length == 0)
                {
                    return;
                }
                else if (_cardNumber.Length == 1)
                {
                    _cardNumber = string.Empty;
                }
                else
                {
                    _cardNumber = _cardNumber[0..^1];
                }
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.NumPad0 || e.KeyCode == System.Windows.Forms.Keys.D0)
            {
                _cardNumber += "0";
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.NumPad1 || e.KeyCode == System.Windows.Forms.Keys.D1)
            {
                _cardNumber += "1";
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.NumPad2 || e.KeyCode == System.Windows.Forms.Keys.D2)
            {
                _cardNumber += "2";
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.NumPad3 || e.KeyCode == System.Windows.Forms.Keys.D3)
            {
                _cardNumber += "3";
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.NumPad4 || e.KeyCode == System.Windows.Forms.Keys.D4)
            {
                _cardNumber += "4";
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.NumPad5 || e.KeyCode == System.Windows.Forms.Keys.D5)
            {
                _cardNumber += "5";
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.NumPad6 || e.KeyCode == System.Windows.Forms.Keys.D6)
            {
                _cardNumber += "6";
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.NumPad7 || e.KeyCode == System.Windows.Forms.Keys.D7)
            {
                _cardNumber += "7";
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.NumPad8 || e.KeyCode == System.Windows.Forms.Keys.D8)
            {
                _cardNumber += "8";
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.NumPad9 || e.KeyCode == System.Windows.Forms.Keys.D9)
            {
                _cardNumber += "9";
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.F5)
            {
                //先清空本地人脸图。
                _hubHelper.InitDataAsync(true);
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.F6)
            {
                //从服务器获取数据到本地
                _hubHelper.DeleteDataAsync();
            }
            else
            {
                _logger.LogWarning("输入无效字符：{0} ", e.KeyCode);
            }
        }

        public async void InitOrderAsync()
        {
            //初始化服务器连接
            _hubHelper.StartAsync();

            //初始化读卡器设备,获取完服务器数据后才能初始化读卡器，否则数据不正确
            var serialDeviceDeviceService = ContainerHelper.Resolve<ISerialDeviceService>();
            await serialDeviceDeviceService.InitAllCardDeviceAsync();

            //启动http服务
            _nettyServerHost = ContainerHelper.Resolve<INettyServerHost>();
            await _nettyServerHost.RunAsync(_appSettings.Port + 1);
        }

        internal async Task EndAsync()
        {
            if (_nettyServerHost != null)
            {
                await _nettyServerHost.CloseAsync();
            }
        }

        public WarnTipControl WarnTipControl
        {
            get
            {
                return _warnTipControl;
            }

            set
            {
                SetProperty(ref _warnTipControl, value);
            }
        }

        public UserControl ShowControl
        {
            get
            {
                return _showControl;
            }

            set
            {
                SetProperty(ref _showControl, value);
            }
        }
    }
}
