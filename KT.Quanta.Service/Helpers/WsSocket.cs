using Coldairarrow.DotNettySocket;
using KT.Common.WpfApp.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HelperTools;
using ContralServer.CfgFileRead;

namespace KT.Quanta.WebApi.Common.WsSocket
{
    public class WsSocket
    {
        public  IWebSocketServer theServer;
        public DisbSend _DisbSend;
        public List<DisbSend> ListDisbSend;
        public Wsmodel _Wsmodel;
        public List<Wsmodel> _ListWsmodel;
        private ILogger<WsSocket> _logger;
        public WsSocket(ILogger<WsSocket> _Logger)
        {
            _logger = _Logger;           
        }
        public async void WsSocket_Init()
        {
            await WebSocket();            
            _ListWsmodel = new List<Wsmodel>();
            _DisbSend = new DisbSend();
            ListDisbSend = new List<DisbSend>();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 20;
            timer.Elapsed += RunQueue;
            timer.Enabled = true;
            timer.Start();
        }
        private async void RunQueue(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                dynamic JsonObj = new DynamicObj();
                DisbSend Req = new DisbSend();
                if (ListDisbSend.Count > 0)
                {
                    Req = ListDisbSend[0];
                    ListDisbSend.RemoveAt(0);
                    //_logger.LogInformation($"队列 WS=>{JsonConvert.SerializeObject(Req)}");
                    switch (Req.sourceReq)
                    {
                        case "Elevator_CardDeviceController":
                            if (Req.Mothed == "AddOrEditCardDevice")
                            {                                
                                JsonObj.Mothed = Req.Mothed;
                                JsonObj.data = Req.data;
                                JsonObj.sourceReq = Req.sourceReq;
                                await Req.ConnectionId.Send(JsonConvert.SerializeObject(JsonObj._values));
                            }
                            else if (Req.Mothed == "DeleteCardDevice")
                            {
                                JsonObj.Mothed = Req.Mothed;
                                JsonObj.id = Req.id;
                                JsonObj.time = Req.time;
                                JsonObj.sourceReq = Req.sourceReq;
                                await Req.ConnectionId.Send(JsonConvert.SerializeObject(JsonObj._values));                                
                            }
                            break;
                        case "Elevator_PassRightController":
                            if (Req.Mothed == "AddOrEditPassRight")
                            {
                                JsonObj.Mothed = Req.Mothed;
                                JsonObj.data = Req.data;
                                JsonObj.sourceReq = Req.sourceReq;
                                await Req.ConnectionId.Send(JsonConvert.SerializeObject(JsonObj._values));
                                //await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("AddOrEditPassRight", Req.data);
                            }
                            else if (Req.Mothed == "DeletePassRight")
                            {
                                JsonObj.Mothed = Req.Mothed;
                                JsonObj.id = Req.id;
                                JsonObj.time = Req.time;
                                JsonObj.sourceReq = Req.sourceReq;
                                await Req.ConnectionId.Send(JsonConvert.SerializeObject(JsonObj._values));
                                //await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("DeletePassRight", Req.id, Req.time);
                            }
                            break;
                        case "HandleElevatorDeviceController":
                            if (Req.Mothed== "AddOrEditHandleElevatorDevice")
                            {
                                JsonObj.Mothed = Req.Mothed;
                                JsonObj.data = Req.data;
                                JsonObj.sourceReq = Req.sourceReq;
                                await Req.ConnectionId.Send(JsonConvert.SerializeObject(JsonObj._values));
                            }
                            else if (Req.Mothed == "DeleteHandleElevatorDevice")
                            {
                                JsonObj.Mothed = Req.Mothed;
                                JsonObj.id = Req.id;                             
                                JsonObj.sourceReq = Req.sourceReq;
                                await Req.ConnectionId.Send(JsonConvert.SerializeObject(JsonObj._values));
                            }
                            break;
                        case "Elevator_PassRightController_Qutan":
                            if (Req.Mothed == "AddOrEditPassRight")
                            {
                                JsonObj.Mothed = Req.Mothed;
                                JsonObj.data = Req.data;
                                JsonObj.sourceReq = Req.sourceReq;
                                await Req.ConnectionId.Send(JsonConvert.SerializeObject(JsonObj._values));
                                //await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("AddOrEditPassRight", Req.data);
                            }
                            else if (Req.Mothed == "DeletePassRight")
                            {
                                JsonObj.Mothed = Req.Mothed;
                                JsonObj.id = Req.id;
                                JsonObj.time = Req.time;
                                JsonObj.sourceReq = Req.sourceReq;
                                await Req.ConnectionId.Send(JsonConvert.SerializeObject(JsonObj._values));
                                //await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("DeletePassRight", Req.id, Req.time);
                            }
                            break;
                        case "TurnstileCardDeviceController":
                            if (Req.Mothed == "AddOrEditCardDevice")
                            {
                                JsonObj.Mothed = Req.Mothed;
                                JsonObj.data = Req.data;
                                JsonObj.sourceReq = Req.sourceReq;
                                await Req.ConnectionId.Send(JsonConvert.SerializeObject(JsonObj._values));
                                //await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("AddOrEditCardDevice", Req.data);
                            }
                            else if (Req.Mothed == "DeleteCardDevice")
                            {
                                JsonObj.Mothed = Req.Mothed;
                                JsonObj.id = Req.id;
                                JsonObj.time = Req.time;
                                JsonObj.sourceReq = Req.sourceReq;
                                await Req.ConnectionId.Send(JsonConvert.SerializeObject(JsonObj._values));
                                //await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("DeleteCardDevice", Req.id, Req.time);
                            }
                            break;
                        case "TurnstileCardDeviceRightGroupService":
                            if (Req.Mothed == "AddOrEditRightGroup")
                            {
                                JsonObj.Mothed = Req.Mothed;
                                JsonObj.data = Req.data;
                                JsonObj.sourceReq = Req.sourceReq;
                                await Req.ConnectionId.Send(JsonConvert.SerializeObject(JsonObj._values));
                                //await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("AddOrEditRightGroup", Req.data);
                            }
                            else if (Req.Mothed == "DeleteRightGroup")
                            {
                                JsonObj.Mothed = Req.Mothed;
                                JsonObj.id = Req.id;
                                JsonObj.time = Req.time;
                                JsonObj.sourceReq = Req.sourceReq;
                                await Req.ConnectionId.Send(JsonConvert.SerializeObject(JsonObj._values));
                                //await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("DeleteRightGroup", Req.id, Req.time);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public async Task WebSocket()
        {
            try
            {
                int Port = Convert.ToInt32(AppConfigurtaionServices.Configuration["AppSettings:Port"].ToString().Trim()) - 200;
                theServer = await SocketBuilderFactory.GetWebSocketServerBuilder(Port)
                    .OnConnectionClose((server, connection) =>
                    {
                        _ListWsmodel.RemoveAll(x => x.ConnectId == connection.ConnectionId);
                        _logger.LogInformation($"连接关闭,连接名[{connection.ConnectionId}],当前连接数:{server.GetConnectionCount()}");
                    })
                    .OnException(ex =>
                    {
                        _logger.LogInformation($"服务端异常:{ex.Message}");
                    })
                    .OnNewConnection((server, connection) =>
                    {
                        connection.ConnectionName = $"名字{connection.ConnectionId}";
                        _Wsmodel = new Wsmodel();
                        _Wsmodel.ConnectId = connection.ConnectionId;
                        _ListWsmodel.Add(_Wsmodel);
                        //向客户端请求终端信息  当前握手状态，不能发帧信息
                        /*dynamic JsonObj = new DynamicObj();
                        JsonObj.Mothed = "ReqGetClientModel";
                        JsonObj.data = string.Empty;
                        JsonObj.sourceReq = "ServerRequest";
                        connection.Send(JsonConvert.SerializeObject(JsonObj._values));
                        _logger.LogInformation($"请求终端信息:{connection.ConnectionName}--{JsonConvert.SerializeObject(JsonObj._values)}");*/
                    })
                    .OnRecieve((server, connection, msg) =>
                    {
                        try
                        {
                            JObject Obj = JObject.Parse(msg);
                            switch ((string)Obj["Mothed"])
                            {
                                case "ReturnClientModel":
                                    _ListWsmodel.RemoveAll(x => x.ConnectId == connection.ConnectionId);
                                    Wsmodel Ws = (Wsmodel)JsonConvert.DeserializeObject(Obj["Data"].ToString(), typeof(Wsmodel));                                  
                                    Ws.ConnectId = connection.ConnectionId;
                                    _ListWsmodel.Add(Ws);
                                    
                                    _logger.LogInformation($"ReturnClientModel:{JsonConvert.SerializeObject(_ListWsmodel)}");
                                    break;
                                case "RequestData":
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation($"ReturnClientModel:{JsonConvert.SerializeObject(_ListWsmodel)} msg={msg}");
                        }
                        //_logger.LogInformation($"服务端 OnRecieve:数据{msg}");
                        //connection.Send($"服务端 OnRecieve:数据{msg}  client={connection.ConnectionId}");
                        //SeverTOClient(connection,msg);
                    })
                    .OnSend((server, connection, msg) =>
                    {
                        //Console.WriteLine($"向连接名[{connection.ConnectionName}]发送数据:{msg}");
                    })
                    .OnServerStarted(server =>
                    {
                        _logger.LogInformation("WS-Socket服务启动");

                    }).BuildAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"WS-Socket start err :{ex.Message}");
            }
            //Console.ReadLine();
        }
        /// <summary>
        public async void SeverTOClient(IWebSocketConnection Client,string Msg)
        {
           await Client.Send(Msg);
        }
        public class DisbSend
        {
            public IWebSocketConnection ConnectionId { get; set; }  
            public string Mothed { get; set; }
            public object data { get; set; } = new object();
            public string id { get; set; }
            public long time { get; set; }
            public string sourceReq { get; set; }
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
