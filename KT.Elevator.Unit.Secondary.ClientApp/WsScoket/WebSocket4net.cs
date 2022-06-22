using ContralServer.CfgFileRead;
using HelperTools;
using KT.Common.WpfApp.Helpers;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Secondary.ClientApp.Events;
using KT.Elevator.Unit.Secondary.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Events;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;

namespace KT.Elevator.Unit.Secondary.ClientApp.WsScoket
{
    public class WebSocket4net
    {
        public WebSocket4Net.WebSocket _webSocket =null;        
        private ILogger _logger;
        private IEventAggregator _eventAggregator;
        private ConfigHelper _configHelper;
        public WebSocket4net()
        {
            _logger = ContainerHelper.Resolve<ILogger>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
        }     
        public async Task WebSocketInit()
        {
            try
            {
                string Ip = AppConfigurtaionServices.Configuration["AppSettings:ServerIp"].ToString().Trim();
                int Port = Convert.ToInt32(AppConfigurtaionServices.Configuration["AppSettings:ServerPort"].ToString().Trim()) - 200;
                string Url = $"ws://{Ip}:{Port}";
                _logger.LogInformation($"URL={Url}");
                _webSocket = new WebSocket4Net.WebSocket(Url);
                _webSocket.Opened += WebSocket_Opened;
                _webSocket.Error += WebSocket_Error;
                _webSocket.Closed += WebSocket_Closed;
                _webSocket.MessageReceived += WebSocket_MessageReceived;
                bool Flg = await Start();
            }
            catch (Exception ex)
            { 
            }
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
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = 5000;
                timer.Elapsed += RConnection;
                timer.Enabled = true;
                timer.Start();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"WebSocket Err:{ex.Message}");
                result = false;
            }
          
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
            _logger.LogInformation($"客户端:收到数据:{e.Message}");
            DataChange(e.Message);
        }
        public async Task DataChange(string ServerData)
        {
            try
            {
                JObject O = JObject.Parse(ServerData);
                switch ((string)O["sourceReq"])
                {
                    case "Elevator_CardDeviceController":
                        if ((string)O["Mothed"] == "AddOrEditCardDevice")
                        {
                            UnitCardDeviceEntity U = (UnitCardDeviceEntity)JsonConvert.DeserializeObject(O["data"].ToString(), typeof(UnitCardDeviceEntity));
                            var service = ContainerHelper.Resolve<ICardDeviceService>();
                            await service.AddOrUpdateAsync(U);
                            _logger.LogInformation($"客户端:=>Elevator_CardDeviceController=>AddOrEditCardDevice");
                        }
                        else if ((string)O["Mothed"] == "DeleteCardDevice")
                        {
                            var service = ContainerHelper.Resolve<ICardDeviceService>();
                            await service.Delete((string)O["id"], (long)O["time"]);
                            _logger.LogInformation($"客户端:=>Elevator_CardDeviceController=>DeleteCardDevice");
                        }

                        break;
                    case "Elevator_PassRightController":
                        if ((string)O["Mothed"] == "AddOrEditPassRight")
                        {
                            UnitPassRightEntity U = (UnitPassRightEntity)JsonConvert.DeserializeObject(O["data"].ToString(), typeof(UnitPassRightEntity));
                            var service = ContainerHelper.Resolve<IPassRightService>();
                            var passRight = await service.AddOrUpdateAsync(U);
                            _eventAggregator.GetEvent<AddOrEditPassRightsEvent>().Publish(new List<UnitPassRightEntity>() { passRight });
                            _logger.LogInformation($"客户端:=>Elevator_PassRightController=>AddOrEditPassRight");
                        }
                        else if ((string)O["Mothed"] == "DeletePassRight")
                        {
                            var service = ContainerHelper.Resolve<IPassRightService>();
                            await service.Delete((string)O["id"], (long)O["time"]);
                            _eventAggregator.GetEvent<DeletePassRightEvent>().Publish((string)O["id"]);
                            _logger.LogInformation($"客户端:=>Elevator_PassRightController=>DeletePassRight");
                        }
                        break;
                    /*case "Elevator_PassRightController_Qutan":
                        if ((string)O["Mothed"] == "AddOrEditPassRight")
                        {
                            UnitPassRightEntity U = (UnitPassRightEntity)JsonConvert.DeserializeObject(O["data"].ToString(), typeof(UnitPassRightEntity));
                            var service = ContainerHelper.Resolve<IPassRightService>();
                            var passRight = await service.AddOrUpdateAsync(U);
                            _eventAggregator.GetEvent<AddOrEditPassRightsEvent>().Publish(new List<UnitPassRightEntity>() { passRight });
                            _logger.LogInformation($"客户端:=>Elevator_PassRightController_Qutan=>AddOrEditPassRight");
                        }
                        else if ((string)O["Mothed"] == "DeletePassRight")
                        {
                            var service = ContainerHelper.Resolve<IPassRightService>();
                            await service.Delete((string)O["id"], (long)O["time"]);
                            _eventAggregator.GetEvent<DeletePassRightEvent>().Publish((string)O["id"]);
                            _logger.LogInformation($"客户端:=>Elevator_PassRightController_Qutan=>DeletePassRight");
                        }
                        break;*/
                    case "HandleElevatorDeviceController":
                        if ((string)O["Mothed"] == "AddOrEditHandleElevatorDevice")
                        {
                            UnitHandleElevatorDeviceModel U = (UnitHandleElevatorDeviceModel)JsonConvert.DeserializeObject(O["data"].ToString(), typeof(UnitHandleElevatorDeviceModel));
                            var service = ContainerHelper.Resolve<IHandleElevatorDeviceService>();
                            _configHelper.LocalConfig = await service.AddOrEditAsync(U, _configHelper.LocalConfig);
                            _logger.LogInformation($"客户端:=>HandleElevatorDeviceController=>AddOrEditHandleElevatorDevice");
                        }
                        else if ((string)O["Mothed"] == "DeleteHandleElevatorDevice")
                        {
                            var service = ContainerHelper.Resolve<IHandleElevatorDeviceService>();
                            await service.DeleteAsync((string)O["id"]);
                            _logger.LogInformation($"客户端:=>HandleElevatorDeviceController=>DeleteHandleElevatorDevice");
                        }
                        break;
                    case "ServerRequest":
                        if ((string)O["Mothed"] == "ReqGetClientModel")
                        {
                            /*Wsmodel ws = new Wsmodel();
                            ws.ConnectId = string.Empty;
                            ws.ClientIp = AppConfigurtaionServices.Configuration["AppSettings:StartWithIp"].ToString(); 
                            ws.ClientPort = AppConfigurtaionServices.Configuration["AppSettings:Port"].ToString(); ;
                            ws.DeviceType = AppConfigurtaionServices.Configuration["AppSettings:DeviceType"].ToString();
                            dynamic JsonObj = new DynamicObj();
                            JsonObj.Mothed = "ReturnClientModel";
                            JsonObj.Data = ws;
                            _logger.LogInformation($"上传终端信息：{JsonConvert.SerializeObject(JsonObj._values)}");
                            _webSocket.Send(JsonConvert.SerializeObject(JsonObj._values));
                            */
                            
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
