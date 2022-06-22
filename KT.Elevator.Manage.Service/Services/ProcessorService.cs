using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Elevator.Common;
using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using KT.Quanta.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    /// <summary>
    /// 边缘处理器
    /// </summary>
    public class ProcessorService : IProcessorService
    {
        private IProcessorDao _dao;
        private ISystemConfigService _systemConfigDataService;
        private SeekSendHelper _seekUdpHelper;
        private RemoteDeviceList _remoteDeviceList;
        private ILogger<ProcessorService> _logger;
        private AppSettings _appSettings;
        private IHandleElevatorDeviceDao _handleElevatorDeviceDao;
        private IDistributeErrorService _distributeErrorService;

        public ProcessorService(IProcessorDao processorDataDao,
            ISystemConfigService systemConfigDataService,
            RemoteDeviceList remoteDeviceList,
            SeekSendHelper seekUdpHelper,
            ILogger<ProcessorService> logger,
            IOptions<AppSettings> appSettings,
            IHandleElevatorDeviceDao handleElevatorDeviceDao,
            IDistributeErrorService distributeErrorService)
        {
            _dao = processorDataDao;
            _systemConfigDataService = systemConfigDataService;
            _remoteDeviceList = remoteDeviceList;
            _seekUdpHelper = seekUdpHelper;
            _logger = logger;
            _appSettings = appSettings.Value;
            _handleElevatorDeviceDao = handleElevatorDeviceDao;
            _distributeErrorService = distributeErrorService;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task<ProcessorModel> GetByIdAsync(string id)
        {
            var entity = await _dao.GetByIdAsync(id);
            var model = ProcessorModel.ToModel(entity);

            return model;
        }

        public async Task<List<ProcessorModel>> GetAllAsync()
        {
            var entities = await _dao.GetAllAsync();

            var models = ProcessorModel.ToModels(entities);

            return models;
        }

        public async Task<List<ProcessorModel>> GetWidthCardDevicesAsync()
        {
            var entities = await _dao.GetWidthCardDevicesAsync();

            entities?.ForEach(x => x.CardDevices.ForEach(y => y.Processor = null));
            var models = ProcessorModel.ToModels(entities);

            return models;
        }

        public async Task<ProcessorModel> AddOrEditAsync(ProcessorModel model)
        {
            var entity = await _dao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = ProcessorModel.ToEntity(model);
                entity.ProcessorKey = IdUtil.NewId();
                await _dao.AddAsync(entity);
            }
            else
            {
                //修改指定的列，其它列不修改
                entity = ProcessorModel.SetEditEntity(entity, model);

                await _dao.EditAsync(entity);
            }

            //更新静态列表数据   
            model.Floor = FloorModel.ToModel(entity.Floor);

            //加入远程设备列表
            var remoteDeviceInfo = ProcessorModel.ToRemoteDevice(model);
            var remoteDevice = await _remoteDeviceList.AddOrUpdateContentAsync(remoteDeviceInfo);

            //推送数据
            if (string.IsNullOrEmpty(remoteDevice.RemoteDeviceInfo.ConnectionId))
            {
                var seekSocket = new SeekSocketModel();
                seekSocket.Header = _appSettings.SeekHeader;
                seekSocket.ClientIp = model.IpAddress;
                seekSocket.ClientPort = model.Port;
                await _seekUdpHelper.SendDataAsync(seekSocket);
            }

            return model;
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _dao.SelectByIdAsync(id);
            if (entity != null)
            {
                await _dao.DeleteAsync(entity);

                //清除静态列表数据
                await _remoteDeviceList.RemoveByKeyAsync(entity.ProcessorKey);
            }
        }

        public async Task<ProcessorModel> GetByKeyAsync(string processorKey)
        {
            var entity = await _dao.SelectFirstByLambdaAsync(x => x.ProcessorKey == processorKey);
            var model = ProcessorModel.ToModel(entity);

            return model;
        }

        /// <summary>
        /// 初始化数据本地数据
        /// </summary>
        public async Task InitLoadAsync()
        {
            //初始化所有本地边缘处理器
            var models = await GetWidthCardDevicesAsync();
            if (models?.FirstOrDefault() != null)
            {
                foreach (var item in models)
                {
                    var remoteProcessorDevice = ProcessorModel.ToRemoteDevice(item);
                    await _remoteDeviceList.AddAsync(remoteProcessorDevice);

                    if (item.CardDevices?.FirstOrDefault() != null)
                    {
                        foreach (var obj in item.CardDevices)
                        {
                            var remoteCardDevice = CardDeviceModel.ToRemoteDevice(obj);
                            await _remoteDeviceList.AddAsync(remoteCardDevice);
                        }
                    }
                }
            }

            //本地端口
            StaticInfo.Port = _appSettings.Port;
            try
            {
                //创建当前程序Key
                var serverKey = await _systemConfigDataService.AddOrEditByKeyAsync(SystemConfigEnum.SERVER_KEY, IdUtil.NewId());
                //不存在边缘处理器 
                StaticInfo.ServerKey = serverKey;
            }
            catch (Exception ex)
            {
                _logger.LogError("初始化错误：ex:{0} ", ex);
            }
        }

        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <param name="seekSocket">客户端数据</param>
        /// <param name="connectionId">sinalr连接id</param>
        /// <returns>是否存在错误数据</returns>
        public async Task<bool> LinkReturnHasErrorAsync(SeekSocketModel seekSocket, string connectionId)
        {
            //修改静态数据
            var remoteDevice = await _remoteDeviceList.AddOrUpdateLinkAsync(seekSocket, connectionId);
            if (remoteDevice == null)
            {
                return false;
            }

            //修改本地数据 TODOO:异步
            await LinkPersistence(remoteDevice);

            //存在错误数据直接返回结果
            if (remoteDevice.RemoteDeviceInfo.HasDistributeError)
            {
                return true;
            }

            //从数据库中查找是否有错误数据
            var hasError = await _distributeErrorService.IsExistsByDeviceKeyAsync(remoteDevice.RemoteDeviceInfo.DeviceKey);
            return hasError;
        }

        /// <summary>
        /// 连接修改本地数据
        /// </summary>
        /// <param name="remoteDevice">设备数据</param> 
        private async Task LinkPersistence(IRemoteDevice remoteDevice)
        {
            //修改派梯边缘处理器
            if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value)
            {
                //更新本地数据
                var entity = await _dao.SelectByIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId);
                if (entity != null && entity.ProcessorKey != remoteDevice.RemoteDeviceInfo.DeviceKey)
                {
                    entity.ProcessorKey = remoteDevice.RemoteDeviceInfo.DeviceKey;
                    await _dao.AttachUpdateAsync(entity);
                }
            }
            //修改派梯设备
            else
            {
                //更新本地数据
                var entity = await _handleElevatorDeviceDao.GetByIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId);
                if (entity != null && entity.DeviceKey != remoteDevice.RemoteDeviceInfo.DeviceKey)
                {
                    entity.DeviceKey = remoteDevice.RemoteDeviceInfo.DeviceKey;
                    await _handleElevatorDeviceDao.AttachUpdateAsync(entity);
                }
            }
        }
    }
}


//var remoteDevice = _remoteDeviceList.GetByKey(seekSocket.DeviceKey);
//if (remoteDevice == null)
//{
//    //根据Key查询不存在则找出地址相同的数据更改
//    remoteDevice = _remoteDeviceList.GetByIpAndPortId(seekSocket.ClientIp, seekSocket.ClientPort);
//    if (remoteDevice != null)
//    {
//        remoteDevice.Type = seekSocket.DeviceType;
//        remoteDevice.DeviceKey = seekSocket.DeviceKey;

//        //向数据库中写入Key
//        if (remoteDevice.Type == RemoteDeviceTypeEnum.ELEVATOR_PROCESSOR.Value)
//        {
//            //更新本地数据
//            var entity = await _dao.SelectByIdAsync(remoteDevice.DeviceId);
//            if (entity != null)
//            {
//                entity.ProcessorKey = remoteDevice.DeviceKey;
//                await _dao.UpdateAsync(entity);
//            }
//        }
//        else
//        {
//            //更新本地数据
//            var entity = await _handleElevatorDeviceDao.GetByIdAsync(remoteDevice.DeviceId);
//            if (entity != null)
//            {
//                entity.DeviceKey = remoteDevice.DeviceKey;
//                await _handleElevatorDeviceDao.UpdateAsync(entity);
//            }
//        }
//    }
//}

//if (remoteDevice == null)
//{
//    _logger.LogError($"找不到连接设备：SeekSocket:{JsonConvert.SerializeObject(seekSocket, JsonUtil.JsonSettings)} ");
//    return false;
//}

////更新在线状态
//remoteDevice.IsOnline = true;
//remoteDevice.ConnectionId = connectionId;

////更新静态列表Key
// _remoteDeviceList.AddOrUpdate(remoteDevice, true);

////存在错误数据直接返回结果
//if (remoteDevice.HasDistributeError)
//{
//    return true;
//}

////从数据库中查找是否有错误数据
//var hasError = _distributeErrorService.IsExistsByDeviceKeyAsync(remoteDevice.DeviceKey);
