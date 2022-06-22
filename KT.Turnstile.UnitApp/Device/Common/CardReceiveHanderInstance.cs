
using HelperTools;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Device;
using KT.Device.Unit.CardReaders.Events;
using KT.Device.Unit.CardReaders.Models;
using KT.Quanta.Common.Enums;
using KT.Quanta.Common.Models;
using KT.Turnstile.Unit.ClientApp.Device.IDevices;
using KT.Turnstile.Unit.ClientApp.Events;
using KT.Turnstile.Unit.ClientApp.Service.Helpers;
using KT.Turnstile.Unit.ClientApp.Service.IServices;
using KT.Turnstile.Unit.Entity.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Data;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using KT.Elevator.Unit.Entity.Models;

namespace KT.Turnstile.Unit.ClientApp.Device.Handlers
{
    /// <summary>
    /// 接收到卡号操作
    /// </summary>
    public class CardReceiveHanderInstance
    {
        private ILogger _logger;
        private AppSettings _appSettings;
        private IContainerProvider _containerProvider;
        private SocketDeviceList _socketDeviceList;
        private HubHelper _hubHelper;
        private IDeviceList _deviceList;
        private IEventAggregator _eventAggregator;
        
        public CardReceiveHanderInstance()
        {
            _logger = ContainerHelper.Resolve<ILogger>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _containerProvider = ContainerHelper.Resolve<IContainerProvider>();
            _socketDeviceList = ContainerHelper.Resolve<SocketDeviceList>();
            _hubHelper = ContainerHelper.Resolve<HubHelper>();
            _deviceList = ContainerHelper.Resolve<IDeviceList>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

        }
        public Task StartAsync()
        {            
            _eventAggregator.GetEvent<CardDeviceAnalyzedEvent>().Subscribe(CardDeviceAnalyzedAsync);
            //订阅从数据库获取的闸机开门命令
            _eventAggregator.GetEvent<CardGetMysqlDataEvent>().Subscribe(CardGetMysqlData);
            return Task.CompletedTask;
        }
        /// <summary>
        /// 数据库取值--闸机授权方法
        /// </summary>
        /// <param name="data"></param>
        private async void CardGetMysqlData(TurnstileUnitCardDeviceEntity data)
        {
            //开门
            await PassOperateAsync(data);
        }
        /// <summary>
        /// QR-IC入口  2021-11-01  变更 signlar 入口
        /// </summary>
        /// <param name="data"></param>
        private async void CardDeviceAnalyzedAsync(CardDeviceAnalyzedModel data)        
        {
            _logger.LogInformation($"QR-IC读入原始数据：{JsonConvert.SerializeObject(data)}");
            
                      
            string Controller = AppConfigurtaionServices.Configuration["RightController"].ToString().Trim();
            string IfApiOrSql = AppConfigurtaionServices.Configuration["IfApiOrSql"].ToString().Trim();
            DataTable Db = new DataTable();
            JObject JsonData;
            JArray Tb = new JArray();
            string Result = string.Empty;
            string Req= string.Empty;
            var passShow = new PassDisplayModel();
            passShow.AccessType = data.CardReceive.AccessType;
            passShow.Time = DateTimeUtil.UtcNowMillis();
            passShow.Sign = data.CardReceive.CardNumber;
            if (IfApiOrSql == "API")//API
            {
                _logger.LogInformation("SIGNLAR");
                string Str = JsonConvert.SerializeObject(data.CardDeviceInfo);
                JObject Obj = JObject.Parse(Str);
                
                //SIGNLAR 通道
                ReqElvator O = new ReqElvator
                {

                    Ip = AppConfigurtaionServices.Configuration["AppSettings:StartWithIp"].ToString().Trim(),
                    Sign = data.CardReceive.CardNumber,
                    Port = AppConfigurtaionServices.Configuration["AppSettings:Port"].ToString().Trim(),
                    CardId = Obj["Id"].ToString(),
                    PassFlg = AppConfigurtaionServices.Configuration["PassFlg"].ToString().Trim(),
                    AccessType = data.CardReceive.AccessType
                };

                _logger.LogInformation($"PassFlg={AppConfigurtaionServices.Configuration["PassFlg"].ToString().Trim()}");
                try
                {
                    //SIGNLAR 通道
                    Req = await _hubHelper.ReqElevatorPassright(O);
                    JsonData = JObject.Parse(Req);
                    _logger.LogInformation($"SIGNLAR ==>{JsonConvert.SerializeObject(JsonData)}");
                    if ((string)JsonData["Flg"] == "0")
                    {
                        passShow.DisplayType = PassDisplayTypeEnum.NoRight.Value;
                        _eventAggregator.GetEvent<PassDisplayEvent>().Publish(passShow);
                        return;
                    }
                    Tb = (JArray)JsonData["Data"];
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"API请求错误==>{ex}");
                    dynamic JsonObj = new DynamicObj();
                    JsonObj.code = "000";
                    JsonObj.Flg = "0";
                    JsonObj.Message = "API请求错误";
                    JsonObj.Data = new JArray();
                    passShow.DisplayType = PassDisplayTypeEnum.API.Value;
                    _eventAggregator.GetEvent<PassDisplayEvent>().Publish(passShow);
                    return;
                }
                bool SerTypeFlg = false;
                for (int x = 0; x < Tb.Count; x++)
                {
                    if (Tb[x]["RightType"].ToString().Trim().ToUpper() == "TURNSTILE")
                    {
                        SerTypeFlg = true;
                        break;
                    }
                }
                if (SerTypeFlg == false)
                {
                    _logger.LogError("\r\n==>>找不到任何通行设备");
                    passShow.DisplayType = PassDisplayTypeEnum.NoRight.Value;
                    _eventAggregator.GetEvent<PassDisplayEvent>().Publish(passShow);
                    return;
                }
                _logger.LogInformation($"\r\n Tb Array===>{JsonConvert.SerializeObject(Tb)}");
                PassAndTransArray(Tb, passShow, data);
            }
            else    //本地鉴权
            {
                if (data.CardDeviceInfo is TurnstileUnitCardDeviceEntity unitCardDevice)
                {
                    await ReceivedAsync(unitCardDevice, data.CardReceive);
                }
                else
                {
                    _logger.LogError($"接收数据类型错误：type:{data.CardDeviceInfo?.GetType()?.FullName} need:{typeof(TurnstileUnitCardDeviceEntity).FullName} ");
                }
            }
        }
        /// <summary>
        /// 在线权限通行 2021-06-27
        /// </summary>
        public async void PassAndTransArray(JArray Tb, 
            PassDisplayModel passShow, CardDeviceAnalyzedModel data)
        {
            int Num = Tb.Count;
            int Flg = 0;
            string Card = JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings);
            JObject Obj = JObject.Parse(Card);
            string ElevatorType=AppConfigurtaionServices.Configuration["ElevatorType"].ToString().Trim();
            string IFdispatch = AppConfigurtaionServices.Configuration["IFdispatch"].ToString().Trim();
            TurnstileUnitCardDeviceEntity datamodel = new TurnstileUnitCardDeviceEntity
            {
                BrandModel = (Num>1)?Tb[1]["BrandModel"].ToString(): Tb[0]["BrandModel"].ToString(),
                DeviceType = (Num > 1) ? Tb[1]["CardDeviceType"].ToString():Tb[0]["CardDeviceType"].ToString(),
                RelayCommunicateType = (Num > 1) ? Tb[1]["CommunicateType"].ToString(): Tb[0]["CommunicateType"].ToString(),
                RelayDeviceIp = (Num > 1) ? Tb[1]["IpAddress"].ToString(): Tb[0]["IpAddress"].ToString(),
                RelayDevicePort = Convert.ToInt32((Num > 1) ? Tb[1]["Port"].ToString(): Tb[0]["Port"].ToString()),
                RelayDeviceOut = Convert.ToInt16((Num > 1) ? Tb[1]["RelayDeviceOut"].ToString(): Tb[0]["RelayDeviceOut"].ToString())
            };
            _logger.LogInformation($"\r\n===>通行:{JsonConvert.SerializeObject(passShow)}");
            if (Tb[Flg]["HandleElevatorDeviceId"].ToString().Trim() == "0")//没有派梯设备是离开
            {
                passShow.DisplayType = PassDisplayTypeEnum.Leave.Value;
                _eventAggregator.GetEvent<PassDisplayEvent>().Publish(passShow);
            }
            else
            {
                passShow.DisplayType = PassDisplayTypeEnum.Enter.Value;
                _eventAggregator.GetEvent<PassDisplayEvent>().Publish(passShow);
            }        
            //发布开门命令
            try
            {         
                await PassOperateAsync(datamodel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"\r\n==>>通讯指令 Err  {ex}");                
            }
            /*同时派梯  日立本地派梯 其他由服务端派*/
            if (Tb[0]["srcid"].ToString() == Tb[0]["DesFloorId"].ToString())
            {
                _logger.LogError("\r\n==>>源楼层和目的楼层相同，不派梯");
            }
            else
            {
                if (Tb[0]["HandleElevatorDeviceId"].ToString().Trim() != "0"
                    && Tb[0]["DesFloorId"].ToString().Trim() != "0")//进门派提
                {
                    if (ElevatorType == "1")//客户端派梯
                    {                        
                        Task.Run(() =>
                        {
                            var messageKey = IdUtil.NewId();
                            Dictionary<string, string> Floor = new Dictionary<string, string>()
                            {
                                { "CardDeviceId",Tb[1]["CardDeviceId"].ToString()},
                                { "CardDeviceType",Tb[1]["CardDeviceType"].ToString()},
                                { "AccessType",data.CardReceive.AccessType},
                                { "PassRightSign",data.CardReceive.AccessType=="FACE"?Tb[0]["PersonId"].ToString():Tb[0]["Sign"].ToString()},
                                { "HandleElevatorDeviceId",Tb[0]["HandleElevatorDeviceId"].ToString()},
                                { "Remark",Tb[0]["Name"].ToString()},
                                { "CommBox",Tb[0]["CommBox"].ToString()},
                                { "RealFloorId",Tb[0]["RealFloorId"].ToString()},
                                { "SourceFloorId",Tb[0]["srcid"].ToString()},
                                { "MessageKey",messageKey.ToString()}
                            };
                            _logger.LogInformation($"Floor:{JsonConvert.SerializeObject(Floor)}");
                            string Uri = "http://127.0.0.1:22361/api/ElevatorControl/LocalApiRequest";
                            HostReqModel HostReq = new HostReqModel();
                            string Result = HostReq.SendHttpRequest($"{Uri}", JsonConvert.SerializeObject(Floor));
                            JObject Disp = JObject.Parse(Result);
                            HandleElevatorDisplayModel _DisplayModel = new HandleElevatorDisplayModel();
                            _DisplayModel.DestinationFloorName = Disp["FloorName"].ToString();
                            _DisplayModel.ElevatorName = Disp["ElevatorName"].ToString();
                            _eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Publish(_DisplayModel);
                            string Msg = $"\r\n==>派梯成功==>{JsonConvert.SerializeObject(Disp)}";
                            _logger.LogInformation(Msg);

                            //DisElevitor.FloorDispElevitor(Tb,data.CardReceive.AccessType);
                        });

                    }
                    else//服务端API派梯
                    {
                        //API提交
                        string IfSingR = AppConfigurtaionServices.Configuration["IfSingR"].ToString().Trim();
                        _logger.LogInformation($"IfSingR{IfSingR}");
                        #region  走sign 1
                        if (IfSingR == "0")
                        {
                            Dictionary<string, string> SendData = new Dictionary<string, string>
                            {
                                {"Sign", Tb[0]["Sign"].ToString()},
                                {"AccessType", $"{Tb[0]["AccessType"].ToString()},{data.CardReceive.AccessType}"},
                                {"DeviceType", Tb[0]["DeviceType"].ToString() },
                                { "DeviceId", Tb[0]["CardDeviceId"].ToString()},
                                { "DestinationFloorId",Tb[0]["DesFloorId"].ToString()},
                                { "floorId",Tb[0]["DesFloorId"].ToString() },
                                { "HandleElevatorDeviceId",Tb[0]["HandleElevatorDeviceId"].ToString()},
                                { "CommBox",""}
                            };
                            _logger.LogError($"dict==>{JsonConvert.SerializeObject(SendData)}");
                            string Controller = "elevator/handle";
                            Task.Run(() =>
                            {
                                //Dispatch Api = new Dispatch();
                                var Api = ContainerHelper.Resolve<Dispatch>();
                                var Req = Api.ApiGetData(Controller, SendData);
                            });
                            #endregion
                        }
                        else
                        {
                            UnitHandleElevatorModel _handleElevator = new UnitHandleElevatorModel();
                            _handleElevator.HandleElevatorRight = new UnitHandleElevatorRightModel();
                            _handleElevator.HandleElevatorRight.PassRightId = null;
                            _handleElevator.HandleElevatorRight.PassRightSign = Tb[0]["Sign"].ToString();
                            _handleElevator.Sign = Tb[0]["Sign"].ToString();
                            _handleElevator.AccessType = Tb[0]["AccessType"].ToString();
                            _handleElevator.DeviceType = Tb[0]["DeviceType"].ToString();
                            _handleElevator.DeviceId = Tb[0]["HandleElevatorDeviceId"].ToString();
                            _handleElevator.HandleElevatorDeviceId = Tb[0]["HandleElevatorDeviceId"].ToString();
                            _handleElevator.DestinationFloorId = Tb[0]["DesFloorId"].ToString();
                            _handleElevator.SourceFloorId = Tb[0]["srcid"].ToString();
                            _handleElevator.DestinationFloorName = Tb[0]["Name"].ToString();

                            //singr方法
                            _eventAggregator.GetEvent<HandledElevatorEvent>().Publish(_handleElevator);
                            await _hubHelper.HandleElevatorAsync(_handleElevator);
                        }
                    }
                }
            }
            /*同时派梯*/
            _logger.LogError("通行记录上传");
            
            TurnstileUnitPassRecordEntity passRecord = new TurnstileUnitPassRecordEntity();
            passRecord.PassRightId = (Num > 1) ? Tb[1]["PassRightId"].ToString(): Tb[0]["PassRightId"].ToString();
            passRecord.CreatedTime = passRecord.EditedTime = passRecord.PassTime = DateTimeUtil.UtcNowMillis();

            /// 设备ID，除了人脸摄像头、门禁读卡器是第三方系统的设备ID，其余都系本系统的设备ID
            passRecord.DeviceId = (Num > 1) ? Tb[1]["CardDeviceId"].ToString(): Tb[0]["CardDeviceId"].ToString();
            ///// 扩展字段，在人脸摄像头时，该字段是人脸服务器的token
            //passRecord.Extra = string.Empty;
            /// 设备类型
            passRecord.DeviceType = (Num > 1) ? Tb[1]["CardDeviceType"].ToString():Tb[0]["CardDeviceType"].ToString();
            /// 通行类型
            passRecord.CardType = data.CardReceive.AccessType;//(Num > 1) ? Tb[1]["AccessType"].ToString():Tb[0]["AccessType"].ToString(); 
            /// 通行码，IC卡、二维码、人脸ID
            string Face = (Num > 1) ? Tb[1]["AccessType"].ToString().Trim() : Tb[0]["AccessType"].ToString().Trim();
            if (Face == "FACE")
            {
                passRecord.CardNumber = (Num > 1) ? Tb[1]["PersonId"].ToString(): Tb[0]["PersonId"].ToString();
            }
            else
            {
                passRecord.CardNumber = (Num > 1) ? Tb[1]["Sign"].ToString(): Tb[0]["Sign"].ToString();
            }
            /// 通行时间，2019-11-06 15:20:45 
            passRecord.PassLocalTime = passRecord.PassTime.ToZoneDateTimeByMillis()?.ToSecondString();
            await PushAsync(passRecord);
        }
        public async void PassAndTrans(DataTable Tb, PassDisplayModel passShow, CardDeviceAnalyzedModel data)
        {
            int Flg = 0;
            string Card =JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings);
            JObject Obj = JObject.Parse(Card);
            for (int x = 0; x < Tb.Rows.Count; x++)
            {
                if (Obj["cardDeviceInfo"]["portName"].ToString().ToUpper() == Tb.Rows[x]["PortName"].ToString().ToUpper())
                {
                    Flg = x;
                    _logger.LogInformation($"\r\n找到设备ID for x=>{Obj["cardDeviceInfo"]["portName"]}：{Tb.Rows[x]["CardDeviceId"]}");
                    break;
                }
            }
            TurnstileUnitCardDeviceEntity datamodel = new TurnstileUnitCardDeviceEntity
            {
                BrandModel = Tb.Rows[Flg]["BrandModel"].ToString(),
                DeviceType = Tb.Rows[Flg]["CardDeviceType"].ToString(),
                RelayCommunicateType = Tb.Rows[Flg]["CommunicateType"].ToString(),
                RelayDeviceIp = Tb.Rows[Flg]["IpAddress"].ToString(),
                RelayDevicePort = Convert.ToInt32(Tb.Rows[Flg]["Port"].ToString()),
                RelayDeviceOut = Convert.ToInt16(Tb.Rows[Flg]["RelayDeviceOut"].ToString())
            };
            _logger.LogInformation($"\r\n===>通行:{JsonConvert.SerializeObject(passShow)}");
            passShow.DisplayType = PassDisplayTypeEnum.Enter.Value;
            _eventAggregator.GetEvent<PassDisplayEvent>().Publish(passShow);
            //发布开门命令
            try
            {                
                await PassOperateAsync(datamodel);
            }
            catch(Exception ex)
            {
                _logger.LogError("\r\n==>>通讯指令 Err  {ex}");
            }

            _logger.LogError("通行记录上传");
            TurnstileUnitPassRecordEntity passRecord = new TurnstileUnitPassRecordEntity();
            passRecord.PassRightId = Tb.Rows[Flg]["PassRightId"].ToString();
            passRecord.CreatedTime = passRecord.EditedTime = passRecord.PassTime = DateTimeUtil.UtcNowMillis();

            /// 设备ID，除了人脸摄像头、门禁读卡器是第三方系统的设备ID，其余都系本系统的设备ID
            passRecord.DeviceId = Tb.Rows[Flg]["CardDeviceId"].ToString();
            ///// 扩展字段，在人脸摄像头时，该字段是人脸服务器的token
            //passRecord.Extra = string.Empty;
            /// 设备类型
            passRecord.DeviceType = Tb.Rows[Flg]["carddevicetype"].ToString();
            /// 通行类型
            passRecord.CardType = data.CardReceive.AccessType;// Tb.Rows[0]["AccessType"].ToString();
            /// 通行码，IC卡、二维码、人脸ID
            passRecord.CardNumber = Tb.Rows[Flg]["Sign"].ToString();
            /// 通行时间，2019-11-06 15:20:45 
            passRecord.PassLocalTime = passRecord.PassTime.ToZoneDateTimeByMillis()?.ToSecondString();
            await PushAsync(passRecord);
        }
        private async Task ReceivedAsync(TurnstileUnitCardDeviceEntity cardDevice, CardReceiveModel data)
        {
            if (cardDevice == null)
            {
                _logger.LogError("通行读卡器设备为空！");
                return;
            }

            _logger.LogInformation($"刷卡通行：cardDevice:{JsonConvert.SerializeObject(cardDevice, JsonUtil.JsonPrintSettings)} " +
                $"cardReceive:{ JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

            var passShow = new PassDisplayModel();
            passShow.AccessType = data.AccessType;
            passShow.Time = DateTimeUtil.UtcNowMillis();
            passShow.Sign = data.CardNumber;

            //较验二维码时间
            if (data.IsCheckDate)
            {
                _logger.LogInformation("DateTimeCheck==================================");
                var now = DateTimeUtil.UtcNowSeconds();
                _logger.LogInformation($"DateTimeCheck: now {now} ==================================");
                if (data.StartTime.HasValue && data.StartTime > now)
                {
                    passShow.DisplayType = PassDisplayTypeEnum.NoRight.Value;
                    _eventAggregator.GetEvent<PassDisplayEvent>().Publish(passShow);

                    _logger.LogWarning("二维码起用时间未到：qrEndDate:{0} now:{1} ", data.EndTime, now);
                    return;
                }
                if (data.EndTime.HasValue && data.EndTime < now)
                {
                    passShow.DisplayType = PassDisplayTypeEnum.NoRight.Value;
                    _eventAggregator.GetEvent<PassDisplayEvent>().Publish(passShow);

                    _logger.LogWarning("二维码过期：qrEndDate:{0} now:{1} ", data.EndTime, now);
                    return;
                }
                _logger.LogInformation("DateTimeCheck: end   ==================================");
            }

            var passRightService = _containerProvider.Resolve<IPassRightService>();
            //1、判断是否有权限操作
            var rightRecord = await passRightService.GetByCardNubmerAndCardDeviceId(data.CardNumber, cardDevice.Id);

            //无权限返回
            if (rightRecord == null)
            {
                passShow.DisplayType = PassDisplayTypeEnum.NoRight.Value;
                _eventAggregator.GetEvent<PassDisplayEvent>().Publish(passShow);

                _logger.LogError("无权限通行：cardNumber:{0} cardDeviceId:{1} ", data.CardNumber, cardDevice.Id);
                return;
            }
            _logger.LogInformation("正常通行：cardNumber:{0} cardDeviceId:{1} ", data.CardNumber, cardDevice.Id);
                       
            //通行操作，开门
            await PassOperateAsync(cardDevice);

            _logger.LogInformation($"正常通行完成：timeSpan:{(DateTimeUtil.UtcNowMillis() - passShow.Time) / 1000.0}s ");

            //异步数据上传
            RecordPushAsync(rightRecord, cardDevice, data.AccessType);
        }

        /// <summary>
        /// 通行设备操作
        /// </summary>
        /// <param name="cardDevice">读卡器</param>
        /// <returns></returns>
        private async Task PassOperateAsync(TurnstileUnitCardDeviceEntity cardDevice)
        {
            ISocketDevice relayDevice = await _socketDeviceList.GetOrAddAsync(cardDevice);

            //2、找出继电器
            if (relayDevice == null)
            {
                _logger.LogError("未找到继电器：ip:{0} port:{0} ", cardDevice.RelayDeviceIp, cardDevice.RelayDevicePort);
                return;
            }

            //3、发关开关门指令 只适合泥人继电器[AT+STACH1=1,1]
            string cmd = $"AT+STACH{cardDevice.RelayDeviceOut}={_appSettings.RelayOpenCmd}\r\n";

            //发送指令
            await relayDevice.SendExceAnsyc(cmd);
        }

        private async void RecordPushAsync(TurnstileUnitPassRightEntity rightRecord, TurnstileUnitCardDeviceEntity cardDevice, string accessType)
        {
            TurnstileUnitPassRecordEntity passRecord = new TurnstileUnitPassRecordEntity();
            passRecord.PassRightId = rightRecord.Id;
            passRecord.CreatedTime = passRecord.EditedTime = passRecord.PassTime = DateTimeUtil.UtcNowMillis();

            /// 设备ID，除了人脸摄像头、门禁读卡器是第三方系统的设备ID，其余都系本系统的设备ID
            passRecord.DeviceId = cardDevice.Id;
            ///// 扩展字段，在人脸摄像头时，该字段是人脸服务器的token
            //passRecord.Extra = string.Empty;
            /// 设备类型
            passRecord.DeviceType = cardDevice.DeviceType;
            /// 通行类型
            passRecord.CardType = accessType;
            /// 通行码，IC卡、二维码、人脸ID
            passRecord.CardNumber = rightRecord.CardNumber.ToString();
            /// 通行时间，2019-11-06 15:20:45 
            passRecord.PassLocalTime = passRecord.PassTime.ToZoneDateTimeByMillis()?.ToSecondString();

            await PushAsync(passRecord);
        }

        private async Task PushAsync(TurnstileUnitPassRecordEntity passRecord)
        {
            try
            {
                await _hubHelper.PushPassRecordAsync(passRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError("上传数据错误：{0} ", ex);
                _ = PresencePassRecordAsync(passRecord);
            }
        }

        /// <summary>
        /// 上传记录错误，保存到本地
        /// </summary>
        /// <param name="passRecord"></param>
        /// <returns></returns>
        private async Task PresencePassRecordAsync(TurnstileUnitPassRecordEntity passRecord)
        {
            try
            {
                var passRecordDataService = _containerProvider.Resolve<IPassRecordService>();
                //1、判断是否有权限操作
                await passRecordDataService.AddAsync(passRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError("保存通行记录失败： data:{0} ex:{1} ", JsonConvert.SerializeObject(passRecord, JsonUtil.JsonPrintSettings), ex);
            }
        }
    }
}
