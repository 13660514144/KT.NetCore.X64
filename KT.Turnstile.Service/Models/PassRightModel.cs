using System.Collections.Generic;
using KT.Common.Core.Utils;
using KT.Turnstile.Entity.Entities;
using KT.Turnstile.Common.Enums;
using System.Linq;

namespace KT.Turnstile.Model.Models
{
    /// <summary>
    /// 通行权限
    /// </summary>
    public class PassRightModel : BaseTurnstileModel
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 当前时间，UTC毫秒
        /// </summary>
        public long TimeNow { get; set; }

        /// <summary>
        /// 过期时间，UTC毫秒
        /// </summary>
        public long TimeOut { get; set; }

        /// <summary>
        /// 关联通道id
        /// </summary>
        public List<string> CardDeviceRightGroupIds { get; set; }

        /// <summary>
        /// 关联通道
        /// </summary>
        public List<CardDeviceRightGroupModel> CardDeviceRightGroups { get; set; }

        public static PassRightEntity SetEntity(PassRightEntity entity, PassRightModel model)
        {
            if (model == null)
            {
                entity = null;
                return entity;
            }
            if (entity == null)
            {
                entity = new PassRightEntity();
            }

            entity.Id = model.Id;
            entity.CardNumber = model.CardNumber;
            entity.TimeNow = model.TimeNow;
            entity.TimeOut = model.TimeOut;
            entity.EditedTime = model.EditedTime;

            //关联卡通道
            entity.RelationCardDeviceRightGroups = new List<PassRightRelationCardDeviceRightGroupEntity>();
            if (model.CardDeviceRightGroupIds != null && model.CardDeviceRightGroupIds.FirstOrDefault() != null)
            {
                foreach (var item in model.CardDeviceRightGroupIds)
                {
                    var passAisleEntity = new PassRightRelationCardDeviceRightGroupEntity();
                    passAisleEntity.PassRight = entity;

                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    passAisleEntity.CardDeviceRightGroup = new CardDeviceRightGroupEntity();
                    passAisleEntity.CardDeviceRightGroup.Id = item;

                    entity.RelationCardDeviceRightGroups.Add(passAisleEntity);
                }
            }

            return entity;
        }

        public static List<PassRightModel> ToModels(List<PassRightEntity> entities)
        {
            var results = new List<PassRightModel>();
            if (entities == null)
            {
                return results;
            }
            foreach (var item in entities)
            {
                var result = ToModel(item);
                results.Add(result);
            }
            return results;
        }

        public static PassRightEntity ToEntity(PassRightModel model)
        {
            var entity = new PassRightEntity();
            var result = SetEntity(entity, model);
            return result;
        }

        public static PassRightModel ToModel(PassRightEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new PassRightModel();
            model = SetModel(model, entity);
            return model;
        }
        public static PassRightModel SetModel(PassRightModel model, PassRightEntity entity)
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
            model.CardNumber = entity.CardNumber;
            model.TimeNow = entity.TimeNow;
            model.TimeOut = entity.TimeOut;
            model.EditedTime = entity.EditedTime;

            //关联通道
            model.CardDeviceRightGroupIds = new List<string>();
            model.CardDeviceRightGroups = new List<CardDeviceRightGroupModel>();
            if (entity.RelationCardDeviceRightGroups != null && entity.RelationCardDeviceRightGroups.FirstOrDefault() != null)
            {
                foreach (var item in entity.RelationCardDeviceRightGroups)
                {
                    var passAisleModel = CardDeviceRightGroupModel.ToModel(item.CardDeviceRightGroup);
                    if (passAisleModel == null)
                    {
                        continue;
                    }
                    model.CardDeviceRightGroupIds.Add(passAisleModel.Id);
                    model.CardDeviceRightGroups.Add(passAisleModel);
                }
            }

            return model;
        }
    }
}
