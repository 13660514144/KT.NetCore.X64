
using HelperTools;
using KT.Common.WpfApp.Helpers;
using KT.Turnstile.Unit.ClientApp.Service.Helpers;
using KT.Turnstile.Unit.ClientApp.Service.IServices;
using KT.Turnstile.Unit.Entity.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Events;
using Prism.Ioc;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;

namespace KT.Turnstile.Unit.ClientApp.WsScoket
{
    public class WebSocket4net
    {
        public WebSocket _webSocket=null;        
        private ILogger _logger;
        private IEventAggregator _eventAggregator;
        private ConfigHelper _configHelper;
        private IContainerProvider _containerProvider;
        public WebSocket4net()
        {
            _logger = ContainerHelper.Resolve<ILogger>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _containerProvider = ContainerHelper.Resolve<IContainerProvider>();
        }     
        public async Task WebSocketInit()
        {
            string Ip = AppConfigurtaionServices.Configuration["AppSettings:ServerIp"].ToString().Trim();
            int Port = Convert.ToInt32(AppConfigurtaionServices.Configuration["AppSettings:ServerPort"].ToString().Trim())-200;
            string Url = $"ws://{Ip}:{Port}";
            _webSocket = new WebSocket(Url);
            _webSocket.Opened += WebSocket_Opened;
            _webSocket.Error += WebSocket_Error;
            _webSocket.Closed += WebSocket_Closed;
            _webSocket.MessageReceived += WebSocket_MessageReceived;
            bool Flg = await Start();
        }
        /// <summary>
        /// 连接方法
        /// <returns></returns>
        public async Task<bool> Start()
        {
            bool result = true;
            try
            {
                _webSocket.Open();
                _logger.LogInformation($"WebSocket OPEN");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"WebSocket Err:{ex.Message}");
                result = false;
            }
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 5000;
            timer.Elapsed += RConnection;
            timer.Enabled = true;
            timer.Start();
            return result;
        }
        private void RConnection(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (_webSocket.State != WebSocket4Net.WebSocketState.Open &&
                    _webSocket.State != WebSocket4Net.WebSocketState.Connecting)
                {
                    _webSocket.Close();
                    _webSocket.Open();
                    _logger.LogInformation("正在重连.....");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
            }
        }

        /// <summary>
        /// Socket关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebSocket_Closed(object sender, EventArgs e)
        {
            _logger.LogInformation("Socket关闭事件");
        }
        /// <summary>
        /// Socket报错事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebSocket_Error(object sender, ErrorEventArgs e)
        {
            _logger.LogInformation($"websocket_err={e.Exception.Message}");
        }
        /// <summary>
        /// Socket打开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebSocket_Opened(object sender, EventArgs e)
        {
            try
            {
                _logger.LogInformation("websocket_Opened");
                SendClientMsg();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"ws socket err:{ex.Message}");
            }
        }
        private void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
           //_logger.LogInformation($"客户端:收到数据:{e.Message}");
            DataChange(e.Message);
        }
        public async Task DataChange(string ServerData)
        {
            try
            {
                JObject O = JObject.Parse(ServerData);
                switch ((string)O["sourceReq"])
                {
                    case "TurnstileCardDeviceRightGroupService":
                        if ((string)O["Mothed"] == "AddOrEditRightGroup")
                        {
                            TurnstileUnitRightGroupEntity U = (TurnstileUnitRightGroupEntity)JsonConvert.DeserializeObject(O["data"].ToString(), typeof(TurnstileUnitRightGroupEntity));
                            var service = _containerProvider.Resolve<IRightGroupService>();
                            await service.AddOrUpdateAsync(U);
                            _logger.LogInformation($"客户端:=>TurnstileCardDeviceRightGroupService=>AddOrEditRightGroup");
                        }
                        else if ((string)O["Mothed"] == "DeleteRightGroup")
                        {
                            var service = _containerProvider.Resolve<IRightGroupService>();
                            await service.DeleteAsync((string)O["id"], (long)O["time"]);
                            _logger.LogInformation($"客户端:=>Elevator_CardDeviceController=>DeleteCardDevice");
                        }

                        break;
                    case "TurnstileCardDeviceController":
                        if ((string)O["Mothed"] == "AddOrEditCardDevice")
                        {
                            TurnstileUnitCardDeviceEntity U = (TurnstileUnitCardDeviceEntity)JsonConvert.DeserializeObject(O["data"].ToString(), typeof(TurnstileUnitCardDeviceEntity));
                            var service = _containerProvider.Resolve<ICardDeviceService>();
                            await service.AddOrUpdateAsync(U);
                            _logger.LogInformation($"客户端:=>TurnstileCardDeviceController=>AddOrEditCardDevice");
                        }
                        else if ((string)O["Mothed"] == "DeleteCardDevice")
                        {
                            var service = _containerProvider.Resolve<ICardDeviceService>();
                            await service.DeleteAsync((string)O["id"], (long)O["time"]);
                            _logger.LogInformation($"客户端:=>TurnstileCardDeviceController=>DeleteCardDevice");
                        }
                        break;
                    case "Elevator_PassRightController_Qutan":
                        if ((string)O["Mothed"] == "AddOrEditPassRight")
                        {
                            TurnstileUnitPassRightEntity U = (TurnstileUnitPassRightEntity)JsonConvert.DeserializeObject(O["data"].ToString(), typeof(TurnstileUnitPassRightEntity));
                            var service = _containerProvider.Resolve<IPassRightService>();
                            await service.AddOrUpdateAsync(U);
                            _logger.LogInformation($"客户端:=>Elevator_PassRightController_Qutan=>AddOrEditPassRight");
                        }
                        else if ((string)O["Mothed"] == "DeletePassRight")
                        {
                            var service = _containerProvider.Resolve<IPassRightService>();
                            await service.Delete((string)O["id"], (long)O["time"]);
                            _logger.LogInformation($"客户端:=>Elevator_PassRightController_Qutan=>DeletePassRight");
                        }
                        break;
                    
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"WS 客户端:ERROR:{ex.Message}");
            }
        }

        private void SendClientMsg()
        {
            Wsmodel ws = new Wsmodel();
            ws.ConnectId = string.Empty;
            ws.ClientIp = AppConfigurtaionServices.Configuration["AppSettings:StartWithIp"].ToString();
            ws.ClientPort = AppConfigurtaionServices.Configuration["AppSettings:Port"].ToString(); ;
            ws.DeviceType = AppConfigurtaionServices.Configuration["AppSettings:DeviceType"].ToString();
            dynamic JsonObj = new DynamicObj();
            JsonObj.Mothed = "ReturnClientModel";
            JsonObj.Data = ws;
            _logger.LogInformation($"上传终端信息：{JsonConvert.SerializeObject(JsonObj._values)}");
            _webSocket.Send(JsonConvert.SerializeObject(JsonObj._values));
        }
        public class Wsmodel
        {
            public string ConnectId { get; set; }
            public string ClientIp { get; set; }
            public string ClientPort { get; set; }
            public string DeviceType { get; set; }
        }
    }
}
