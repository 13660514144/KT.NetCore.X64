using KT.Quanta.Common.Enums;
using KT.Quanta.Model.Elevator;
using KT.Quanta.Service.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Service.Models
{
    public class ElevatorGroupModel : BaseQuantaModel
    {
        public ElevatorGroupModel()
        {
            ElevatorGroupFloorIds = new List<string>();
            ElevatorGroupFloors = new List<ElevatorGroupFloorModel>();
            ElevatorServerIds = new List<string>();
            ElevatorServers = new List<ElevatorServerModel>();
            ElevatorInfoIds = new List<string>();
            ElevatorInfos = new List<ElevatorInfoModel>();
        }

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
        public List<string> ElevatorGroupFloorIds { get; set; }

        /// <summary>
        /// 关联可去楼层
        /// </summary>
        public List<ElevatorGroupFloorModel> ElevatorGroupFloors { get; set; }

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

        public static List<RemoteDeviceModel> ToRemoteDevices(List<ElevatorGroupModel> models)
        {
            var remoteDevices = new List<RemoteDeviceModel>();
            if (models?.FirstOrDefault() != null)
            {
                foreach (var item in models)
                {
                    var remoteDevice = ToRemoteDevice(item);
                    remoteDevices.Add(remoteDevice);
                }
            }
            return remoteDevices;
        }

        public static RemoteDeviceModel ToRemoteDevice(ElevatorGroupModel model)
        {
            var remoteModel = new RemoteDeviceModel();
            remoteModel.Name = model.Name;
            remoteModel.DeviceType = DeviceTypeEnum.ELEVATOR_SERVER_GROUP.Value;
            remoteModel.BrandModel = model.BrandModel;
            remoteModel.Version = model.Version;
            remoteModel.DeviceId = model.Id;
             
            return remoteModel;
        }

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
            entity.ElevatorGroupFloors = new List<ElevatorGroupFloorEntity>();
            if (model.ElevatorGroupFloors?.FirstOrDefault() != null)
            {
                foreach (var item in model.ElevatorGroupFloors)
                {
                    var passAisleEntity = new ElevatorGroupFloorEntity();
                    passAisleEntity.RealFloorId = item.RealFloorId;
                    passAisleEntity.ElevatorGroup = entity;

                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    passAisleEntity.Floor = new FloorEntity();
                    passAisleEntity.Floor.Id = item.FloorId;

                    entity.ElevatorGroupFloors.Add(passAisleEntity);
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
         
        public static ElevatorGroupEntity ToEntity(ElevatorGroupModel model)
        {
            var entity = new ElevatorGroupEntity();
            var result = SetEntity(entity, model);
            return result;
        }
    }
}
