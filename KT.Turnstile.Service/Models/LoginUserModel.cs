using KT.Common.Data.Models;
using KT.Turnstile.Entity.Entities;
using System.Collections.Generic;

namespace KT.Turnstile.Model.Models
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class LoginUserModel : BaseTurnstileModel
    {
        /// <summary>
        /// 用户Id,关联WinPak系统中的用户
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 服务器数据库地址
        /// </summary>
        public string DBAddr { get; set; }

        /// <summary>
        /// 服务器数据库名称
        /// </summary>
        public string DBName { get; set; }

        /// <summary>
        /// 服务器数据库用户名
        /// </summary>
        public string DBUser { get; set; }

        /// <summary>
        /// 服务器数据库密码
        /// </summary>
        public string DBPassword { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string PCAddr { get; set; }

        /// <summary>
        /// 服务器系统用户名
        /// </summary>
        public string PCUser { get; set; }

        /// <summary>
        /// 服务器系统密码
        /// </summary>
        public string PCPassword { get; set; }

        /// <summary>
        /// 后台服务器地址
        /// </summary>
        public string ServerAddress { get; set; }


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
            entity.EditedTime = model.EditedTime;

            entity.UserId = model.UserId;
            entity.DBAddr = model.DBAddr;
            entity.DBName = model.DBName;
            entity.DBUser = model.DBUser;
            entity.DBPassword = model.DBPassword;
            entity.PCAddr = model.PCAddr;
            entity.PCUser = model.PCUser;
            entity.PCPassword = model.PCPassword;
            entity.ServerAddress = model.ServerAddress;

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
            model.Id = entity.Id;
            model.EditedTime = entity.EditedTime;

            model.UserId = entity.UserId;
            model.DBAddr = entity.DBAddr;
            model.DBName = entity.DBName;
            model.DBUser = entity.DBUser;
            model.DBPassword = entity.DBPassword;
            model.PCAddr = entity.PCAddr;
            model.PCUser = entity.PCUser;
            model.PCPassword = entity.PCPassword;
            model.ServerAddress = entity.ServerAddress;

            return model;
        }
    }
}













