using System.Collections.Generic;
using KT.Common.Core.Utils;
using KT.Turnstile.Entity.Entities;
using KT.Turnstile.Common.Enums;
using System.Linq;

namespace KT.Turnstile.Model.Models
{
    /// <summary>
    /// 通行权限组
    /// </summary>
    public class CardDeviceRightGroupModel : BaseTurnstileModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 关联读卡器id
        /// </summary>
        public virtual List<string> CardDeviceIds { get; set; }

        /// <summary>
        /// 关联读卡器
        /// </summary>
        public List<CardDeviceModel> CardDevices { get; set; }

        public static CardDeviceRightGroupEntity SetEntity(CardDeviceRightGroupEntity entity, CardDeviceRightGroupModel model)
        {
            if (model == null)
            {
                entity = null;
                return entity;
            }
            if (entity == null)
            {
                entity = new CardDeviceRightGroupEntity();
            }

            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.EditedTime = model.EditedTime;

            //关联卡类型
            entity.RelationCardDevices = new List<CardDeviceRightGroupRelationCardDeviceEntity>();
            if (model.CardDeviceIds != null && model.CardDeviceIds.FirstOrDefault() != null)
            {
                foreach (var item in model.CardDeviceIds)
                {
                    var passAisleEntity = new CardDeviceRightGroupRelationCardDeviceEntity();
                    passAisleEntity.CardDeviceRightGroup = entity;

                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    passAisleEntity.CardDevice = new CardDeviceEntity();
                    passAisleEntity.CardDevice.Id = item;

                    entity.RelationCardDevices.Add(passAisleEntity);
                }
            }

            return entity;
        }

        public static List<CardDeviceRightGroupModel> ToModels(List<CardDeviceRightGroupEntity> entities)
        {
            var results = new List<CardDeviceRightGroupModel>();
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

        public static CardDeviceRightGroupEntity ToEntity(CardDeviceRightGroupModel model)
        {
            var entity = new CardDeviceRightGroupEntity();
            var result = SetEntity(entity, model);
            return result;
        }

        public static CardDeviceRightGroupModel ToModel(CardDeviceRightGroupEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new CardDeviceRightGroupModel();
            model = SetModel(model, entity);
            return model;
        }
        public static CardDeviceRightGroupModel SetModel(CardDeviceRightGroupModel model, CardDeviceRightGroupEntity entity)
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
            model.Name = entity.Name;
            model.EditedTime = entity.EditedTime;

            //关联卡类型
            model.CardDeviceIds = new List<string>();
            model.CardDevices = new List<CardDeviceModel>();
            if (entity.RelationCardDevices != null && entity.RelationCardDevices.FirstOrDefault() != null)
            {
                foreach (var item in entity.RelationCardDevices)
                {
                    var passAisleModel = CardDeviceModel.ToModel(item.CardDevice);
                    if (passAisleModel == null)
                    {
                        continue;
                    }
                    model.CardDeviceIds.Add(passAisleModel.Id);
                    model.CardDevices.Add(passAisleModel);
                }
            }

            return model;
        }
    }
}
