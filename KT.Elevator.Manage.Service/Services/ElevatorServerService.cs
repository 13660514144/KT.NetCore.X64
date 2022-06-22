using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    public class ElevatorServerService : IElevatorServerService
    {
        private IElevatorServerDao _cardDeviceDataDao;

        public ElevatorServerService(IElevatorServerDao cardDeviceDataDao)
        {
            _cardDeviceDataDao = cardDeviceDataDao;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _cardDeviceDataDao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _cardDeviceDataDao.DeleteByIdAsync(id);
        }

        public async Task<ElevatorServerModel> AddOrEditAsync(ElevatorServerModel model)
        {
            var entity = await _cardDeviceDataDao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = ElevatorServerModel.ToEntity(model);
                await _cardDeviceDataDao.AddAsync(entity);
            }
            else
            {
                entity = ElevatorServerModel.SetEntity(entity, model);
                await _cardDeviceDataDao.EditAsync(entity);
            }

            model = ElevatorServerModel.SetModel(model, entity);

            return model;
        }

        public async Task<List<ElevatorServerModel>> GetAllAsync()
        {
            var entities = await _cardDeviceDataDao.GetAllAsync();

            var models = ElevatorServerModel.ToModels(entities);

            return models;
        }

        public async Task<ElevatorServerModel> GetByIdAsync(string id)
        {
            var entity = await _cardDeviceDataDao.GetByIdAsync(id);
            var model = ElevatorServerModel.ToModel(entity);
            return model;
        }
    }
}
