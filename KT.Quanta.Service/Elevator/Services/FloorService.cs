using KT.Common.Core.Exceptions;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    public class FloorService : IFloorService
    {
        private IFloorDao _dao;
        private IEdificeDao _edificeDao;

        public FloorService(IFloorDao cardDeviceDataDao,
            IEdificeDao edificeDao)
        {
            _dao = cardDeviceDataDao;
            _edificeDao = edificeDao;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _dao.DeleteByIdAsync(id);
        }

        public async Task<FloorModel> AddOrEditAsync(FloorModel model)
        {
            var edifice = await _edificeDao.SelectByIdAsync(model.EdificeId);
            if (edifice == null)
            {
                throw CustomException.Run("关联大厦不能为空！");
            }
            var entity = await _dao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = FloorModel.ToEntity(model);
                entity.Edifice = edifice;
                await _dao.InsertAsync(entity);
            }
            else
            {
                // 不修改楼层方向
                model.IsFront = entity.IsFront;
                model.IsRear = entity.IsRear;

                entity = FloorModel.SetEntity(entity, model);
                entity.Edifice = edifice;
                await _dao.AttachAsync(entity);
            }

            model = FloorModel.SetModel(model, entity);

            return model;
        }

        public async Task<FloorModel> GetByIdAsync(string id)
        {
            var entity = await _dao.GetByIdAsync(id);
            var model = FloorModel.ToModel(entity);
            return model;
        }

        public async Task<List<FloorModel>> GetAllAsync()
        {
            var entities = await _dao.GetAllAsync();

            var models = FloorModel.ToModels(entities);

            return models;
        }

        public async Task<List<FloorModel>> GetByEdificeIdAsync(string id)
        {
            var entities = await _dao.GetByEdificeIdAsync(id);

            var models = FloorModel.ToModels(entities);

            return models;
        }


        public async Task<List<FloorModel>> EditDirectionsAsync(List<FloorModel> models)
        {
            var ids = models.Select(x => x.Id).ToList();
            var floors = await _dao.SelectByLambdaAsync(x => ids.Contains(x.Id));
            if (floors?.FirstOrDefault() == null)
            {
                return null;
            }

            foreach (var item in floors)
            {
                var model = models.FirstOrDefault(x => x.Id == item.Id);
                item.IsFront = model.IsFront;
                item.IsRear = model.IsRear;

                await _dao.UpdateAsync(item, false);
            }

            await _dao.SaveChangesAsync();

            models = FloorModel.ToModels(floors);
            return models;
        }
    }
}
