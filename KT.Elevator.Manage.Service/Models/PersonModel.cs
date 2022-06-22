﻿using KT.Elevator.Manage.Service.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;

namespace KT.Elevator.Manage.Service.Models
{
    /// <summary>
    /// 大厦
    /// </summary>
    public class PersonModel : BaseElevatorModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 大厦名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }

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

            entity.Number = model.Number;
            entity.Name = model.Name;
            entity.CardNumber = model.CardNumber;

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

            model.Number = entity.Number;
            model.Name = entity.Name;
            model.CardNumber = entity.CardNumber;

            return model;
        }
    }
}