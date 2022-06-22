using HelperTools;
using KT.Common.WpfApp.Helpers;
using KT.Device.Unit.CardReaders.Datas;
using KT.Device.Unit.CardReaders.Events;
using KT.Device.Unit.CardReaders.Models;
using KT.Quanta.Common.Models;
using KT.Turnstile.Unit.ClientApp.Events;
using KT.Turnstile.Unit.ClientApp.Service.Helpers;
using KT.Turnstile.Unit.ClientApp.Service.IServices;
using Microsoft.Extensions.Logging;
using Prism.Events;
using Prism.Mvvm;
using System.Linq;
using System.Windows.Controls;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Turnstile.Unit.ClientApp.Device.IDevices;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;
using KT.Turnstile.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace KT.Turnstile.Unit.ClientApp.Views
{
    public class MainWindowViewModel : BindableBase
    {
        private string _cardNumber = string.Empty;

        private WelcomeControl _welcomeControl;
        private SuccessShowControl _successShowControl;
        private UserControl showControl;
        private HubHelper _hubHelper;
        private ConfigHelper _configHelper;
        private IEventAggregator _eventAggregator;
        private ILogger _logger;
        private AppSettings _appSettings;
        private string Controller = AppConfigurtaionServices.Configuration["RightController"].ToString().Trim();
        private string IfApiOrSql = AppConfigurtaionServices.Configuration["IfApiOrSql"].ToString().Trim();
        
        public MainWindowViewModel()
        {
            _welcomeControl = ContainerHelper.Resolve<WelcomeControl>();
            _successShowControl = ContainerHelper.Resolve<SuccessShowControl>();

            _hubHelper = ContainerHelper.Resolve<HubHelper>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            ShowControl = _welcomeControl;
            
            //派梯操作，显示派梯结果页面
            _eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Subscribe(HandledElevator);
            _eventAggregator.GetEvent<PassDisplayEvent>().Subscribe(PassShow);
            //返回首页
            _eventAggregator.GetEvent<LinkToHomeEvent>().Subscribe(LinkToHome);
            //初始化日立串口
            //string ElevatorType = AppConfigurtaionServices.Configuration["ElevatorType"].ToString().Trim();
            //if (ElevatorType == "1")
            //{
            //    var HIpatchComm = ContainerHelper.Resolve<ScommModel>();
            //    HIpatchComm.HipatchStart();
            //}
            
        }

        private void PassShow(PassDisplayModel obj)
        {
            ShowControl = _successShowControl;
        }

        private void HandledElevator(HandleElevatorDisplayModel obj)
        {
            ShowControl = _successShowControl;
        }

        private void LinkToHome()
        {
            ShowControl = _welcomeControl;
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
                string _cARD = _cardNumber;
                _cardNumber = string.Empty;
                CardNumberPassAsync(_cARD);                
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
                InitDataAsync();
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.F6)
            {
                //从服务器获取数据到本地
                _hubHelper.DeleteDataAsync();
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.F7)
            {
                //从服务器获取数据到本地
                _hubHelper.InitRightAsync(false, true);
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.F9)
            {
                //Test
                var bx5nTest = ContainerHelper.Resolve<IQscsBx5nCardDeviceAnalyze>();
                var bytes1 = new byte[] { 36, 36, 50, 51, 51, 49, 54, 51 };
                bx5nTest.Analyze("COM1", bytes1);
                var bytes2 = new byte[] { 55, 51, 48, 35, 35 };
                bx5nTest.Analyze("COM1", bytes2);

            }
            else
            {
                _logger.LogWarning("输入无效字符：{0} ", e.KeyCode);
            }

            _logger.LogInformation($"输入字符结果：{_cardNumber} ");
        }

        private async void InitDataAsync()
        {
            //从服务器获取数据到本地
            await _hubHelper.InitDataAsync(true);
        }

        private async void CardNumberPassAsync(string cardNumber)
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
            _logger.LogInformation($"模拟键盘Object：{JsonConvert.SerializeObject(data)} ");
            _eventAggregator.GetEvent<CardDeviceAnalyzedEvent>().Publish(data);
            
        }
        /// <summary>
        /// 在线权限通行 2021-06-27
        /// </summary>
        /// 
        public async void PassAndTransArray(JArray Tb, PassDisplayModel passShow, string AccessType)
        {
            TurnstileUnitCardDeviceEntity datamodel = new TurnstileUnitCardDeviceEntity
            {
                BrandModel = Tb[0]["BrandModel"].ToString(),
                DeviceType = Tb[0]["CardDeviceType"].ToString(),
                RelayCommunicateType = Tb[0]["CommunicateType"].ToString(),
                RelayDeviceIp = Tb[0]["IpAddress"].ToString(),
                RelayDevicePort = Convert.ToInt32(Tb[0]["Port"].ToString()),
                RelayDeviceOut = Convert.ToInt16(Tb[0]["RelayDeviceOut"].ToString())
            };
            _logger.LogInformation($"\r\n===>键盘通行:{JsonConvert.SerializeObject(passShow)}");
            if (datamodel.RelayDeviceOut.ToString() == "2")
            {
                passShow.DisplayType = PassDisplayTypeEnum.Enter.Value;
                _eventAggregator.GetEvent<PassDisplayEvent>().Publish(passShow);               
            }
            if (datamodel.RelayDeviceOut.ToString() == "1")
            {
                passShow.DisplayType = PassDisplayTypeEnum.Leave.Value;
                _eventAggregator.GetEvent<PassDisplayEvent>().Publish(passShow);
            }
            //发布开门命令
            _eventAggregator.GetEvent<CardGetMysqlDataEvent>().Publish(datamodel);



            TurnstileUnitPassRecordEntity passRecord = new TurnstileUnitPassRecordEntity();
            passRecord.PassRightId = Tb[0]["PassRightId"].ToString();
            passRecord.CreatedTime = passRecord.EditedTime = passRecord.PassTime = DateTimeUtil.UtcNowMillis();

            /// 设备ID，除了人脸摄像头、门禁读卡器是第三方系统的设备ID，其余都系本系统的设备ID
            passRecord.DeviceId = Tb[0]["CardDeviceId"].ToString();
            ///// 扩展字段，在人脸摄像头时，该字段是人脸服务器的token
            //passRecord.Extra = string.Empty;
            /// 设备类型
            passRecord.DeviceType = Tb[0]["CardDeviceType"].ToString();
            /// 通行类型
            passRecord.CardType = AccessType;// Tb.Rows[0]["AccessType"].ToString(); 
            /// 通行码，IC卡、二维码、人脸ID
            passRecord.CardNumber = Tb[0]["Sign"].ToString();
            /// 通行时间，2019-11-06 15:20:45 
            passRecord.PassLocalTime = passRecord.PassTime.ToZoneDateTimeByMillis()?.ToSecondString();
            try
            {
                await _hubHelper.PushPassRecordAsync(passRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError("通行记录上传数据错误：{0} ", ex);
            }
        }
        public async void PassAndTrans(DataTable Tb, PassDisplayModel passShow,string AccessType)
        {
            TurnstileUnitCardDeviceEntity datamodel = new TurnstileUnitCardDeviceEntity
            {
                BrandModel = Tb.Rows[0]["BrandModel"].ToString(),
                DeviceType = Tb.Rows[0]["CardDeviceType"].ToString(),
                RelayCommunicateType = Tb.Rows[0]["CommunicateType"].ToString(),
                RelayDeviceIp = Tb.Rows[0]["IpAddress"].ToString(),
                RelayDevicePort = Convert.ToInt32(Tb.Rows[0]["Port"].ToString()),
                RelayDeviceOut = Convert.ToInt16(Tb.Rows[0]["RelayDeviceOut"].ToString())
            };
            _logger.LogInformation($"\r\n===>通行:{JsonConvert.SerializeObject(passShow)}");
            passShow.DisplayType = PassDisplayTypeEnum.Enter.Value;
            _eventAggregator.GetEvent<PassDisplayEvent>().Publish(passShow);
            //发布开门命令
            _eventAggregator.GetEvent<CardGetMysqlDataEvent>().Publish(datamodel);
            

            TurnstileUnitPassRecordEntity passRecord = new TurnstileUnitPassRecordEntity();
            passRecord.PassRightId = Tb.Rows[0]["PassRightId"].ToString();
            passRecord.CreatedTime = passRecord.EditedTime = passRecord.PassTime = DateTimeUtil.UtcNowMillis();

            /// 设备ID，除了人脸摄像头、门禁读卡器是第三方系统的设备ID，其余都系本系统的设备ID
            passRecord.DeviceId = Tb.Rows[0]["CardDeviceId"].ToString();
            ///// 扩展字段，在人脸摄像头时，该字段是人脸服务器的token
            //passRecord.Extra = string.Empty;
            /// 设备类型
            passRecord.DeviceType = Tb.Rows[0]["carddevicetype"].ToString();
            /// 通行类型
            passRecord.CardType = AccessType;// Tb.Rows[0]["AccessType"].ToString(); 
            /// 通行码，IC卡、二维码、人脸ID
            passRecord.CardNumber = Tb.Rows[0]["Sign"].ToString(); 
            /// 通行时间，2019-11-06 15:20:45 
            passRecord.PassLocalTime = passRecord.PassTime.ToZoneDateTimeByMillis()?.ToSecondString();
            try
            {
                await _hubHelper.PushPassRecordAsync(passRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError("通行记录上传数据错误：{0} ", ex);                
            }
        }
        public UserControl ShowControl
        {
            get
            {
                return showControl;
            }

            set
            {
                SetProperty(ref showControl, value);
            }
        }

    }
}
