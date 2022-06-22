using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Turnstile.Dtos;
using System.Collections.Generic;

namespace KT.Quanta.Service.Models
{
    /// <summary>
    /// 大厦
    /// </summary>
    public class TurnstilePersonModel : BaseQuantaModel
    {
        public TurnstilePersonModel()
        {
            PassRights = new List<TurnstilePassRightModel>();
        }

        /// <summary>
        /// 大厦名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 人员通行权限
        /// </summary>
        public List<TurnstilePassRightModel> PassRights { get; set; }

        public static PersonEntity ToEntity(TurnstilePersonModel model)
        {
            var entity = new PersonEntity();
            entity = SetEntity(entity, model);
            return entity;
        }

        public static List<TurnstilePersonModel> ToModels(List<PersonEntity> entities)
        {
            var models = new List<TurnstilePersonModel>();
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

        public static PersonEntity SetEntity(PersonEntity entity, TurnstilePersonModel model)
        {
            entity.Id = model.Id;
            entity.EditedTime = model.EditedTime;

            entity.Name = model.Name;

            return entity;
        }

        public static TurnstilePersonModel ToModel(PersonEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new TurnstilePersonModel();
            model = SetModel(model, entity);
            return model;
        }

        public static TurnstilePersonModel SetModel(TurnstilePersonModel model, PersonEntity entity)
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

            return model;
        }
    }
}
