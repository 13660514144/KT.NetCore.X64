using KT.Common.Core.Utils;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    public class EdificeService : IEdificeService
    {
        private IEdificeDao _dao;
        private IFloorDao _floorDao;

        public EdificeService(IEdificeDao cardDeviceDataDao,
            IFloorDao floorDao)
        {
            _dao = cardDeviceDataDao;
            _floorDao = floorDao;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _dao.DeleteByIdAsync(id);
        }

        public async Task<EdificeModel> AddOrEditAsync(EdificeModel model)
        {
            var entity = await _dao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = EdificeModel.ToEntity(model, model.Floors);
                await _dao.InsertAsync(entity);
            }
            else
            {
                entity = EdificeModel.SetEntity(entity, model, model.Floors);
                await _dao.AttachAsync(entity);
            }

            model = EdificeModel.SetModel(model, entity);

            return model;
        }

        public async Task<List<EdificeModel>> GetAllAsync()
        {
            var entities = await _dao.SelectAllAsync();

            var models = EdificeModel.ToModels(entities);

            return models;
        }

        public async Task<EdificeModel> GetByIdAsync(string id)
        {
            var entity = await _dao.SelectByIdAsync(id);
            var model = EdificeModel.ToModel(entity);
            return model;
        }

        public async Task<List<EdificeModel>> GetAllWithFloorAsync()
        {
            var entities = await _dao.GetAllWithFloorAsync();

            var models = EdificeModel.ToModels(entities);

            //对楼层重新排序
            if (models?.FirstOrDefault() != null)
            {
                foreach (var item in models)
                {
                    item.Floors = item.Floors.OrderBy(x => ConvertUtil.ToInt32(x.PhysicsFloor)).ToList();
                }
            }

            return models;
        }

        public async Task<EdificeModel> GetWithFloorByIdAsync(string id)
        {
            var entity = await _dao.GetWithFloorByIdAsync(id);
            var model = EdificeModel.ToModel(entity);
            return model;
        }

        public async Task<EdificeModel> AddOrEditWithFloorAsync(EdificeModel model)
        {
            var entity = await _dao.SelectByIdAsync(model.Id);

            //设置楼层
            var floorEntities = new List<FloorEntity>();
            if (model.Floors?.FirstOrDefault() != null)
            {
                foreach (var item in model.Floors)
                {
                    //从数据库中获取跟综对象
                    var floorEntity = await _floorDao.GetByIdAsync(item.Id);
                    if (floorEntity == null)
                    {
                        floorEntity = FloorModel.ToEntity(item);

                        floorEntities.Add(floorEntity);
                    }
                    else
                    {
                        // 不修改楼层方向
                        item.IsFront = floorEntity.IsFront;
                        item.IsRear = floorEntity.IsRear;

                        floorEntity = FloorModel.SetEntity(floorEntity, item);

                        floorEntities.Add(floorEntity);
                    }
                }
            }

            if (entity == null)
            {
                entity = EdificeModel.ToEntity(model, null);
                entity.Floors = floorEntities;

                await _dao.AddWidthFloorAsync(entity);
            }
            else
            {
                entity = EdificeModel.SetEntity(entity, model, null);
                entity.Floors = floorEntities;

                await _dao.EditWidthFloorAsync(entity);
            }

            model = EdificeModel.SetModel(model, entity);

            return model;
        }

        public async Task DeleteWithFloorAsync(string id)
        {
            await _dao.RemoveWithFloorByIdAsync(id);
        }

        public async Task<List<EdificeModel>> EditDirectionsAsync(List<EdificeModel> models)
        {
            var ids = models.Select(x => x.Id).ToList();
            var entities = await _dao.GeWithFloorByIdsAsync(ids);
            if (entities?.FirstOrDefault() == null)
            {
                return null;
            }

            foreach (var item in entities)
            {
                var model = models.FirstOrDefault(x => x.Id == item.Id);
                if (item.Floors?.FirstOrDefault() == null
                    || model.Floors?.FirstOrDefault() == null)
                {
                    continue;
                }

                foreach (var obj in item.Floors)
                {
                    var floor = model.Floors.FirstOrDefault(x => x.Id == obj.Id);
                    if (floor == null)
                    {
                        continue;
                    }
                    obj.IsFront = floor.IsFront;
                    obj.IsRear = floor.IsRear;
                }
                await _dao.AttachAsync(item, false);
            }

            await _dao.SaveChangesAsync();

            models = EdificeModel.ToModels(entities);
            return models;
        }
    }
}
