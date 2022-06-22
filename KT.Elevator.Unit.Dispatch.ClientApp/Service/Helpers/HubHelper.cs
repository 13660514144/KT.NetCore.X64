using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Elevator.Unit.Dispatch.ClientApp.Events;
using KT.Elevator.Unit.Dispatch.Entity.Entities;
using KT.Elevator.Unit.Dispatch.Entity.Models;
using KT.Quanta.Common.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Service.Helpers
{
    public class HubHelper
    {
        private ConfigHelper _configHelper;
        private ILogger _logger;
        private AppSettings _appSettings;
        private IEventAggregator _eventAggregator;

        public HubHelper()
        {
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
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

                await MasterHub.StartAsync();
            };
            await ConnectServerAsync();
        }

        public Func<string, Task> AppendMessageAction;
        public Func<string, Task> ShowElevatorAction;

        private async Task ConnectServerAsync()
        {
            //派梯
            MasterHub.On<UnitDispatchSendHandleElevatorModel>("HandleElevator", (model) =>
            {
                _logger.LogInformation($"HubOn:HandleElevator:start!! ");
                _logger.LogInformation($"HubOn:HandleElevator:HandleElevator:{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)}");

                _eventAggregator.GetEvent<AutoHandleElevatorEvent>().Publish(model);
                _logger.LogInformation($"HubOn:HandleElevator:end!! ");
            });

            //派梯
            MasterHub.On<UnitDispatchSendHandleElevatorModel>("HandleElevators", (model) =>
            {
                _logger.LogInformation($"HubOn:HandleElevators:start!! ");
                _logger.LogInformation($"HubOn:HandleElevators:HandleElevators:{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)}");

                _eventAggregator.GetEvent<ManualHandleElevatorEvent>().Publish(model);
                _logger.LogInformation($"HubOn:HandleElevators:end!! ");
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

            await LinkAsync();
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
            //_logger.LogInformation($"Send-Link：{JsonConvert.SerializeObject(sendObj, JsonUtil.JsonPrintSettings)}");
            return hasError;
        }

        public async Task PushPassRecordAsync(UnitDispatchPassRecordEntity passRecord)
        {
            await MasterHub.InvokeAsync("PushPassRecord", passRecord);
            _logger.LogInformation($"Send-PushPassRecord：{JsonConvert.SerializeObject(passRecord, JsonUtil.JsonPrintSettings)}");
        }

    }
}
