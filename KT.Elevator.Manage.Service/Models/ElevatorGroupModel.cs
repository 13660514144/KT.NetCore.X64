using KT.Elevator.Manage.Service.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Elevator.Manage.Service.Models
{
    public class ElevatorGroupModel : BaseElevatorModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 品牌型号
        /// </summary>
        [JsonProperty("type")]
        public string BrandModel { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 关联所在大厦Id
        /// </summary>
        public string EdificeId { get; set; }

        /// <summary>
        /// 关联所在大厦
        /// </summary>
        public EdificeModel Edifice { get; set; }

        /// <summary>
        /// 关联可去楼层Id
        /// </summary>
        public List<string> FloorIds { get; set; }

        /// <summary>
        /// 关联可去楼层
        /// </summary>
        public List<FloorModel> Floors { get; set; }

        /// <summary>
        /// 关联电梯服务器
        /// </summary>
        public List<string> ElevatorServerIds { get; set; }

        /// <summary>
        ///关联 电梯服务器
        /// </summary>
        public List<ElevatorServerModel> ElevatorServers { get; set; }

        /// <summary>
        /// 关联电梯服务器
        /// </summary>
        public List<string> ElevatorInfoIds { get; set; }

        /// <summary>
        ///关联 电梯服务器
        /// </summary>
        public List<ElevatorInfoModel> ElevatorInfos { get; set; }


        public static ElevatorGroupEntity SetEntity(ElevatorGroupEntity entity, ElevatorGroupModel model)
        {
            if (model == null)
            {
                entity = null;
                return entity;
            }

            if (entity == null)
            {
                entity = new ElevatorGroupEntity();
            }

            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.BrandModel = model.BrandModel;
            entity.Version = model.Version;
            entity.EditedTime = model.EditedTime;

            //关联所在大厦
            if (!string.IsNullOrEmpty(model.EdificeId))
            {
                if (entity.Edifice == null || entity.Edifice.Id != model.EdificeId)
                {
                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    entity.Edifice = new EdificeEntity();
                    entity.Edifice.Id = model.EdificeId;
                }
            }

            //关联可去楼层
            entity.RelationFloors = new List<ElevatorGroupRelationFloorEntity>();
            if (model.FloorIds != null && model.FloorIds.FirstOrDefault() != null)
            {
                foreach (var item in model.FloorIds)
                {
                    var passAisleEntity = new ElevatorGroupRelationFloorEntity();
                    passAisleEntity.ElevatorGroup = entity;

                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    passAisleEntity.Floor = new FloorEntity();
                    passAisleEntity.Floor.Id = item;

                    entity.RelationFloors.Add(passAisleEntity);
                }
            }

            //关联电梯服务
            entity.ElevatorServers = new List<ElevatorServerEntity>();
            if (model.ElevatorServerIds != null && model.ElevatorServerIds.FirstOrDefault() != null)
            {
                foreach (var item in model.ElevatorServerIds)
                {
                    var elevatorServer = new ElevatorServerEntity();
                    elevatorServer.ElevatorGroup = entity;
                    elevatorServer.Id = item;

                    entity.ElevatorServers.Add(elevatorServer);
                }
            }

            //关联电梯服务
            entity.ElevatorInfos = new List<ElevatorInfoEntity>();
            if (model.ElevatorInfoIds != null && model.ElevatorInfoIds.FirstOrDefault() != null)
            {
                foreach (var item in model.ElevatorInfoIds)
                {
                    var ElevatorInfo = new ElevatorInfoEntity();
                    ElevatorInfo.ElevatorGroup = entity;
                    ElevatorInfo.Id = item;

                    entity.ElevatorInfos.Add(ElevatorInfo);
                }
            }

            return entity;
        }

        public static List<ElevatorGroupModel> ToModels(List<ElevatorGroupEntity> entities)
        {
            var results = new List<ElevatorGroupModel>();
            if (entities == null)
            {
                return results;
            }
            foreach (var item in entities)
            {
                var result = ToModel(item, item.ElevatorServers);
                results.Add(result);
            }
            return results;
        }

        public static ElevatorGroupEntity ToEntity(ElevatorGroupModel model)
        {
            var entity = new ElevatorGroupEntity();
            var result = SetEntity(entity, model);
            return result;
        }

        public static ElevatorGroupModel ToModel(ElevatorGroupEntity entity, List<ElevatorServerEntity> elevatorServers)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new ElevatorGroupModel();
            model = SetModel(model, entity, elevatorServers);
            return model;
        }

        public static ElevatorGroupModel SetModel(ElevatorGroupModel model, ElevatorGroupEntity entity, List<ElevatorServerEntity> elevatorServers)
        {
            if (entity?.ElevatorInfos?.FirstOrDefault() != null)
            {
                entity.ElevatorInfos.ForEach(x => x.ElevatorGroup = null);
            }
            if (entity == null)
            {
                return null;
            }
            if (model == null)
            {
                return ToModel(entity, elevatorServers);
            }
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.BrandModel = entity.BrandModel;
            model.Version = entity.Version;
            model.EditedTime = entity.EditedTime;

            //关联所在大厦 
            model.EdificeId = entity.Edifice?.Id;
            model.Edifice = EdificeModel.SetModel(model.Edifice, entity.Edifice);

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

            //关联电梯服务
            model.ElevatorServerIds = new List<string>();
            model.ElevatorServers = new List<ElevatorServerModel>();
            if (entity.ElevatorServers != null && entity.ElevatorServers.FirstOrDefault() != null)
            {
                foreach (var item in entity.ElevatorServers)
                {
                    item.ElevatorGroup = null;
                    var passAisleModel = ElevatorServerModel.ToModel(item);
                    if (passAisleModel == null)
                    {
                        continue;
                    }
                    model.ElevatorServerIds.Add(passAisleModel.Id);
                    model.ElevatorServers.Add(passAisleModel);
                }
            }

            //关联电梯服务
            model.ElevatorInfoIds = new List<string>();
            model.ElevatorInfos = new List<ElevatorInfoModel>();
            if (entity.ElevatorInfos != null && entity.ElevatorInfos.FirstOrDefault() != null)
            {
                foreach (var item in entity.ElevatorInfos)
                {
                    item.ElevatorGroup = null;
                    var passAisleModel = ElevatorInfoModel.ToModel(item);
                    if (passAisleModel == null)
                    {
                        continue;
                    }
                    model.ElevatorInfoIds.Add(passAisleModel.Id);
                    model.ElevatorInfos.Add(passAisleModel);
                }
            }

            return model;
        }
    }
}
