using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Common.Models;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Quanta.Models;
using KT.Quanta.Service.Elevator.Dtos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Helpers;
using KT.Quanta.Service.Hubs;
using KT.Quanta.Service.IDaos;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Quanta.DistributeDatas
{
    public class QuantaDisplayDistributeDataService : IQuantaDisplayDistributeDataService
    {
        private IHubContext<QuantaDistributeHub> _distributeHub;
        private RemoteDeviceList _remoteDeviceList;
        private ILogger<QuantaDisplayDistributeDataService> _logger;
        private IElevatorInfoDao _elevatorInfoDao;
        private IHandleElevatorDeviceDao _handleElevatorDeviceDao;
        private FloorHandleElevatorResponseList _floorHandleElevatorResponseList;
        private ICardDeviceDao _cardDeviceDao;
        private readonly IElevatorGroupFloorDao _elevatorGroupFloorDao;

        public QuantaDisplayDistributeDataService(IHubContext<QuantaDistributeHub> distributeHub,
            RemoteDeviceList processorDeviceList,
            ILogger<QuantaDisplayDistributeDataService> logger,
            IElevatorInfoDao elevatorInfoDao,
            IHandleElevatorDeviceDao handleElevatorDeviceDao,
            FloorHandleElevatorResponseList floorHandleElevatorResponseList,
            ICardDeviceDao cardDeviceDao,
            IElevatorGroupFloorDao elevatorGroupFloorDao)
        {
            _distributeHub = distributeHub;
            _remoteDeviceList = processorDeviceList;
            _logger = logger;
            _elevatorInfoDao = elevatorInfoDao;
            _handleElevatorDeviceDao = handleElevatorDeviceDao;
            _floorHandleElevatorResponseList = floorHandleElevatorResponseList;
            _cardDeviceDao = cardDeviceDao;
            _elevatorGroupFloorDao = elevatorGroupFloorDao;
        }


        public async Task HandleElevatorError(ElevatorDisplayModel elevatorDisplay, string message)
        {
            _logger.LogInformation($"派梯失败结果回调： paddle:{ JsonConvert.SerializeObject(elevatorDisplay, JsonUtil.JsonPrintSettings)} message:{message} ");

            //获取派梯设备
            var remoteDevice = await _remoteDeviceList.GetByIdAsync(elevatorDisplay.HandleElevatorDeviceId);
            if (remoteDevice == null)
            {
                throw CustomException.Run($"获取派梯返回设备信息出错：找不到派梯设备！");
            }
            if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_GATE_DISPLAY.Value
            || remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SECONDARY.Value
            || remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value
            || remoteDevice.RemoteDeviceInfo.DeviceType==DeviceTypeEnum.ELEVATOR_CLIENT.Value)
            {
                var result = new HandleElevatorErrorModel();
                result.Message = message;

                foreach (var item in remoteDevice.CommunicateDevices)
                {
                    await _distributeHub.Clients.Client(item.CommunicateDeviceInfo.ConnectionId).SendAsync("HandleElevatorError", result);
                }

                _logger.LogInformation($"派梯失败结果回调：result:{ JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");
            }
            //不同类型不同传送方式
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SELECTOR.Value)
            {
                //电梯边缘处理器
                var cardDevice = await _cardDeviceDao.GetByIdAsync(elevatorDisplay.CardDeviceId);
                if (!string.IsNullOrEmpty(cardDevice?.ProcessorId))
                {
                    remoteDevice = await _remoteDeviceList.GetByIdAsync(cardDevice.ProcessorId);
                    if (remoteDevice != null)
                    {
                        var result = new HandleElevatorErrorModel();
                        result.Message = message;

                        foreach (var item in remoteDevice.CommunicateDevices)
                        {
                            await _distributeHub.Clients.Client(item.CommunicateDeviceInfo.ConnectionId).SendAsync("HandleElevatorError", result);
                        }

                        _logger.LogInformation($"派梯失败结果回调：result:{ JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");
                    }
                }
            }
            else
            {
                _logger.LogError($"派梯失败结果回调失败：未找到设备类型：{remoteDevice.RemoteDeviceInfo.DeviceType} ");
            }
        }
        /// <summary>
        /// 派梯结果回调
        /// </summary>
        /// <param name="paddle"></param>
        /// <returns></returns>
        public async Task HandleElevatorSuccess(ElevatorDisplayModel paddle)
        {
            _logger.LogInformation($"派梯结果回调： paddle:{ JsonConvert.SerializeObject(paddle, JsonUtil.JsonPrintSettings)} ");

            var handleResult = new FloorHandleElevatorSuccessModel();
            handleResult.FloorName = paddle.FloorName;

            //获取派梯设备
            var remoteDevice = await _remoteDeviceList.GetByIdAsync(paddle.HandleElevatorDeviceId);
            if (remoteDevice == null)
            {
                throw CustomException.Run($"获取派梯返回设备信息出错：找不到派梯设备！");
            }

            ElevatorGroupFloorEntity destinationFloor = null;
            //查找目地信息
            if (!string.IsNullOrEmpty(paddle?.DestinationFloor))
            {
                // 查找电梯组目地楼层
                destinationFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndRealFloorIdAsync(remoteDevice.RemoteDeviceInfo.ParentId, paddle.DestinationFloor);
                handleResult.FloorName = destinationFloor?.Floor?.Name;
            }

            var elevatorName = string.Empty;
            ElevatorInfoEntity elevatorInfo = null;
            //较验派梯楼层，防止配置出错
            if (!string.IsNullOrEmpty(paddle?.ElevatorId))
            {
                //获取电梯信息
                var elevatorRealId = paddle.ElevatorId.ToString();
                elevatorInfo = await _elevatorInfoDao.GetByElevatorGroupIdAndRealIdAsync(remoteDevice.RemoteDeviceInfo.ParentId, elevatorRealId);
                elevatorName = string.IsNullOrEmpty(elevatorInfo?.Name) ? elevatorRealId : elevatorInfo.Name;
                _logger.LogWarning($"派梯结果梯号不能为空  HandleElevatorSuccess=>elevatorName=={elevatorName}");
            }
 
            //派梯完成处理，对于等待返回结果数据必须执行此操作，否则不会返回结果
            //TODOO增加一层派梯结果操作公共处理类 
            if (!string.IsNullOrEmpty(paddle.MessageId))
            {
                handleResult.ElevatorName = elevatorName;

                _floorHandleElevatorResponseList.EndHandle(paddle.MessageId, handleResult);
            }

            //不同类型不同传送方式
            if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_GATE_DISPLAY.Value)
            {
                var result = new HandleElevatorDisplayModel();
                result.DestinationFloorName = handleResult.FloorName;
                result.ElevatorName = elevatorName;

                //闸机宝盾显示屏参数
                result.PhysicsFloor = ConvertUtil.ToInt32(destinationFloor?.Floor?.PhysicsFloor, () =>
                {
                    return ConvertUtil.ToInt32(paddle?.DestinationFloor, 0);
                });
                result.ElevatorNumber = ConvertUtil.ToInt32(elevatorInfo?.RealId, () =>
                {
                    return ConvertUtil.ToInt32(paddle?.ElevatorId, 0);
                });

                if (result.PhysicsFloor == 0 || result.ElevatorNumber == 0)
                {
                    _logger.LogWarning($"目的楼层或电梯为0：physicsFloor:{result.PhysicsFloor} elevatorNumber:{result.ElevatorNumber} ");
                }

                foreach (var item in remoteDevice.CommunicateDevices)
                {                    
                        await _distributeHub.Clients.Client(item.CommunicateDeviceInfo.ConnectionId).SendAsync("HandleElevatorSuccess", result);                    
                }

                _logger.LogInformation($"派梯结果回调成功：ELEVATOR_GATE_DISPLAY:{ JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");
            }
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SECONDARY.Value
                 || remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value
                 || remoteDevice.RemoteDeviceInfo.DeviceType==DeviceTypeEnum.ELEVATOR_CLIENT.Value)
            {
                var result = new HandleElevatorDisplayModel();
                result.DestinationFloorName = handleResult.FloorName;
                result.ElevatorName = elevatorName;

                foreach (var item in remoteDevice.CommunicateDevices)
                {                   
                        await _distributeHub.Clients.Client(item.CommunicateDeviceInfo.ConnectionId).SendAsync("HandleElevatorSuccess", result);                    
                }

                _logger.LogInformation($"派梯结果回调成功：ELEVATOR_CLIENT:{ JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");
            }
            //不同类型不同传送方式
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SELECTOR.Value)
            {
                //电梯边缘处理器
                var cardDevice = await _cardDeviceDao.GetByIdAsync(paddle.CardDeviceId);
                if (!string.IsNullOrEmpty(cardDevice?.ProcessorId))
                {
                    var sendRemoteDevice = await _remoteDeviceList.GetByIdAsync(cardDevice.ProcessorId);
                    if (sendRemoteDevice != null)
                    {
                        var result = new HandleElevatorDisplayModel();
                        // 通力没有梯号返回
                        if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.KONE_DCS_SELECTOR.Value
                            && sendRemoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value)
                        {
                            result.DestinationFloorName = handleResult.FloorName;
                            result.ElevatorName = elevatorName;
                        }
                        else
                        {
                            result.DestinationFloorName = handleResult.FloorName;
                            result.ElevatorName = elevatorName;
                        }

                        foreach (var item in sendRemoteDevice.CommunicateDevices)
                        {                           
                                await _distributeHub.Clients.Client(item.CommunicateDeviceInfo.ConnectionId).SendAsync("HandleElevatorSuccess", result);                            
                        }

                        _logger.LogInformation($"派梯结果回调成功：ELEVATOR_SELECTOR:{ JsonConvert.SerializeObject(paddle, JsonUtil.JsonPrintSettings)} ");
                    }
                }
            }
            else
            {
                _logger.LogError($"派梯结果回调失败：未找到设备类型：{remoteDevice.RemoteDeviceInfo.DeviceType} ");
            }
        }

        public async Task HandleElevatorStatus(ElevatorDisplayModel paddle)
        {
            _logger.LogInformation($"派梯结果回调： paddle:{ JsonConvert.SerializeObject(paddle, JsonUtil.JsonPrintSettings)} ");

            var handleResult = new FloorHandleElevatorSuccessModel();
            handleResult.FloorName = paddle.FloorName;

            //获取派梯设备
            var remoteDevice = await _remoteDeviceList.GetByIdAsync(paddle.HandleElevatorDeviceId);
            if (remoteDevice == null)
            {
                throw CustomException.Run($"获取派梯返回设备信息出错：找不到派梯设备！");
            }

            ElevatorGroupFloorEntity destinationFloor = null;
            //查找目地信息
            if (string.IsNullOrEmpty(handleResult.FloorName)
                && !string.IsNullOrEmpty(paddle?.DestinationFloor))
            {
                // 查找电梯组目地楼层
                destinationFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndRealFloorIdAsync(remoteDevice.RemoteDeviceInfo.ParentId, paddle.DestinationFloor);
                handleResult.FloorName = destinationFloor?.Floor?.Name;
            }

            var elevatorName = string.Empty;
            ElevatorInfoEntity elevatorInfo = null;
            //较验派梯楼层，防止配置出错
            if (!string.IsNullOrEmpty(paddle?.ElevatorId))
            {
                //获取电梯信息
                var elevatorRealId = paddle.ElevatorId.ToString();
                elevatorInfo = await _elevatorInfoDao.GetByElevatorGroupIdAndRealIdAsync(remoteDevice.RemoteDeviceInfo.ParentId, elevatorRealId);
                elevatorName = string.IsNullOrEmpty(elevatorInfo?.Name) ? elevatorRealId : elevatorInfo.Name;
                _logger.LogWarning($"派梯结果电梯不能为空！");
            }
  
            //派梯完成处理，对于等待返回结果数据必须执行此操作，否则不会返回结果
            //TODOO增加一层派梯结果操作公共处理类 
            if (!string.IsNullOrEmpty(paddle.MessageId))
            {
                handleResult.ElevatorName = elevatorName;

                _floorHandleElevatorResponseList.EndHandle(paddle.MessageId, handleResult);
            }

            //不同类型不同传送方式
            if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_GATE_DISPLAY.Value)
            {
                var result = new HandleElevatorDisplayModel();
                result.DestinationFloorName = handleResult.FloorName;
                result.ElevatorName = elevatorName;

                //闸机宝盾显示屏参数
                result.PhysicsFloor = ConvertUtil.ToInt32(destinationFloor?.Floor?.PhysicsFloor, () =>
                {
                    return ConvertUtil.ToInt32(paddle?.DestinationFloor, 0);
                });
                result.ElevatorNumber = ConvertUtil.ToInt32(elevatorInfo?.RealId, () =>
                {
                    return ConvertUtil.ToInt32(paddle?.ElevatorId, 0);
                });

                foreach (var item in remoteDevice.CommunicateDevices)
                {
                    await _distributeHub.Clients.Client(item.CommunicateDeviceInfo.ConnectionId).SendAsync("HandleElevatorStatus", result);
                }

                _logger.LogInformation($"派梯结果回调成功：result:{ JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");
            }
            //不同类型不同传送方式
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SECONDARY.Value
                 || remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value
                 || remoteDevice.RemoteDeviceInfo.DeviceType==DeviceTypeEnum.ELEVATOR_CLIENT.Value)
            {
                var result = new HandleElevatorDisplayModel();
                result.DestinationFloorName = handleResult.FloorName;
                result.ElevatorName = elevatorName;

                foreach (var item in remoteDevice.CommunicateDevices)
                {
                    await _distributeHub.Clients.Client(item.CommunicateDeviceInfo.ConnectionId).SendAsync("HandleElevatorStatus", result);
                }

                _logger.LogInformation($"派梯结果回调成功：result:{ JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");
            }
            //不同类型不同传送方式
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SELECTOR.Value)
            {
                //电梯边缘处理器
                var cardDevice = await _cardDeviceDao.GetByIdAsync(paddle.CardDeviceId);
                if (!string.IsNullOrEmpty(cardDevice?.ProcessorId))
                {
                    var sendRemoteDevice = await _remoteDeviceList.GetByIdAsync(cardDevice.ProcessorId);
                    if (sendRemoteDevice != null)
                    {
                        var result = new HandleElevatorDisplayModel();
                        // 通力没有梯号返回
                        if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.KONE_DCS_SELECTOR.Value
                            && sendRemoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value)
                        {
                            result.DestinationFloorName = handleResult.FloorName;
                            result.ElevatorName = elevatorName;
                        }
                        else
                        {
                            result.DestinationFloorName = handleResult.FloorName;
                            result.ElevatorName = elevatorName;
                        }

                        foreach (var item in sendRemoteDevice.CommunicateDevices)
                        {
                            await _distributeHub.Clients.Client(item.CommunicateDeviceInfo.ConnectionId).SendAsync("HandleElevatorStatus", result);
                        }

                        _logger.LogInformation($"派梯结果回调成功：result:{ JsonConvert.SerializeObject(paddle, JsonUtil.JsonPrintSettings)} ");
                    }
                }
            }
            else
            {
                _logger.LogError($"派梯结果回调失败：未找到设备类型：{remoteDevice.RemoteDeviceInfo.DeviceType} ");
            }
        }

        /// <summary>
        /// 派梯结束，不用显示结果
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public Task HandleElevatorSuccess(string messageId)
        {
            var handleResult = new FloorHandleElevatorSuccessModel();
            handleResult.FloorName = "";
            handleResult.ElevatorName = "";

            _floorHandleElevatorResponseList.EndHandle(messageId, handleResult);

            return Task.CompletedTask;
        }
    }
}
