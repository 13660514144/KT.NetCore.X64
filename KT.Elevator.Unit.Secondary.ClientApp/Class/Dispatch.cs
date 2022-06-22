using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Extensions.Logging;
using KT.Common.WpfApp.Helpers;
using Newtonsoft.Json;
using KT.Elevator.Unit.Secondary.ClientApp.Views;
using KT.Elevator.Unit.Secondary.ClientApp.Device.Hitach;
using ContralServer.CfgFileRead;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Secondary.ClientApp.ViewModels;
using KT.Common.Core.Utils;

namespace HelperTools
{
    public class Dispatch
    {
        private ILogger _Log;
        private ScommModel _scommModel;
        private string Dbconn;
        public int IFdispatch;
        
        public Dispatch()
        {
            Dbconn = AppConfigurtaionServices.Configuration["Dbconn"].ToString();
            _Log = ContainerHelper.Resolve<ILogger>();
            _scommModel = ContainerHelper.Resolve<ScommModel>();
            IFdispatch = Convert.ToInt16(AppConfigurtaionServices.Configuration["IFdispatch"].ToString());            
        }
        public async Task<string> ApiElevatorServer(UnitManualHandleElevatorRequestModel Handle)
        {
            string Result=string.Empty;
            string Controller = "api/PassRecoadGuest/SecondElvatorApi";
            string Uri = AppConfigurtaionServices.Configuration["AppSettings:ServerIp"].ToString();
            string Port = AppConfigurtaionServices.Configuration["AppSettings:ServerPort"].ToString();
            string Url = $"http://{Uri}:{Port}/";
            try
            {
                HostReqModel HostReq = new HostReqModel();
                Result = HostReq.SendAsyncHttpRequest($"{Url}{Controller}", JsonConvert.SerializeObject(Handle)).Result;
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
            _Log.LogInformation($"API 派梯{JsonConvert.SerializeObject(Result)}");
            return Result;
        }
        /// <summary>
        /// 通过API获取权限和派梯数据
        /// </summary>
        /// <param name="CollerName"></param>
        /// <param name="Obj"></param>
        /// <returns></returns>
        public string ApiGetData(string CollerName,Dictionary<string,string> Obj)
        {
            string Uri = AppConfigurtaionServices.Configuration["AppSettings:ServerIp"].ToString();
            string Port = AppConfigurtaionServices.Configuration["AppSettings:ServerPort"].ToString();
            string Url = $"http://{Uri}:{Port}/";
            string Result = string.Empty;            
            try
            {
                HostReqModel HostReq = new HostReqModel();
                Result = HostReq.SendAsyncHttpRequest($"{Url}{CollerName}", JsonConvert.SerializeObject(Obj)).Result;              
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
        /// 通过API获取权限和派梯数据
        /// </summary>
        /// <param name="CollerName"></param>
        /// <param name="Obj"></param>
        /// <returns></returns>
        public string ApiPostPss(string CollerName, Dictionary<string, string> Obj)
        {
            string Uri = AppConfigurtaionServices.Configuration["AppSettings:ServerIp"].ToString();
            string Port = AppConfigurtaionServices.Configuration["AppSettings:ServerPort"].ToString();
            string Url = $"http://{Uri}:{Port}/";
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
        /// 访客公区楼层
        /// </summary>
        /// <returns></returns>
        public DataTable NoPassFloor()
        {
            DataTable Tb = new DataTable();
            try
            {
                IDalMySql dal;
                dal = DalFactory.CreateMysqlDal("mysql", Dbconn);

                string Sql = $@"
                select distinct a.FloorId,b.RealFloorId,c.Name,d.CommBox,d.id,
                        c.IsPublic,b.ElevatorGroupId,c.EdificeId,e.Name EdificeName
                    from pass_right a
                    inner join elevator_group_floor b on a.FloorId= b.FloorId 
                    inner join floor c on b.FloorId=c.id
                    inner join handle_elevator_device d 
                    on  b.ElevatorGroupId=d.ElevatorGroupId 
                    inner join edifice e on c.EdificeId=e.id
                        and d.DeviceType='ELEVATOR_SECONDARY'
                where c.IsPublic=1 and  CHAR_LENGTH(trim(d.CommBox))>0
                        and a.RightType='ELEVATOR'  order by a.FloorId
                ";
                _Log.LogInformation($"\r\n通行==>{Sql}");
                Tb = dal.MySqlListData(Sql);
            }
            catch (Exception ex)
            {
                _Log.LogInformation($"\r\n公区楼层数据查询错误==>{ex.Message}");
                return null;
            }
            return Tb;
        }
        /// <summary>
        /// 获取派梯权限  API方法
        /// </summary>
        /// <param name="Sign"></param>
        /// <returns></returns>
        public JObject ElevotarRecord_Api(string Sign,string accessType)
        {
            //string Controller = "api/PassRecoadGuest/GetElevatorRigth";
            string Controller = AppConfigurtaionServices.Configuration["AppSettings:Controller"].ToString(); 
            JObject Data = new JObject();
            try
            {
                Dictionary<string, string> SendData = new Dictionary<string, string>
                {
                {"Ip", AppConfigurtaionServices.Configuration["AppSettings:StartWithIp"].ToString().Trim() },
                {"Sign", Sign},
                {"Port", AppConfigurtaionServices.Configuration["AppSettings:Port"].ToString().Trim() },
                    { "AccessType",accessType}
                 };
                _Log.LogInformation($"API请求开始{JsonConvert.SerializeObject(SendData)}");
                string Result = RightGetModel.ApiGetDataWording(Controller, SendData);
                _Log.LogInformation($"API请求返回{JsonConvert.SerializeObject(Result)}");
                Data = JObject.Parse(Result);
            }
            catch (Exception ex)
            {
                _Log.LogInformation($"API error {ex}");
                dynamic JsonObj = new DynamicObj();
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = "查询错误";
                JsonObj.Data = new JArray();
                Data = JObject.Parse(JsonConvert.SerializeObject(JsonObj._values));
            }
            return Data;
        }

        /// <summary>
        /// 获取派梯权限  Mysql方法
        /// </summary>
        /// <param name="Sign"></param>
        /// <returns></returns>
        public DataTable ElevotarRecord(string Sign)
        {
            DataTable Tb = new DataTable();
            IDalMySql dal;
            dal = DalFactory.CreateMysqlDal("mysql", Dbconn);
            try
            {
                string IP= AppConfigurtaionServices.Configuration["AppSettings:StartWithIp"].ToString().Trim();
                string PORT = AppConfigurtaionServices.Configuration["AppSettings:Port"].ToString().Trim();
                string Today = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
               
                string Sql = $@"
												
                    select aa.*,bb.RealFloorId,bb.id,bb.CommBox,bb.ElevatorGroupId,
                        bb.DeviceType,bb.Sourceid  from  
                        (select distinct a.Sign,f.FloorId,f.PassRightId,
                             c.IsPublic,c.EdificeId,c.Name,e.Name EdificeName,a.AccessType,
                                a.TimeNow,a.TimeOut
                             from pass_right a
                             inner join pass_right_relation_floor f on a.id=f.PassRightId
                             inner join floor c on f.FloorId=c.id
                             inner join edifice e on c.EdificeId=e.id
                             where a.Sign='{Sign}' 
                            and a.RightType='ELEVATOR') aa												
                    ,											
                    (select 	b.FloorId,b.RealFloorId,
                    d.id,d.CommBox,d.ElevatorGroupId,k.EdificeId,h.Name EdificeName,
                    d.DeviceType,D.floorid  as Sourceid
                    from elevator_group_floor b		
                    inner join handle_elevator_device d 
                    on b.ElevatorGroupId=d.ElevatorGroupId
                    inner join elevator_group k on b.ElevatorGroupId=k.id
                    inner join edifice h on k.EdificeId=h.id
                    where  	d.DeviceType='ELEVATOR_SECONDARY'		
                    and d.IpAddress='{IP}' and d.Port={PORT}	)bb

                    where aa.FloorId=bb.FloorId and aa.EdificeId=bb.EdificeId
                ";
                _Log.LogInformation($"\r\n通行==>{Sql}");
                Tb = dal.MySqlListData(Sql);
            }
            catch (Exception ex)
            {
                _Log.LogInformation($"\r\n通行权限数据查询错误==>{ex.Message}");
                return Tb;
            }
            
            if (Tb.Rows.Count == 0)
            {
                return Tb;
            }
            //公区楼层
            string SQL = $@"
			    select distinct a.id as floorid ,a.IsPublic,a.name,b.RealFloorId from floor a
			    inner join elevator_group_floor b on a.id=b.FloorId
			    and b.ElevatorGroupId={Tb.Rows[0]["ElevatorGroupId"]}
			    where a.EdificeId={Tb.Rows[0]["EdificeId"]}	and a.IsPublic=1			
            ";
            DataTable Tb1 = new DataTable();
            try
            {
                Tb1 = dal.MySqlListData(SQL);
            }
            catch (Exception ex)
            {
                return Tb;
            }
            if (Tb1.Rows.Count > 0)
            {
                for (int x = 0; x < Tb1.Rows.Count; x++)
                {
                    DataRow[] datarows = Tb.Select("floorid = '" + Tb1.Rows[x]["floorid"].ToString().Trim()+ "'");
                    if (datarows.Length == 0)
                    {
                        string sign = Tb.Rows[0]["Sign"].ToString();
                        string FloorId = Tb1.Rows[x]["floorid"].ToString();
                        string PassRightId = "";
                        string IsPublic = Tb1.Rows[x]["IsPublic"].ToString();
                        string EdificeId = Tb.Rows[0]["EdificeId"].ToString();
                        string Name = Tb1.Rows[x]["name"].ToString();
                        string EdificeName = Tb.Rows[0]["EdificeName"].ToString();
                        string AccessType = Tb.Rows[0]["AccessType"].ToString();
                        string DeviceType = Tb.Rows[0]["DeviceType"].ToString();
                        string TimeNow = Tb.Rows[0]["TimeNow"].ToString();
                        string TimeOut = Tb.Rows[0]["TimeOut"].ToString();
                        string RealFloorId = Tb1.Rows[x]["RealFloorId"].ToString();
                        string id = Tb.Rows[0]["id"].ToString();
                        string CommBox = Tb.Rows[0]["CommBox"].ToString();
                        string ElevatorGroupId = Tb.Rows[0]["ElevatorGroupId"].ToString();
                        string Sourceid= Tb.Rows[0]["Sourceid"].ToString();
                        DataRow newRow = Tb.NewRow();
                        newRow["Sign"] = sign;
                        newRow["FloorId"] = Convert.ToInt64(FloorId);
                        newRow["PassRightId"] = PassRightId;
                        newRow["IsPublic"] = IsPublic; ;
                        newRow["EdificeId"] = EdificeId;
                        newRow["Name"] = Name;
                        newRow["EdificeName"] = EdificeName;
                        newRow["AccessType"] = AccessType;
                        newRow["DeviceType"] = DeviceType;
                        newRow["TimeNow"] = Convert.ToInt64(TimeNow);
                        newRow["TimeOut"] = Convert.ToInt64(TimeOut);
                        newRow["RealFloorId"] = RealFloorId;
                        newRow["id"] = Convert.ToInt32(id);
                        newRow["CommBox"] = CommBox;
                        newRow["ElevatorGroupId"] = ElevatorGroupId;
                        newRow["Sourceid"] = Sourceid;
                        Tb.Rows.Add(newRow);
                    }
                }
            }
            return Tb;
        }
        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="Tb"></param>
        public void DispElevitorDt(DataTable Tb)
        {
            _Log.LogInformation($"{JsonConvert.SerializeObject(Tb)}");
            if (IFdispatch == 0)
            {
                _Log.LogInformation("\r\n=>>配置不需要派梯");
                return;
            }

            string Floor = Tb.Rows[0]["RealFloorId"].ToString().Trim();
            string Sign = Tb.Rows[0]["Sign"].ToString().Trim();


            lock (this)
            {
                try
                {
                    _scommModel.SendPara.Floor = Floor;
                    _scommModel.SendPara.Sign = Sign;
                    _Log.LogInformation($"\r\n==>>_scommModel:{JsonConvert.SerializeObject(_scommModel.SendPara)}");
                    bool CommExists = false;
                    for (int x = 0; x < _scommModel.ListComms.Count; x++)
                    {
                        //找设备对应输出的串口号
                        if (Tb.Rows[0]["CommBox"].ToString().Trim() == _scommModel.ListComms[x].PortName.ToString().Trim())
                        {
                            _scommModel.SendPara.CommBox = _scommModel.ListComms[x].PortName.ToString();
                            CommExists = true;
                            _scommModel._DisplayModel.DeviceId = Tb.Rows[0]["id"].ToString();
                            _scommModel._DisplayModel.DestinationFloorName = Tb.Rows[0]["Name"].ToString();
                            _Log.LogInformation("\r\n==>开始派梯");
                            byte[] SendData = _scommModel.AutoSendAnalyze();
                            _Log.LogInformation($"\r\n==>派梯数据包：{BitConverter.ToString(SendData)}");
                            _scommModel.SendAsync(SendData, _scommModel.ListComms[x]);
                            break;
                        }
                    }
                    if (CommExists == false)
                    {
                        _Log.LogInformation($"\r\n==>Info:找不到对应通讯COM口");
                    }
                }
                catch (Exception ex)
                {
                    _Log.LogInformation($"\r\n==>派梯 error {ex.Message}");
                }
            }
        }
        /// <summary>
        /// 派梯 日立 API方法 直接派梯
        /// </summary>
        /// <param name="Tb"></param>
        public void DispElevitor_Api(JArray Tb)
        {
            _Log.LogInformation($"{JsonConvert.SerializeObject(Tb)}");
            if (IFdispatch == 0)
            {
                _Log.LogInformation("\r\n=>>配置不需要派梯");
                return;
            }

            string Floor = Tb[0]["RealFloorId"].ToString().Trim();
            string Sign = Tb[0]["Sign"].ToString().Trim();
            
            
            lock (this)
            {
                try
                {
                    _scommModel.SendPara.Floor = Floor;
                    _scommModel.SendPara.Sign = Sign;
                    _Log.LogInformation($"\r\n==>>_scommModel:{JsonConvert.SerializeObject(_scommModel.SendPara)}");
                    bool CommExists = false;
                    for (int x = 0; x < _scommModel.ListComms.Count; x++)
                    {
                        //找设备对应输出的串口号
                        if (Tb[0]["CommBox"].ToString().Trim() == _scommModel.ListComms[x].PortName.ToString().Trim())
                        {
                            _scommModel.SendPara.CommBox = _scommModel.ListComms[x].PortName.ToString();
                            CommExists = true;
                            _scommModel._DisplayModel.DeviceId = Tb[0]["id"].ToString();
                            _scommModel._DisplayModel.DestinationFloorName = Tb[0]["Name"].ToString();
                            _Log.LogInformation("\r\n==>开始派梯");
                            byte[] SendData = _scommModel.AutoSendAnalyze();
                            _Log.LogInformation($"\r\n==>派梯数据包：{BitConverter.ToString(SendData)}");
                            _scommModel.SendAsync(SendData, _scommModel.ListComms[x]);
                            break;
                        }
                    }
                    if (CommExists == false)
                    {
                        _Log.LogInformation($"\r\n==>Info:找不到对应通讯COM口");
                    }
                }
                catch (Exception ex)
                {
                    _Log.LogInformation($"\r\n==>派梯 error {ex.Message}");
                }
            }
        }
        /// <summary>
        /// 派梯 日立 MYSQL方法 直接派梯
        /// </summary>
        /// <param name="Tb"></param>
        public void DispElevitor_Mysql(DataTable Tb)
        {
            _Log.LogInformation($"{JsonConvert.SerializeObject(Tb)}");
            if (IFdispatch == 0)
            {
                _Log.LogInformation("\r\n=>>配置不需要派梯");
                return;
            }

            string Floor = Tb.Rows[0]["RealFloorId"].ToString().Trim();
            string Sign = Tb.Rows[0]["Sign"].ToString().Trim();


            lock (this)
            {
                try
                {
                    _scommModel.SendPara.Floor = Floor;
                    _scommModel.SendPara.Sign = Sign;
                    _Log.LogInformation($"\r\n==>>_scommModel:{JsonConvert.SerializeObject(_scommModel.SendPara)}");
                    bool CommExists = false;
                    for (int x = 0; x < _scommModel.ListComms.Count; x++)
                    {
                        //找设备对应输出的串口号
                        if (Tb.Rows[0]["CommBox"].ToString().Trim() == _scommModel.ListComms[x].PortName.ToString().Trim())
                        {
                            _scommModel.SendPara.CommBox = _scommModel.ListComms[x].PortName.ToString();
                            CommExists = true;
                            _scommModel._DisplayModel.DeviceId = Tb.Rows[0]["id"].ToString();
                            _scommModel._DisplayModel.DestinationFloorName = Tb.Rows[0]["Name"].ToString();
                            _Log.LogInformation("\r\n==>开始派梯");
                            byte[] SendData = _scommModel.AutoSendAnalyze();
                            _Log.LogInformation($"\r\n==>派梯数据包：{BitConverter.ToString(SendData)}");
                            _scommModel.SendAsync(SendData, _scommModel.ListComms[x]);
                            break;
                        }
                    }
                    if (CommExists == false)
                    {
                        _Log.LogInformation($"\r\n==>Info:找不到对应通讯COM口");
                    }
                }
                catch (Exception ex)
                {
                    _Log.LogInformation($"\r\n==>派梯 error {ex.Message}");
                }
            }
        }

        public void FloorDispElevitor(FloorViewModel floor,
            UnitHandleElevatorModel Handler)
        {
            if (IFdispatch == 0)
            {
                //配置不需要派梯
                _Log.LogInformation("\r\n==>配置不需要派梯");
                //return;
            }
            /*if (_scommModel._QueueElevaor.Count > 0)
            {
                //上一派梯没完成
                return;
            }*/
            try
            {
                
                _scommModel.SendPara.Floor = floor.RealFloorId;
                _scommModel.SendPara.Sign = floor.Sign;                
                bool CommExists = false;
                int _CommFlg = 0;
                for (int x = 0; x < _scommModel.ListComms.Count; x++)
                {
                    //找设备对应输出的串口号
                    if (floor.CommBox.ToString().Trim() == _scommModel.ListComms[x].PortName.ToString().Trim())
                    {                        
                        CommExists = true;
                        _CommFlg = x;
                        break;
                    }
                }
                if (CommExists == false)
                {
                    _Log.LogInformation($"\r\n==>Info:找不到对应通讯COM口,默认输出COM口");
                }
                _scommModel.SendPara.CommBox = _scommModel.ListComms[_CommFlg].PortName.ToString();
                _scommModel._DisplayModel.DeviceId = floor.HandDeviceid;
                _scommModel._DisplayModel.DestinationFloorName = floor.Name;
                _Log.LogInformation($"\r\n==>>_scommModel:{JsonConvert.SerializeObject(_scommModel.SendPara)}");
                byte[] ByteSendData = _scommModel.AutoSendAnalyze();
                //添加队列
                long utctime= DateTimeUtil.UtcNowMillis();
                _Log.LogInformation($"\r\n==>utctime:{utctime}");
                _scommModel.Queue.Instructions= ByteSendData;
                _scommModel.Queue.ScommFlg = _CommFlg;
                _scommModel.Queue.UtcTime = utctime;
                _scommModel.Queue.Handler = Handler;
                _scommModel._QueueElevaor.Add(_scommModel.Queue);                                

                /*派梯记录*/
                /*Task.Run( () => {
                    Dictionary<string, string> passRecord = new Dictionary<string, string>()
            {
                { "Extra",floor.HandDeviceid.ToString()},
                { "DeviceType",floor.DeviceType.ToString()},
                { "AccessType",floor.AccessType.ToString()},
                { "PassRightSign",floor.Sign.ToString()},
                { "DeviceId",floor.HandDeviceid.ToString()},
                { "PassTime",DateTimeUtil.UtcNowMillis().ToString()},
                { "PassLocalTime",DateTimeUtil.UtcNowMillis().ToZoneDateTimeByMillis()?.ToSecondString()},
                { "Remark",floor.Name.ToString()}
            };
                    string Req = ApiPostPss("Api/PassRecoadGuest/ElevatorPassRecord", passRecord);
                    _Log.LogInformation($"派梯上传:{JsonConvert.SerializeObject(Req)}");
                });*/
                /*派梯记录*/
            }
            catch (Exception ex)
            {
                _Log.LogInformation($"\r\n==>派梯错误：{ex}");
            }
        }
        /// <summary>
        /// 派梯保留  可去楼层选择派梯  日立
        /// </summary>
        /// <param name="Sign"></param>
        /// <param name="DeveiceId"></param>
        /// <param name="DestinationFloorId"></param>
        public void DispatchElevitor(string Sign, string RealFloorId = "", string EleGroupId = "")
        {            
            if (IFdispatch == 0)
            {
                //配置不需要派梯
                _Log.LogInformation("\r\n==>配置不需要派梯");
                return;
            }
            string IfApiOrSql = AppConfigurtaionServices.Configuration["IfApiOrSql"].ToString().Trim();

            if (IfApiOrSql == "API")
            {
                #region api 方法   
                JObject JsonData=new JObject();
                JArray Jdt=new JArray();
                try
                {
                    JsonData = RightGetModel.GetLastRightFromApi(Sign, RealFloorId, EleGroupId);
                    if ((string)JsonData["Flg"] == "0")
                    {
                        _Log.LogInformation("没有派梯数据");
                        return;
                    }
                    Jdt = (JArray)JsonData["Data"];
                }
                catch (Exception ex)
                {
                    _Log.LogInformation($"API查询错误:{ex}");
                    return;
                }
                try
                {
                    _scommModel.SendPara.Floor = RealFloorId;
                    _scommModel.SendPara.Sign = Sign;
                    _Log.LogInformation($"\r\n==>>_scommModel:{JsonConvert.SerializeObject(_scommModel.SendPara)}");
                    bool CommExists = false;
                    for (int x = 0; x < _scommModel.ListComms.Count; x++)
                    {
                        //找设备对应输出的串口号
                        if (Jdt[0]["CommBox"].ToString().Trim() == _scommModel.ListComms[x].PortName.ToString().Trim())
                        {
                            _scommModel.SendPara.CommBox = _scommModel.ListComms[x].PortName.ToString();

                            CommExists = true;
                            _scommModel._DisplayModel.DeviceId = Jdt[0]["id"].ToString();
                            _scommModel._DisplayModel.DestinationFloorName = Jdt[0]["Name"].ToString();
                            _Log.LogInformation("\r\n==>开始派梯");
                            byte[] ByteSendData = _scommModel.AutoSendAnalyze();
                            _Log.LogInformation($"\r\n==>派梯数据包：{BitConverter.ToString(ByteSendData)}");
                            _scommModel.SendAsync(ByteSendData, _scommModel.ListComms[x]);
                            break;
                        }
                    }
                    if (CommExists == false)
                    {
                        _Log.LogInformation($"\r\n==>Info:找不到对应通讯COM口,默认输出COM口");
                    }
                }
                catch (Exception ex)
                {
                    _Log.LogInformation($"\r\n==>派梯错误：{ex}");
                }
                #endregion
            }
            else
            {
                #region mysql  直接查数据库方法
                DataTable dt = new DataTable();
                try
                {
                    dt = RightGetModel.GetLastRightFromMysql(Sign, RealFloorId, EleGroupId, Dbconn);
                    if (dt.Rows.Count == 0)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    _Log.LogInformation($"数据库查询错误：{ex}");
                    return;
                }

                //lock (this)
                //{
                try
                {
                    _scommModel.SendPara.Floor = RealFloorId;
                    _scommModel.SendPara.Sign = Sign;
                    _Log.LogInformation($"\r\n==>>_scommModel:{JsonConvert.SerializeObject(_scommModel.SendPara)}");
                    bool CommExists = false;
                    for (int x = 0; x < _scommModel.ListComms.Count; x++)
                    {
                        //找设备对应输出的串口号
                        if (dt.Rows[0]["CommBox"].ToString().Trim() == _scommModel.ListComms[x].PortName.ToString().Trim())
                        {
                            _scommModel.SendPara.CommBox = _scommModel.ListComms[x].PortName.ToString();

                            CommExists = true;
                            _scommModel._DisplayModel.DeviceId = dt.Rows[0]["id"].ToString();
                            _scommModel._DisplayModel.DestinationFloorName = dt.Rows[0]["Name"].ToString();
                            _Log.LogInformation("\r\n==>开始派梯");
                            byte[] ByteSendData = _scommModel.AutoSendAnalyze();
                            _Log.LogInformation($"\r\n==>派梯数据包：{BitConverter.ToString(ByteSendData)}");
                            _scommModel.SendAsync(ByteSendData, _scommModel.ListComms[x]);
                            break;
                        }
                    }
                    if (CommExists == false)
                    {
                        _Log.LogInformation($"\r\n==>Info:找不到对应通讯COM口,默认输出COM口");
                    }
                }
                catch (Exception ex)
                {
                    _Log.LogInformation($"\r\n==>派梯错误：{ex}");
                }
                #endregion
            }
            //}

        }

        /// <summary>
        /// 派梯保留  可去楼层选择派梯  三菱
        /// </summary>
        /// <param name="Sign"></param>
        /// <param name="DeveiceId"></param>
        /// <param name="DestinationFloorId"></param>
        public UnitManualHandleElevatorRequestModel DispatchElevitor_M(string Sign, 
            string RealFloorId ,
            string EleGroupId ,string DestinationFloorId,string Sourceid)
        {
            UnitManualHandleElevatorRequestModel Handele = new UnitManualHandleElevatorRequestModel();
            if (IFdispatch == 0)
            {
                //配置不需要派梯
                _Log.LogInformation("\r\n==>配置不需要派梯");
                return Handele;
            }
            string IfApiOrSql= AppConfigurtaionServices.Configuration["IfApiOrSql"].ToString().Trim();
            #region API方法
            if (IfApiOrSql == "API")
            {
                try
                {
                    JObject Obj = RightGetModel.GetLastRightFromApi_Mitsubishi_ELSGW(Sign,
                        RealFloorId,
                        EleGroupId,DestinationFloorId,Sourceid);
                    JArray Jdt = new JArray();
                    if ((string)Obj["Flg"] == "0")
                    {
                        return Handele;
                    }
                    Jdt = (JArray)Obj["Data"];
                    Handele.Sign = Sign;
                    Handele.AccessType = Jdt[0]["AccessType"].ToString();
                    Handele.DeviceType = Jdt[0]["DeviceType"].ToString();
                    Handele.DeviceId = Jdt[0]["id"].ToString();
                    Handele.DestinationFloorId = Jdt["FloorId"].ToString();
                    Handele.HandleElevatorDeviceId = Jdt[0]["id"].ToString();
                    Handele.SourceFloorId = Jdt[0]["SourceFloorId"].ToString();
                    Handele.ElevatorGroupId = Jdt["elevatorgroupid"].ToString();
                    Handele.DestinationFloorIds = new List<string>();
                    Handele.DestinationFloorIds.Add(Jdt[0]["FloorId"].ToString());
                }
                catch (Exception ex)
                {
                    _Log.LogInformation($"api error:{ex}");
                    return Handele;
                }
                _Log.LogInformation($"api data:{JsonConvert.SerializeObject(Handele)}");
            }
            #endregion
            else
            {
                #region mysql方法
                DataTable dt = new DataTable();
                try
                {
                    dt = RightGetModel.GetLastRightFromMysql_Mitsubishi_ELSGW(Sign, RealFloorId, EleGroupId, Dbconn);
                }
                catch (Exception ex)
                {
                    _Log.LogInformation($"数据查询错误 {ex}");
                    return Handele;
                }
                if (dt.Rows.Count == 0)
                {
                    return Handele;
                }
                Handele.Sign = Sign;
                Handele.AccessType = dt.Rows[0]["AccessType"].ToString();
                Handele.DeviceType = dt.Rows[0]["DeviceType"].ToString();
                Handele.DeviceId = dt.Rows[0]["id"].ToString();
                Handele.DestinationFloorId = dt.Rows[0]["FloorId"].ToString();
                Handele.HandleElevatorDeviceId = dt.Rows[0]["id"].ToString();
                Handele.SourceFloorId = dt.Rows[0]["SourceFloorId"].ToString();
                Handele.ElevatorGroupId = dt.Rows[0]["elevatorgroupid"].ToString();
                Handele.DestinationFloorIds = new List<string>();
                Handele.DestinationFloorIds.Add(dt.Rows[0]["FloorId"].ToString());
                #endregion
            }
            return Handele;
        }
    }
}
