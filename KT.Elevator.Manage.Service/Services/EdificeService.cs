using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    public class EdificeService : IEdificeService
    {
        private IEdificeDao _cardDeviceDataDao;
        private IFloorDao _floorDao;

        public EdificeService(IEdificeDao cardDeviceDataDao,
            IFloorDao floorDao)
        {
            _cardDeviceDataDao = cardDeviceDataDao;
            _floorDao = floorDao;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _cardDeviceDataDao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _cardDeviceDataDao.DeleteByIdAsync(id);
        }

        public async Task<EdificeModel> AddOrEditAsync(EdificeModel model)
        {
            var entity = await _cardDeviceDataDao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = EdificeModel.ToEntity(model, model.Floors);
                await _cardDeviceDataDao.InsertAsync(entity);
            }
            else
            {
                entity = EdificeModel.SetEntity(entity, model, model.Floors);
                await _cardDeviceDataDao.AttachUpdateAsync(entity);
            }

            model = EdificeModel.SetModel(model, entity);

            return model;
        }

        public async Task<List<EdificeModel>> GetAllAsync()
        {
            var entities = await _cardDeviceDataDao.SelectAllAsync();

            var models = EdificeModel.ToModels(entities);

            return models;
        }

        public async Task<EdificeModel> GetByIdAsync(string id)
        {
            var entity = await _cardDeviceDataDao.SelectByIdAsync(id);
            var model = EdificeModel.ToModel(entity);
            return model;
        }

        public async Task<List<EdificeModel>> GetAllWithFloorAsync()
        {
            var entities = await _cardDeviceDataDao.GetAllWithFloorAsync();

            var models = EdificeModel.ToModels(entities);

            return models;
        }

        public async Task<EdificeModel> GetWithFloorByIdAsync(string id)
        {
            var entity = await _cardDeviceDataDao.GetWithFloorByIdAsync(id);
            var model = EdificeModel.ToModel(entity);
            return model;
        }

        public async Task<EdificeModel> AddOrEditWithFloorAsync(EdificeModel model)
        {
            var entity = await _cardDeviceDataDao.SelectByIdAsync(model.Id);

            //设置楼层
            var floorEntities = new List<FloorEntity>();
            if (model.Floors?.FirstOrDefault() != null)
            {
                foreach (var item in model.Floors)
                {
                    //从数据库中获取跟综对象
                    var floorEntity = await _floorDao.GetByIdAsync(item.Id);
                    if (floorEntity != null)
                    {
                        floorEntity = FloorModel.SetEntity(floorEntity, item);

                        floorEntities.Add(floorEntity);
                    }
                    else
                    {
                        floorEntity = FloorModel.ToEntity(item);

                        floorEntities.Add(floorEntity);
                    }
                }
            }

            if (entity == null)
            {
                entity = EdificeModel.ToEntity(model, null);
                entity.Floors = floorEntities;

                await _cardDeviceDataDao.AddWidthFloorAsync(entity);
            }
            else
            {
                entity = EdificeModel.SetEntity(entity, model, null);
                entity.Floors = floorEntities;

                await _cardDeviceDataDao.EditWidthFloorAsync(entity);
            }

            model = EdificeModel.SetModel(model, entity);

            return model;
        }

        public async Task DeleteWithFloorAsync(string id)
        {
            await _cardDeviceDataDao.RemoveWithFloorByIdAsync(id);
        }
    }
}
