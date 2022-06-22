using HelperTools;
using KT.Turnstile.Unit.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KT.Common.Core.Utils;
using Microsoft.Extensions.Logging;
using KT.Quanta.WebApi.Controllers.Turnstile;
using KT.Quanta.Service.Turnstile.Services;
using Microsoft.Extensions.DependencyInjection;
using KT.Elevator.Unit.Entity.Entities;
using KT.Quanta.Service.Services;
using System.Data;
using ContralServer.CfgFileRead;
using KT.Elevator.Unit.Entity.Models;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Handlers;
using System.IO;
using System.Text;

namespace KT.Quanta.WebApi.Controllers.GuestMothed
{
    [Route("Api/PassRecoadGuest")]
    [ApiController]    
    public class PassRecoadGuestController : ControllerBase
    {        
        private readonly ILogger<TurnstilePassRightController> _logger;
        private IServiceProvider _serviceProvider;
        private PushRecordHandler _pushRecordHanlder;
        private string Dbconn= AppConfigurtaionServices.Configuration["ConnectionStrings:MysqlConnection"].ToString().Trim();
        public PassRecoadGuestController(
            ILogger<TurnstilePassRightController> logger,
            IServiceProvider serviceProvider, PushRecordHandler pushRecordHanlder
        )
        {            
            _serviceProvider = serviceProvider;
            _logger = logger;
            _pushRecordHanlder = pushRecordHanlder;
        }
        public class VerMsg
        { 
            public string Ver { get; set; }
            public string DeviceType { get; set; }
        }
        public class UpVerMsg
        {
            public string CurrentVer { get; set; }
            public string UpVer { get; set; }
            public string DeviceType { get; set; }
            public string Ip { get; set; }
            public string Port { get; set; }
            public string UpFlg { get; set; }
        }
        [HttpPost("UpVerMessage")]
        public async Task<string> UpVerMessage(UpVerMsg Obj)
        {
            dynamic JsonObj = new DynamicObj();
            string Result = string.Empty;
            try
            {
                IDalMySql dal;
                dal = DalFactory.CreateMysqlDal("mysql", Dbconn);
                string SQL = $@"
			        insert into upvermessage (CurrentVer,UpVer,DeviceType,Ip,Port,UpFlg)
                        values 
                    ('{Obj.CurrentVer}','{Obj.UpVer}','{Obj.DeviceType}','{Obj.Ip}','{Obj.Port}','{Obj.UpFlg}')                    
            ";
                long r = dal.ExecuteMySql(SQL);
                JsonObj.code = "200";
                JsonObj.Flg = "1";
                JsonObj.Message = "更新上传成功";
                JsonObj.Data = new JArray();
                Result = JsonConvert.SerializeObject(JsonObj._values);
            }
            catch (Exception ex)
            {
                JsonObj.code = "500";
                JsonObj.Flg = "0";
                JsonObj.Message = "更新上传失败";
                JsonObj.Data = new JArray();
                Result = JsonConvert.SerializeObject(JsonObj._values);
            }
            _logger.LogInformation($"upvermsg={JsonConvert.SerializeObject(Obj)}");
            return Result;
        }
        [HttpPost("ChkVer")]
        public async Task<string> ChkVer(VerMsg Obj)
        {
            _logger.LogInformation($"CHKVER={JsonConvert.SerializeObject(Obj)}");
            dynamic JsonObj = new DynamicObj();
            string Result = string.Empty;
            StringBuilder Sb=new StringBuilder();
            bool ChangVer = false;
            try
            {
                StreamReader sr = new StreamReader("Ver.json", System.Text.Encoding.Default);
                string line;
                while ((line = sr.ReadLine()) != null)
                {                   
                    Sb.Append(line);
                }
                sr.Close();
                sr.Dispose();
                JArray Arr = JArray.Parse(Sb.ToString());
                
                int len = Arr.Count - 1;
                for (int x = len; x>=0; x--)
                {
                    JObject J = (JObject)Arr[x];
                    if (J["DeviceType"].ToString().Trim() == Obj.DeviceType)
                    {
                        if (J["Ver"].ToString().Trim() != Obj.Ver)
                        {
                            string Pkzip=AppDomain.CurrentDomain.BaseDirectory+$@"\wwwroot\dw\{J["PackageFile"]}";
                            long Isize = new FileInfo(Pkzip).Length;
                            ChangVer = true;
                            JsonObj.DeviceType = Obj.DeviceType;
                            JsonObj.Ver = J["Ver"].ToString().Trim();
                            JsonObj.PackageFile = J["PackageFile"].ToString().Trim();
                            JsonObj.FileSize = Isize;
                            JsonObj.Flg = 1;                            
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"CHKVER={ex.Message}");
            }
            if (ChangVer == false)
            {
                JsonObj.DeviceType = string.Empty;
                JsonObj.Ver = string.Empty;
                JsonObj.PackageFile = string.Empty;
                JsonObj.FileSize = 0;
                JsonObj.Flg = 0;
            }
            Result= JsonConvert.SerializeObject(JsonObj._values);
            return Result;
        }
        /// <summary>
        /// 闸机端直接派梯返回派梯记录
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("ElevatorPassRecord")]
        public async Task<string> ElevatorPassRecord(PassRecordModel value)
        {
            _logger.LogInformation($"闸机上传派梯记录=>{JsonConvert.SerializeObject(value)}");
            dynamic JsonObj = new DynamicObj();
            string Result = string.Empty;
            try
            {
                bool s = await _pushRecordHanlder.StartPushAsync(value);
                if (s)
                {
                    JsonObj.code = "200";
                    JsonObj.Flg = "1";
                    JsonObj.Message = "派梯记录上传成功";
                    JsonObj.Data = new JArray();
                    Result = JsonConvert.SerializeObject(JsonObj._values);
                }
                else
                {
                    JsonObj.code = "200";
                    JsonObj.Flg = "0";
                    JsonObj.Message = "派梯记录上传失败";
                    JsonObj.Data = new JArray();
                    Result = JsonConvert.SerializeObject(JsonObj._values);
                }
            }
            catch (Exception ex)
            {
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = $"派梯记录上传异常{ex}";
                JsonObj.Data = new JArray();
                Result = JsonConvert.SerializeObject(JsonObj._values);
            }
            return Result;
        }
        /// <summary>
        /// 陌生人
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("GuestPassAllPublic")]
        public string GuestPassAllPublic([FromBody] ClientRight value)
        {
            dynamic JsonObj = new DynamicObj();
            string Result = string.Empty;
            DataTable Tb = new DataTable();
            IDalMySql dal;
            dal = DalFactory.CreateMysqlDal("mysql", Dbconn);
            string SQL = $@"
			        select distinct '0000' as Sign, 
					    a.id as FloorId ,'' as PassRightId,a.IsPublic,
                        k.EdificeId, a.Name,h.name as EdificeName,
                        '' as AccessType,0 as TimeNow,0 as TimeOut, 
                        b.RealFloorId,c.id,c.CommBox,c.DeviceType,
                        c.FloorId as Sourceid,b.ElevatorGroupId,'-1' as  PersonId
				    from 
						handle_elevator_device c	
						inner join elevator_group k on c.ElevatorGroupId=k.id							 inner join elevator_group_floor b 
							on k.id=b.ElevatorGroupId					
						inner join floor a on b.FloorId=a.id
			            inner join edifice h on k.EdificeId=h.id																								
			        where a.IsPublic=1
							and c.IpAddress='{value.Ip}'  										
            ";
            try
            {
                Tb = dal.MySqlListData(SQL);
                if (Tb.Rows.Count == 0)
                {
                    JsonObj.code = "200";
                    JsonObj.Flg = "0";
                    JsonObj.Message = "没有任何权限数据";
                    JsonObj.Data = new JArray();
                    Result = JsonConvert.SerializeObject(JsonObj._values);
                }
                else
                {
                    JsonObj.code = "200";
                    JsonObj.Flg = "1";
                    JsonObj.Message = "查询结果返回";
                    JsonObj.Data = Tb;
                    Result = JsonConvert.SerializeObject(JsonObj._values);
                }
            }
            catch (Exception ex)
            {
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = $"查询错误{ex}";
                JsonObj.Data = new JArray();
                Result = JsonConvert.SerializeObject(JsonObj._values);
                _logger.LogInformation($"\r\nSQL===>{ex}");
            }
            _logger.LogInformation($"\r\n来宾 SQL===>{SQL}");
            return Result;
            
        }
        /// <summary>
        /// 通行权限
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("RightTrunsite")]
        public string RightTrunsite([FromBody] ClientRight value)
        {
            dynamic JsonObj = new DynamicObj();
            string Result = string.Empty;
            string Today = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DataTable Tb = new DataTable();
            IDalMySql dal;
            dal = DalFactory.CreateMysqlDal("mysql", Dbconn);
            lock (this)
            {
                string SqlPass = $@"
                SELECT distinct a.id as PassRightId, a.AccessType,a.Sign,
                    d.id as CardDeviceId,
                    d.BrandModel,d.CardDeviceType,d.DeviceType,
                    d.RelayDeviceOut,
                    IFNULL(d.HandleElevatorDeviceId,0) as HandleElevatorDeviceId,
                    e.CommunicateType,e.IpAddress,e.Port,d.PortName,a.PersonId,
                    IFNULL(A.FloorId,0) AS DesFloorId,a.RightType,d.ProcessorId,
                    k.CommBox,IFNULL(j.RealFloorId,0) as RealFloorId,
                    m.Name,IFNULL(k.FloorId,0) as srcid,k.ElevatorGroupId
                FROM pass_right a 
                inner join pass_right_relation_card_device_right_group b 
                    on a.id=b.PassRightId
                inner join card_device_right_group_relation_card_device c 
                    on b.CardDeviceRightGroupId=c.CardDeviceRightGroupId
                inner join card_device d 
                    on c.CardDeviceId=d.id                      
                    and d.id={value.CardId}
                inner join processor p on d.ProcessorId=p.id
					and p.IpAddress='{value.Ip}' and p.Port={value.Port}
                inner join relay_device e 
                    on d.RelayDeviceId=e.id
                left join handle_elevator_device k 
					on d.HandleElevatorDeviceId=k.id
				left join elevator_group_floor j
					on a.FloorId=j.FloorId and k.ElevatorGroupId=j.ElevatorGroupId
				left join floor m on j.FloorId=m.id                
                where a.sign='{value.Sign}' and a.AccessType='{value.AccessType}'			
                and (A.RightType='TURNSTILE' or A.RightType='ELEVATOR')
                and  unix_timestamp('{Today}')>=A.TimeNow/1000
                and  unix_timestamp('{Today}')<=A.TimeOut/1000	
                order by RightType
                ";

                string Sql = $@"
                SELECT distinct a.id as PassRightId, a.AccessType,a.Sign,
                    d.id as CardDeviceId,
                    d.BrandModel,d.CardDeviceType,d.DeviceType,
                    d.RelayDeviceOut,
                    IFNULL(d.HandleElevatorDeviceId,0) as HandleElevatorDeviceId,
                    e.CommunicateType,e.IpAddress,e.Port,d.PortName,a.PersonId,
                    IFNULL(A.FloorId,0) AS DesFloorId,a.RightType,d.ProcessorId,
                    k.CommBox,IFNULL(j.RealFloorId,0) as RealFloorId,
                    m.Name,IFNULL(p.FloorId,0) as srcid,k.ElevatorGroupId
                FROM pass_right a              
                inner join card_device d 
                    on  d.id={value.CardId}
                inner join processor p on d.ProcessorId=p.id
					and p.IpAddress='{value.Ip}' and p.Port={value.Port}
                inner join relay_device e 
                    on d.RelayDeviceId=e.id
                left join handle_elevator_device k 
					on d.HandleElevatorDeviceId=k.id
				left join elevator_group_floor j
					on a.FloorId=j.FloorId and k.ElevatorGroupId=j.ElevatorGroupId
				left join floor m on j.FloorId=m.id 
                where a.sign='{value.Sign}' and a.AccessType='{value.AccessType}'				
                and (A.RightType='TURNSTILE' or A.RightType='ELEVATOR')
                and  unix_timestamp('{Today}')>=A.TimeNow/1000
                and  unix_timestamp('{Today}')<=A.TimeOut/1000
                order by RightType
            ";
                try
                {
                    if (value.PassFlg.ToString().Trim() == "0")
                    {
                        Tb = dal.MySqlListData(Sql);
                    }
                    else
                    {
                        Tb = dal.MySqlListData(SqlPass);
                    }

                    if (Tb.Rows.Count == 0)
                    {
                        JsonObj.code = "200";
                        JsonObj.Flg = "0";
                        JsonObj.Message = "没有任何权限数据";
                        JsonObj.Data = new JArray();
                        Result = JsonConvert.SerializeObject(JsonObj._values);
                    }
                    else
                    {
                        JsonObj.code = "200";
                        JsonObj.Flg = "1";
                        JsonObj.Message = "查询结果返回";
                        JsonObj.Data = Tb;
                        Result = JsonConvert.SerializeObject(JsonObj._values);
                    }
                }
                catch (Exception ex)
                {
                    JsonObj.code = "000";
                    JsonObj.Flg = "0";
                    JsonObj.Message = "查询错误";
                    JsonObj.Data = new JArray();
                    Result = JsonConvert.SerializeObject(JsonObj._values);
                    _logger.LogInformation($"\r\nSQL===>{ex}");
                }
                if (value.PassFlg.ToString().Trim() == "1")
                {
                    _logger.LogInformation($"\r\nSQLpass===>{SqlPass}\r\n PassFlg={value.PassFlg}");
                }
                else
                {
                    _logger.LogInformation($"\r\nSQL===>{Sql}\r\n PassFlg={value.PassFlg}");
                }
                _logger.LogInformation($"\r\nnSQL tb===>{JsonConvert.SerializeObject(Tb)}");
            }
            return Result;
        }
        /// <summary>
        /// 闸机通行记录
        /// </summary>
        /// <param name="passRecord"></param>
        /// <returns></returns>
        [HttpPost("PassTurnsite")]        
        public string PassTurnsite([FromBody] TurnstileUnitPassRecordEntity passRecord)
        {
            _logger.LogError($"Api Right 闸机通行记录");
            dynamic JsonObj = new DynamicObj();
            try
            {                
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<ITurnstilePassRecordService>();
                    service.PushPassRecord(passRecord);
                    _logger.LogInformation("闸机上传通行记录完成！");
                }                
                JsonObj.code = "200";
                JsonObj.Flg = "1";
                JsonObj.Message = "上传完成";
                JsonObj.Data = new JArray();                
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"闸机上传通行记录：data:{ JsonConvert.SerializeObject(passRecord, JsonUtil.JsonPrintSettings)} ");
                _logger.LogInformation($"上传错误==>>{ex}");
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = "上传失败";
                JsonObj.Data = new JArray();
            }
            return JsonConvert.SerializeObject(JsonObj._values);
        }

        /// <summary>
        /// 派梯通行记录
        /// </summary>
        /// <param name="passRecord"></param>
        /// <returns></returns>
        [HttpPost("PassElevator")]
        public string PassElevator([FromBody] UnitPassRecordEntity passRecord)
        {
            _logger.LogInformation($"API==>派梯上传通行记录：data:{ JsonConvert.SerializeObject(passRecord, JsonUtil.JsonPrintSettings)} ");
            dynamic JsonObj = new DynamicObj();
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IPassRecordService>();
                    service.PushPassRecord(passRecord);
                }
                JsonObj.code = "200";
                JsonObj.Flg = "0";
                JsonObj.Message = "API上传完成";
                JsonObj.Data = new JArray();
            }
            catch (Exception ex)
            {                
                _logger.LogInformation($"API上传错误==>>{ex}");
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = $"API上传失败{ex}";
                JsonObj.Data = new JArray();
            }
            return JsonConvert.SerializeObject(JsonObj._values);
        }
        /// <summary>
        /// 闸机权限
        /// </summary>
        [HttpPost("GetTrunsiteRigth")]
        public string GetTrunsiteRigth([FromBody] ClientRight Right)
        {
            _logger.LogError($"Api Right 闸机权限请求");
            dynamic JsonObj = new DynamicObj();
            string Result = string.Empty;
            string Today = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                string Sql = $@"
                SELECT distinct a.id as PassRightId, a.AccessType,a.Sign,c.CardDeviceId,
                    d.BrandModel,d.CardDeviceType,d.DeviceType,
                    d.RelayDeviceOut,d.HandleElevatorDeviceId,
                    e.CommunicateType,e.IpAddress,e.Port,d.PortName
                FROM pass_right a 
                inner join pass_right_relation_card_device_right_group b 
                    on a.id=b.PassRightId
                inner join card_device_right_group_relation_card_device c 
                    on b.CardDeviceRightGroupId=c.CardDeviceRightGroupId
                inner join card_device d 
                    on c.CardDeviceId=d.id 
                    and d.ProcessorId=(SELECT id from processor where IpAddress='{Right.Ip}' ) 
                inner join relay_device e 
                    on d.RelayDeviceId=e.id
                where a.sign='{Right.Sign}' 			
                and A.RightType='TURNSTILE'
                and  unix_timestamp('{Today}')>=A.TimeNow/1000
                and  unix_timestamp('{Today}')<=A.TimeOut/1000		 
                ";
                DataTable Tb = null;
                IDalMySql dal;
                dal = DalFactory.CreateMysqlDal("mysql", Dbconn);
                Tb = dal.MySqlListData(Sql);
                if (Tb.Rows.Count == 0)
                {
                    JsonObj.code = "200";
                    JsonObj.Flg = "0";
                    JsonObj.Message = "没有任何权限数据";
                    JsonObj.Data = new JArray();
                    Result = JsonConvert.SerializeObject(JsonObj._values);
                }
                else
                {
                    JsonObj.code = "200";
                    JsonObj.Flg = "1";
                    JsonObj.Message = "成功返回权限";
                    JsonObj.Data = Tb;
                    Result = JsonConvert.SerializeObject(JsonObj._values);
                }
            }
            catch (Exception ex)
            {
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = ex.Message.ToString();
                JsonObj.Data = new JArray();
                Result = JsonConvert.SerializeObject(JsonObj._values);
                _logger.LogError($"Api Right Error==>{ex}");
            }
            return Result;
        }
        /// <summary>
        /// 电梯权限  公用API 日立，三菱，通力
        /// </summary>
        [HttpPost("GetElevatorRigth")]
        public string GetElevatorRigth([FromBody] ClientRight Right)
        {
            _logger.LogError($"Api Right 电梯权限请求");
            dynamic JsonObj = new DynamicObj();
            string Result = string.Empty;
            
            string Today = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                string Sql = $@"
												
                    select aa.*,bb.RealFloorId,bb.id,bb.CommBox,bb.ElevatorGroupId,
                        bb.DeviceType,bb.Sourceid from  
                        (select distinct a.Sign,f.FloorId,f.PassRightId,
                             c.IsPublic,c.EdificeId,c.Name,e.Name EdificeName,a.AccessType,
                                a.TimeNow,a.TimeOut,a.PersonId
                             from pass_right a
                             inner join pass_right_relation_floor f on a.id=f.PassRightId
                             inner join floor c on f.FloorId=c.id
                             inner join edifice e on c.EdificeId=e.id
                             where a.Sign='{Right.Sign}' and a.AccessType='{Right.AccessType}'
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
                    and d.IpAddress='{Right.Ip}' and d.Port={Right.Port})bb

                    where aa.FloorId=bb.FloorId and aa.EdificeId=bb.EdificeId
                    ORDER BY aa.FloorId
                ";
                _logger.LogInformation($"楼层查询{Sql}");
                DataTable Tb = new DataTable();
                IDalMySql dal;
                dal = DalFactory.CreateMysqlDal("mysql", Dbconn);
                
                Tb = dal.MySqlListData(Sql);
                _logger.LogInformation($"tb record{JsonConvert.SerializeObject(Tb)}");
                
                //else
                //{
                    //guolu
                    //公区楼层
                    string SQL = $@"
			        select distinct a.id as floorid ,a.IsPublic,
                    a.name,b.RealFloorId from floor a
			        inner join elevator_group_floor b on a.id=b.FloorId
			        and b.ElevatorGroupId={Tb.Rows[0]["ElevatorGroupId"]}
			        where a.EdificeId={Tb.Rows[0]["EdificeId"]}	and a.IsPublic=1			
                    ";
                    _logger.LogInformation($"楼层查询公共{Sql}");
                    DataTable Tb1 = new DataTable();
                    try
                    {
                        Tb1 = dal.MySqlListData(SQL);
                    }
                    catch (Exception ex)
                    {
                        //return Tb;
                        _logger.LogInformation($"公区楼层查询错误{ex}");
                    }
                    if (Tb1.Rows.Count > 0)
                    {
                        for (int x = 0; x < Tb1.Rows.Count; x++)
                        {
                            DataRow[] datarows = Tb.Select("floorid = '" + Tb1.Rows[x]["floorid"].ToString().Trim() + "'");
                            if (datarows.Length == 0)
                            {
                                string sign = Tb.Rows[0]["Sign"].ToString();
                                string FloorId = Tb1.Rows[x]["floorid"].ToString();
                                string PassRightId =Tb.Rows[0]["PassRightId"].ToString();
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
                                string Sourceid = Tb.Rows[0]["Sourceid"].ToString();
                                string PersonId = Tb.Rows[0]["PersonId"].ToString();
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
                                newRow["PersonId"] = PersonId;
                                Tb.Rows.Add(newRow);
                            }
                        }
                    }
                if (Tb.Rows.Count == 0)
                {
                    JsonObj.code = "200";
                    JsonObj.Flg = "0";
                    JsonObj.Message = "没有任何 主要权限数据";
                    JsonObj.Data = new JArray();
                    Result = JsonConvert.SerializeObject(JsonObj._values);
                }
                else
                {
                    JsonObj.code = "200";
                    JsonObj.Flg = "1";
                    JsonObj.Message = "成功返回权限";
                    JsonObj.Data = Tb;
                    Result = JsonConvert.SerializeObject(JsonObj._values);
                }
                //}
            }
            catch (Exception ex)
            {
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = ex.Message.ToString();
                JsonObj.Data = new JArray();
                Result = JsonConvert.SerializeObject(JsonObj._values);
                _logger.LogError($"Api GetElevatorRigth Error==>{ex}");
            }
            return Result;
        }
        /// <summary>
        /// 可去楼层派梯  日立
        /// </summary>
        [HttpPost("GetFloorRigth")]
        public string GetFloorRigth([FromBody] FloorRight Right)
        {
            _logger.LogError($"Api Right 目的楼楼层权限请求");
            dynamic JsonObj = new DynamicObj();
            string Result = string.Empty;
            string Today = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                string Sql = $@"
                    select distinct 
                    a.realfloorid,a.elevatorgroupid,b.CommBox,b.id,c.Name,
                    b.DeviceType,'{Right.Sign}' as Sign,kk.AccessType
                    from elevator_group_floor a 
                    inner join handle_elevator_device b
                    on a.elevatorgroupid=b.elevatorgroupid 
                    inner join floor c on a.FloorId=c.id
					inner join 
					(
						SELECT distinct id,AccessType,sign   from pass_right 
						where sign='{Right.Sign}' and RightType='ELEVATOR'											
				    ) kk on Sign=kk.sign
                    where 
                    a.elevatorgroupid={Right.ElevatorGroupId} 
                    and b.DeviceType='ELEVATOR_SECONDARY'
                    and b.IpAddress='{Right.Ip}' and b.Port={Right.Port}
                    and  a.realfloorid={Right.RealFloorId}  LIMIT 1			
                ";
                DataTable Tb = null;
                IDalMySql dal;
                dal = DalFactory.CreateMysqlDal("mysql", Dbconn);
                Tb = dal.MySqlListData(Sql);
                if (Tb.Rows.Count == 0)
                {
                    JsonObj.code = "200";
                    JsonObj.Flg = "0";
                    JsonObj.Message = "没有任何权限数据";
                    JsonObj.Data = new JArray();
                    Result = JsonConvert.SerializeObject(JsonObj._values);
                }
                else
                {
                    JsonObj.code = "200";
                    JsonObj.Flg = "1";
                    JsonObj.Message = "成功返回权限";
                    JsonObj.Data = Tb;
                    Result = JsonConvert.SerializeObject(JsonObj._values);
                }
            }
            catch (Exception ex)
            {
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = ex.Message.ToString();
                JsonObj.Data = new JArray();
                Result = JsonConvert.SerializeObject(JsonObj._values);
                _logger.LogError($"Api Right Error==>{ex}");
            }
            return Result;
        }

        /// <summary>
        /// 可去楼层派梯  三菱
        /// </summary>
        [HttpPost("GetFloorRigthMitsubishi")]
        public string GetFloorRigthMitsubishi([FromBody] FloorRight Right)
        {
            _logger.LogError($"Api Right 目的楼楼层权限请求");
            dynamic JsonObj = new DynamicObj();
            string Result = string.Empty;
            string Today = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                string Sql = $@"
                    select distinct 
                    a.realfloorid,a.elevatorgroupid,b.CommBox,b.id,c.Name,
                    b.FloorId as SourceFloorId,
                    b.DeviceType,'{Right.Sign}' as Sign,kk.AccessType,a.FloorId
                    
                    from elevator_group_floor a 
                    inner join handle_elevator_device b
                    on a.elevatorgroupid=b.elevatorgroupid 
                    inner join floor c on a.FloorId=c.id
					inner join 
					(
						SELECT distinct id,AccessType,sign   from pass_right 
						where sign='{Right.Sign}' and RightType='ELEVATOR'											
				    ) kk on Sign=kk.sign
                    where 
                    a.elevatorgroupid={Right.ElevatorGroupId} 
                    and b.DeviceType='ELEVATOR_SECONDARY'
                    and b.IpAddress='{Right.Ip}' and b.Port={Right.Port}
                    and  a.realfloorid={Right.RealFloorId}  LIMIT 1			
                ";
                DataTable Tb = null;
                IDalMySql dal;
                dal = DalFactory.CreateMysqlDal("mysql", Dbconn);
                Tb = dal.MySqlListData(Sql);
                if (Tb.Rows.Count == 0)
                {
                    JsonObj.code = "200";
                    JsonObj.Flg = "0";
                    JsonObj.Message = "没有任何权限数据";
                    JsonObj.Data = new JArray();
                    Result = JsonConvert.SerializeObject(JsonObj._values);
                }
                else
                {
                    JsonObj.code = "200";
                    JsonObj.Flg = "1";
                    JsonObj.Message = "成功返回权限";
                    JsonObj.Data = Tb;
                    Result = JsonConvert.SerializeObject(JsonObj._values);
                }
            }
            catch (Exception ex)
            {
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = ex.Message.ToString();
                JsonObj.Data = new JArray();
                Result = JsonConvert.SerializeObject(JsonObj._values);
                _logger.LogError($"Api Right Error==>{ex}");
            }
            return Result;
        }

        /// <summary>
        /// 三菱派梯服务  二次派梯终端
        /// </summary>
        /// <returns></returns>
        [HttpPost("SecondElvatorApi")]
        public string SecondElvatorApi([FromBody] UnitManualHandleElevatorRequestModel handleElevator)
        {
            _logger.LogError($"Api Right 三菱派梯服务  二次派梯终端 请求");
            dynamic Obj = new DynamicObj();
            #region
            /*var distributeHandle = new DistributeHandleElevatorModel();
            DataTable dt = new DataTable();
            try
            {
                IDalMySql dal;
                dal = DalFactory.CreateMysqlDal("mysql", Dbconn);

                string Sql = $@"
                SELECT distinct e.Id, e.CreatedTime, e.Creator, e.EditedTime, e.Editor, 
                e.ElevatorGroupId, e.FloorId, e.RealFloorId, f.Id, f.CreatedTime, 
                f.Creator, f.EdificeId, f.EditedTime, f.Editor, f.IsFront, 
                f.IsPublic, f.IsRear, f.Name, f.PhysicsFloor,'1' AS Flg
                FROM ELEVATOR_GROUP_FLOOR AS e
                LEFT JOIN FLOOR AS f ON e.FloorId = f.Id
                WHERE (e.ElevatorGroupId = {handleElevator.ElevatorGroupId}) 
                AND (e.FloorId = {handleElevator.SourceFloorId})
                
                    union
                SELECT distinct e.Id, e.CreatedTime, e.Creator, e.EditedTime, e.Editor, 
                e.ElevatorGroupId, e.FloorId, e.RealFloorId, f.Id, f.CreatedTime, 
                f.Creator, f.EdificeId, f.EditedTime, f.Editor, f.IsFront, 
                f.IsPublic, f.IsRear, f.Name, f.PhysicsFloor,'2' AS Flg
                FROM ELEVATOR_GROUP_FLOOR AS e
                LEFT JOIN FLOOR AS f ON e.FloorId = f.Id
                WHERE (e.ElevatorGroupId = {handleElevator.ElevatorGroupId}) 
                AND (e.FloorId = {handleElevator.DestinationFloorId})
               
            ";
                _logger.LogInformation(Sql);
                dt = dal.MySqlListData(Sql);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex}");
            }

            distributeHandle.MessageId = Guid.NewGuid().ToString();
            distributeHandle.DeviceId = handleElevator.HandleElevatorDeviceId;
            distributeHandle.SourceFloor = ElevatorFloorModel.Set(
                long.Parse(dt.Rows[0]["RealFloorId"].ToString()),
                dt.Rows[0]["IsFront"].ToString()=="0"?false:true,
                dt.Rows[0]["IsRear"].ToString()=="0"?false:true);

            distributeHandle.DestinationFloor = ElevatorFloorModel.Set(
                long.Parse(dt.Rows[1]["RealFloorId"].ToString()),
                dt.Rows[1]["IsFront"].ToString() == "0" ? false : true,
                dt.Rows[1]["IsRear"].ToString() == "0" ? false : true);

            distributeHandle.DestinationFloorName = dt.Rows[1]["Name"].ToString();
            distributeHandle.RealDeviceId = handleElevator.HandleElevatorDeviceId;
            distributeHandle.CardNumber = handleElevator.Sign;
            
            _logger.LogInformation($"二次派梯：SecondaryHandleElevator:{ JsonConvert.SerializeObject(distributeHandle, JsonUtil.JsonPrintSettings) } ");
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceService>();
                //service.SecondaryHandleElevator(handleElevator, distributeHandle);
                service.HandleElevator(handleElevator);
            }*/
            #endregion
            /*走1.5.4旧通道*/
            UnitManualHandleElevatorRequestModel Nmodel = new UnitManualHandleElevatorRequestModel();
            Nmodel.SourceFloorId = handleElevator.SourceFloorId;
            Nmodel.DestinationFloorIds = null;
            Nmodel.ElevatorGroupId = handleElevator.ElevatorGroupId;
            Nmodel.Sign = handleElevator.Sign;
            Nmodel.AccessType = handleElevator.AccessType;
            Nmodel.DeviceType = "FACE_CAMERA";
            Nmodel.DeviceId = string.Empty;
            Nmodel.DestinationFloorId = handleElevator.DestinationFloorId;
            Nmodel.HandleElevatorDeviceId = handleElevator.HandleElevatorDeviceId;
            
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceService>();
                service.HandleElevator(Nmodel);
            }
            return JsonConvert.SerializeObject(Obj._values);
        }
    }
    public class ClientRight
    { 
        public string Ip { get; set; }
        public string Sign { get; set; }
        public string Port { get; set; }
        public string CardId { get; set; } = string.Empty;
        public string PassFlg { get; set; } = "1";
        public string AccessType { get; set; }
    }
    public class FloorRight
    {
        public string RealFloorId { get; set; }
        public string Sign { get; set; }
        public string ElevatorGroupId { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }
        public string DestinationFloorId { get; set; }
        public string Sourceid { get; set; }
    }
}
