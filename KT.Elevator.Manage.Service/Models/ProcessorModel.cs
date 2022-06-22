using KT.Common.Core.Utils;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Manage.Service.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using KT.Elevator.Common.Enums;
using Newtonsoft.Json;

namespace KT.Elevator.Manage.Service.Models
{
    /// <summary>
    /// 边缘处理器
    /// </summary>
    public class ProcessorModel : BaseElevatorModel
    {
        /// <summary>
        /// 边缘处理器自动生成Key
        /// </summary>
        public string ProcessorKey { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 品牌型号
        /// </summary>
        [JsonProperty("type")]
        public string BrandModel { get; set; }

        /// <summary>
        /// 所在位置
        /// </summary>
        public string FloorId { get; set; }

        /// <summary>
        /// 所在位置
        /// </summary>
        public FloorModel Floor { get; set; }

        /// <summary>
        /// 所在位置
        /// </summary>
        public List<string> ProcessorFloorIds { get; set; }

        /// <summary>
        /// 所在位置
        /// </summary>
        public List<ProcessorFloorModel> ProcessorFloors { get; set; }

        /// <summary>
        /// 关联卡设备Ids
        /// </summary>
        public List<string> CardDeviceIds { get; set; }

        /// <summary>
        /// 关联卡设备
        /// </summary>
        public List<CardDeviceModel> CardDevices { get; set; }

        ///// <summary>
        ///// 是否在线
        ///// </summary>
        //public bool IsOnline { get; set; }

        ///// <summary>
        ///// 最后同步数据时间
        ///// </summary>
        //public long SyncDataTime { get; set; }

        ///// <summary>
        ///// 是否正在同步数据，同步数据不能同时进行，不持久化
        ///// </summary>
        //public bool IsSyncingData { get; set; }

        /////// <summary>
        /////// 是否有未完成同步的数据完成，不持久化
        /////// 设备新接入时，会直接推送数据库中的所有数据，当前标识记录同步状态
        /////// 有新数据传入时，会自动推送当条数据，如果已经同步完成，会时时更新最后同步时间，否则不更新最后同步时间
        /////// </summary>
        ///// <summary>
        ///// 是否有推送错误数据
        ///// </summary>
        //public bool HasDistributeError { get; set; }

        ///// <summary>
        ///// 连接Id,每次连接都不一样，所以不用持久化
        ///// </summary>
        //public string ConnectionId { get; set; }

        public static ProcessorModel ToModel(ProcessorEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new ProcessorModel();
            model = SetModel(model, entity);
            return model;
        }

        public static ProcessorModel SetModel(ProcessorModel model, ProcessorEntity entity)
        {
            if (entity?.ProcessorFloors?.FirstOrDefault() != null)
            {
                entity.ProcessorFloors.ForEach(x => x.Processor = null);
            }

            if (entity == null)
            {
                return null;
            }
            if (model == null)
            {
                return ToModel(entity);
            }

            model.Id = entity.Id;
            model.Name = entity.Name;
            model.IpAddress = entity.IpAddress;
            model.Port = entity.Port;
            model.BrandModel = entity.BrandModel;
            model.ProcessorKey = entity.ProcessorKey;
            model.EditedTime = entity.EditedTime;

            //model.SyncDataTime = entity.SyncDataTime;
            //model.HasDistributeError = entity.HasDistributeError;

            //关联目的楼层 
            model.FloorId = entity.Floor?.Id;
            model.Floor = FloorModel.SetModel(model.Floor, entity.Floor);

            //关联可去楼层
            model.ProcessorFloorIds = new List<string>();
            model.ProcessorFloors = new List<ProcessorFloorModel>();
            if (entity.ProcessorFloors != null && entity.ProcessorFloors.FirstOrDefault() != null)
            {
                foreach (var item in entity.ProcessorFloors)
                {
                    var passAisleModel = ProcessorFloorModel.ToModel(item);
                    if (passAisleModel == null)
                    {
                        continue;
                    }
                    model.ProcessorFloorIds.Add(passAisleModel.Id);
                    model.ProcessorFloors.Add(passAisleModel);
                }
            }

            //关联可去楼层
            model.CardDeviceIds = new List<string>();
            model.CardDevices = new List<CardDeviceModel>();
            if (entity.CardDevices != null && entity.CardDevices.FirstOrDefault() != null)
            {
                foreach (var item in entity.CardDevices)
                {
                    var passAisleModel = CardDeviceModel.ToModel(item);
                    if (passAisleModel == null)
                    {
                        continue;
                    }
                    model.CardDeviceIds.Add(passAisleModel.Id);
                    model.CardDevices.Add(passAisleModel);
                }
            }

            return model;
        }

        public static List<ProcessorModel> ToModels(List<ProcessorEntity> entities)
        {
            var models = new List<ProcessorModel>();
            if (entities == null)
            {
                return models;
            }
            foreach (var item in entities)
            {
                var model = ToModel(item);

                models.Add(model);
            }
            return models;
        }

        public static ProcessorEntity SetEntity(ProcessorEntity entity, ProcessorModel model)
        {
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.BrandModel = model.BrandModel;
            entity.EditedTime = model.EditedTime;
            entity.ProcessorKey = model.ProcessorKey;

            //entity.SyncDataTime = model.SyncDataTime;
            //entity.HasDistributeError = model.HasDistributeError;

            //关联目的楼层 
            if (!string.IsNullOrEmpty(model.FloorId))
            {
                if (entity.Floor == null || entity.Floor.Id != model.FloorId)
                {
                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    entity.Floor = new FloorEntity();
                    entity.Floor.Id = model.FloorId;
                }
            }

            //关联楼层映射
            entity.ProcessorFloors = new List<ProcessorFloorEntity>();
            if (model.ProcessorFloorIds != null && model.ProcessorFloorIds.FirstOrDefault() != null)
            {
                foreach (var item in model.ProcessorFloorIds)
                {
                    var passAisleEntity = new ProcessorFloorEntity();
                    passAisleEntity.Id = item;
                    passAisleEntity.Processor = entity;

                    entity.ProcessorFloors.Add(passAisleEntity);
                }
            }

            //关联卡设备
            entity.CardDevices = new List<CardDeviceEntity>();
            if (model.CardDeviceIds != null && model.CardDeviceIds.FirstOrDefault() != null)
            {
                foreach (var item in model.CardDeviceIds)
                {
                    var passAisleEntity = new CardDeviceEntity();
                    passAisleEntity.Id = item;
                    passAisleEntity.Processor = entity;

                    entity.CardDevices.Add(passAisleEntity);
                }
            }

            return entity;
        }

        /// <summary>
        /// 旨定界面数据可修改的列
        /// </summary>
        /// <param name="entity">要修改的对象</param>
        /// <param name="model">接收的修改数据</param>
        /// <returns>新的要修改的对象</returns>
        public static ProcessorEntity SetEditEntity(ProcessorEntity entity, ProcessorModel model)
        {
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.BrandModel = model.BrandModel;

            //关联目的楼层 
            if (!string.IsNullOrEmpty(model.FloorId))
            {
                if (entity.Floor == null || entity.Floor.Id != model.FloorId)
                {
                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    entity.Floor = new FloorEntity();
                    entity.Floor.Id = model.FloorId;
                }
            }

            return entity;
        }

        public static ProcessorEntity ToEntity(ProcessorModel model)
        {
            var entity = new ProcessorEntity();

            entity = SetEntity(entity, model);

            return entity;
        }

        internal static List<RemoteDeviceModel> ToRemoteDevices(List<ProcessorModel> models)
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

        internal static RemoteDeviceModel ToRemoteDevice(ProcessorModel model)
        {
            var remoteModel = new RemoteDeviceModel();
            remoteModel.DeviceKey = model.ProcessorKey;
            remoteModel.DeviceId = model.Id;
            remoteModel.DeviceType = DeviceTypeEnum.ELEVATOR_PROCESSOR.Value;
            remoteModel.BrandModel = model.BrandModel;
            remoteModel.RealId = string.Empty;
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
