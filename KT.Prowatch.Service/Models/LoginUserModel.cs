using KT.Common.Core.Utils;
using KT.Prowatch.Service.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KT.Prowatch.Service.Models
{
    public class LoginUserModel
    {
        /// <summary>
        /// Id主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 数据连接地址
        /// </summary>
        public string DBAddr { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DBName { get; set; }

        /// <summary>
        /// 数据库用户
        /// </summary>
        public string DBUser { get; set; }

        /// <summary>
        /// 数据库密码
        /// </summary>
        public string DBPassword { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string PCAddr { get; set; }

        /// <summary>
        /// 服务器用户名
        /// </summary>
        public string PCUser { get; set; }

        /// <summary>
        /// 服务器密码
        /// </summary>
        public string PCPassword { get; set; }

        /// <summary>
        /// 后台服务器地址
        /// </summary>
        public string ServerAddress { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public long EditedTime { get; set; }

        public static LoginUserEntity ToEntity(LoginUserModel model)
        {
            var entity = new LoginUserEntity();
            entity = SetEntity(entity, model);
            return entity;
        }

        public static List<LoginUserModel> ToModels(List<LoginUserEntity> entities)
        {
            var models = new List<LoginUserModel>();
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

        public static LoginUserEntity SetEntity(LoginUserEntity entity, LoginUserModel model)
        {
            entity.Id = model.Id;
            entity.DBAddr = model.DBAddr;
            entity.DBName = model.DBName;
            entity.DBUser = model.DBUser;
            entity.DBPassword = model.DBPassword;
            entity.PCAddr = model.PCAddr;
            entity.PCUser = model.PCUser;
            entity.PCPassword = model.PCPassword;
            entity.ServerAddress = model.ServerAddress;
            entity.EditedTime = model.EditedTime;

            return entity;
        }
        public static LoginUserModel ToModel(LoginUserEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new LoginUserModel();
            model = SetModel(model, entity);
            return model;
        }

        public static LoginUserModel SetModel(LoginUserModel model, LoginUserEntity entity)
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
            model.DBAddr = entity.DBAddr;
            model.DBName = entity.DBName;
            model.DBUser = entity.DBUser;
            model.DBPassword = entity.DBPassword;
            model.PCAddr = entity.PCAddr;
            model.PCUser = entity.PCUser;
            model.PCPassword = entity.PCPassword;
            model.ServerAddress = entity.ServerAddress;
            model.EditedTime = entity.EditedTime;

            return model;
        }
    }
}