using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Service.Turnstile.Dtos
{
    /// <summary>
    /// 边缘处理器
    /// </summary>
    public class TurnstileProcessorModel : BaseQuantaModel
    {
        public TurnstileProcessorModel()
        {
            CardDeviceIds = new List<string>();
            CardDevices = new List<CardDeviceModel>();
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 品牌型号
        /// </summary>
        [JsonProperty("type")]
        public string BrandModel { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 所在位置
        /// </summary>
        public string FloorId { get; set; }

        /// <summary>
        /// 所在位置
        /// </summary>
        public FloorModel Floor { get; set; }

        /// <summary>
        /// 关联卡设备Ids
        /// </summary>
        public List<string> CardDeviceIds { get; set; }

        /// <summary>
        /// 关联卡设备
        /// </summary>
        public List<CardDeviceModel> CardDevices { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }

        //public static TurnstileProcessorModel ToModel(ProcessorEntity entity)
        //{
        //    if (entity == null)
        //    {
        //        return null;
        //    }
        //    var model = new TurnstileProcessorModel();
        //    model = SetModel(model, entity);
        //    return model;
        //}
        //public static TurnstileProcessorModel SetModel(TurnstileProcessorModel model, ProcessorEntity entity)
        //{
        //    if (entity == null)
        //    {
        //        return null;
        //    }
        //    if (model == null)
        //    {
        //        return ToModel(entity);
        //    }

        //    model.Id = entity.Id;
        //    model.Name = entity.Name;
        //    model.DeviceType = entity.DeviceType;
        //    model.BrandModel = entity.BrandModel;
        //    model.IpAddress = entity.IpAddress;
        //    model.Port = entity.Port;
        //    model.EditedTime = entity.EditedTime;

        //    //关联所在楼层
        //    model.FloorId = entity.Floor?.Id;
        //    model.Floor = FloorModel.SetModel(model.Floor, entity.Floor);

        //    //关联可去楼层
        //    model.CardDeviceIds = new List<string>();
        //    model.CardDevices = new List<CardDeviceModel>();
        //    if (entity.CardDevices != null && entity.CardDevices.FirstOrDefault() != null)
        //    {
        //        foreach (var item in entity.CardDevices)
        //        {
        //            var passAisleModel = CardDeviceModel.ToModel(item);
        //            if (passAisleModel == null)
        //            {
        //                continue;
        //            }
        //            model.CardDeviceIds.Add(passAisleModel.Id);
        //            model.CardDevices.Add(passAisleModel);
        //        }
        //    }

        //    return model;
        //}

        //public static List<TurnstileProcessorModel> ToModels(List<ProcessorEntity> entities)
        //{
        //    var models = new List<TurnstileProcessorModel>();
        //    if (entities == null)
        //    {
        //        return models;
        //    }
        //    foreach (var item in entities)
        //    {
        //        var model = ToModel(item);

        //        models.Add(model);
        //    }
        //    return models;
        //}

        public static ProcessorEntity SetEntity(ProcessorEntity entity, TurnstileProcessorModel model)
        {
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.DeviceType = model.DeviceType;
            entity.BrandModel = model.BrandModel;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
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

        public static ProcessorEntity ToEntity(TurnstileProcessorModel model)
        {
            var entity = new ProcessorEntity();

            entity = SetEntity(entity, model);

            return entity;
        }

        public static ProcessorEntity SetEditEntity(ProcessorEntity entity, TurnstileProcessorModel model)
        {
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.BrandModel = model.BrandModel;
            entity.DeviceType = model.DeviceType;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;

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

        public string GetHttpAddress()
        {
            var url = "http://" + IpAddress + ":" + Port + "/";
            return url;
        }

        public static RemoteDeviceModel ToRemoteDevice(TurnstileProcessorModel model)
        {
            var remoteModel = new RemoteDeviceModel();
            remoteModel.Name = model.Name;
            remoteModel.DeviceId = model.Id;
            remoteModel.DeviceType = model.DeviceType;
            remoteModel.BrandModel = model.BrandModel;
            remoteModel.RealId = string.Empty;
            //remoteModel.IpAddress = model.IpAddress;
            //remoteModel.Port = model.Port;

            remoteModel.CommunicateDeviceInfos = new List<CommunicateDeviceInfoModel>();
            var communicateDeviceInfo = new CommunicateDeviceInfoModel();
            communicateDeviceInfo.IpAddress = model.IpAddress;
            communicateDeviceInfo.Port = model.Port;
            remoteModel.CommunicateDeviceInfos.Add(communicateDeviceInfo);

            //remoteModel.IsOnline = model.IsOnline;
            //remoteModel.SyncDataTime = model.SyncDataTime;
            //remoteModel.IsSyncingData = model.IsSyncingData;
            //remoteModel.HasDistributeError = model.HasDistributeError;
            //remoteModel.ConnectionId = model.ConnectionId;

            return remoteModel;
        }
    }
}
