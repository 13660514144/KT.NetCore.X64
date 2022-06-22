using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ContralServer.CfgFileRead;
using KT.Common.WpfApp.Helpers;

namespace HelperTools
{
    public class RightGetModel
    {        
        public DataTable GetRightFromMysql()
        {
            DataTable Dt = new DataTable();
            return Dt;
        }
        public JArray GetRightFromApi()
        {
            JArray Dt = new JArray();
            return Dt;
        }
        /// <summary>
        /// 选层派梯 MYsql 查询数据方法 日立
        /// </summary>
        /// <param name="Sign"></param>
        /// <param name="RealFloorId"></param>
        /// <param name="EleGroupId"></param>
        /// <param name="Dbconn"></param>
        /// <returns></returns>
        public static DataTable GetLastRightFromMysql(string Sign, 
            string RealFloorId , string EleGroupId ,string Dbconn)
        {
            DataTable dt = new DataTable();
            string Ip = AppConfigurtaionServices.Configuration["AppSettings:StartWithIp"].ToString().Trim();
            string Port = AppConfigurtaionServices.Configuration["AppSettings:Port"].ToString().Trim();
            string Sql = $@"
                    select distinct 
                    a.realfloorid,a.elevatorgroupid,b.CommBox,b.id,c.Name,
                    b.DeviceType,'{Sign}' as Sign,kk.AccessType
                    from elevator_group_floor a 
                    inner join handle_elevator_device b
                    on a.elevatorgroupid=b.elevatorgroupid 
                    inner join floor c on a.FloorId=c.id
					inner join 
					(
						SELECT distinct id,AccessType,sign   from pass_right 
						where sign='{Sign}' and RightType='ELEVATOR'											
				    ) kk on Sign=kk.sign
                    where 
                    a.elevatorgroupid={EleGroupId} 
                    and b.DeviceType='ELEVATOR_SECONDARY'
                    and b.IpAddress='{Ip}' and b.Port={Port}
                    and  a.realfloorid={RealFloorId}  LIMIT 1			
                ";
            try
            {
                IDalMySql dal;
                dal = DalFactory.CreateMysqlDal("mysql", Dbconn);
                dt = dal.MySqlListData(Sql);
            }
            catch (Exception ex)
            {
                //_Log.LogInformation($"数据查询错误 {ex}");
                return dt;
            }
            if (dt.Rows.Count == 0)
            {
                return dt;
            }
            return dt;
        }
        /// <summary>
        /// 选层派梯 API方法 日立
        /// </summary>
        /// <param name="Sign"></param>
        /// <param name="RealFloorId"></param>
        /// <param name="EleGroupId"></param>
        /// <returns></returns>
        public static JObject GetLastRightFromApi(string Sign,string RealFloorId,string EleGroupId)
        {
            dynamic JsonObj = new DynamicObj();
            JArray Dt = new JArray();
            string Controller = "api/PassRecoadGuest/GetFloorRigth";
            Dictionary<string, string> SendData = new Dictionary<string, string>
            {
                {"Ip", AppConfigurtaionServices.Configuration["AppSettings:StartWithIp"].ToString().Trim() },
                {"Sign", Sign},
                {"Port", AppConfigurtaionServices.Configuration["AppSettings:Port"].ToString().Trim() },
                {"RealFloorId", RealFloorId},
                {"ElevatorGroupId", EleGroupId}
            };
            JObject JsonData;
            try
            {
                
                string Result = ApiGetDataWording(Controller, SendData);
                JsonData = JObject.Parse(Result);                
            }
            catch (Exception ex)
            {
                //_Log.LogInformation($"\r\n==>目的派梯错误：{ex}");
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = "查询错误";
                JsonObj.Data = new JArray();
                JsonData = JObject.Parse(JsonConvert.SerializeObject(JsonObj._values));
            }
            return JsonData;
        }
        public static string ApiRequestMothed(string CollerName, Dictionary<string, string> Obj,string URLip,int URLport)
        {
            
            string Url = $"http://{URLip}:{URLport}/";


            string Result = string.Empty;
            try
            {
                //必需同步获取
                HostReqModel HostReq = new HostReqModel();
                Result = HostReq.SendHttpRequest($"{Url}{CollerName}", JsonConvert.SerializeObject(Obj));
            }
            catch (Exception ex)
            {
                //_Log.LogError($"\r\nAPI 错误 Result==>{JsonConvert.SerializeObject(Result)}");
                //_Log.LogError($"\r\nAPI 错误==>{ex}");
                dynamic JsonObj = new DynamicObj();
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = ex.Message.ToString();
                JsonObj.Data = new JArray();
                Result = JsonConvert.SerializeObject(JsonObj._values);
            }
            return Result;
        }
        public static string ApiGetDataWording(string CollerName, Dictionary<string, string> Obj)
        {
            string Uri = AppConfigurtaionServices.Configuration["AppSettings:ApiServer"].ToString();
            string Port = AppConfigurtaionServices.Configuration["AppSettings:ApiPort"].ToString();
            string Url = $"http://{Uri}:{Port}/";
            
            
            string Result = string.Empty;
            try
            {
                //必需同步获取
                HostReqModel HostReq = new HostReqModel();
                Result = HostReq.SendHttpRequest($"{Url}{CollerName}", JsonConvert.SerializeObject(Obj));
            }
            catch (Exception ex)
            {
                //_Log.LogError($"\r\nAPI 错误 Result==>{JsonConvert.SerializeObject(Result)}");
                //_Log.LogError($"\r\nAPI 错误==>{ex}");
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
        /// 选层派梯 MYsql 查询数据方法 三菱
        /// </summary>
        /// <param name="Sign"></param>
        /// <param name="RealFloorId"></param>
        /// <param name="EleGroupId"></param>
        /// <param name="Dbconn"></param>
        /// <returns></returns>
        public static DataTable GetLastRightFromMysql_Mitsubishi_ELSGW(string Sign,
            string RealFloorId, string EleGroupId, string Dbconn)
        {
            DataTable dt = new DataTable();
            string Ip = AppConfigurtaionServices.Configuration["AppSettings:StartWithIp"].ToString().Trim();
            string Port = AppConfigurtaionServices.Configuration["AppSettings:Port"].ToString().Trim();
            string Sql = $@"
                    select distinct 
                    a.realfloorid,a.elevatorgroupid,b.CommBox,b.id,c.Name,
                    b.FloorId as SourceFloorId,
                    b.DeviceType,'{Sign}' as Sign,kk.AccessType,a.FloorId
                    
                    from elevator_group_floor a 
                    inner join handle_elevator_device b
                    on a.elevatorgroupid=b.elevatorgroupid 
                    inner join floor c on a.FloorId=c.id
					inner join 
					(
						SELECT distinct id,AccessType,sign   from pass_right 
						where sign='{Sign}' and RightType='ELEVATOR'											
				    ) kk on Sign=kk.sign
                    where 
                    a.elevatorgroupid={EleGroupId} 
                    and b.DeviceType='ELEVATOR_SECONDARY'
                    and b.IpAddress='{Ip}' and b.Port={Port}
                    and  a.realfloorid={RealFloorId}  LIMIT 1			
                ";
            try
            {
                IDalMySql dal;
                dal = DalFactory.CreateMysqlDal("mysql", Dbconn);
                dt = dal.MySqlListData(Sql);
            }
            catch (Exception ex)
            {
                //_Log.LogInformation($"数据查询错误 {ex}");
                return dt;
            }        
            return dt;
        }
        /// <summary>
        /// 选层派梯 API方法 三菱
        /// </summary>
        /// <param name="Sign"></param>
        /// <param name="RealFloorId"></param>
        /// <param name="EleGroupId"></param>
        /// <returns></returns>
        public static JObject GetLastRightFromApi_Mitsubishi_ELSGW(string Sign, 
            string RealFloorId, 
            string EleGroupId,string DestinationFloorId,string Sourceid)
        {
            dynamic JsonObj = new DynamicObj();
            JArray Dt = new JArray();
            string Controller = "api/PassRecoadGuest/GetFloorRigthMitsubishi";
            Dictionary<string, string> SendData = new Dictionary<string, string>
            {
                {"Ip", AppConfigurtaionServices.Configuration["AppSettings:StartWithIp"].ToString().Trim() },
                {"Sign", Sign},
                {"Port", AppConfigurtaionServices.Configuration["AppSettings:Port"].ToString().Trim() },
                {"RealFloorId", RealFloorId},
                {"ElevatorGroupId", EleGroupId},
                { "DestinationFloorId",DestinationFloorId},
                { "Sourceid",Sourceid}
            };
            JObject JsonData;
            try
            {

                string Result = ApiGetDataWording(Controller, SendData);
                JsonData = JObject.Parse(Result);
            }
            catch (Exception ex)
            {
                //_Log.LogInformation($"\r\n==>目的派梯错误：{ex}");
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = $"{ex}";
                JsonObj.Data = new JArray();
                JsonData = JObject.Parse(JsonConvert.SerializeObject(JsonObj._values));
            }
            return JsonData;
        }
    }
}
