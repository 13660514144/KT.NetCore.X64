using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Elevator.Manage.Service.Models
{
    public class HandleElevatorDeviceModel : BaseElevatorModel
    {
        /// <summary>
        /// 设备Key
        /// </summary>
        public string DeviceKey { get; set; }

        /// <summary>
        /// 派梯设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        [JsonProperty("type")]
        public string ProductType { get; set; }

        /// <summary>
        /// 品牌型号
        /// </summary>
       [JsonProperty("equipmentType")]
        public string DeviceType { get; set; }

        /// <summary>
        /// 通信类型，如TCP、UDP
        /// </summary>
        public string CommunicateType { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 人脸AppID
        /// </summary>
        public string FaceAppId { get; set; }

        /// <summary>
        /// 人脸SDK KEY
        /// </summary>
        public string FaceSdkKey { get; set; }

        /// <summary>
        /// 人脸激活码
        /// </summary>
        public string FaceActivateCode { get; set; }

        /// <summary>
        /// 所在位置
        /// </summary>
        public string FloorId { get; set; }
         
        /// <summary>
        /// 关联所在楼层
        /// </summary>
        public FloorModel Floor { get; set; }

        /// <summary>
        /// 关联电梯组Id
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 关联电梯组
        /// </summary>
        public ElevatorGroupModel ElevatorGroup { get; set; }

        public static HandleElevatorDeviceEntity ToEntity(HandleElevatorDeviceModel model)
        {
            var entity = new HandleElevatorDeviceEntity();
            entity = SetEntity(entity, model);
            return entity;
        }

        public static List<HandleElevatorDeviceModel> ToModels(List<HandleElevatorDeviceEntity> entities)
        {
            var models = new List<HandleElevatorDeviceModel>();
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

        public static HandleElevatorDeviceEntity SetEntity(HandleElevatorDeviceEntity entity, HandleElevatorDeviceModel model)
        {
            entity.Id = model.Id; 
            //entity.DeviceKey = model.DeviceKey;
            entity.DeviceId = model.DeviceId;
            entity.Name = model.Name;
            entity.ProductType = model.ProductType;
            entity.DeviceType = model.DeviceType;
            entity.CommunicateType = model.CommunicateType;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.FaceAppId = model.FaceAppId;
            entity.FaceSdkKey = model.FaceSdkKey;
            entity.FaceActivateCode = model.FaceActivateCode;
            entity.EditedTime = model.EditedTime;

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

        public static HandleElevatorDeviceModel ToModel(HandleElevatorDeviceEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new HandleElevatorDeviceModel();
            model = SetModel(model, entity);
            return model;
        }

        public static HandleElevatorDeviceModel SetModel(HandleElevatorDeviceModel model, HandleElevatorDeviceEntity entity)
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
            model.DeviceKey = entity.DeviceKey;
            model.DeviceId = entity.DeviceId;
            model.Name = entity.Name;
            model.ProductType = entity.ProductType;
            model.DeviceType = entity.DeviceType;
            model.CommunicateType = entity.CommunicateType;
            model.IpAddress = entity.IpAddress;
            model.Port = entity.Port;
            model.FaceAppId = entity.FaceAppId;
            model.FaceSdkKey = entity.FaceSdkKey;
            model.FaceActivateCode = entity.FaceActivateCode;
            model.EditedTime = entity.EditedTime;

            //关联所在楼层
            model.FloorId = entity.Floor?.Id;
            model.Floor = FloorModel.SetModel(model.Floor, entity.Floor);

            //关联电梯组
            model.ElevatorGroupId = entity.ElevatorGroup?.Id;
            model.ElevatorGroup = ElevatorGroupModel.SetModel(model.ElevatorGroup, entity.ElevatorGroup, entity.ElevatorGroup?.ElevatorServers);

            return model;
        }

        internal static List<RemoteDeviceModel> ToRemoteDevices(List<HandleElevatorDeviceModel> models)
        {
            var remoteDevices = new List<RemoteDeviceModel>();
            if (models == null || models.FirstOrDefault() == null)
            {
                return remoteDevices;
            }
            foreach (var item in models)
            {
                remoteDevices.Add(ToRemoteDevice(item));
            }
            return remoteDevices;
        }

        internal static RemoteDeviceModel ToRemoteDevice(HandleElevatorDeviceModel model)
        {
            var remoteModel = new RemoteDeviceModel();
            remoteModel.DeviceType = model.DeviceType;
            remoteModel.DeviceKey = model.DeviceKey;
            remoteModel.DeviceId = model.Id;
            remoteModel.RealId = model.DeviceId;
            remoteModel.IpAddress = model.IpAddress;
            remoteModel.Port = model.Port;

            //remoteModel.IsOnline = model.IsOnline;
            //remoteModel.SyncDataTime = model.SyncDataTime;
            //remoteModel.IsSyncingData = model.IsSyncingData;
            //remoteModel.HasDistributeError = model.HasDistributeError;
            //remoteModel.ConnectionId = model.ConnectionId;

            return remoteModel;
        }
    }
}
