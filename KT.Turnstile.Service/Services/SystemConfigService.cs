using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Turnstile.Manage.Service.IDaos;
using KT.Turnstile.Entity.Entities;
using KT.Turnstile.Common.Enums;
using KT.Turnstile.Model.Models;
using KT.Turnstile.Model.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.Services
{
    public class SystemConfigService : ISystemConfigService
    {
        private ISystemConfigDao _passRightDataDao;
        private AppSettings _appSettings;

        public SystemConfigService(ISystemConfigDao passRightDataDao,
            IOptions<AppSettings> appSettings)
        {
            _passRightDataDao = passRightDataDao;
            _appSettings = appSettings.Value;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _passRightDataDao.HasInstanceByIdAsync(id);
        }

        public async Task<SystemConfigModel> AddAsync(SystemConfigModel model)
        {
            var entity = SystemConfigModel.ToEntity(model);

            entity = await _passRightDataDao.AddAsync(entity);

            model = SystemConfigModel.SetModel(model, entity);

            return model;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _passRightDataDao.DeleteAsync(id);
        }

        public async Task<SystemConfigModel> EditAsync(SystemConfigModel model)
        {
            var entity = await _passRightDataDao.GetByIdAsync(model.Id);
            if (entity == null)
            {
                throw CustomException.Run("系统中找不到当前系统配置数据：id:{0} ", model.Id);
            }
     
            entity = SystemConfigModel.SetEntity(entity, model);

            entity = await _passRightDataDao.EditAsync(entity);

            model = SystemConfigModel.SetModel(model, entity);

            return model;
        }
        public async Task<SystemConfigModel> AddOrEditByKeyAsync(string key, string value)
        {
            var entity = await _passRightDataDao.GetByKeyAsync(key);
            if (entity == null)
            {
                entity = new SystemConfigEntity();
                entity.Key = key;
                entity.Value = value;
                entity = await _passRightDataDao.AddAsync(entity);
            }
            else if (entity.Value != entity.Value)
            {
                entity.Key = key;
                entity.Value = value;
                entity = await _passRightDataDao.EditAsync(entity);
            }
            var model = SystemConfigModel.ToModel(entity);
            return model;
        }

        public async Task<List<SystemConfigModel>> GetAllAsync()
        {
            var entities = await _passRightDataDao.GetAllAsync();
            var models = SystemConfigModel.ToModels(entities);
            return models;
        }

        public async Task<SystemConfigModel> GetByIdAsync(string id)
        {
            var entity = await _passRightDataDao.GetByIdAsync(id);

            var model = SystemConfigModel.ToModel(entity);

            return model;
        }

        public async Task<SystemConfigModel> GetByKeyAsync(string key)
        {
            var entity = await _passRightDataDao.GetByKeyAsync(key);

            var model = SystemConfigModel.ToModel(entity);

            return model;
        }

        public async Task<SystemConfigModel> GetByKeyWithNewAsync(string key, string val)
        {
            var result = await GetByKeyAsync(key);
            if (result != null)
            {
                return result;
            }

            //如果不存在，则新增
            result = new SystemConfigModel();
            result.Key = key;
            result.Value = val;
            return await AddAsync(result);
        }
         
        public async Task<string> AddOrEditByKeyAsync(SystemConfigEnum config, string value)
        {
            var result = await AddOrEditByKeyAsync(config.Value, value);
            return result.Value;
        }
    }
}
