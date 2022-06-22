using AutoMapper;
using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Common.Models;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    /// <summary>
    /// 边缘处理器
    /// </summary>
    public class ProcessorService : IProcessorService
    {
        private IProcessorDao _dao;
        private ISystemConfigService _systemConfigDataService;
        private RemoteDeviceList _remoteDeviceList;
        private CommunicateDeviceList _communicateDeviceList;
        private ILogger<ProcessorService> _logger;
        private AppSettings _appSettings;
        private IHandleElevatorDeviceDao _handleElevatorDeviceDao;
        private IDistributeErrorService _distributeErrorService;
        private readonly IMapper _mapper;

        public ProcessorService(IProcessorDao processorDataDao,
            ISystemConfigService systemConfigDataService,
            RemoteDeviceList remoteDeviceList,
            ILogger<ProcessorService> logger,
            IOptions<AppSettings> appSettings,
            IHandleElevatorDeviceDao handleElevatorDeviceDao,
            IDistributeErrorService distributeErrorService,
            CommunicateDeviceList communicateDeviceList,
            IMapper mapper)
        {
            _dao = processorDataDao;
            _systemConfigDataService = systemConfigDataService;
            _remoteDeviceList = remoteDeviceList;
            _logger = logger;
            _appSettings = appSettings.Value;
            _handleElevatorDeviceDao = handleElevatorDeviceDao;
            _distributeErrorService = distributeErrorService;
            _communicateDeviceList = communicateDeviceList;
            _mapper = mapper;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task<ProcessorModel> GetByIdAsync(string id)
        {
            var entity = await _dao.GetByIdAsync(id);
            var model = _mapper.Map<ProcessorModel>(entity);

            return model;
        }

        public async Task<List<ProcessorModel>> GetFromDeviceTypeAsync()
        {
            var entities = await _dao.GetByDeviceTypeAsync(DeviceTypeEnum.ELEVATOR_PROCESSOR.Value);

            var models = _mapper.Map<List<ProcessorModel>>(entities);

            return models;
        }

        public async Task<List<ProcessorModel>> GetWidthCardDevicesAsync()
        {
            var entities = await _dao.GetWidthCardDevicesAsync();

            entities?.ForEach(x => x.CardDevices.ForEach(y => y.Processor = null));
            var models = _mapper.Map<List<ProcessorModel>>(entities);

            return models;
        }

        public async Task<ProcessorModel> AddOrEditAsync(ProcessorModel model)
        {
            model.DeviceType = DeviceTypeEnum.ELEVATOR_PROCESSOR.Value;

            var entity = await _dao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = ProcessorModel.ToEntity(model);
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

            return model;
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _dao.SelectByIdAsync(id);
            if (entity != null)
            {
                await _dao.DeleteAsync(entity);

                //清除静态列表数据
                await _remoteDeviceList.RemoveByIdAsync(entity.Id);
            }
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
                    if (_appSettings.IsAsyncInitRemoteDevice)
                    {
                        Task.Run(async () =>
                        {
                            try
                            {
                                await InitRemoteDeviceAsync(item);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "异步执行边缘处理器初始化失败！");
                            }
                        });
                    }
                    else
                    {
                        await InitRemoteDeviceAsync(item);
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

        private async Task InitRemoteDeviceAsync(ProcessorModel item)
        {
            var remoteProcessorDevice = ProcessorModel.ToRemoteDevice(item);
            await _remoteDeviceList.AddAsync(remoteProcessorDevice);

            if (item.CardDevices?.FirstOrDefault() != null)
            {
                foreach (var obj in item.CardDevices)
                {
                    if (_appSettings.IsAsyncInitRemoteDevice)
                    {
                        Task.Run(async () =>
                        {
                            try
                            {
                                await InitCardDeviceAsync(remoteProcessorDevice, obj);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "异步执行读卡器初始化失败！");
                            }
                        });
                    }
                    else
                    {
                        await InitCardDeviceAsync(remoteProcessorDevice, obj);
                    }
                }
            }
        }

        private async Task InitCardDeviceAsync(RemoteDeviceModel remoteProcessorDevice, CardDeviceModel obj)
        {
            //正常判断是不远程设备根据通信方式，目前无通信方式暂时根据类型来判断
            if (obj.BrandModel == BrandModelEnum.HIKVISION_DS_K1T672MW.Value
                || obj.BrandModel == BrandModelEnum.HIKVISION_DS_K5604Z_ZZH.Value
                || obj.BrandModel == BrandModelEnum.HIKVISION_DS_5607Z_ZZH.Value)
            {
                try
                {
                    var remoteCardDevice = CardDeviceModel.ToRemoteDevice(obj);
                    remoteCardDevice.ParentId = remoteProcessorDevice.DeviceId;
                    await _remoteDeviceList.AddAsync(remoteCardDevice);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"创建边缘处理器与读卡器设备出错：{ex} ");
                }
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

            ////修改本地数据 TODOO:异步
            //await LinkPersistence(remoteDevice);

            //存在错误数据直接返回结果
            if (remoteDevice.RemoteDeviceInfo.HasDistributeError)
            {
                return true;
            }

            //从数据库中查找是否有错误数据
            var hasError = await _distributeErrorService.IsExistsByDeviceIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId);
            return hasError;
        }

        public async Task<ProcessorModel> GetWithProcessorFloorsByIdAsync(string processorId)
        {
            var entity = await _dao.GetWithProcessorFloorsByIdAsync(processorId);
            var model = _mapper.Map<ProcessorModel>(entity);

            return model;
        }
    }
}
