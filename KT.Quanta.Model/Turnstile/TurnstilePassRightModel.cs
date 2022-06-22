using KT.Common.Core.Utils;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Service.Turnstile.Dtos
{
    /// <summary>
    /// 通行权限
    /// </summary>
    public class TurnstilePassRightModel : BaseQuantaModel
    {
        public TurnstilePassRightModel()
        {
            CardDeviceRightGroupIds = new List<string>();
            CardDeviceRightGroups = new List<TurnstileCardDeviceRightGroupModel>();
        }

        /// <summary>
        /// Id主键
        /// </summary>
        [JsonIgnore]
        public new string Id { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 通行类型
        /// </summary>
        [JsonProperty("type")]
        public string AccessType { get; set; }

        /// <summary>
        /// 权限类型，如梯控、闸机、门禁等 
        /// <see cref="KT.Quanta.Common.Enums.RightTypeEnum"/>
        /// </summary>
        public string RightType { get; set; }

        /// <summary>
        /// 当前时间，UTC毫秒
        /// </summary>
        public long TimeNow { get; set; }

        /// <summary>
        /// 过期时间，UTC毫秒
        /// </summary>
        public long TimeOut { get; set; }

        /// <summary>
        /// 关联通道id
        /// </summary>
        public List<string> CardDeviceRightGroupIds { get; set; }

        /// <summary>
        /// 关联通道
        /// </summary>
        public List<TurnstileCardDeviceRightGroupModel> CardDeviceRightGroups { get; set; }

        /// <summary>
        /// 人员Id
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// 关联人员
        /// </summary>
        public TurnstilePersonModel Person { get; set; }

        /// <summary>
        /// 打印日志
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            return $"sign:{Sign} accessType:{AccessType} rightType:{RightType} personId:{PersonId} personName:{Person?.Name} cardDeviceIds:{CardDeviceRightGroupIds?.ToCommaString()}";
        }

        public static PassRightEntity SetEntity(PassRightEntity entity, TurnstilePassRightModel model, List<string> relationCardDeviceRightGroups)
        {
            if (model == null)
            {
                entity = null;
                return entity;
            }
            if (entity == null)
            {
                entity = new PassRightEntity();
            }

            entity.Id = model.Id;
            entity.Sign = model.Sign;
            entity.AccessType = model.AccessType;
            entity.RightType = model.RightType;
            entity.TimeNow = model.TimeNow;
            entity.TimeOut = model.TimeOut;
            entity.EditedTime = model.EditedTime;

            //关联卡通道
            if (relationCardDeviceRightGroups != null)
            {
                entity.RelationCardDeviceRightGroups = new List<PassRightRelationCardDeviceRightGroupEntity>();
                if (model.CardDeviceRightGroupIds.FirstOrDefault() != null)
                {
                    foreach (var item in model.CardDeviceRightGroupIds)
                    {
                        var passAisleEntity = new PassRightRelationCardDeviceRightGroupEntity();
                        passAisleEntity.PassRight = entity;

                        //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                        passAisleEntity.CardDeviceRightGroup = new CardDeviceRightGroupEntity();
                        passAisleEntity.CardDeviceRightGroup.Id = item;

                        entity.RelationCardDeviceRightGroups.Add(passAisleEntity);
                    }
                }
            }

            //关联人员楼层
            if (!string.IsNullOrEmpty(model.PersonId))
            {
                if (entity.Person == null || entity.Person.Id != model.PersonId)
                {
                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    entity.Person = new PersonEntity();
                    entity.Person.Id = model.PersonId;
                }
            }

            return entity;
        }

        public static PassRightEntity ToEntity(TurnstilePassRightModel model)
        {
            var entity = new PassRightEntity();
            var result = SetEntity(entity, model, model.CardDeviceRightGroupIds);
            return result;
        }
    }
}
