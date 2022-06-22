using ContralServer.CfgFileRead;
using HelperTools;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Secondary.ClientApp.Events;
using KT.Elevator.Unit.Secondary.ClientApp.Models;
using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using KT.Quanta.Common.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Helpers
{
    public class HubHelper
    {
        private ConfigHelper _configHelper;
        private ILogger _logger;
        private InitDataHelper _initDataHelper;
        private AppSettings _appSettings;
        private IEventAggregator _eventAggregator;
        private IContainerProvider _containerProvider;

        public HubHelper()
        {
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _initDataHelper = ContainerHelper.Resolve<InitDataHelper>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _containerProvider = ContainerHelper.Resolve<IContainerProvider>();
        }

        public HubConnection MasterHub;

        public async void StartAsync()
        {
            _logger.LogInformation($"开始连接：ip:{_appSettings.ServerIp} port:{_appSettings.ServerPort} ");

            MasterHub = new HubConnectionBuilder()
                .WithAutomaticReconnect(new[]
                {
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(7),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(15),
                    TimeSpan.FromSeconds(30),
                    TimeSpan.FromSeconds(20),
                    TimeSpan.FromSeconds(7),
                    TimeSpan.FromSeconds(3)
                })
               .WithUrl($"http://{_appSettings.ServerIp}:{_appSettings.ServerPort}/distribute")
               //.WithAutomaticReconnect()
               .ConfigureLogging(logging =>
               {
                   logging.AddConsole();
                   logging.SetMinimumLevel(LogLevel.Information);
               })
               .Build();

            //断开连接后重试
            MasterHub.Closed += async (error) =>
            {
                _logger.LogInformation($"服务器连接已断开：ex:{error} ");

                // 设备不在线
                _eventAggregator.GetEvent<IsOnlineEvent>().Publish(false);

                await MasterHub.StartAsync();
            };
            await ConnectServerAsync();
        }

        public Func<string, Task> AppendMessageAction;
        public Func<string, Task> ShowElevatorAction;

        private async Task ConnectServerAsync()
        {           
            //新增或修改权限
            MasterHub.On<List<UnitPassRightEntity>>("AddOrEditPassRights", async (datas) =>
            {
                _logger.LogInformation($"HubOn:AddOrEditPassRights:start!! ");
                //_logger.LogInformation($"HubOn:AddOrEditPassRights:{JsonConvert.SerializeObject(datas, JsonUtil.JsonPrintSettings)} ");
               /*  不做权限处理 */
                var service = ContainerHelper.Resolve<IPassRightService>();

                datas = await service.AddOrUpdateAsync(datas);

                _eventAggregator.GetEvent<AddOrEditPassRightsEvent>().Publish(datas);
                
                _logger.LogInformation($"HubOn:AddOrEditPassRights:end!! ");
            });

            //新增或修改权限
            MasterHub.On<UnitPassRightEntity>("AddOrEditPassRight", async (passRight) =>
            {
                _logger.LogInformation($"HubOn:AddOrEditPassRight:start!! ");
               // _logger.LogInformation($"HubOn:AddOrEditPassRight:{JsonConvert.SerializeObject(passRight, JsonUtil.JsonPrintSettings)} ");
                /*  不做权限处理*/
                var service = ContainerHelper.Resolve<IPassRightService>();
                passRight = await service.AddOrUpdateAsync(passRight);                

                _eventAggregator.GetEvent<AddOrEditPassRightsEvent>().Publish(new List<UnitPassRightEntity>() { passRight });
                
                _logger.LogInformation($"HubOn:AddOrEditPassRight:end!! ");
            });

            //删除权限
            MasterHub.On<string, long>("DeletePassRight", async (id, time) =>
            {
                _logger.LogInformation($"HubOn:DeletePassRight:start!! ");
                //_logger.LogInformation($"HubOn:DeletePassRight:id:{id} time:{time}");
                /*  不做权限处理 */
                var service = ContainerHelper.Resolve<IPassRightService>();
                await service.Delete(id, time);
                _eventAggregator.GetEvent<DeletePassRightEvent>().Publish(id);
                
                _logger.LogInformation($"HubOn:DeletePassRight:end!! ");
            });

            //新增或修改读卡器
            MasterHub.On<List<UnitCardDeviceEntity>>("AddOrEditCardDevices", async (datas) =>
            {
                _logger.LogInformation($"HubOn:AddOrEditCardDevices:start!! ");
                //_logger.LogInformation($"HubOn:AddOrEditCardDevices:{JsonConvert.SerializeObject(datas, JsonUtil.JsonPrintSettings)} ");

                var service = ContainerHelper.Resolve<ICardDeviceService>();
                await service.AddOrUpdateAsync(datas);
                _logger.LogInformation($"HubOn:AddOrEditCardDevices:end!! ");
            });

            //新增或修改读卡器
            MasterHub.On<UnitCardDeviceEntity>("AddOrEditCardDevice", async (data) =>
            {
                _logger.LogInformation($"HubOn:AddOrEditCardDevice:start!! ");
                //_logger.LogInformation($"HubOn:AddOrEditCardDevice:{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

                var service = ContainerHelper.Resolve<ICardDeviceService>();
                await service.AddOrUpdateAsync(data);
                _logger.LogInformation($"HubOn:AddOrEditCardDevice:end!! ");

            });

            //删除读卡器
            MasterHub.On<string, long>("DeleteCardDevice", async (id, time) =>
            {
                _logger.LogInformation($"HubOn:DeleteCardDevice:start!! ");
                //_logger.LogInformation($"HubOn:DeleteCardDevice:id:{id} time:{time}");

                var service = ContainerHelper.Resolve<ICardDeviceService>();
                await service.Delete(id, time);
                _logger.LogInformation($"HubOn:DeleteCardDevice:end!! ");
            });

            //派梯结果返回
            MasterHub.On<HandleElevatorDisplayModel>("HandleElevatorSuccess", (handledElevatorSuccess) =>
            {
                _logger.LogInformation($"HubOn:HandleElevatorSuccess:start!! ");
                _logger.LogInformation($"HubOn:HandleElevatorSuccess:handledElevatorSuccess:{JsonConvert.SerializeObject(handledElevatorSuccess, JsonUtil.JsonPrintSettings)} ");

                _eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Publish(handledElevatorSuccess);
                _logger.LogInformation($"HubOn:HandleElevatorSuccess:end!! ");
            });

            //派梯结果返回
            MasterHub.On<HandleElevatorErrorModel>("HandleElevatorError", (handleElevatorError) =>
            {
                _logger.LogInformation($"HubOn:HandleElevatorError:start!! ");
                _logger.LogInformation($"HubOn:HandleElevatorError:HandleElevatorError:{JsonConvert.SerializeObject(handleElevatorError, JsonUtil.JsonPrintSettings)} ");

                _eventAggregator.GetEvent<HandledElevatorErrorEvent>().Publish(handleElevatorError);
                _logger.LogInformation($"HubOn:HandleElevatorError:end!! ");
            });

            //新增或修改派梯设备
            MasterHub.On<UnitHandleElevatorDeviceModel>("AddOrEditHandleElevatorDevice", async (model) =>
            {
                _logger.LogInformation($"HubOn:AddOrEditHandleElevatorDevice:start!! ");
                _logger.LogInformation($"HubOn:AddOrEditHandleElevatorDevice:model:{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");

                var service = ContainerHelper.Resolve<IHandleElevatorDeviceService>();
                _configHelper.LocalConfig = await service.AddOrEditAsync(model, _configHelper.LocalConfig);
                _logger.LogInformation($"HubOn:AddOrEditHandleElevatorDevice:end!! ");
            });

            //删除派梯设备
            MasterHub.On<string>("DeleteHandleElevatorDevice", async (handleElevatorDeviceId) =>
            {
                _logger.LogInformation($"HubOn:DeleteHandleElevatorDevice:start!! ");
                _logger.LogInformation($"HubOn:DeleteHandleElevatorDevice:handleElevatorDeviceId:{handleElevatorDeviceId}");
                var service = ContainerHelper.Resolve<IHandleElevatorDeviceService>();
                await service.DeleteAsync(handleElevatorDeviceId);
                _logger.LogInformation($"HubOn:DeleteHandleElevatorDevice:end!! ");
            });

            //派梯
            MasterHub.On<UintRightHandleElevatorRequestModel>("RightHandleElevator", (model) =>
            {
                _logger.LogInformation($"HubOn:RightHandleElevator:start!! ");
                _logger.LogInformation($"HubOn:RightHandleElevator:RightHandleElevator:{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");

                var handleModel = new UnitHandleElevatorModel();
                handleModel.AccessType = model.AccessType;
                handleModel.DeviceId = model.DeviceId;
                handleModel.DeviceType = model.DeviceType;

                handleModel.HandleElevatorRight = new UnitHandleElevatorRightModel();
                handleModel.HandleElevatorRight.PassRightSign = model.Sign;

                _eventAggregator.GetEvent<CardInputedEvent>().Publish(handleModel);
                _logger.LogInformation($"HubOn:RightHandleElevator:end!! ");
            });

            await StartLinkAsync();
        }

        public async Task StartLinkAsync()
        {
            try
            {
                await MasterHub.StartAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("连接服务器出错：{0} ", ex);
                _logger.LogInformation($"连接服务器出错：{ex.Message} ");
            }

            _logger.LogInformation("Connection started!!!!!!!!!!!!!");

            await InitLinkAsync();
        }
        /// <summary>
        /// SignLar 请求权限
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<string> ReqElevatorPassright(ReqElvator obj)
        {
            string Restlt = string.Empty;
            try
            {
                Restlt=await MasterHub.InvokeAsync<string>("GetElevatorPassright", obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetElevatorPassright remote invoke errror!! ");
            }
            return Restlt;
        }
        /// <summary>
        /// SignLar 请求权限 公共权限
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<string> ReqElevatorPassrightGuest(ReqElvator obj)
        {
            string Restlt = string.Empty;
            try
            {
                Restlt = await MasterHub.InvokeAsync<string>("GetElevatorPassrightGuest", obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetElevatorPassrightGuest remote invoke errror!! ");
            }
            return Restlt;
        }
        /// <summary>
        /// 哌替请求
        /// </summary>
        /// <param name="handleElevator"></param>
        /// <returns></returns>
        internal async Task HandleElevatorAsync(UnitManualHandleElevatorRequestModel handleElevator)
        {
            ////使用子类出错，需手动转换
            //var data = new UnitManualHandleElevatorRequestModel();
            //data.SourceFloorId = handleElevator.SourceFloorId;
            //data.DestinationFloorId = handleElevator.DestinationFloorId;
            //data.HandleElevatorDeviceId = handleElevator.HandleElevatorDeviceId;
            try
            {
                await MasterHub.InvokeAsync("HandleElevator", handleElevator);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HandleElevator remote invoke errror!! ");
            }
        }

        public async Task InitLinkAsync()
        {
            //发送心跳 
            bool hasError = await LinkAsync();

            //更数据
            InitDataAsync(false);
        }

        public async Task<bool> LinkAsync()
        {
            var sendObj = new SeekSocketModel();
            sendObj.DeviceType = _appSettings.DeviceType;

            sendObj.ClientIp = MachineUtil.GetIp()?.FirstOrDefault(x => x.StartsWith(_appSettings.StartWithIp));
            sendObj.ClientPort = _appSettings.Port;
            if (string.IsNullOrEmpty(sendObj.ClientIp))
            {
                sendObj.ClientIp = _appSettings.StartWithIp;
            }

            var hasError = await MasterHub.InvokeAsync<bool>("Link", sendObj);
            _logger.LogInformation($"Send-Link：{JsonConvert.SerializeObject(sendObj, JsonUtil.JsonPrintSettings)} ");
            return hasError;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="hasError">是否更新错误数据</param>
        /// <param name="isForce">是否更新所有数据</param>
        /// <returns></returns>
        public async void InitDataAsync(bool isForce)
        {
            // 正在加载数据

            //_eventAggregator.GetEvent<IsLoadingDataEvent>().Publish(true);
            _logger.LogInformation($"正在更新数据：isForce:{isForce} ");
            try
            {
                await _initDataHelper.InitAsync(MasterHub, isForce);
            }
            catch (Exception ex)
            {
                _logger.LogError($"更新数据错误：{ex} ");
                return;
            }
            finally
            {
                // 加载数据完成
                _eventAggregator.GetEvent<IsLoadingDataEvent>().Publish(false);
            }
            _logger.LogInformation("更新数据完成!!!!!!!!!!!!!");
        }

        public async Task PushPassRecordAsync(UnitPassRecordEntity passRecord)
        {          
            await MasterHub.InvokeAsync("PushPassRecord", passRecord);
            _logger.LogInformation($"Send-PushPassRecord-Api：{JsonConvert.SerializeObject(passRecord, JsonUtil.JsonPrintSettings)} ");
        }

        public async Task<UnitFaceModel> GetFaceBytesByIdAsync(string faceId)
        {
            var faceModel = await MasterHub.InvokeAsync<UnitFaceModel>("GetFaceBytesById", faceId);
            _logger.LogInformation($"GetFaceBytesById：faceId:{ faceId }");
            return faceModel;
        }

        public async void DeleteDataAsync()
        {
            // 正在加载数据
            _eventAggregator.GetEvent<IsLoadingDataEvent>().Publish(true);

            _logger.LogInformation($"更新删除权限！");
            try
            {
                await DeletePassRight(MasterHub);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"更新删除权限错误! ");
                return;
            }
            finally
            {
                // 加载数据完成
                _eventAggregator.GetEvent<IsLoadingDataEvent>().Publish(false);
            }
            _logger.LogInformation($"更新删除权限完成！！！！");
        }

        private async Task DeletePassRight(HubConnection masterHub)
        {
            int page = 1;
            int size = 1;
            while (true)
            {
                var hasNotExists = false;
                _logger.LogInformation($"更新删除权限：page:{page} size:{size}");
                var passRightService = _containerProvider.Resolve<IPassRightService>();
                var passRightPage = await passRightService.GetPageWithDetailAsync(page, size);
                if (passRightPage.List?.FirstOrDefault() == null)
                {
                    //没有数据退出操作
                    break;
                }

                foreach (var item in passRightPage.List)
                {
                    var isExists = await masterHub.InvokeAsync<bool>("IsExistsPassRightById", item.Id);
                    if (!isExists)
                    {
                        hasNotExists = true;
                        _logger.LogInformation($"更新删除权限：id:{item.Id} sign:{item.Sign}");
                        await passRightService.Deletesync(item);
                    }
                }
                //存在错误继续校验当前页面，否则删除了数据查询下一页会遗漏掉数据
                if (!hasNotExists)
                {
                    page++;
                }

            }
            _logger.LogInformation($"更新删除权限完成！");
        }
    }
}
