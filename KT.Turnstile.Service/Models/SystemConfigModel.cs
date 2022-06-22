using KT.Turnstile.Entity.Entities;
using System.Collections.Generic;
using KT.Turnstile.Common.Enums;
using KT.Common.Core.Utils;

namespace KT.Turnstile.Model.Models
{
    /// <summary>
    /// 系统配置信息
    /// </summary>
    public class SystemConfigModel : BaseTurnstileModel
    {
        /// <summary>
        /// 关键字
        /// <see cref="SystemConfigEnum"/>
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }


        public static SystemConfigEntity ToEntity(SystemConfigModel model)
        {
            if (model == null)
            {
                return null;
            }
            var entity = new SystemConfigEntity();
            entity = SetEntity(entity, model);

            return entity;
        }
        public static SystemConfigModel ToModel(SystemConfigEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new SystemConfigModel();
            model = SetModel(model, entity);
            return model;
        }

        public static SystemConfigModel SetModel(SystemConfigModel model, SystemConfigEntity entity)
        {
            if(entity == null)
            {
                return null;
            }
            if (model == null)
            {
                return ToModel(entity);
            }
            model.Id = entity.Id;
            model.Key = entity.Key;
            model.Value = entity.Value;
            model.EditedTime = entity.EditedTime;

            return model;
        }

        public static SystemConfigEntity SetEntity(SystemConfigEntity entity, SystemConfigModel model)
        {
            if (model == null)
            {
                return null;
            }
            entity.Id = model.Id;
            entity.Key = model.Key;
            entity.Value = model.Value;
            entity.EditedTime = model.EditedTime;

            return entity;
        }

        public static List<SystemConfigModel> ToModels(List<SystemConfigEntity> entities)
        {
            var models = new List<SystemConfigModel>();
            if (entities == null)
            {
                return models;
            }
            foreach (var item in entities)
            {
                var model = ToModel(item);
                models.Add(model);
            }

            return models;
        }
    }
}
