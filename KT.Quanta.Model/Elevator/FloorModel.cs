﻿using KT.Quanta.Service.Entities;
using System.Collections.Generic;

namespace KT.Quanta.Service.Models
{
    /// <summary>
    /// 楼层
    /// </summary>
    public class FloorModel : BaseQuantaModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否开启前门
        /// </summary>
        public bool IsFront { get; set; } = true;

        /// <summary>
        /// 是否开启前门
        /// </summary>
        public bool IsRear { get; set; } = true;

        /// <summary>
        /// 物理楼层
        /// </summary>
        public string PhysicsFloor { get; set; }

        /// <summary>
        /// 是否为公共楼层
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// 所属大厦
        /// </summary>
        public string EdificeId { get; set; }

        /// <summary>
        /// 所属大厦
        /// </summary>
        public EdificeModel Edifice { get; set; }

        public static FloorEntity ToEntity(FloorModel model)
        {
            var entity = new FloorEntity();
            entity = SetEntity(entity, model);
            return entity;
        }

        public static List<FloorModel> ToModels(List<FloorEntity> entities)
        {
            var models = new List<FloorModel>();
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

        public static FloorEntity SetEntity(FloorEntity entity, FloorModel model)
        {
            entity.Id = model.Id;
            entity.EditedTime = model.EditedTime;

            entity.Name = model.Name;
            entity.PhysicsFloor = model.PhysicsFloor;
            entity.IsPublic = model.IsPublic;
            entity.IsFront = model.IsFront;
            entity.IsRear = model.IsRear;

            //关联边缘处理器
            if (!string.IsNullOrEmpty(model.EdificeId))
            {
                if (entity.Edifice == null || entity.Edifice.Id != model.EdificeId)
                {
                    entity.Edifice = new EdificeEntity();
                    entity.Edifice.Id = model.EdificeId;
                }
            }

            return entity;
        }

        public static FloorModel ToModel(FloorEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new FloorModel();
            model = SetModel(model, entity);
            return model;
        }

        public static FloorModel SetModel(FloorModel model, FloorEntity entity)
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
            model.PhysicsFloor = entity.PhysicsFloor;
            model.IsPublic = entity.IsPublic;
            model.IsFront = entity.IsFront;
            model.IsRear = entity.IsRear;

            //关联边缘处理器
            model.EdificeId = entity.Edifice?.Id;
            model.Edifice = EdificeModel.SetModel(model.Edifice, entity.Edifice);

            return model;
        }
    }
}