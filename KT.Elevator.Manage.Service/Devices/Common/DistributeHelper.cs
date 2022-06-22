using KT.Common.Core.Utils;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.Hubs;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Common
{
    public class DistributeHelper
    {
        private ILogger<DistributeHelper> _logger;
        private IHubContext<DistributeHub> _distributeHub;
        private RemoteDeviceList _remoteDeviceList;
        private IServiceScopeFactory _serviceScopeFactory;

        public DistributeHelper(ILogger<DistributeHelper> logger,
            IHubContext<DistributeHub> distributeHub,
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
                _logger.LogWarning("分发数据-远程设备不存在：data:{0} ", JsonConvert.SerializeObject(data, JsonUtil.JsonSettings));
                return;
            }

            if (remoteDevice.IsOnline)
            {
                try
                {
                    _logger.LogDebug("分发数据：processor:{0} data:{1} ", JsonConvert.SerializeObject(remoteDevice, JsonUtil.JsonSettings),
                        JsonConvert.SerializeObject(data, JsonUtil.JsonSettings));
                    await sendAction?.Invoke(remoteDevice);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("分发数据-分发出错：data:{0} ex:{1} ", JsonConvert.SerializeObject(data, JsonUtil.JsonSettings), ex);
                    await errorAction?.Invoke(ex.Message, remoteDevice);
                }
            }
            else if (!string.IsNullOrEmpty(remoteDevice.DeviceKey))
            {
                _logger.LogWarning("分发数据-远程设备离线：data:{0} ", JsonConvert.SerializeObject(data, JsonUtil.JsonSettings));
                await errorAction?.Invoke("边缘处理器离线", remoteDevice);
            }
            else
            {
                _logger.LogWarning("分发数据-远程设备未连接：data:{0} ", JsonConvert.SerializeObject(data, JsonUtil.JsonSettings));
            }
        }

        ///// <summary>
        ///// 发送所有远程设备
        ///// </summary>
        ///// <param name="conditionAction">过虑条件</param>
        ///// <param name="data">发送数据，用于日志</param>
        ///// <param name="sendAction">发送操作</param>
        ///// <param name="errorAction">错误操作</param>
        ///// <returns></returns>
        //public async Task DistributeAllAsync(object data,
        //    Func<RemoteDeviceModel, bool> conditionAction,
        //    Func<RemoteDeviceModel, Task> sendAction,
        //    Func<string, RemoteDeviceModel, Task> errorAction)
        //{
        //    await _remoteDeviceList.ExecAllAsync(async (remoteDevice) =>
        //    {
        //        var isExec = conditionAction?.Invoke(remoteDevice);
        //        if (isExec != true)
        //        {
        //            return;
        //        }
        //        await DistributeAsync(remoteDevice, data, sendAction, errorAction);
        //    });
        //}


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
