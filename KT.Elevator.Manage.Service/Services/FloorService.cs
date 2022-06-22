using KT.Common.Core.Exceptions;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    public class FloorService : IFloorService
    {
        private IFloorDao _cardDeviceDataDao;
        private IEdificeDao _edificeDao;

        public FloorService(IFloorDao cardDeviceDataDao,
            IEdificeDao edificeDao)
        {
            _cardDeviceDataDao = cardDeviceDataDao;
            _edificeDao = edificeDao;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _cardDeviceDataDao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _cardDeviceDataDao.DeleteByIdAsync(id);
        }

        public async Task<FloorModel> AddOrEditAsync(FloorModel model)
        {
            var edifice = await _edificeDao.SelectByIdAsync(model.EdificeId);
            if (edifice == null)
            {
                throw CustomException.Run("关联大厦不能为空！");
            }
            var entity = await _cardDeviceDataDao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = FloorModel.ToEntity(model);
                entity.Edifice = edifice;
                await _cardDeviceDataDao.InsertAsync(entity);
            }
            else
            {
                entity = FloorModel.SetEntity(entity, model);
                entity.Edifice = edifice;
                await _cardDeviceDataDao.AttachUpdateAsync(entity);
            }

            model = FloorModel.SetModel(model, entity);

            return model;
        }

        public async Task<FloorModel> GetByIdAsync(string id)
        {
            var entity = await _cardDeviceDataDao.GetByIdAsync(id);
            var model = FloorModel.ToModel(entity);
            return model;
        }

        public async Task<List<FloorModel>> GetAllAsync()
        {
            var entities = await _cardDeviceDataDao.GetAllAsync();

            var models = FloorModel.ToModels(entities);

            return models;
        }

        public async Task<List<FloorModel>> GetByEdificeIdAsync(string id)
        {
            var entities = await _cardDeviceDataDao.GetByEdificeIdAsync(id);

            var models = FloorModel.ToModels(entities);

            return models;
        }

        public async Task<FloorModel> GetByRealIdAsync(string realId)
        {
            var entity = await _cardDeviceDataDao.SelectFirstByLambdaAsync(x => x.RealFloorId == realId);

            var model = FloorModel.ToModel(entity);

            return model;
        }
    }
}
