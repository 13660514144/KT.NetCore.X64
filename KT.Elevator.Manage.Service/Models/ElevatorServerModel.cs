using KT.Elevator.Manage.Service.Entities;
using System.Collections.Generic;

namespace KT.Elevator.Manage.Service.Models
{
    /// <summary>
    /// 电梯服务
    /// </summary>
    public class ElevatorServerModel : BaseElevatorModel
    {
        /// <summary>
        /// Ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 服务器账号
        /// </summary>
        public string PCAccount { get; set; }

        /// <summary>
        /// 服务器密码
        /// </summary>
        public string PCPassword { get; set; }

        /// <summary>
        /// 数据库账号
        /// </summary>
        public string DBAccount { get; set; }

        /// <summary>
        /// 数据库密码
        /// </summary>
        public string DBPassword { get; set; }

        /// <summary>
        /// 是否为主服务器
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// 关联电梯组Id
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 关联电梯组
        /// </summary>
        public ElevatorGroupModel ElevatorGroup { get; set; }

        public static ElevatorServerEntity SetEntity(ElevatorServerEntity entity, ElevatorServerModel model)
        {
            if (model == null)
            {
                entity = null;
                return entity;
            }
            if (entity == null)
            {
                entity = new ElevatorServerEntity();
            }

            entity.Id = model.Id;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.PCAccount = model.PCAccount;
            entity.PCPassword = model.PCPassword;
            entity.DBAccount = model.DBAccount;
            entity.DBPassword = model.DBPassword;
            entity.EditedTime = model.EditedTime;
            entity.IsMain = model.IsMain;

            //关联电梯组
            if (!string.IsNullOrEmpty(model.ElevatorGroupId))
            {
                if (entity.ElevatorGroup == null || entity.ElevatorGroup.Id != model.ElevatorGroupId)
                {
                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    entity.ElevatorGroup = new ElevatorGroupEntity();
                    entity.ElevatorGroup.Id = model.ElevatorGroupId;
                }
            }

            return entity;
        }

        public static List<ElevatorServerModel> ToModels(List<ElevatorServerEntity> entities)
        {
            var results = new List<ElevatorServerModel>();
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

        public static ElevatorServerEntity ToEntity(ElevatorServerModel model)
        {
            var entity = new ElevatorServerEntity();
            var result = SetEntity(entity, model);
            return result;
        }

        public static ElevatorServerModel ToModel(ElevatorServerEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new ElevatorServerModel();
            model = SetModel(model, entity);
            return model;
        }

        public static ElevatorServerModel SetModel(ElevatorServerModel model, ElevatorServerEntity entity)
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
            model.IpAddress = entity.IpAddress;
            model.Port = entity.Port;
            model.PCAccount = entity.PCAccount;
            model.PCPassword = entity.PCPassword;
            model.DBAccount = entity.DBAccount;
            model.DBPassword = entity.DBPassword;
            model.EditedTime = entity.EditedTime;
            model.IsMain = entity.IsMain;

            //关联电梯组  
            model.ElevatorGroupId = entity.ElevatorGroup?.Id;
            model.ElevatorGroup = ElevatorGroupModel.SetModel(model.ElevatorGroup, entity.ElevatorGroup, null);


            return model;
        }
    }
}
