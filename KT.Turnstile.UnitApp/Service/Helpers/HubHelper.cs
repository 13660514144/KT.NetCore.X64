using HelperTools;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Elevator.Unit.Entity.Models;
using KT.Quanta.Common.Models;
using KT.Turnstile.Unit.ClientApp.Events;
using KT.Turnstile.Unit.ClientApp.Service.IServices;
using KT.Turnstile.Unit.Entity.Entities;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Service.Helpers
{
    public class HubHelper
    {
        private ConfigHelper _configHelper;
        private ILogger _logger;
        private HubDataHelper _initDataHelper;
        private AppSettings _appSettings;
        private IEventAggregator _eventAggregator;
        private IContainerProvider _containerProvider;


        public HubHelper(ILogger logger,
            ConfigHelper configHelper,
            HubDataHelper hubDataHelper,
            AppSettings appSettings,
           IEventAggregator eventAggregator,
           IContainerProvider containerProvider)
        {
            _logger = logger;
            _configHelper = configHelper;
            _initDataHelper = hubDataHelper;
            _appSettings = appSettings;
            _eventAggregator = eventAggregator;
            _containerProvider = containerProvider;
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

                   //logging.AddConsole();
                   //// Register your providers

                   //// Set the default log level to Information, but to Debug for SignalR-related loggers.
                   //logging.SetMinimumLevel(LogLevel.Debug);
               })
               .Build();

            //断开连接后重试 
            MasterHub.Closed += async (error) =>
            {
                _logger.LogInformation($"服务器连接已断开！");

                // 设备不在线
                _eventAggregator.GetEvent<IsOnlineEvent>().Publish(false);

                await MasterHub.StartAsync();
            };

            await ConnectServerAsync();
        }

        private async Task ConnectServerAsync()
        {
            MasterHub.On<List<TurnstileUnitPassRightEntity>>("AddOrEditPassRights", async (datas) =>
            {
                _logger.LogInformation($"HubOn:AddOrEditPassRights:start datas!! ");
                _logger.LogInformation($"HubOn:AddOrEditPassRights:{JsonConvert.SerializeObject(datas, JsonUtil.JsonPrintSettings)} ");
                var service = _containerProvider.Resolve<IPassRightService>();
                await service.AddOrUpdateAsync(datas);
                _logger.LogInformation($"HubOn:AddOrEditPassRights:end!! ");
            });
            
            MasterHub.On<TurnstileUnitPassRightEntity>("AddOrEditPassRight", async (message) =>
            {
                _logger.LogInformation($"HubOn:AddOrEditPassRight:start  message!! ");
                _logger.LogInformation($"HubOn:AddOrEditPassRight:{JsonConvert.SerializeObject(message, JsonUtil.JsonPrintSettings)} ");
                var service = _containerProvider.Resolve<IPassRightService>();
                await service.AddOrUpdateAsync(message);
                _logger.LogInformation($"HubOn:AddOrEditPassRight:end!! ");
            });
            
            MasterHub.On<string, long>("DeletePassRight", async (id, time) =>
            {
                _logger.LogInformation($"HubOn:DeletePassRight:start!! ");
                _logger.LogInformation($"HubOn:DeletePassRight:id:{id} time:{time}");

                var service = _containerProvider.Resolve<IPassRightService>();
                await service.Delete(id, time);
                _logger.LogInformation($"HubOn:DeletePassRight:end!! ");
            });

            MasterHub.On<List<TurnstileUnitCardDeviceEntity>>("AddOrEditCardDevices", async (datas) =>
            {
                _logger.LogInformation($"HubOn:AddOrEditCardDevices:start!! ");
                _logger.LogInformation($"HubOn:AddOrEditCardDevices:{JsonConvert.SerializeObject(datas, JsonUtil.JsonPrintSettings)} ");

                var service = _containerProvider.Resolve<ICardDeviceService>();
                await service.AddOrUpdateAsync(datas);
                _logger.LogInformation($"HubOn:AddOrEditCardDevices:end!! ");
            });

            MasterHub.On<TurnstileUnitCardDeviceEntity>("AddOrEditCardDevice", async (data) =>
            {
                _logger.LogInformation($"HubOn:AddOrEditCardDevice:start!! ");
                _logger.LogInformation($"HubOn:AddOrEditCardDevice:{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

                var service = _containerProvider.Resolve<ICardDeviceService>();
                await service.AddOrUpdateAsync(data);
                _logger.LogInformation($"HubOn:AddOrEditCardDevice:end!! ");
            });

            MasterHub.On<string, long>("DeleteCardDevice", async (id, time) =>
            {
                _logger.LogInformation($"HubOn:DeleteCardDevice:start!! ");
                _logger.LogInformation($"HubOn:DeleteCardDevice:id:{id} time:{time}");

                var service = _containerProvider.Resolve<ICardDeviceService>();
                await service.DeleteAsync(id, time);
                _logger.LogInformation($"HubOn:DeleteCardDevice:end!! ");
            });

            MasterHub.On<TurnstileUnitRightGroupEntity>("AddOrEditRightGroup", async (data) =>
            {
                _logger.LogInformation($"HubOn:AddOrEditRightGroup:start!! ");
                _logger.LogInformation($"HubOn:AddOrEditRightGroup:{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");
                var service = _containerProvider.Resolve<IRightGroupService>();
                await service.AddOrUpdateAsync(data);
                _logger.LogInformation($"HubOn:AddOrEditRightGroup:end!! ");
            });

            MasterHub.On<string, long>("DeleteRightGroup", async (id, time) =>
            {
                _logger.LogInformation($"HubOn:DeleteRightGroup:start!! ");
                _logger.LogInformation($"HubOn:DeleteRightGroup:id:{id} time:{time}");
                var service = _containerProvider.Resolve<IRightGroupService>();
                await service.DeleteAsync(id, time);
                _logger.LogInformation($"HubOn:DeleteRightGroup:end!! ");
            });

            //派梯结果返回
            MasterHub.On<HandleElevatorDisplayModel>("HandleElevatorSuccess", (handledElevatorSuccess) =>
            {
                _logger.LogInformation($"HubOn:HandleElevatorSuccess:start!! ");
                _logger.LogInformation($"HubOn:HandleElevatorSuccess: handledElevatorSuccess:{JsonConvert.SerializeObject(handledElevatorSuccess, JsonUtil.JsonPrintSettings)} ");

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
            //2021-07-25
            //APi服务请求 客户端派梯
            MasterHub.On<Dictionary<string,string>>("OtherApiRequest", (ApiDictObject) =>
            {                
                _logger.LogInformation($"APi服务请求 客户端派梯:{JsonConvert.SerializeObject(ApiDictObject, JsonUtil.JsonPrintSettings)} ");
                _eventAggregator.GetEvent<ApiDictObjectEvent>().Publish(ApiDictObject);
                _logger.LogInformation($"HubOn:ApiDictObjectEvent:end!! ");
                return null;
            });
            await StartLinkAsync();
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
                Restlt = await MasterHub.InvokeAsync<string>("RightTrunsite", obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get Trunsite remote invoke errror!! ");
            }
            return Restlt;
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

        public async Task InitLinkAsync()
        {
            //发送心跳 
            var hasError = await LinkAsync();

            //更数据 
            await InitDataAsync(false);
        }

        public async Task<bool> LinkAsync()
        {
            //发送心跳 
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

        public async void DeleteDataAsync()
        {
            // 正在加载数据
            _eventAggregator.GetEvent<IsLoadingDataEvent>().Publish(true);

            _logger.LogInformation($"更新删除权限！");
            try
            {
                await _initDataHelper.DeleteAsync(MasterHub);
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

        public async Task InitDataAsync(bool isForce)
        {
            // 正在加载数据
            _eventAggregator.GetEvent<IsLoadingDataEvent>().Publish(true);

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

        public async void InitRightAsync(bool hasError, bool isForce)
        {
            // 正在加载数据
            _eventAggregator.GetEvent<IsLoadingDataEvent>().Publish(true);

            _logger.LogInformation($"正在更新权限：hasError:{hasError} isForce:{isForce} ");
            try
            {
                await _initDataHelper.InitRightAsync(MasterHub, isForce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"更新权限错误! ");
                return;
            }
            finally
            {
                // 加载数据完成
                _eventAggregator.GetEvent<IsLoadingDataEvent>().Publish(false);
            }

            _logger.LogInformation("更新权限完成!!!!!!!!!!!!!");
        }

        public async Task PushPassRecordAsync(TurnstileUnitPassRecordEntity passRecord)
        {
            await MasterHub.InvokeAsync("TurnstilePushPassRecord", passRecord);
            _logger.LogInformation($"Send-TurnstilePushPassRecord：{JsonConvert.SerializeObject(passRecord, JsonUtil.JsonPrintSettings)} ");
        }
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
    }
}
