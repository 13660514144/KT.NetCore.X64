using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.Hubs;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Elevator.Unit.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KT.Quanta.Service.Elevator.Dtos;
using KT.Quanta.Service.Helpers;
using Microsoft.Extensions.Options;
using HelperTools;
using ContralServer.CfgFileRead;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KT.Quanta.Service.Devices.Common;
using System.Threading;
using KT.Quanta.WebApi.Common.Helper;
using KT.Quanta.Service.Handlers;

namespace KT.Quanta.WebApi.Controllers.Elevator
{
    /// <summary>
    /// 电梯服务
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ElevatorController : ControllerBase
    {
        private readonly ILogger<ElevatorController> _logger;
        private IHandleElevatorDeviceService _handleElevatorDeviceService;
        private IHubContext<QuantaDistributeHub> _distributeHub;
        private PushRecordHandler _pushRecordHanlder;
        private readonly FloorHandleElevatorResponseList _floorHandleElevatorResponseList;
        private readonly AppSettings _appSettings;
        private string Dbconn = AppConfigurtaionServices.Configuration["ConnectionStrings:MysqlConnection"].ToString().Trim();
        private string IFdispatch= AppConfigurtaionServices.Configuration["IFdispatch"].ToString().Trim();
        private string ElevatorType = AppConfigurtaionServices.Configuration["ElevatorType"].ToString().Trim();
        private RemoteDeviceList _remoteDeviceList;
        private ElevatorMessageKey _ElevatorMessageKey;
        private int _Sleep;
        private int _ElevatorPort;
        private int _ForStep;
        public ElevatorController(ILogger<ElevatorController> logger,
            IHandleElevatorDeviceService handleElevatorDeviceService,
            IHubContext<QuantaDistributeHub> distributeHub,
            FloorHandleElevatorResponseList floorHandleElevatorResponseList,
            IOptions<AppSettings> appSettings,
            RemoteDeviceList remoteDeviceList, ElevatorMessageKey elevatorMessageKey,
            PushRecordHandler pushRecordHanlder)
        {
            _logger = logger;
            _handleElevatorDeviceService = handleElevatorDeviceService;
            _distributeHub = distributeHub;
            _pushRecordHanlder = pushRecordHanlder;
            _floorHandleElevatorResponseList = floorHandleElevatorResponseList;
            _appSettings = appSettings.Value;
            _remoteDeviceList = remoteDeviceList;
            _ElevatorMessageKey = elevatorMessageKey;
            _Sleep=Convert.ToInt32(AppConfigurtaionServices.Configuration["Sleep"].ToString());
            _ElevatorPort = Convert.ToInt32(AppConfigurtaionServices.Configuration["ElevatorPort"].ToString());
            _ForStep = Convert.ToInt32(AppConfigurtaionServices.Configuration["ForStep"].ToString());
        }

        /// <summary>
        /// 第三方派梯，梯号回显,放进队列，等API轮询
        /// </summary>
        /// <returns></returns>
        [HttpPost("QueElevatorMsg")]
        public string QueElevatorMsg([FromBody] dynamic Dobj)
        {
            string Result=string.Empty;
            try
            {
                JObject O = JObject.Parse($"{Dobj}");
                _logger.LogInformation($"API梯号返回==>{Dobj}");
                for (int x = 0; x < _ElevatorMessageKey.ListKey.Count; x++)
                {
                    if (_ElevatorMessageKey.ListKey[x].Key == O["Key"].ToString())
                    {
                        _ElevatorMessageKey.ListKey[x].ElevatorName = O["ElevatorName"].ToString();
                        _ElevatorMessageKey.ListKey[x].ReturnKey = O["Key"].ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"API 回显错误{ex}");
            }
            long Sutc = DateTimeUtil.UtcNowMillis() / 1000;
            int Len = _ElevatorMessageKey.ListKey.Count-1;
            //删除2秒前的队列
            for (int x = Len; x >=0; x--)
            {
                if (Sutc - _ElevatorMessageKey.ListKey[x].UtcTime >= _Sleep)
                {
                    _ElevatorMessageKey.ListKey.RemoveAt(x);
                }
            }
            return Result;
        }
        /// <summary>
        /// 第三方通过API派梯
        /// </summary>
        /// <returns>电梯服务信息</returns>
        [HttpPost("handle")]
        public async Task<DataResponse<FloorHandleElevatorSuccessModel>> HandleAsync(
            UintRightHandleElevatorRequestModel model)
        {

            _logger.LogError($"\r\n==>>三方API 收到派梯求--HandleAsync{JsonConvert.SerializeObject(model)}\r\nmodel floorid={model.DestinationFloorId}");
            IDalMySql dal;
            DataTable Db;            
            if (ElevatorType == "2" || ElevatorType == "3") //服务端派梯
            {
                bool isResult = false;
                FloorHandleElevatorSuccessModel result = null;
                var messageKey = IdUtil.NewId();
                var action = new Action<FloorHandleElevatorSuccessModel>((handleResult) =>
                {
                    isResult = true;
                    result = handleResult;
                });

                //等待派梯结果
                _floorHandleElevatorResponseList.Add(messageKey, action);
                string PersonId = string.Empty;
                string[] TypeAcc = model.AccessType.ToString().Split(',');
                if (TypeAcc[0] == "FACE")
                //if (model.AccessType == "FACE")
                {
                    
                    try
                    {
                        dal = DalFactory.CreateMysqlDal("mysql", Dbconn);
                        string Sql = $@"
                        SELECT a.id,a.PersonId
                        FROM pass_right a  where a.sign='{model.Sign}'                	 
                        ";
                        _logger.LogInformation($"\r\nSQL===>{Sql}");
                        Db = dal.MySqlListData(Sql);
                        PersonId = Db.Rows[0]["PersonId"].ToString();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"\r\n==>>SQL ERR{ex}");
                    }
                }
                if (TypeAcc.Length > 1)
                {
                    model.AccessType = TypeAcc[1];
                }
                else
                {
                    model.AccessType = TypeAcc[0];
                }
                //派梯
                await _handleElevatorDeviceService.RightHandleElevator(model, messageKey, PersonId);

                //等待电梯结果返回
                int times = 0;
                int TotalTimes = _appSettings.SocketReturnTimeOutMillis / 50;
                while (!isResult)
                {
                    if (times >= TotalTimes)
                    {
                        break;
                    }
                    times++;
                    await Task.Delay(50);
                }
                if (result == null)
                {
                    throw new Exception("派梯结果未返回！");
                }
                return DataResponse<FloorHandleElevatorSuccessModel>.Ok(result);
            }
            else  //客户端派梯 HTTP API模式
            {
                dynamic Robj = new DynamicObj();
                bool isResult = false;
                FloorHandleElevatorSuccessModel F = new FloorHandleElevatorSuccessModel()
                {
                    ElevatorName = string.Empty,
                    FloorName = string.Empty
                };
                //需要根据派梯设备ID 找到对应边缘处理器ID
                try
                {
                    dal = DalFactory.CreateMysqlDal("mysql", Dbconn);
                    string Sql = $@"
                        SELECT a.id as CardDeviceId,b.DeviceType as CardDeviceType,
                                '{model.AccessType}' as AccessType,b.id  as HandleElevatorDeviceId,
                               b.CommBox,IFNULL(a.FloorId,0) as SourceFloorId,a.IpAddress,a.Port
                        FROM processor a,handle_elevator_device b  where 
                            b.id={model.HandleElevatorDeviceId}
                            and a.id=b.ProcessorId   LIMIT 1                   	 
                        ";
                    string Sql1 = $@"
                        SELECT  c.Name,'{model.Sign}' as Sign,a.PersonId,
                            b.RealFloorId,A.FloorId
                        FROM pass_right a
                        inner join elevator_group_floor b on a.FloorId=b.FloorId
                        inner join floor c on b.FloorId=c.id 
                        where a.Sign='{model.Sign}' and RightType='ELEVATOR'
                         LIMIT 1
                        ";
                    _logger.LogInformation($"\r\nSQL===>{Sql}");
                    _logger.LogInformation($"\r\nSQL===>{Sql1}");
                    Db = dal.MySqlListData(Sql);
                    DataTable Db1 = dal.MySqlListData(Sql1);
                    if (Db.Rows.Count > 0 && Db1.Rows.Count>0)
                    {
                        F.FloorName= Db1.Rows[0]["Name"].ToString();
                        if (Db.Rows[0]["SourceFloorId"].ToString() == Db1.Rows[0]["FloorId"].ToString())
                        {
                            _logger.LogError($"源楼层和目的楼层相同");                                                                             
                            var actions = new Action<FloorHandleElevatorSuccessModel>((handleResult) =>
                            {
                                isResult = true;
                                F = handleResult;
                            });
                            return DataResponse<FloorHandleElevatorSuccessModel>.Result(500, F, $"源楼层和目的楼层相同");
                        }
                        List<IRemoteDevice> remoteDevices = await _remoteDeviceList.GetByIpAndPortAsync($"{Db.Rows[0]["IpAddress"].ToString().Trim()}:{Db.Rows[0]["Port"].ToString().Trim()}");
                        if (remoteDevices == null)
                        {
                            _logger.LogError($"找不到派梯的设备：SeekSocket:{JsonConvert.SerializeObject(Db, JsonUtil.JsonPrintSettings)}");                                                 
                            var actions = new Action<FloorHandleElevatorSuccessModel>((handleResult) =>
                            {
                                isResult = true;
                                F = handleResult;
                            });
                            return DataResponse<FloorHandleElevatorSuccessModel>.Result(500, F, $"找不到派梯的设备");
                        }
                        _logger.LogInformation($"remoteDevices 查询结果{JsonConvert.SerializeObject(remoteDevices)}");
                        lock (this)
                        {
                            string ConnectionId = remoteDevices[0].CommunicateDevices[0].CommunicateDeviceInfo.ConnectionId;
                            //通知边缘处理器派梯 重新绑定数据
                            var messageKey = IdUtil.NewId();
                            Dictionary<string, string> Floor = new Dictionary<string, string>()
                            {
                                { "CardDeviceId",Db.Rows[0]["CardDeviceId"].ToString()},
                                { "CardDeviceType",Db.Rows[0]["CardDeviceType"].ToString()},
                                { "AccessType",model.AccessType},
                                { "PassRightSign",model.AccessType=="FACE"?Db1.Rows[0]["PersonId"].ToString():model.Sign},
                                { "HandleElevatorDeviceId",Db.Rows[0]["HandleElevatorDeviceId"].ToString()},
                                { "Remark",Db1.Rows[0]["Name"].ToString()},
                                { "CommBox",Db.Rows[0]["CommBox"].ToString()},
                                { "RealFloorId",Db1.Rows[0]["RealFloorId"].ToString()},
                                { "SourceFloorId",Db.Rows[0]["SourceFloorId"].ToString()},
                                { "MessageKey",messageKey.ToString()}
                            };
                            //走signalr 通道
                            //_distributeHub.Clients.Client(ConnectionId).SendAsync("OtherApiRequest", Floor);
                            //走API
                            string IP = Db.Rows[0]["IpAddress"].ToString();
                            string Uri = $"http://{IP}:{_ElevatorPort}/api/ElevatorControl/OtherApiRequest";
                            //开线程请求边缘处理器派梯
                            Task.Run(() =>
                            {
                                RequestElvator(Uri,Floor);
                            });
                            //异步上传派梯记录
                            Task.Run(() => {
                                 ApipassrecordAsync(Floor);
                            });
                            //增加Floor队列
                            long Sutc = DateTimeUtil.UtcNowMillis() / 1000;
                            _ElevatorMessageKey.MsgInfo.FloorName = F.FloorName;
                            _ElevatorMessageKey.MsgInfo.Key = messageKey.ToString();
                            _ElevatorMessageKey.MsgInfo.UtcTime = Sutc;
                            _ElevatorMessageKey.ListKey.Add(_ElevatorMessageKey.MsgInfo);
                            //等待2秒查询队列MessgKey,3秒后返回空
                            int Len = _ElevatorMessageKey.ListKey.Count ;
                            for (int x = 0; x < _ForStep; x++)
                            {                                
                                for (int y = 0; y < Len; y++)
                                {
                                    if (_ElevatorMessageKey.ListKey[y].ReturnKey==messageKey)
                                    {
                                        //有结果返回                                        
                                        F.ElevatorName = _ElevatorMessageKey.ListKey[y].ElevatorName;                                       
                                        _ElevatorMessageKey.ListKey.RemoveAt(y);                                     
                                        var actions = new Action<FloorHandleElevatorSuccessModel>((handleResult) =>
                                        {
                                            isResult = true;
                                            F = handleResult;
                                        });
                                        return DataResponse<FloorHandleElevatorSuccessModel>.Result(200, F, $"返回梯号");
                                    }
                                }
                                Thread.Sleep(100);
                            }
                            for (int y = 0; y < Len; y++)
                            {
                                if (_ElevatorMessageKey.ListKey[y].Key == messageKey)
                                {
                                    //无结果返回删除
                                    _ElevatorMessageKey.ListKey.RemoveAt(y);
                                    break;                                 
                                }
                            }                            
                            var action = new Action<FloorHandleElevatorSuccessModel>((handleResult) =>
                            {
                                isResult = true;
                                F = handleResult;
                            });
                            return DataResponse<FloorHandleElevatorSuccessModel>.Result(500, F, $"超时无返回");
                        }
                    }
                    else
                    {
                        _logger.LogError($"找不到边缘处理器");                        
                        var action = new Action<FloorHandleElevatorSuccessModel>((handleResult) =>
                        {
                            isResult = true;
                            F = handleResult;
                        });
                        return DataResponse<FloorHandleElevatorSuccessModel>.Result(500,F, "找不到边缘处理器");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"\r\n==>>SQL ERR{ex}");                                    
                    var action = new Action<FloorHandleElevatorSuccessModel>((handleResult) =>
                    {
                        isResult = true;
                        F = handleResult;
                    });
                    return DataResponse<FloorHandleElevatorSuccessModel>.Result(500, F, $"发生错误==>{ex}");
                }
            }
            //return null;
        }
        /// <summary>
        /// 提交边缘处理器派梯
        /// </summary>
        /// <param name="Uri"></param>
        /// <param name="Floor"></param>
        public async Task RequestElvator(string Uri, Dictionary<string,string> Floor)
        {
            try
            {
                HostReqModel HostReq = new HostReqModel();
                var Result = HostReq.SendHttpRequest($"{Uri}", JsonConvert.SerializeObject(Floor));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"API派梯请求错误：{ex}");
            }
        }
        /// <summary>
        /// 格式化返回值 
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Message"></param>
        /// <param name="F"></param>
        /// <returns></returns>
        public dynamic Respon(string Code,string Message, FloorHandleElevatorSuccessModel F)
        {
            dynamic Robj = new DynamicObj();
            Robj.Code = Code;
            Robj.Message = Message;
            Robj.Data = F;
            return Robj;
        }
        /// <summary>
        /// API派梯记录上传
        /// </summary>
        /// <param name="floor"></param>
        public async Task ApipassrecordAsync(Dictionary<string,string> floor)
        {
            try
            {
                PassRecordModel Precord = new PassRecordModel()
                {
                    Extra= floor["CardDeviceId"].ToString(),
                    DeviceType= floor["CardDeviceType"].ToString(),
                    AccessType= floor["AccessType"].ToString(),
                    PassRightSign= floor["PassRightSign"].ToString(),
                    DeviceId= floor["HandleElevatorDeviceId"].ToString(),
                    PassTime= DateTimeUtil.UtcNowMillis(),
                    PassLocalTime = DateTimeUtil.UtcNowMillis().ToZoneDateTimeByMillis()?.ToSecondString(),
                    Remark = floor["Remark"].ToString()
                };
                bool s = await _pushRecordHanlder.StartPushAsync(Precord);
                _logger.LogInformation($"派梯上传:{JsonConvert.SerializeObject(Precord)}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"派梯上传{ex}");
            }
        }
    }
}
