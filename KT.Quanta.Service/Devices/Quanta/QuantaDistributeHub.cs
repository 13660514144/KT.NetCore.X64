using ContralServer.CfgFileRead;
using HelperTools;
using KT.Common.Core.Utils;
using KT.Elevator.Unit.Dispatch.Entity.Models;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using KT.Quanta.Common.Models;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Hitachi.Handlers;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Services;
using KT.Quanta.Service.Turnstile.IServices;
using KT.Quanta.Service.Turnstile.Services;
using KT.Turnstile.Unit.Entity.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Hubs
{
    public class QuantaDistributeHub : Hub
    {

        private RemoteDeviceList _remoteDeviceList;
        private IServiceProvider _serviceProvider;
        private ILogger<QuantaDistributeHub> _logger;
        private string Dbconn = AppConfigurtaionServices.Configuration["ConnectionStrings:MysqlConnection"].ToString().Trim();
        public QuantaDistributeHub(RemoteDeviceList processorDeviceList,
            IServiceProvider serviceProvider,
            ILogger<QuantaDistributeHub> logger)
        {
            _remoteDeviceList = processorDeviceList;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        /// <summary>
        /// 远程设备连接
        /// </summary>
        /// <param name="seekSocket"></param>
        /// <returns>是否存在错误数据</returns>
        public async Task<bool> Link(SeekSocketModel seekSocket)
        {
            _logger.LogInformation("远程设备连接：seek:{0} ", JsonConvert.SerializeObject(seekSocket, JsonUtil.JsonPrintSettings));
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IProcessorService>();
                return await service.LinkReturnHasErrorAsync(seekSocket, Context.ConnectionId);
            }
        }
        /// <summary>
        /// 闸机权限  客户端通过Signlar
        /// </summary>
        /// <returns></returns>
        public async Task<string> RightTrunsite(ClientRight Right)
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
                    and d.id={Right.CardId}
                inner join processor p on d.ProcessorId=p.id
					and p.IpAddress='{Right.Ip}' and p.Port={Right.Port}
                inner join relay_device e 
                    on d.RelayDeviceId=e.id
                left join handle_elevator_device k 
					on d.HandleElevatorDeviceId=k.id
				left join elevator_group_floor j
					on a.FloorId=j.FloorId and k.ElevatorGroupId=j.ElevatorGroupId
				left join floor m on j.FloorId=m.id                
                where a.sign='{Right.Sign}' and a.AccessType='{Right.AccessType}'			
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
                    on  d.id={Right.CardId}
                inner join processor p on d.ProcessorId=p.id
					and p.IpAddress='{Right.Ip}' and p.Port={Right.Port}
                inner join relay_device e 
                    on d.RelayDeviceId=e.id
                left join handle_elevator_device k 
					on d.HandleElevatorDeviceId=k.id
				left join elevator_group_floor j
					on a.FloorId=j.FloorId and k.ElevatorGroupId=j.ElevatorGroupId
				left join floor m on j.FloorId=m.id 
                where a.sign='{Right.Sign}' and a.AccessType='{Right.AccessType}'				
                and (A.RightType='TURNSTILE' or A.RightType='ELEVATOR')
                and  unix_timestamp('{Today}')>=A.TimeNow/1000
                and  unix_timestamp('{Today}')<=A.TimeOut/1000
                order by RightType
            ";
                try
                {
                    if (Right.PassFlg.ToString().Trim() == "0")
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
                if (Right.PassFlg.ToString().Trim() == "1")
                {
                    _logger.LogInformation($"\r\nSQLpass===>{SqlPass}\r\n PassFlg={Right.PassFlg}");
                }
                else
                {
                    _logger.LogInformation($"\r\nSQL===>{Sql}\r\n PassFlg={Right.PassFlg}");
                }
                _logger.LogInformation($"\r\nnSQL tb===>{JsonConvert.SerializeObject(Tb)}");
            }
            return Result;
        }
        /// <summary>
        /// 客户端通过Signlar 请求哌替数据 公共权限  二次哌替
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetElevatorPassrightGuest(ClientRight Right)
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
						inner join elevator_group k on c.ElevatorGroupId=k.id							 
                        inner join elevator_group_floor b 
							on k.id=b.ElevatorGroupId					
						inner join floor a on b.FloorId=a.id
			            inner join edifice h on k.EdificeId=h.id																								
			        where a.IsPublic=1
							and c.IpAddress='{Right.Ip}'  										
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
            _logger.LogInformation($"\r\n来宾 Signlar SQL===>{SQL}");
            _logger.LogInformation($"Signlar GetElevatorPassrightGuest ={Result}");
            return Result;
        }
        /// <summary>
        /// 客户端通过Signlar 请求哌替数据 二次哌替
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetElevatorPassright(ClientRight Right)
        {   
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
                                string PassRightId = Tb.Rows[0]["PassRightId"].ToString();
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
                _logger.LogError($"Signlar GetElevatorRigth Error==>{ex}");
            }
            _logger.LogInformation($"Signlar GetElevatorRigth ={Result}");
            return Result;
        }
        /// <summary>
        /// 获取所有读卡器
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        public async Task<List<UnitCardDeviceEntity>> GetCardDevices(string deviceType)
        {
            _logger.LogInformation($"获取所有读卡器-GetCardDevices：connectionId:{Context.ConnectionId} deviceType:{deviceType} ");
            var remoteDevice = await GetRemoteByConnectIdAndDeviceTypeAsync(deviceType);

            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ICardDeviceService>();
                var results = await service.GetUnitByProcessorId(remoteDevice.RemoteDeviceInfo.DeviceId);
                return results;
            }
        }
       
        /// <summary>
        /// 获取所有通行权限
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        public async Task<List<UnitPassRightEntity>> GetPassRights(int page, int size)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IPassRightService>();
                var results = await service.GetUnitByPageAsync(page, size);
                return results;
            }
        }

        /// <summary>
        /// 获取所有错误数据
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        public async Task<List<UnitErrorModel>> GetUnitErrors(string deviceType, int page, int size)
        {
            _logger.LogInformation($"获取所有错误数据-GetUnitErrors：connectionId:{Context.ConnectionId} deviceType:{deviceType} ");
            var remoteDevice = await GetRemoteByConnectIdAndDeviceTypeAsync(deviceType);

            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IDistributeErrorService>();
                var results = await service.GetUnitPageAndDeleteByDeviceIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId, page, size);
                return results;
            }
        }

        /// <summary>
        /// 推送通行记录  派梯通行
        /// </summary>
        /// <param name="passRecord"></param>
        /// <returns></returns>
        public async Task PushPassRecord(UnitPassRecordEntity passRecord)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IPassRecordService>();
                await service.PushPassRecord(passRecord);
            }
        }

        /// <summary>
        /// 二次派梯
        /// </summary>
        /// <param name="floorId">目标楼层</param>
        /// <param name="handleElevatorDeviceId">派梯设备</param>
        public async Task HandleElevator(UnitManualHandleElevatorRequestModel handleElevator)
        {
            _logger.LogInformation($"派梯：handleElevator:{ JsonConvert.SerializeObject(handleElevator, JsonUtil.JsonPrintSettings) } ");
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceService>();
                await service.HandleElevator(handleElevator);
            }
        }

        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="floorId">目标楼层</param>
        /// <param name="handleElevatorDeviceId">派梯设备</param>
        public async Task SecondaryHandleElevator(UnitManualHandleElevatorRequestModel handleElevator)
        {
            _logger.LogInformation($"二次派梯：SecondaryHandleElevator:{ JsonConvert.SerializeObject(handleElevator, JsonUtil.JsonPrintSettings) } ");
           /* using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceService>();
                await service.SecondaryHandleElevator(handleElevator);
            }*/
        }

        /// <summary>
        /// 派梯，根据卡号与派梯设备派梯
        /// </summary>
        /// <param name="rightHandleElevator">派梯参数</param> 
        public async Task<HandleElevatorDisplayModel> RightHandleElevator(UintRightHandleElevatorRequestModel rightHandleElevator)
        {
            _logger.LogInformation($"派梯：RightHandleElevator:{ JsonConvert.SerializeObject(rightHandleElevator, JsonUtil.JsonPrintSettings) } ");
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceService>();
                var messageKey = IdUtil.NewId();
                return await service.RightHandleElevator(rightHandleElevator, messageKey);
            }
        }

        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="floorId">目标楼层</param>
        /// <param name="handleElevatorDeviceId">派梯设备</param>
        public async Task ManualHandleElevator(UnitManualHandleElevatorRequestModel handleElevator)
        {
            _logger.LogInformation($"派梯：ManualHandleElevator:{ JsonConvert.SerializeObject(handleElevator, JsonUtil.JsonPrintSettings) } ");
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceService>();
                await service.ManualHandleElevator(handleElevator);
            }
        }

        /// <summary>
        /// 上传二次派梯输入设备
        /// </summary> 
        public async Task HandleElevatorInputDevices(List<UnitHandleElevatorInputDeviceEntity> inputDevices)
        {
            _logger.LogInformation($"HandleElevatorInputDevices：inputDevices:{ JsonConvert.SerializeObject(inputDevices, JsonUtil.JsonPrintSettings) } ");
            var remoteDevice = await GetRemoteByConnectIdAsync();

            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IHandleElevatorInputDeviceService>();
                await service.AddOrEditUnitAsync(inputDevices, remoteDevice);
            }
        }

        /// <summary>
        /// 获取二次派梯输入设备
        /// </summary> 
        public async Task<UnitHandleElevatorDeviceModel> GetHandleElevatorDevice(string deviceType)
        {
            _logger.LogInformation($"更新派梯设备-GetHandleElevatorDevice：connectionId:{Context.ConnectionId} deviceType:{deviceType} ");
            var remoteDevice = await GetRemoteByConnectIdAndDeviceTypeAsync(deviceType);

            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceService>();
                return await service.GetUnitByDeviceId(remoteDevice.RemoteDeviceInfo.DeviceId);
            }
        }

        private async Task<IRemoteDevice> GetRemoteByConnectIdAndDeviceTypeAsync(string deviceType)
        {
            _logger.LogInformation($"获取远程设备：connectionId:{Context.ConnectionId} deviceType:{deviceType} ");
            var remoteDevices = await _remoteDeviceList.GetByConnectionIdAndDeviceTypeAsync(Context.ConnectionId, deviceType);
            if (remoteDevices?.FirstOrDefault() == null)
            {
                throw new Exception($"获取远程设备错误：找不到远程设备：connectionId:{Context.ConnectionId} deviceType:{deviceType} ");
            }

            var remoteDevice = remoteDevices.FirstOrDefault();
            if (remoteDevices.Count > 1)
            {
                _logger.LogWarning($"获取到多个远程设备设备：remoteDevices:{JsonConvert.SerializeObject(remoteDevices, JsonUtil.JsonPrintSettings)} ");
            }

            _logger.LogInformation($"获取到的远程设备设备：remoteDevice:{JsonConvert.SerializeObject(remoteDevice, JsonUtil.JsonPrintSettings)} ");

            return remoteDevice;
        }

        private async Task<IRemoteDevice> GetRemoteByConnectIdAsync()
        {
            _logger.LogInformation($"获取远程设备：connectionId:{Context.ConnectionId} ");
            var remoteDevices = await _remoteDeviceList.GetByConnectionIdAsync(Context.ConnectionId);
            if (remoteDevices?.FirstOrDefault() == null)
            {
                throw new Exception($"获取远程设备错误：找不到远程设备：connectionId:{Context.ConnectionId} ");
            }

            var remoteDevice = remoteDevices.FirstOrDefault();
            if (remoteDevices.Count > 1)
            {
                _logger.LogWarning($"获取到多个远程设备设备：remoteDevices:{JsonConvert.SerializeObject(remoteDevices, JsonUtil.JsonPrintSettings)} ");
            }

            _logger.LogInformation($"获取到的远程设备设备：remoteDevice:{JsonConvert.SerializeObject(remoteDevice, JsonUtil.JsonPrintSettings)} ");

            return remoteDevice;
        }

        /// <summary>
        /// 获取二次派梯输入设备
        /// </summary> 
        public async Task<UnitFaceModel> GetFaceBytesById(string id)
        {
            _logger.LogInformation($"获取人脸-GetFaceBytesById：id:{id} ");
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IFaceInfoService>();
                return await service.GetBytesById(id);
            }
        }

        /// <summary>
        /// 获取所有读卡器
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        public async Task<List<TurnstileUnitCardDeviceEntity>> TurnstileGetCardDevices(string deviceType)
        {
            _logger.LogInformation($"边缘处理器获取读卡器信息-TurnstileGetCardDevices：connectionId:{Context.ConnectionId} deviceType:{deviceType} ");
            var remoteDevice = await GetRemoteByConnectIdAndDeviceTypeAsync(deviceType);

            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ITurnstileCardDeviceService>();
                var results = await service.GetUnitByProcessorId(remoteDevice.RemoteDeviceInfo.DeviceId);
                _logger.LogInformation($"边缘处理器获取读卡器信息：data:{JsonConvert.SerializeObject(results, JsonUtil.JsonPrintSettings)} ");
                return results;
            }
        }

        /// <summary>
        /// 获取所有通行权限
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        public async Task<List<TurnstileUnitRightGroupEntity>> TurnstileGetRightGroups(int page, int size)
        {
            _logger.LogInformation("边缘处理器获取权限组信息：page:{0} size:{1} ", page, size);
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ITurnstileCardDeviceRightGroupService>();
                var results = await service.GetUnitAllAsync(page, size);
                _logger.LogInformation($"边缘处理器获取权限组信息：data:{JsonConvert.SerializeObject(results, JsonUtil.JsonPrintSettings)} ");
                return results;
            }
        }

        /// <summary>
        /// 获取所有通行权限
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        public async Task<List<TurnstileUnitPassRightEntity>> TurnstileGetPassRights(int page, int size)
        {
            _logger.LogInformation("边缘处理器获取通行权限信息：page:{0} size:{1} ", page, size);
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ITurnstilePassRightService>();
                var results = await service.GetUnitPageAsync(page, size);
                _logger.LogInformation($"边缘处理器获取通行权限信息：data:{JsonConvert.SerializeObject(results, JsonUtil.JsonPrintSettings)} ");
                return results;
            }
        }

        /// <summary>
        /// 获取所有通行权限
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        public async Task<bool> IsExistsPassRightById(string id)
        {
            _logger.LogInformation($"查询是否存在通行权限信息：id:{id} ");
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ITurnstilePassRightService>();
                var result = await service.IsExistsAsync(id);
                _logger.LogInformation($"查询是否存在通行权限信息：data:{result} ");
                return result;
            }
        }

        /// <summary>
        /// 推送通行记录  增加控制器方法
        /// </summary>
        /// <param name="passRecord"></param>
        /// <returns></returns>
        public async Task TurnstilePushPassRecord(TurnstileUnitPassRecordEntity passRecord)
        {
            _logger.LogInformation($"边缘处理器上传通行记录：data:{ JsonConvert.SerializeObject(passRecord, JsonUtil.JsonPrintSettings)} ");
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ITurnstilePassRecordService>();
                await service.PushPassRecord(passRecord);
                _logger.LogInformation("边缘处理器上传通行记录完成！");
            }
        }

        /// <summary>
        /// 日立派梯返回结果 -2021-07-30
        /// </summary>
        /// <param name="passRecord"></param>
        /// <returns></returns>
        public async Task HitachiCallResponse(UnitDispatchReceiveHandleElevatorModel response)
        {
            _logger.LogInformation($"日立派梯返回结果：data:{ JsonConvert.SerializeObject(response, JsonUtil.JsonPrintSettings)} ");
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IHitachiCallReponseHandler>();
                await service.PaddleAsync(response);
                _logger.LogInformation("日立派梯返回结果处理完成！");
            }
        }

        /// <summary>
        /// 获取utc时间戳
        /// </summary>
        /// <returns>utc时间戳</returns>
        public Task<long> GetUtcMillis()
        {
            var utcMillis = DateTimeUtil.UtcNowMillis();
            _logger.LogInformation($"获取utc时间戳：{utcMillis} ");

            return Task.FromResult(utcMillis);
        }

        /// <summary>
        /// 连接事件
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("远程设备连接：connectionId:{0} ", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation($"远程设备断开连接：connectionId:{Context.ConnectionId} ex:{exception} ");
            await _remoteDeviceList.DislinkAsync(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
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
