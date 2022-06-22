using KT.Quanta.Service.Daos;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Quanta.Service.Turnstile.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Turnstile.Services
{
    public class TurnstileRelayDeviceService : ITurnstileRelayDeviceService
    {
        private IRelayDeviceDao _relayDeviceDataDao;
        public TurnstileRelayDeviceService(IRelayDeviceDao relayDeviceDataDao)
        {
            _relayDeviceDataDao = relayDeviceDataDao;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _relayDeviceDataDao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _relayDeviceDataDao.DeleteAsync(id);
        }

        public async Task<TurnstileRelayDeviceModel> AddOrEditAsync(TurnstileRelayDeviceModel model)
        {
            var entity = await _relayDeviceDataDao.GetByIdAsync(model.Id);
            if (entity == null)
            {
                entity = TurnstileRelayDeviceModel.ToEntity(model);

                entity = await _relayDeviceDataDao.AddAsync(entity);

                model = TurnstileRelayDeviceModel.SetModel(model, entity);
            }
            else
            {
                entity = TurnstileRelayDeviceModel.SetEntity(entity, model);

                entity = await _relayDeviceDataDao.EditAsync(entity);

                model = TurnstileRelayDeviceModel.SetModel(model, entity);
            }
            return model;
        }

        public async Task<List<TurnstileRelayDeviceModel>> GetAllAsync()
        {
            var entities = await _relayDeviceDataDao.GetAllAsync();

            var models = TurnstileRelayDeviceModel.ToModels(entities);

            return models;
        }

        public async Task<TurnstileRelayDeviceModel> GetByIdAsync(string id)
        {
            var entity = await _relayDeviceDataDao.GetByIdAsync(id);
            var model = TurnstileRelayDeviceModel.ToModel(entity);
            return model;
        }
    }
}
