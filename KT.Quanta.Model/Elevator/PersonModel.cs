using KT.Quanta.Service.Entities;
using System.Collections.Generic;

namespace KT.Quanta.Service.Models
{
    /// <summary>
    /// 大厦
    /// </summary>
    public class PersonModel : BaseQuantaModel
    {
        public PersonModel()
        {
            PassRights = new List<PassRightModel>();
        }

        /// <summary>
        /// 大厦名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 人员通行权限
        /// </summary>
        public List<PassRightModel> PassRights { get; set; }

        public static PersonEntity ToEntity(PersonModel model)
        {
            var entity = new PersonEntity();
            entity = SetEntity(entity, model);
            return entity;
        }

        public static List<PersonModel> ToModels(List<PersonEntity> entities)
        {
            var models = new List<PersonModel>();
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

        public static PersonEntity SetEntity(PersonEntity entity, PersonModel model)
        {
            entity.Id = model.Id;
            entity.EditedTime = model.EditedTime;

            entity.Name = model.Name;

            return entity;
        }

        public static PersonModel ToModel(PersonEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new PersonModel();
            model = SetModel(model, entity);
            return model;
        }

        public static PersonModel SetModel(PersonModel model, PersonEntity entity)
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
