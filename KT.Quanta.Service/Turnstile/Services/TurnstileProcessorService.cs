using AutoMapper;
using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Common.Models;
using KT.Quanta.Service;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Quanta.Service.Turnstile.IServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    /// <summary>
    /// 边缘处理器
    /// </summary>
    public class TurnstileProcessorService : ITurnstileProcessorService
    {
        private IProcessorDao _dao;
        private ISystemConfigService _systemConfigDataService;
        private RemoteDeviceList _remoteDeviceList;
        private ILogger<TurnstileProcessorService> _logger;
        private AppSettings _appSettings;
        private IHandleElevatorDeviceDao _handleElevatorDeviceDao;
        private IDistributeErrorService _distributeErrorService;
        private readonly IMapper _mapper;

        public TurnstileProcessorService(IProcessorDao processorDataDao,
            ISystemConfigService systemConfigDataService,
            RemoteDeviceList remoteDeviceList,
            ILogger<TurnstileProcessorService> logger,
            IOptions<AppSettings> appSettings,
            IHandleElevatorDeviceDao handleElevatorDeviceDao,
            IDistributeErrorService distributeErrorService,
            IMapper mapper)
        {
            _dao = processorDataDao;
            _systemConfigDataService = systemConfigDataService;
            _remoteDeviceList = remoteDeviceList;
            _logger = logger;
            _appSettings = appSettings.Value;
            _handleElevatorDeviceDao = handleElevatorDeviceDao;
            _distributeErrorService = distributeErrorService;
            _mapper = mapper;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task<TurnstileProcessorModel> GetByIdAsync(string id)
        {
            var entity = await _dao.GetByIdAsync(id);
            var model = _mapper.Map<TurnstileProcessorModel>(entity);

            return model;
        }

        public async Task<List<TurnstileProcessorModel>> GetFromDeviceTypeAsync()
        {
            var entities = await _dao.GetByDeviceTypeAsync(DeviceTypeEnum.TURNSTILE_PROCESSOR.Value);

            var models = _mapper.Map<List<TurnstileProcessorModel>>(entities);

            return models;
        }

        public async Task<List<TurnstileProcessorModel>> GetWidthCardDevicesAsync()
        {
            var entities = await _dao.GetWidthCardDevicesAsync();

            entities?.ForEach(x => x.CardDevices.ForEach(y => y.Processor = null));
            var models = _mapper.Map<List<TurnstileProcessorModel>>(entities);

            return models;
        }

        public async Task<TurnstileProcessorModel> AddOrEditAsync(TurnstileProcessorModel model)
        {
            model.DeviceType = DeviceTypeEnum.TURNSTILE_PROCESSOR.Value;

            var entity = await _dao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = TurnstileProcessorModel.ToEntity(model);
                await _dao.AddAsync(entity);
            }
            else
            {
                //修改指定的列，其它列不修改
                entity = TurnstileProcessorModel.SetEditEntity(entity, model);

                await _dao.EditAsync(entity);
            }

            //更新静态列表数据   
            model.Floor = FloorModel.ToModel(entity.Floor);

            //加入远程设备列表
            var remoteDeviceInfo = TurnstileProcessorModel.ToRemoteDevice(model);
            await _remoteDeviceList.AddOrUpdateContentAsync(remoteDeviceInfo);

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
                    var remoteProcessorDevice = TurnstileProcessorModel.ToRemoteDevice(item);
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

            //存在错误数据直接返回结果
            if (remoteDevice.RemoteDeviceInfo.HasDistributeError)
            {
                return true;
            }

            //从数据库中查找是否有错误数据
            var hasError = await _distributeErrorService.IsExistsByDeviceIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId);
            return hasError;
        }

        public async Task<List<TurnstileProcessorModel>> GetStaticListAsync()
        {
            var results = new List<TurnstileProcessorModel>();
            //获取分发设备 
            await _remoteDeviceList.ExecuteAsync(x =>
            {
                return x.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.TURNSTILE_PROCESSOR.Value;
            },
            (remoteDevice) =>
            {
                var result = new TurnstileProcessorModel();
                result.Id = remoteDevice.RemoteDeviceInfo.DeviceId;
                result.IsOnline = remoteDevice.CommunicateDevices.Any(y => y.CommunicateDeviceInfo.IsOnline);
                results.Add(result);

                return Task.CompletedTask;
            });
            return results;
        }
    }
}
