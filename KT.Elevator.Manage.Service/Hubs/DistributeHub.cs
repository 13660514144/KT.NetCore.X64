using KT.Common.Core.Utils;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.IHubs;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Services;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using KT.Quanta.Common.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Hubs
{
    public class DistributeHub : Hub, IDistributeHub
    {
        private RemoteDeviceList _remoteDeviceList;
        private IServiceProvider _serviceProvider;
        private ILogger<DistributeHub> _logger;

        public DistributeHub(RemoteDeviceList processorDeviceList,
            IServiceProvider serviceProvider,
            ILogger<DistributeHub> logger)
        {
            _remoteDeviceList = processorDeviceList;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        /// <summary>
        /// 远程设备连接
        /// </summary>
        /// <param name="processorKey"></param>
        /// <returns>是否存在错误数据</returns>
        public async Task<bool> Link(SeekSocketModel seekSocket)
        {
            _logger.LogInformation("远程设备连接：seek:{0} ", JsonConvert.SerializeObject(seekSocket, JsonUtil.JsonSettings));
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IProcessorService>();
                return await service.LinkReturnHasErrorAsync(seekSocket, Context.ConnectionId);
            }
        }

        /// <summary>
        /// 获取所有读卡器
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        public async Task<List<UnitCardDeviceEntity>> GetCardDevices(string processorKey)
        {
            var remoteDevice =await _remoteDeviceList.GetByConnectionIdAsync(processorKey);
            if (remoteDevice == null)
            {
                remoteDevice =await _remoteDeviceList.GetByConnectionIdAsync(Context.ConnectionId);
            }
            if (remoteDevice == null)
            {
                return new List<UnitCardDeviceEntity>();
            }
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
        /// 获取所有通行权限
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        public async Task<List<UnitErrorModel>> GetUnitErrors(string processorKey, int page, int size)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IDistributeErrorService>();
                var results = await service.GetUnitPageAndDeleteByProcessorKeyAsync(processorKey, page, size);
                return results;
            }
        }

        /// <summary>
        /// 推送通行记录
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
        /// 派梯
        /// </summary>
        /// <param name="floorId">目标楼层</param>
        /// <param name="handleElevatorDeviceId">派梯设备</param>
        public async Task HandleElevator(HandleElevatorModel handleElevator)
        {
            _logger.LogInformation($"派梯：handleElevator:{ JsonConvert.SerializeObject(handleElevator, JsonUtil.JsonSettings) } ");
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceService>();
                await service.HandleElevator(handleElevator);
            }
        }

        /// <summary>
        /// 派梯，根据卡号与派梯设备派梯
        /// </summary>
        /// <param name="rightHandleElevator">派梯参数</param> 
        public async Task<HandledElevatorSuccessModel> RightHandleElevator(RightHandleElevatorModel rightHandleElevator)
        {
            _logger.LogInformation($"派梯：RightHandleElevator:{ JsonConvert.SerializeObject(rightHandleElevator, JsonUtil.JsonSettings) } ");
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceService>();
                return await service.RightHandleElevator(rightHandleElevator);
            }
        }

        /// <summary>
        /// 上传二次派梯输入设备
        /// </summary> 
        public async Task HandleElevatorInputDevices(List<UnitHandleElevatorInputDeviceEntity> inputDevices)
        {
            _logger.LogInformation($"HandleElevatorInputDevices：inputDevices:{ JsonConvert.SerializeObject(inputDevices, JsonUtil.JsonSettings) } ");
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IHandleElevatorInputDeviceService>();
                await service.AddOrEditUnitAsync(inputDevices);
            }
        }

        /// <summary>
        /// 获取二次派梯输入设备
        /// </summary> 
        public async Task<UnitHandleElevatorDeviceModel> GetHandleElevatorDevice(string deviceKey)
        {
            _logger.LogInformation($"更新派梯设备-GetHandleElevatorDevice：deviceKey:{deviceKey} ");
            var remoteDevice = await _remoteDeviceList.GetByKeyAsync(deviceKey);
            if (remoteDevice == null)
            {
                _logger.LogInformation($"更新派梯设备错误：找不到派梯设备：deviceKey:{deviceKey} ");
                return null;
            }
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceService>();
                return await service.GetUnitByDeviceId(remoteDevice.RemoteDeviceInfo.DeviceId);
            }
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
}
