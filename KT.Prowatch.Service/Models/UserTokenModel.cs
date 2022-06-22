using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpModel;
using KT.Prowatch.Service.Entities;
using System.Collections.Generic;

namespace KT.Prowatch.Service.Models
{
    public class UserTokenModel
    {
        /// <summary>
        /// Id主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Token值
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 当前时间
        /// </summary>
        public long TimeNow { get; set; }
        /// <summary>
        /// 当前时间
        /// </summary>
        public long TimeOut { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public long EditedTime { get; set; }

        /// <summary>
        /// 数据库连接数据
        /// </summary>
        public string LoginUserId { get; set; }

        /// <summary>
        /// 数据库连接数据
        /// </summary>
        public LoginUserModel LoginUser { get; set; }

        public UserTokenModel()
        {
            Token = IdUtil.NewId();
            TimeNow = DateTimeUtil.UtcNowMillis();
            TimeOut = TimeNow.AddDayMillis(365000);
        }

        public static UserTokenEntity ToEntity(UserTokenModel model)
        {
            var entity = new UserTokenEntity();
            entity = SetEntity(entity, model);
            return entity;
        }

        public static List<UserTokenModel> ToModels(List<UserTokenEntity> entities)
        {
            var models = new List<UserTokenModel>();
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

        public static UserTokenEntity SetEntity(UserTokenEntity entity, UserTokenModel model)
        {
            entity.Id = model.Id;
            entity.Token = model.Token;
            entity.TimeNow = model.TimeNow;
            entity.TimeOut = model.TimeOut;
            entity.EditedTime = model.EditedTime;

            //关联边缘处理器
            if (!string.IsNullOrEmpty(model.LoginUserId))
            {
                if (entity.LoginUser == null || entity.LoginUser.Id != model.LoginUserId)
                {
                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    entity.LoginUser = new LoginUserEntity();
                    entity.LoginUser.Id = model.LoginUserId;
                }
            }

            return entity;
        }
        public static UserTokenModel ToModel(UserTokenEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new UserTokenModel();
            model = SetModel(model, entity);
            return model;
        }

        public static UserTokenModel SetModel(UserTokenModel model, UserTokenEntity entity)
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
            model.Token = entity.Token;
            model.TimeNow = entity.TimeNow;
            model.TimeOut = entity.TimeOut;
            model.EditedTime = entity.EditedTime;

            //关联边缘处理器
            model.LoginUserId = entity.LoginUser?.Id;
            model.LoginUser = LoginUserModel.SetModel(model.LoginUser, entity.LoginUser);

            return model;
        }
    }
}
