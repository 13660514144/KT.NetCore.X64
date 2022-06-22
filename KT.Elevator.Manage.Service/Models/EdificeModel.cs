using KT.Elevator.Manage.Service.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;

namespace KT.Elevator.Manage.Service.Models
{
    /// <summary>
    /// 大厦
    /// </summary>
    public class EdificeModel : BaseElevatorModel
    {
        /// <summary>
        /// 大厦名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public List<FloorModel> Floors { get; set; }

        public static EdificeEntity ToEntity(EdificeModel model, List<FloorModel> floors)
        {
            var entity = new EdificeEntity();
            entity = SetEntity(entity, model, floors);
            return entity;
        }

        public static List<EdificeModel> ToModels(List<EdificeEntity> entities)
        {
            var models = new List<EdificeModel>();
            if (entities == null)
            {
                return models;
            }
            foreach (var item in entities)
            {
                var entity = ToModel(item);

                models.Add(entity);
            }
            return models;
        }

        public static EdificeEntity SetEntity(EdificeEntity entity, EdificeModel model, List<FloorModel> floors)
        {
            entity.Id = model.Id;
            entity.EditedTime = model.EditedTime;

            entity.Name = model.Name;

            if (floors?.FirstOrDefault() != null)
            {
                entity.Floors = new List<FloorEntity>();
                foreach (var item in floors)
                {
                    var floorEntity = FloorModel.ToEntity(item);
                    floorEntity.Edifice = entity;
                    entity.Floors.Add(floorEntity);
                }
            }

            return entity;
        }

        public static EdificeModel ToModel(EdificeEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new EdificeModel();
            model = SetModel(model, entity);
            return model;
        }

        public static EdificeModel SetModel(EdificeModel model, EdificeEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            if (model == null)
            {
                return ToModel(entity);
            }
            model.Id = entity.Id;
            model.EditedTime = entity.EditedTime;

            model.Name = entity.Name;

            model.Floors = new List<FloorModel>();
            if (entity.Floors != null && entity.Floors.FirstOrDefault() != null)
            {
                foreach (var item in entity.Floors)
                {
                    item.Edifice = null;
                    var floorModel = FloorModel.ToModel(item);
                    model.Floors.Add(floorModel);
                }
            }

            return model;
        }
    }
}