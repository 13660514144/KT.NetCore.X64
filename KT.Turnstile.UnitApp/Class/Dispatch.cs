using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Extensions.Logging;
using KT.Common.WpfApp.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using KT.Elevator.Unit.Entity.Models;
using KT.Common.Core.Utils;
using Prism.Events;
using KT.Turnstile.Unit.ClientApp.Events;

namespace HelperTools
{
    public class Dispatch
    {
        private ILogger _Log;        
        private string Dbconn;
        public int IFdispatch;
        private ScommModel _scommModel;
        public string Uri = AppConfigurtaionServices.Configuration["AppSettings:ServerIp"].ToString();
        public string Port = AppConfigurtaionServices.Configuration["AppSettings:ServerPort"].ToString();
        private IEventAggregator _eventAggregator;
        public Dispatch()
        {
            Dbconn = AppConfigurtaionServices.Configuration["Dbconn"].ToString();
            _Log = ContainerHelper.Resolve<ILogger>();
            _scommModel = ContainerHelper.Resolve<ScommModel>();
            IFdispatch = Convert.ToInt16(AppConfigurtaionServices.Configuration["IFdispatch"].ToString());
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<ApiDictObjectEvent>().Subscribe(ApiDictObject);
        }
        /// <summary>
        /// API请求通行权限
        /// </summary>
        /// <param name="CollerName"></param>
        /// <param name="Obj"></param>
        /// <returns></returns>
        public string ApiGetData(string CollerName, Dictionary<string, string> Obj)
        {            
            string Url = $"http://{Uri}:{Port}/";
            string Result = string.Empty;
            try
            {
                HostReqModel HostReq = new HostReqModel();
                Result =  HostReq.SendHttpRequest($"{Url}{CollerName}", JsonConvert.SerializeObject(Obj));
            }
            catch (Exception ex)
            {
                _Log.LogError($"\r\nAPI 错误 Result==>{JsonConvert.SerializeObject(Result)}");
                _Log.LogError($"\r\nAPI 错误==>{ex}");
                dynamic JsonObj = new DynamicObj();
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = ex.Message.ToString();
                JsonObj.Data = new JArray();
                Result = JsonConvert.SerializeObject(JsonObj._values);
            }
            return Result;
        }
        public string ApiChkVer(string CollerName, Dictionary<string, string> Obj,string URLip,int urlport)
        {
            string Url = $"http://{URLip}:{urlport}/";
            string Result = string.Empty;
            try
            {
                HostReqModel HostReq = new HostReqModel();
                Result = HostReq.SendHttpRequest($"{Url}{CollerName}", JsonConvert.SerializeObject(Obj));
            }
            catch (Exception ex)
            {
                _Log.LogError($"\r\nAPI 错误 Result==>{JsonConvert.SerializeObject(Result)}");
                _Log.LogError($"\r\nAPI 错误==>{ex}");
                dynamic JsonObj = new DynamicObj();
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = ex.Message.ToString();
                JsonObj.Data = new JArray();
                Result = JsonConvert.SerializeObject(JsonObj._values);
            }
            return Result;
        }
        /// <summary>
        /// 由第三方通过API向客户端发起派梯  弃用，统一走API派梯服务
        /// </summary>
        /// <param name="Dict"></param>
        public void ApiDictObject(Dictionary<string, string> Dict)
        {
            if (IFdispatch == 0)
            {
                //配置不需要派梯
                _Log.LogInformation("\r\n==>配置不需要派梯");
                return;
            }
            JObject floor = JObject.Parse(JsonConvert.SerializeObject(Dict));
            try
            {                
                _scommModel.SendPara.Floor = floor["RealFloorId"].ToString();
                _scommModel.SendPara.Sign = floor["PassRightSign"].ToString();
                bool CommExists = false;
                int _CommFlg = 0;
                for (int x = 0; x < _scommModel.ListComms.Count; x++)
                {
                    //找设备对应输出的串口号
                    if (floor["CommBox"].ToString().Trim().ToUpper() == _scommModel.ListComms[x].PortName.ToString().Trim().ToUpper())
                    {
                        _CommFlg = x;
                        CommExists = true;
                        break;                        
                    }
                }
                /*派梯记录*/
                if (CommExists == false)
                {
                    _Log.LogInformation($"\r\n==>Info:找不到对应通讯COM口==>{floor["CommBox"]},使用客户端默认值 ");
                    return;
                }
                _scommModel.SendPara.CommBox = _scommModel.ListComms[_CommFlg].PortName.ToString();
                _scommModel._DisplayModel.DeviceId = floor["HandleElevatorDeviceId"].ToString();
                _scommModel._DisplayModel.DestinationFloorName = floor["Remark"].ToString();
                _Log.LogInformation($"\r\n==>>_scommModel:{JsonConvert.SerializeObject(_scommModel.SendPara)}");
                byte[] ByteSendData = _scommModel.AutoSendAnalyze();
                //添加队列
                long utctime = DateTimeUtil.UtcNowMillis();
                _Log.LogInformation($"\r\n==>utctime:{utctime}");
                _scommModel.Queue.Instructions = ByteSendData;
                _scommModel.Queue.ScommFlg = _CommFlg;
                _scommModel.Queue.UtcTime = utctime;
                _scommModel.Queue.MessageKey = floor["MessageKey"].ToString();
                _scommModel._QueueElevaor.Add(_scommModel.Queue);              
                /*派梯记录*/
                Task.Run(() => {
                    Apipassrecord(floor);
                });

            }
            catch (Exception ex)
            {
                _Log.LogInformation($"\r\n==>派梯错误：{ex}");
            }
        }
        public void FloorDispElevitor(JArray floor, string AccType)
        {
            if (IFdispatch == 0)
            {
                //配置不需要派梯
                _Log.LogInformation("\r\n==>配置不需要派梯");
                return;
            }
 
            try
            {
                int Num = floor.Count;//是否闸机+派梯数据 还是三方纯派梯数据

                _scommModel.SendPara.Floor = floor[0]["RealFloorId"].ToString();
                _scommModel.SendPara.Sign = floor[0]["Sign"].ToString();
                bool CommExists = false;
                int _CommFlg = 0;
                for (int x = 0; x < _scommModel.ListComms.Count; x++)
                {
                    //找设备对应输出的串口号
                    if (floor[0]["CommBox"].ToString().Trim().ToUpper() == _scommModel.ListComms[x].PortName.ToString().Trim().ToUpper())
                    {
                        CommExists = true;
                        _CommFlg = x;
                        break;
                    }
                }
                if (CommExists == false)
                {
                    _Log.LogInformation($"\r\n==>Info:找不到对应通讯COM口,默认输出COM口派梯");
                    return;
                }
                _scommModel.SendPara.CommBox = _scommModel.ListComms[_CommFlg].PortName.ToString();
                _scommModel._DisplayModel.DeviceId = floor[0]["HandleElevatorDeviceId"].ToString();
                _scommModel._DisplayModel.DestinationFloorName = floor[0]["Name"].ToString();
                _Log.LogInformation($"\r\n==>>_scommModel:{JsonConvert.SerializeObject(_scommModel.SendPara)}");
                byte[] ByteSendData = _scommModel.AutoSendAnalyze();
                //添加队列
                long utctime = DateTimeUtil.UtcNowMillis();
                _Log.LogInformation($"\r\n==>utctime:{utctime}");
                _scommModel.Queue.Instructions = ByteSendData;
                _scommModel.Queue.ScommFlg = _CommFlg;
                _scommModel.Queue.UtcTime = utctime;
                _scommModel.Queue.MessageKey = string.Empty;
                _scommModel._QueueElevaor.Add(_scommModel.Queue);

                /*派梯记录*/
                Task.Run(() => {
                    Localpassrecord(floor, AccType);
                });
                /*派梯记录*/

            }
            catch (Exception ex)
            {
                _Log.LogInformation($"\r\n==>派梯错误：{ex}");
            }
        }
        /// <summary>
        /// 本地派梯记录上传
        /// </summary>
        /// <param name="floor"></param>
        public async Task Localpassrecord(JArray floor,string AccType)
        {
            try
            {
                Dictionary<string, string> passRecord = new Dictionary<string, string>()
                            {
                                { "Extra",floor[1]["CardDeviceId"].ToString()},
                                { "DeviceType",floor[1]["CardDeviceType"].ToString()},
                                { "AccessType",AccType},
                                { "PassRightSign",(floor[1]["AccessType"].ToString()=="FACE")?floor[0]["PersonId"].ToString():floor[1]["Sign"].ToString()},
                                { "DeviceId",floor[1]["HandleElevatorDeviceId"].ToString()},
                                { "PassTime",DateTimeUtil.UtcNowMillis().ToString()},
                                { "PassLocalTime",DateTimeUtil.UtcNowMillis().ToZoneDateTimeByMillis()?.ToSecondString()},
                                { "Remark",floor[0]["Name"].ToString()}
                            };
                string Req = ApiGetData("Api/PassRecoadGuest/ElevatorPassRecord", passRecord);
                _Log.LogInformation($"派梯上传:{JsonConvert.SerializeObject(Req)}");
            }
            catch (Exception ex)
            {
                _Log.LogInformation($"派梯上传 Error{ex}");
            }
        }
        /// <summary>
        /// API派梯记录上传
        /// </summary>
        /// <param name="floor"></param>
        private void Apipassrecord(JObject floor)
        {
            Dictionary<string, string> passRecord = new Dictionary<string, string>()
                            {
                                { "Extra",floor["CardDeviceId"].ToString()},
                                { "DeviceType",floor["CardDeviceType"].ToString()},
                                { "AccessType",floor["AccessType"].ToString()},
                                { "PassRightSign",floor["PassRightSign"].ToString()},
                                { "DeviceId",floor["HandleElevatorDeviceId"].ToString()},
                                { "PassTime",DateTimeUtil.UtcNowMillis().ToString()},
                                { "PassLocalTime",DateTimeUtil.UtcNowMillis().ToZoneDateTimeByMillis()?.ToSecondString()},
                                { "Remark",floor["Remark"].ToString()}
                            };
            string Req = ApiGetData("Api/PassRecoadGuest/ElevatorPassRecord", passRecord);
            _Log.LogInformation($"派梯上传:{JsonConvert.SerializeObject(Req)}");
        }
    }
}
