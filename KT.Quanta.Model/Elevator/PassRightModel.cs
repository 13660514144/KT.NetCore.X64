using KT.Common.Core.Utils;
using KT.Quanta.Service.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Service.Models
{
    /// <summary>
    /// 通行权限
    /// </summary>
    public class PassRightModel : BaseQuantaModel
    {
        public PassRightModel()
        {
            FloorIds = new List<string>();
            Floors = new List<FloorModel>();
        }

        /// <summary>
        /// Id主键
        /// </summary>
        [JsonIgnore]
        public new string Id { get; set; }

        /// <summary>
        /// 权限标记
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 通行类型
        /// </summary>
        [JsonProperty("type")]
        public string AccessType { get; set; }

        /// <summary>
        /// 权限类型
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
        /// 关联所在楼层Id
        /// </summary> 
        public string FloorId { get; set; }

        /// <summary>
        /// 关联所在楼层
        /// </summary> 
        public FloorModel Floor { get; set; }

        /// <summary>
        /// 是否开启前门
        /// </summary>
        public bool IsFront { get; set; }

        /// <summary>
        /// 是否开启前门
        /// </summary>
        public bool IsRear { get; set; }

        /// <summary>
        /// 人员Id
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// 关联人员
        /// </summary>
        public PersonModel Person { get; set; }

        /// <summary>
        /// 关联可去楼层Ids
        /// </summary> 
        public List<string> FloorIds { get; set; }

        /// <summary>
        /// 关联可去楼层
        /// </summary> 
        public List<FloorModel> Floors { get; set; }

        /// <summary>
        /// 打印日志
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            return $"sign:{Sign} accessType:{AccessType} rightType:{RightType} personId:{PersonId} personName:{Person?.Name} floorIds:{FloorIds?.ToCommaString()}";
        }

        public static PassRightEntity SetEntity(PassRightEntity entity, PassRightModel model, List<string> floorIds)
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
            entity.IsFront = model.IsFront;
            entity.IsRear = model.IsRear;

            //关联所在楼层
            if (!string.IsNullOrEmpty(model.FloorId))
            {
                if (entity.Floor == null || entity.Floor.Id != model.FloorId)
                {
                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    entity.Floor = new FloorEntity();
                    entity.Floor.Id = model.FloorId;
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

            //关联可去楼层
            if (floorIds != null)
            {
                entity.RelationFloors = new List<PassRightRelationFloorEntity>();
                if (floorIds.FirstOrDefault() != null)
                {
                    foreach (var item in floorIds)
                    {
                        var passAisleEntity = new PassRightRelationFloorEntity();
                        passAisleEntity.PassRight = entity;

                        //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                        passAisleEntity.Floor = new FloorEntity();
                        passAisleEntity.Floor.Id = item;

                        entity.RelationFloors.Add(passAisleEntity);
                    }
                }
            }

            return entity;
        }

        public static List<PassRightModel> ToModels(List<PassRightEntity> entities)
        {
            var results = new List<PassRightModel>();
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

        public static PassRightEntity ToEntity(PassRightModel model)
        {
            var entity = new PassRightEntity();
            var result = SetEntity(entity, model, model.FloorIds);
            return result;
        }

        public static PassRightModel ToModel(PassRightEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new PassRightModel();
            model = SetModel(model, entity);
            return model;
        }

        public static PassRightModel SetModel(PassRightModel model, PassRightEntity entity)
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
            model.Sign = entity.Sign;
            model.AccessType = entity.AccessType;
            model.RightType = entity.RightType;
            model.TimeNow = entity.TimeNow;
            model.TimeOut = entity.TimeOut;
            model.EditedTime = entity.EditedTime;
            model.IsFront = entity.IsFront;
            model.IsRear = entity.IsRear;

            //关联所在楼层
            model.FloorId = entity.Floor?.Id;
            model.Floor = FloorModel.SetModel(model.Floor, entity.Floor);

            //关联人员楼层
            model.PersonId = entity.Person?.Id;
            model.Person = PersonModel.SetModel(model.Person, entity.Person);

            //关联可去楼层
            model.FloorIds = new List<string>();
            model.Floors = new List<FloorModel>();
            if (entity.RelationFloors != null && entity.RelationFloors.FirstOrDefault() != null)
            {
                foreach (var item in entity.RelationFloors)
                {
                    var passAisleModel = FloorModel.ToModel(item.Floor);
                    if (passAisleModel == null)
                    {
                        continue;
                    }
                    model.FloorIds.Add(passAisleModel.Id);
                    model.Floors.Add(passAisleModel);
                }
            }

            return model;
        }
    }
}
