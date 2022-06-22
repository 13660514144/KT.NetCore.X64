using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
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

            await _passRightDataDao.InsertAsync(entity);

            return model;
        }

        public async Task DeleteAsync(string id)
        {
            await _passRightDataDao.DeleteByIdAsync(id);
        }

        public async Task<SystemConfigModel> EditAsync(SystemConfigModel model)
        {
            var entity = await _passRightDataDao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = SystemConfigModel.ToEntity(model);
                await _passRightDataDao.InsertAsync(entity);

                model.Id = entity.Id;
            }
            else
            {
                entity = SystemConfigModel.SetEntity(entity, model);

                await _passRightDataDao.AttachAsync(entity);
            }

            return model;
        }
        public async Task<SystemConfigModel> AddOrEditByKeyAsync(string key, string value)
        {
            var entity = await _passRightDataDao.SelectFirstByLambdaAsync(x => x.Key == key);
            if (entity == null)
            {
                entity = new SystemConfigEntity();
                entity.Key = key;
                entity.Value = value;
                await _passRightDataDao.InsertAsync(entity);
            }
            else if (entity.Value != entity.Value)
            {
                entity.Key = key;
                entity.Value = value;
                await _passRightDataDao.UpdateAsync(entity);
            }
            var model = SystemConfigModel.ToModel(entity);
            return model;
        }

        public async Task<List<SystemConfigModel>> GetAllAsync()
        {
            var entities = await _passRightDataDao.SelectAllAsync();
            var models = SystemConfigModel.ToModels(entities);
            return models;
        }

        public async Task<SystemConfigModel> GetByIdAsync(string id)
        {
            var entity = await _passRightDataDao.SelectByIdAsync(id);

            var model = SystemConfigModel.ToModel(entity);

            return model;
        }

        public async Task<SystemConfigModel> GetByKeyAsync(string key)
        {
            var entity = await _passRightDataDao.SelectFirstByLambdaAsync(x => x.Key == key);

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
