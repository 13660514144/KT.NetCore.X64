using KT.Elevator.Manage.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Models
{
    public class ElevatorInfoModel : BaseElevatorModel
    {
        /// <summary>
        /// 电梯id
        /// </summary>
        public string RealId { get; set; }

        /// <summary>
        /// 电梯名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电梯组Id
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 电梯组
        /// </summary>
        public ElevatorGroupEntity ElevatorGroup { get; set; }

        public static ElevatorInfoEntity SetEntity(ElevatorInfoEntity entity, ElevatorInfoModel model)
        {
            entity.Id = model.Id;
            entity.RealId = model.RealId;
            entity.Name = model.Name;

            entity.ElevatorGroupId = model.ElevatorGroupId;

            return entity;
        }

        public static ElevatorInfoModel ToModel(ElevatorInfoEntity entity)
        {
            var model = new ElevatorInfoModel();
            model.Id = entity.Id;
            model.RealId = entity.RealId;
            model.Name = entity.Name;

            model.ElevatorGroupId = entity.ElevatorGroupId;

            return model;
        }

        internal static ElevatorInfoEntity ToEntity(ElevatorInfoModel model)
        {
            if (model == null)
            {
                return null;
            }
            var entity = new ElevatorInfoEntity();
            entity.Id = model.Id;
            entity.RealId = model.RealId;
            entity.Name = model.Name;

            if (!string.IsNullOrEmpty(model.ElevatorGroupId))
            {
                entity.ElevatorGroupId = model.ElevatorGroupId;
                entity.ElevatorGroup = new ElevatorGroupEntity();
                entity.ElevatorGroup.Id = model.Id;
            }
             
            return entity;
        }
    }
}
