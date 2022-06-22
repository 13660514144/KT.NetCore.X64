using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    public class SerialConfigService : ISerialConfigService
    {
        private ISerialConfigDao _dao;
        public SerialConfigService(ISerialConfigDao serialConfigDataDao)
        {
            _dao = serialConfigDataDao;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _dao.DeleteByIdAsync(id);
        }

        public async Task<SerialConfigModel> AddOrEditAsync(SerialConfigModel model)
        {
            var entity = await _dao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = SerialConfigModel.ToEntity(model);

                await _dao.InsertAsync(entity);

                model.Id = entity.Id;
            }
            else
            {
                entity = SerialConfigModel.SetEntity(entity, model);

                await _dao.AttachAsync(entity);
            }
            model = SerialConfigModel.SetModel(model, entity);

            return model;
        }

        public async Task<List<SerialConfigModel>> GetAllAsync()
        {
            var entities = await _dao.SelectAllAsync();

            var models = SerialConfigModel.ToModels(entities);

            return models;
        }

        public async Task<SerialConfigModel> GetByIdAsync(string id)
        {
            var entity = await _dao.SelectByIdAsync(id);

            var model = SerialConfigModel.ToModel(entity);

            return model;
        }
    }
}
