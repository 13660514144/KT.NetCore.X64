using HelperTools;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
//using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Secondary.ClientApp.Device.Hitach;
using KT.Elevator.Unit.Secondary.ClientApp.Events;
using KT.Elevator.Unit.Secondary.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using KT.Elevator.Unit.Secondary.ClientApp.Service.Network.NettyServer;
using KT.Elevator.Unit.Secondary.ClientApp.ViewModels;
using KT.Elevator.Unit.Secondary.ClientApp.Views.Controls;
using KT.Quanta.Common.Enums;
using KT.Quanta.Common.Models;
using KT.Unit.FaceRecognition.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
//using System.Collections.Generic;
using System.Collections.ObjectModel;
//using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text;
using System;
using KT.Elevator.Unit.Entity.Entities;
using System.Collections.Generic;
using ContralServer.CfgFileRead;
using System.Data;
using System.Runtime.InteropServices;
using KT.Elevator.Unit.Secondary.ClientApp.Models;
using System.Threading;
//using KT.Elevator.Unit.Secondary.ClientApp.WsScoket;//金的还原版本

namespace KT.Elevator.Unit.Secondary.ClientApp.Views
{
    public class MainWindowViewModel : BindableBase
    {
        private UserControl _showControl;
        private WelcomeControl _welcomeControl;
        private WarnTipControl _warnTipControl;

        private string _cardNumber=string.Empty;        
        private long _endTime ;
        private DateTime _changeTime ;
        private bool FlgTime = false;
        private ArcFreeFaceDistinguishControl _arcFreeFaceDistinguishControl;
        private ArcProFaceDistinguishControl _arcProFaceDistinguishControl;

        private DestinationPanelControl _destinationPanelControl;

        private SuccessControl _successControl;
        //private ManageOperateControl _manageOperateControl;

        //private IRegion _welcomeRegion;
        //private IRegion _contentRegion;

        private HubHelper _hubHelper;
        private ConfigHelper _configHelper;
        private INettyServerHost _nettyServerHost;

        private ILogger _logger;
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        //private ArcInitHelper _arcFaceHelper;
        private AppSettings _appSettings;
        //private FaceFactory _faceFactory;
        private readonly FaceFactoryProxy _faceFactoryProxy;
        private readonly FaceRecognitionAppSettings _faceRecognitionAppSettings;
        //private WebSocket4net _WsSocket;//金的还原版本

        public MainWindowViewModel()
        {
            WelcomeControl = ContainerHelper.Resolve<WelcomeControl>();
            WarnTipControl = ContainerHelper.Resolve<WarnTipControl>();

            _welcomeControl.Visibility = Visibility.Collapsed;

            _faceRecognitionAppSettings = ContainerHelper.Resolve<FaceRecognitionAppSettings>();

            _destinationPanelControl = ContainerHelper.Resolve<DestinationPanelControl>();
            _successControl = ContainerHelper.Resolve<SuccessControl>();
            
            if (_faceRecognitionAppSettings.FaceAuxiliaryType == FaceRecognitionTypeEnum.ArcFree22.Value)
            {
                _arcFreeFaceDistinguishControl = ContainerHelper.Resolve<ArcFreeFaceDistinguishControl>();
                ShowControl = _arcFreeFaceDistinguishControl;
            }
            else if (_faceRecognitionAppSettings.FaceAuxiliaryType == FaceRecognitionTypeEnum.ArcPro40.Value)
            {
                _arcProFaceDistinguishControl = ContainerHelper.Resolve<ArcProFaceDistinguishControl>();
                ShowControl = _arcFreeFaceDistinguishControl;
            }
            else
            {
                throw new System.Exception($"找不到人脸识别类型：faceAuxiliaryType:{_faceRecognitionAppSettings.FaceAuxiliaryType} ");
            }

            //_WsSocket = ContainerHelper.Resolve<WebSocket4net>();//金的还原版本
            _regionManager = ContainerHelper.Resolve<IRegionManager>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _faceFactoryProxy = ContainerHelper.Resolve<FaceFactoryProxy>();

            _hubHelper = ContainerHelper.Resolve<HubHelper>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<LinkToHomeEvent>().Subscribe(LinkToHome);
            _eventAggregator.GetEvent<OneComeEvent>().Subscribe(OneCome);
            _eventAggregator.GetEvent<CardInputedEvent>().Subscribe(CardInputed);
            _eventAggregator.GetEvent<FaceNoPassEvent>().Subscribe(FaceNoPass);
            _eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Subscribe(HandledElevatorSuccess);
            _eventAggregator.GetEvent<HandledElevatorEvent>().Subscribe(HandledElevatorAsync);

            // _eventAggregator.GetEvent<FaceInputedEvent>().SubscribeAsync(FaceInputedAsync);
            //_eventAggregator.GetEvent<FaceNoPassEvent>().SubscribeAsync(FaceNoPassAsync);
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 250;
            timer.Elapsed += CheckNumber;
            timer.Enabled = true;
            timer.Start();
        }
        
        private void CheckNumber(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_cardNumber.Length > 7 && (HelperTools.TimeSpanHelp.Timestamp() - _endTime) > 250)
            {
                string Scard = _cardNumber;
                _cardNumber = string.Empty;
                _logger.LogInformation($"输入卡号：{Scard} ");
                try
                {
                    CardNumberPassAsync(Scard);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"输入卡号ERR：{ex} ");
                }
            }
        }
        private void HandledElevatorAsync(UnitHandleElevatorModel obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ShowControl = _successControl;
            });
        }

        private void HandledElevatorSuccess(HandleElevatorDisplayModel handleElevator)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ShowControl = _successControl;
            });
        }

        private void FaceNoPass(UnitHandleElevatorModel handleElevator)
        {
            //FaceNoPassAsync(handleElevator);  统一在线鉴权
            CardInputedAsync(handleElevator);
        }

        private void CardInputed(UnitHandleElevatorModel handleElevator)
        {
            CardInputedAsync(handleElevator);
        }

        private async void FaceNoPassAsync(UnitHandleElevatorModel handleElevator)
        {
            //公区楼层查询
            //SIGNLAR 通道
            string IfApiOrSql = AppConfigurtaionServices.Configuration["IfApiOrSql"].ToString().Trim();
            if (IfApiOrSql == "API")
            {
                //在线鉴权
                ReqElvator O = new ReqElvator
                {
                    Ip = AppConfigurtaionServices.Configuration["AppSettings:StartWithIp"].ToString().Trim(),
                    Sign = "0000",
                    Port = AppConfigurtaionServices.Configuration["AppSettings:Port"].ToString().Trim()
                };
                string Req = await _hubHelper.ReqElevatorPassrightGuest(O);
                //SIGNLAR 通道
                JArray JRightArray = new JArray();
                try
                {
                    //JObject Data = NoRight(handleElevator);//api
                    JObject Data = JObject.Parse(Req);//SIGNLAR
                    _logger.LogInformation($"公区楼层==>{JsonConvert.SerializeObject(Data)}");
                    if ((string)Data["Flg"] == "0")
                    {
                        _eventAggregator.GetEvent<WarnTipEvent>().Publish("抱歉，您无可去楼层权限！");
                        _eventAggregator.GetEvent<NotRightEvent>().Publish();
                        return;
                    }
                    else
                    {
                        JRightArray = (JArray)Data["Data"];
                        var Passfloor = new ObservableCollection<FloorViewModel>();
                        List<UnitFloorEntity> FloorEntity = Package(JRightArray,
                            new DataTable(),
                            handleElevator.Sign, 0, handleElevator);

                        var floorTuple = FloorViewModel.ToModels(FloorEntity);
                        await Application.Current.Dispatcher.Invoke(async () =>
                        {
                            await _destinationPanelControl.ViewModel.InitAsync(handleElevator, floorTuple.AllFloors);
                            _welcomeControl.Visibility = Visibility.Collapsed;
                            ShowControl = _destinationPanelControl;
                        });
                        _eventAggregator.GetEvent<LinkToDestinationPanelEvent>().Publish();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"公区楼层错误==>{ex}");
                    _eventAggregator.GetEvent<WarnTipEvent>().Publish("您无可去楼层权限！");
                    _eventAggregator.GetEvent<NotRightEvent>().Publish();
                }
            }
            else
            {
                //本地鉴权
                //一体机可去楼层
                var floorService = ContainerHelper.Resolve<IFloorService>();
                var floorModels = await floorService.GetAllByElevatorGroupIdAsync(_configHelper.LocalConfig.ElevatorGroupId);
                var floorTuple = FloorViewModel.ToModels(floorModels);

                //无权限
                if (floorTuple.PassableFloors.FirstOrDefault() == null)
                {
                    _eventAggregator.GetEvent<WarnTipEvent>().Publish("抱歉，您无可去楼层权限！");
                    _eventAggregator.GetEvent<NotRightEvent>().Publish();
                    return;
                }
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    //初始化楼层选择数据
                    await _destinationPanelControl.ViewModel.InitAsync(handleElevator, floorTuple.AllFloors);
                    _welcomeControl.Visibility = Visibility.Collapsed;
                    ShowControl = _destinationPanelControl;
                });
                _eventAggregator.GetEvent<LinkToDestinationPanelEvent>().Publish();
            }
            
        }
        private List<UnitFloorEntity> Package(JArray JRightArray,
            DataTable Tb, 
            string Sign,int TbOrJarry, UnitHandleElevatorModel handleElevator)
        {
            List<UnitFloorEntity> FloorEntity = new List<UnitFloorEntity>();
            for (int x = 0; x < JRightArray.Count; x++)
            {
                UnitFloorEntity UnitFloor = new UnitFloorEntity();
                UnitFloor.Name = JRightArray[x]["Name"].ToString();
                UnitFloor.RealFloorId = JRightArray[x]["RealFloorId"].ToString();
                UnitFloor.IsPublic = JRightArray[x]["IsPublic"].ToString().Trim() == "0" ? false : true;
                UnitFloor.EdificeId = JRightArray[x]["EdificeId"].ToString();
                UnitFloor.EdificeName = JRightArray[x]["EdificeName"].ToString();
                UnitFloor.ElevatorGroupId = JRightArray[x]["ElevatorGroupId"].ToString();
                UnitFloor.HandleElevatorDeviceId = JRightArray[x]["id"].ToString();
                UnitFloor.CommBox = JRightArray[x]["CommBox"].ToString();
                UnitFloor.DestinationFloorId = JRightArray[x]["FloorId"].ToString();
                UnitFloor.Sourceid = JRightArray[x]["Sourceid"].ToString();
                UnitFloor.HandDeviceid = JRightArray[x]["id"].ToString();
                UnitFloor.AccessType = handleElevator.AccessType;
                UnitFloor.DeviceType = JRightArray[x]["DeviceType"].ToString(); //handleElevator.DeviceType;                                
                if (handleElevator.AccessType.ToString().Trim() == "FACE")
                //if (handleElevator.DeviceType.ToString().Trim() == "FACE_CAMERA")
                {
                    UnitFloor.Sign = JRightArray[x]["PersonId"].ToString();
                }
                else
                {
                    UnitFloor.Sign = string.IsNullOrEmpty(Sign) == false ? Sign : JRightArray[x]["Sign"].ToString();
                }
                FloorEntity.Add(UnitFloor);
            }
            return FloorEntity;
        }
        private UnitPassRightEntity PackageRight(JArray JRightArray,
            DataTable Tb,
            string Sign, int TbOrJarry)
        {
            UnitPassRightEntity PassEntity = new UnitPassRightEntity();
            PassEntity.Sign= string.IsNullOrEmpty(Sign) == false ? Sign : JRightArray[0]["Sign"].ToString();
            PassEntity.AccessType= JRightArray[0]["AccessType"].ToString();
            PassEntity.TimeStart = 0;
            PassEntity.TimeEnd = 0;
            PassEntity.PassRightDetails = new List<UnitPassRightDetailEntity>();
            for (int x = 0; x < JRightArray.Count; x++)
            {
                UnitPassRightDetailEntity UnitPass = new UnitPassRightDetailEntity();
                UnitPass.PassRightId = JRightArray[x]["PassRightId"].ToString();
                UnitPass.FloorId = JRightArray[x]["FloorId"].ToString();
                UnitPass.FloorName = JRightArray[x]["Name"].ToString().Trim();
                UnitPass.RealFloorId = JRightArray[x]["RealFloorId"].ToString();
                UnitPass.IsPublic = (JRightArray[x]["IsPublic"].ToString().Trim() == "0" ? false : true) ;
                PassEntity.PassRightDetails.Add(UnitPass);
            }            
            return PassEntity;
        }
        /// <summary>
        /// 陌生人公区权限
        /// </summary>
        /// <param name="handleElevator"></param>
        /// <returns></returns>
        private JObject NoRight(UnitHandleElevatorModel handleElevator)
        {
            string Result = string.Empty;
            _logger.LogInformation($"handleElevator==>{JsonConvert.SerializeObject(handleElevator)}");
            string IfApiOrSql = AppConfigurtaionServices.Configuration["IfApiOrSql"].ToString().Trim();
            string ElevatorType = AppConfigurtaionServices.Configuration["ElevatorType"].ToString().Trim();            
            JObject Data = new JObject();
            
            Dictionary<string, string> SendData = new Dictionary<string, string>
                {
                {"Ip", AppConfigurtaionServices.Configuration["AppSettings:StartWithIp"].ToString().Trim() },
                {"Sign", "0000"},
                {"Port", AppConfigurtaionServices.Configuration["AppSettings:Port"].ToString().Trim() }
                };
            string Controller = "Api/PassRecoadGuest/GuestPassAllPublic";
            try
            {
                Result = RightGetModel.ApiGetDataWording(Controller, SendData);
                Data = JObject.Parse(Result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"API error==>{ex}");
                dynamic Obj = new DynamicObj();
                Obj.Code = "000";
                Obj.Flg = "0";
                Obj.Message = $"Error:{ex}";
                Obj.Data = new JArray();
                Data = JObject.Parse(JsonConvert.SerializeObject(Obj));
            }
            return Data;
        }
        /// <summary>
        /// 人脸授权成功
        /// </summary>
        /// <param name="faceId">人脸Id</param>
        private async void CardInputedAsync(UnitHandleElevatorModel handleElevator)
        {
            _logger.LogInformation($"刷卡通行：handleElevator:{JsonConvert.SerializeObject(handleElevator, JsonUtil.JsonPrintSettings)} ");
            string IfApiOrSql = AppConfigurtaionServices.Configuration["IfApiOrSql"].ToString().Trim();
            string ElevatorType = AppConfigurtaionServices.Configuration["ElevatorType"].ToString().Trim();
            var ElevotarDisp = ContainerHelper.Resolve<Dispatch>();
            DataTable RightArray = new DataTable();
            JObject Data = new JObject();
            JArray JRightArray = new JArray();
            _logger.LogInformation(IfApiOrSql);
            try
            {
                if (IfApiOrSql == "API")
                {
                    //在线鉴权
                    #region SIGNLAR 通道
                    /*ReqElvator O = new ReqElvator
                    {
                        Ip= AppConfigurtaionServices.Configuration["AppSettings:StartWithIp"].ToString().Trim(),
                        Sign= handleElevator.Sign,
                        Port= AppConfigurtaionServices.Configuration["AppSettings:Port"].ToString().Trim(),
                        AccessType= handleElevator.AccessType
                    };
                    string Req = await _hubHelper.ReqElevatorPassright(O);
                    if (string.IsNullOrEmpty(Req))
                    {
                        FaceNoPassAsync(handleElevator);
                        Flg = true;
                        return;
                    }
                    else
                    {
                        Data = JObject.Parse(Req);
                        if ((string)Data["Flg"] == "0")
                        {
                            FaceNoPassAsync(handleElevator);
                            Flg = true;
                            return;
                        }
                        JRightArray = (JArray)Data["Data"];
                    }*/
                    #endregion SIGNLAR 通道
                    //  API通道
                    Data = ElevotarDisp.ElevotarRecord_Api(handleElevator.Sign, handleElevator.AccessType);
                    if ((string)Data["Flg"] == "0")
                    {
                        _eventAggregator.GetEvent<WarnTipEvent>().Publish("抱歉，您无可去楼层权限！");
                        _eventAggregator.GetEvent<NotRightEvent>().Publish();
                        return;
                    }
                    JRightArray = (JArray)Data["Data"];
                    var Passfloor = new ObservableCollection<FloorViewModel>();
                    List<UnitFloorEntity> FloorEntity = Package(JRightArray,
                        new DataTable(),
                        string.Empty, 0, handleElevator);
                    var floorTuple = FloorViewModel.ToModels(FloorEntity);
                    
                        await Application.Current.Dispatcher.Invoke(async () =>
                        {
                            await _destinationPanelControl.ViewModel.InitAsync(handleElevator, floorTuple.AllFloors);
                            _welcomeControl.Visibility = Visibility.Collapsed;
                            ShowControl = _destinationPanelControl;
                        });
                        _eventAggregator.GetEvent<LinkToDestinationPanelEvent>().Publish();
                    Thread.Sleep(1000);
                    if (_destinationPanelControl.ViewModel._allFloors.Count == 1)
                    {
                        //单楼层直接触发哌替
                        string fid = _destinationPanelControl.ViewModel._allFloors[0].Id;
                        await _destinationPanelControl.ViewModel.HandleElevatorAsync(fid);
                        _logger.LogWarning($"===>单楼层直接触发哌替！");
                    }

                }
                else
                {
                    //本地鉴权
                    //判断是否有权限 
                    var passRightService = ContainerHelper.Resolve<IPassRightService>();
                    var passRight = await passRightService.GetBySignAsync(handleElevator.HandleElevatorRight.PassRightSign, handleElevator.AccessType);
                    if (passRight != null)
                    {
                        _logger.LogInformation($"刷卡通行权限：passRight:{JsonConvert.SerializeObject(passRight, JsonUtil.JsonPrintSettings)} ");
                    }

                    //一体机可去楼层
                    var floorService = ContainerHelper.Resolve<IFloorService>();
                    var floorModels = await floorService.GetAllByElevatorGroupIdAsync(_configHelper.LocalConfig.ElevatorGroupId);
                    var floorTuple = FloorViewModel.ToModels(floorModels, passRight);

                    //无权限
                    if (floorTuple.PassableFloors.FirstOrDefault() == null)
                    {
                        _eventAggregator.GetEvent<WarnTipEvent>().Publish("抱歉，您无可去楼层权限！");
                        _eventAggregator.GetEvent<NotRightEvent>().Publish();
                        return;
                    }
                    _logger.LogInformation($"floorTuple{JsonConvert.SerializeObject(floorTuple)}");
                    //直接派梯，只直接派有权限的楼层，不直接派公共楼层
                    if (floorTuple.PassableFloors.Count == 1 && floorTuple.RightFloors.FirstOrDefault() != null)
                    {
                        var floor = floorTuple.RightFloors.FirstOrDefault();

                        handleElevator.DestinationFloorId = floor.Id;
                        handleElevator.DestinationFloorName = floor.Name;
                        handleElevator.HandleElevatorDeviceId = _configHelper.LocalConfig.HandleElevatorDeviceId;
                        //handleElevator.SourceFloorId = _configHelper.LocalConfig.DeviceFloorId;
                        handleElevator.Sign = handleElevator.HandleElevatorRight?.PassRightSign;

                        _eventAggregator.GetEvent<HandledElevatorEvent>().Publish(handleElevator);

                        await _hubHelper.HandleElevatorAsync(handleElevator);
                    }
                    //目的层选择器
                    else
                    {
                        //初始化楼层选择页面
                        await Application.Current.Dispatcher.Invoke(async () =>
                        {
                            await _destinationPanelControl.ViewModel.InitAsync(handleElevator, floorTuple.AllFloors);
                            _welcomeControl.Visibility = Visibility.Collapsed;
                            ShowControl = _destinationPanelControl;
                        });
                        _eventAggregator.GetEvent<LinkToDestinationPanelEvent>().Publish();
                    }
                }

            }
            catch (Exception ex)
            {
               
                _eventAggregator.GetEvent<WarnTipEvent>().Publish($"{(string)Data["message"]}");
                _eventAggregator.GetEvent<NotRightEvent>().Publish(); 
            }
            
        }

        private void OneCome()
        {
            _welcomeControl.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 回到首页
        /// </summary>
        private void LinkToHome()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_faceRecognitionAppSettings.FaceAuxiliaryType == FaceRecognitionTypeEnum.ArcFree22.Value)
                {
                    ShowControl = _arcFreeFaceDistinguishControl;
                    Task.Run(async () =>
                    {
                        await Task.Delay((int)(_appSettings.FaceRestartSecondTime * 1000));
                        _arcFreeFaceDistinguishControl.IsCanCheck = true;
                    });
                }
                else if (_faceRecognitionAppSettings.FaceAuxiliaryType == FaceRecognitionTypeEnum.ArcPro40.Value)
                {
                    ShowControl = _arcProFaceDistinguishControl;
                    Task.Run(async () =>
                    {
                        await Task.Delay((int)(_appSettings.FaceRestartSecondTime * 1000));
                        _arcProFaceDistinguishControl.IsCanCheck = true;
                    });
                }
                else
                {
                    throw new System.Exception($"找不到人脸识别类型：faceAuxiliaryType:{_faceRecognitionAppSettings.FaceAuxiliaryType} ");
                }

            });

            ////延迟两秒启动摄像头 
            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    _faceDistinguishControl.rgbVideoSource.Start();
            //});
        }

        /// <summary>
        /// 使用卡号开始派梯
        /// </summary>
        /// <param name="cardNumber">卡号，用作参数，防止异步操作提前删除</param>
        /// <returns></returns>
        internal async void CardNumberPassAsync(string cardNumber)
        {
            //防止重复提交
            if (string.IsNullOrEmpty(cardNumber))
            {
                _logger.LogWarning($"卡号为空！");
                return;
            }

            var handleElevator = new UnitHandleElevatorModel();
            handleElevator.AccessType = AccessTypeEnum.IC_CARD.Value;
            handleElevator.DeviceType = CardDeviceTypeEnum.IC.Value;
            handleElevator.HandleElevatorDeviceId = _configHelper.LocalConfig.HandleElevatorDeviceId;
            //handleElevator.SourceFloorId = _configHelper.LocalConfig.DeviceFloorId;
            handleElevator.Sign = cardNumber;

            //派梯设备Id不存二次派梯一体机，上传数据时从服务端获取--设备ID直接传二次派梯一体机
            handleElevator.DeviceId = _configHelper.LocalConfig.HandleElevatorDeviceId;
            //通行卡权限
            handleElevator.HandleElevatorRight = new UnitHandleElevatorRightModel();
            handleElevator.HandleElevatorRight.PassRightSign = cardNumber;

            _eventAggregator.GetEvent<CardInputedEvent>().Publish(handleElevator);

            await Task.CompletedTask;
        }

        /// <summary>
        /// 键盘输入，读卡器为USB读卡器，监听键盘输入，获取USB读卡器的值
        /// </summary>
        /// <param name="e"></param>
        internal void AddCardNumberKey(System.Windows.Forms.KeyEventArgs e)
        {
            //_logger.LogInformation($"输入字符：{e.KeyCode} ");
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                //异步 通行
                _logger.LogInformation($"输入卡号：{_cardNumber} ");
                string Scard = _cardNumber;
                _cardNumber = string.Empty;
                try
                {
                    CardNumberPassAsync(Scard);
                }
                catch(Exception ex)
                {
                    _logger.LogInformation($"输入卡号ERR：{ex} ");
                }
                
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
            else if (e.KeyCode == System.Windows.Forms.Keys.Oemcomma || e.KeyCode == System.Windows.Forms.Keys.Oemcomma)
            {
                _cardNumber += ",";
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.F5)
            {
                //先清空本地人脸图。
                //_faceFactory.ClearFaces();
                _hubHelper.InitDataAsync(true);
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.F6)
            {
                //从服务器获取数据到本地
                _hubHelper.DeleteDataAsync();
            }
            if (_cardNumber.Length >7)
            {
                _endTime = HelperTools.TimeSpanHelp.Timestamp();
            }
            
            _logger.LogInformation($"Number：{_cardNumber} ");
        }

        public async void InitOrderAsync()
        {
            //////初始化Arc加载动态链接库并初始化引擎
            //FaceFactory.MainFaceProvider = ContainerHelper.Resolve<IFaceProvider>();
            //FaceFactory.MainFaceProvider.FaceEngine = _arcFaceHelper.InitFaceEngine();

            await _faceFactoryProxy.InitAsync();
            //初始化服务器连接
            //_hubHelper.StartAsync();   //App 启动实现

            //初始化读卡器设备,获取完服务器数据后才能初始化读卡器，否则数据不正确
            // 纯疑问 2021-07-10
            var serialDeviceDeviceService = ContainerHelper.Resolve<ISerialDeviceService>();
            var appSettings = ContainerHelper.Resolve<AppSettings>();
            await serialDeviceDeviceService.InitQrCodeDeviceAsync();
     
            //await serialDeviceDeviceService.InitAllCardDeviceAsync();
            //初始化配置二维码读卡器

            //初始化日立串口
            string ElevatorType = AppConfigurtaionServices.Configuration["ElevatorType"].ToString().Trim();
            if (ElevatorType == "1")
            {
                var HIpatchComm = ContainerHelper.Resolve<ScommModel>();
                HIpatchComm.HipatchStart();
            }
            //启动http服务
            //_nettyServerHost = ContainerHelper.Resolve<INettyServerHost>(); App 启动实现
            //await _nettyServerHost.RunAsync(_appSettings.Port + 1);

            //初始化 WS socket 数据通道
            //await _WsSocket.WebSocketInit();//金地还原版本
            _eventAggregator.GetEvent<WarnTipEvent>().Publish("欢迎使用");
            _eventAggregator.GetEvent<NotRightEvent>().Publish();
        }

        internal async Task EndAsync()
        {
            _faceFactoryProxy.Close();
            if (_nettyServerHost != null)
            {
                await _nettyServerHost.CloseAsync();
            }
        }

        public WelcomeControl WelcomeControl
        {
            get
            {
                return _welcomeControl;
            }

            set
            {
                SetProperty(ref _welcomeControl, value);
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