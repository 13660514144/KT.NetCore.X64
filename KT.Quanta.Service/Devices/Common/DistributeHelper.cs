using KT.Common.Core.Utils;
using KT.Quanta.Service.Hubs;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    /// <summary>
    /// 分发数据
    /// </summary>
    public class DistributeHelper
    {
        private ILogger<DistributeHelper> _logger;
        private IHubContext<QuantaDistributeHub> _distributeHub;
        private RemoteDeviceList _remoteDeviceList;
        private IServiceScopeFactory _serviceScopeFactory;

        public DistributeHelper(ILogger<DistributeHelper> logger,
            IHubContext<QuantaDistributeHub> distributeHub,
            RemoteDeviceList processorDeviceList,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _distributeHub = distributeHub;
            _remoteDeviceList = processorDeviceList;
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// 分发数据
        /// </summary>
        /// <param name="remoteDevice">要分发到的远程设备</param>
        /// <param name="data">要发送的数据，用于日志</param>
        /// <param name="sendAction">发送操作</param>
        /// <param name="errorAction">错误操作</param>
        /// <returns></returns>
        public async Task DistributeAsync(RemoteDeviceModel remoteDevice,
            object data, Func<RemoteDeviceModel, Task> sendAction,
            Func<string, RemoteDeviceModel, Task> errorAction)
        {
            if (remoteDevice == null)
            {
                //_logger.LogWarning($"分发数据-远程设备不存在：data:{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");
                return;
            }

            if (remoteDevice.CommunicateDeviceInfos?.FirstOrDefault(x => x.IsOnline) != null)
            {
                try
                {
                    _logger.LogInformation($"分发数据：processor:{ JsonConvert.SerializeObject(remoteDevice, JsonUtil.JsonPrintSettings)} " +
                        $"data:{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");
                    await sendAction?.Invoke(remoteDevice);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"分发数据-分发出错-设备：device:{JsonConvert.SerializeObject(remoteDevice, JsonUtil.JsonPrintSettings)} ");
                    _logger.LogWarning($"分发数据-分发出错-数据：data:{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ex:{ex} ");
                    await errorAction?.Invoke(ex.Message, remoteDevice);
                }
            }
            else if (!string.IsNullOrEmpty(remoteDevice.DeviceId))
            {
                //_logger.LogWarning($"分发数据-远程设备离线-设备：device:{JsonConvert.SerializeObject(remoteDevice, JsonUtil.JsonPrintSettings)} ");
                //_logger.LogWarning($"分发数据-远程设备离线-数据：data:{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");
                await errorAction?.Invoke("边缘处理器离线", remoteDevice);
            }
            else
            {
                _logger.LogWarning($"分发数据-远程设备未连接-设备：device:{ JsonConvert.SerializeObject(remoteDevice, JsonUtil.JsonPrintSettings)} ");
                //_logger.LogWarning($"分发数据-远程设备未连接-数据：data:{ JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");
            }
        }

        public async Task SaveDistributeError(RemoteDeviceModel remoteDevice, DistributeErrorModel distributeErrorModel)
        {
            //时间记录为数据时间
            distributeErrorModel.EditedTime = DateTimeUtil.UtcNowMillis();

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var distributeErrorDataService = scope.ServiceProvider.GetRequiredService<IDistributeErrorService>();
                await distributeErrorDataService.AddRetryAsync(distributeErrorModel);
            }

            remoteDevice.HasDistributeError = true;
        }
    }
}
