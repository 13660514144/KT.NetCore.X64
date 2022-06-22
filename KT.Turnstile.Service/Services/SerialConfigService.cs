using KT.Turnstile.Model.Models;
using KT.Turnstile.Manage.Service.IDaos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.Services
{
    public class SerialConfigService : ISerialConfigService
    {
        private ISerialConfigDao _serialConfigDataDao;
        public SerialConfigService(ISerialConfigDao serialConfigDataDao)
        {
            _serialConfigDataDao = serialConfigDataDao;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _serialConfigDataDao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _serialConfigDataDao.DeleteAsync(id);
        }

        public async Task<SerialConfigModel> AddOrEditAsync(SerialConfigModel model)
        {
            var entity = await _serialConfigDataDao.GetByIdAsync(model.Id);
            if (entity == null)
            {
                entity = SerialConfigModel.ToEntity(model);

                entity = await _serialConfigDataDao.AddAsync(entity);
            }
            else
            {
                entity = SerialConfigModel.SetEntity(entity, model);

                entity = await _serialConfigDataDao.EditAsync(entity);
            }

            model = SerialConfigModel.SetModel(model, entity);

            return model;
        }

        public async Task<List<SerialConfigModel>> GetAllAsync()
        {
            var entities = await _serialConfigDataDao.GetAllAsync();

            var models = SerialConfigModel.ToModels(entities);

            return models;
        }

        public async Task<SerialConfigModel> GetByIdAsync(string id)
        {
            var entity = await _serialConfigDataDao.GetByIdAsync(id);

            var model = SerialConfigModel.ToModel(entity);

            return model;
        }
    }
}
