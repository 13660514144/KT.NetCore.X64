using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Quanta.Common.Models;
using KT.Turnstile.Common;
using KT.Turnstile.Entity.Entities;
using KT.Turnstile.Manage.Service.Helpers;
using KT.Turnstile.Manage.Service.IDaos;
using KT.Turnstile.Manage.Service.IServices;
using KT.Turnstile.Model.Models;
using KT.Turnstile.Model.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.Services
{
    /// <summary>
    /// 边缘处理器
    /// </summary>
    public class ProcessorService : IProcessorService
    {
        private IProcessorDao _dao;
        private ISystemConfigService _systemConfigDataService;
        private SeekSendHelper _seekUdpHelper;
        private ProcessorDeviceList _processorDeviceList;
        private ILogger<ProcessorService> _logger;
        private AppSettings _appSettings;

        public ProcessorService(IProcessorDao processorDataDao,
            ISystemConfigService systemConfigDataService,
            ProcessorDeviceList processorDeviceList,
            SeekSendHelper seekUdpHelper,
            ILogger<ProcessorService> logger,
            IOptions<AppSettings> appSettings)
        {
            _dao = processorDataDao;
            _systemConfigDataService = systemConfigDataService;
            _processorDeviceList = processorDeviceList;
            _seekUdpHelper = seekUdpHelper;
            _logger = logger;
            _appSettings = appSettings.Value;
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

        public async Task<ProcessorModel> AddOrEditAsync(ProcessorModel model)
        {
            var entity = await _dao.GetByIdAsync(model.Id);
            if (entity == null)
            {
                entity = ProcessorModel.ToEntity(model);
                entity = await _dao.AddAsync(entity);
                model = ProcessorModel.SetModel(model, entity);
            }
            else
            {
                //修改指定的列，其它列不修改
                entity.Name = model.Name;
                entity.Remark = model.Remark;
                entity.IpAddress = model.IpAddress;
                entity.Port = model.Port;
                entity.EditedTime = model.EditedTime;

                entity = await _dao.EditAsync(entity);

                model = ProcessorModel.SetModel(model, entity);
            }

            //更新静态列表数据   
            _processorDeviceList.AddOrUpdate(model);

            //推送数据
            if (string.IsNullOrEmpty(model.ConnectionId))
            {
                var seekSocket = new SeekSocketModel();
                seekSocket.ClientIp = model.IpAddress;
                seekSocket.ClientPort = model.Port;
                await _seekUdpHelper.SendDataAsync(seekSocket);
            }

            return model;
        }

        public async Task DeleteAsync(string id)
        {
            await _dao.DeleteAsync(id);

            //清除静态列表数据
            _processorDeviceList.RemoveById(id);
        }

        public async Task<ProcessorModel> AddOrUpdateWithoutSyncDataTimeAsync(ProcessorModel model)
        {
            var entity = await _dao.GetByIdAsync(model.Id);
            if (entity != null)
            {
                //不更新最后同步时间
                model.SyncDataTime = entity.SyncDataTime;

                entity = ProcessorModel.SetEntity(entity, model);

                entity = await _dao.EditAsync(entity);
            }
            else
            {
                entity = new ProcessorEntity();
                entity.Id = model.Id;

                entity = ProcessorModel.SetEntity(entity, model);

                entity = await _dao.AddAsync(entity);
            }

            model = ProcessorModel.SetModel(model, entity);

            return model;
        }

        public async Task EditHasDistributeErrorAsync(string id, bool has)
        {
            var entity = await _dao.GetByIdAsync(id);
            if (entity == null)
            {
                throw CustomException.Run("系统中找不到当前边缘处理器数据：id:{0} ", id);
            }
            entity.HasDistributeError = has;
            await _dao.EditAsync(entity);
        }

        public async Task<ProcessorModel> GetByKeyAsync(string processorKey)
        {
            var entity = await _dao.SelectFirstByLambdaAsync(x => x.ProcessorKey == processorKey);
            var model = ProcessorModel.ToModel(entity);

            return model;
        }

        public async Task InitProcessorAsync()
        {
            //初始化所有本地边缘处理器
            var models = await GetAllAsync();
            _processorDeviceList.Init(models);

            StaticInfo.Port = _appSettings.Port;

            try
            {
                var serverKey = await _systemConfigDataService.AddOrEditByKeyAsync(SystemConfigEnum.SERVER_KEY, IdUtil.NewId());
                //不存在边缘处理器 
                StaticInfo.ServerKey = serverKey;
            }
            catch (Exception ex)
            {
                _logger.LogError("初始化错误：ex:{0} ", ex);
            }

            ////设置当前设备传输值
            //SeekSocketModel.Current.SetValue(StaticInfo.ServerKey, StaticInfo.IpAddress, StaticInfo.Port);
        }

        public async Task<bool> LinkReturnHasErrorAsync(SeekSocketModel seekSocket, string connectionId)
        {
            var processor = _processorDeviceList.GetByKey(seekSocket.DeviceKey);
            if (processor == null)
            {
                var processorKeyValue = _processorDeviceList.GetByIpAndPortId(seekSocket.ClientIp, seekSocket.ClientPort);
                if (processorKeyValue.Value != null)
                {
                    processor = processorKeyValue.Value;

                    processor.ProcessorKey = seekSocket.DeviceKey;
                    //更新本地数据
                    var entity = await _dao.GetByIdAsync(processor.Id);
                    if (entity != null)
                    {
                        entity.ProcessorKey = processor.ProcessorKey;
                        await _dao.AttachUpdateAsync(entity);
                    }
                    //更新静态列表Key
                    _processorDeviceList.RemoveByKey(processorKeyValue.Key);
                    _processorDeviceList.AddOrUpdate(processor);
                }
            }

            if (processor != null)
            {
                //更新在线状态
                processor.ConnectionId = connectionId;
                processor.IsOnline = true;

                //可能会更改IP//默认后台配置，修改出错

                return processor.HasDistributeError;
            }

            return false;
        }

        public void DisLink(string processorKey)
        {
            var processor = _processorDeviceList.GetByKey(processorKey);
            if (processor != null)
            {
                processor.ConnectionId = string.Empty;
                processor.IsOnline = false;
            }
        }


        public List<ProcessorModel> GetStaticListAsync()
        {
            var results = new List<ProcessorModel>();
            _processorDeviceList.ExecAll((processor) =>
            {
                results.Add(processor);
            });
            return results;
        }
    }
}
