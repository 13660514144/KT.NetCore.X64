using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Service.Models
{
    /// <summary>
    /// 边缘处理器
    /// </summary>
    public class ProcessorModel : BaseQuantaModel
    {
        public ProcessorModel()
        {
            ProcessorFloorIds = new List<string>();
            ProcessorFloors = new List<ProcessorFloorModel>();
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

        public static ProcessorEntity SetEntity(ProcessorEntity entity, ProcessorModel model)
        {
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.BrandModel = model.BrandModel;
            entity.DeviceType = model.DeviceType;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.EditedTime = model.EditedTime;

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

        public static ProcessorEntity ToEntity(ProcessorModel model)
        {
            var entity = new ProcessorEntity();

            entity = SetEntity(entity, model);

            return entity;
        }

        public static List<RemoteDeviceModel> ToRemoteDevices(List<ProcessorModel> models)
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

        public static RemoteDeviceModel ToRemoteDevice(ProcessorModel model)
        {
            var remoteModel = new RemoteDeviceModel();
            remoteModel.Name = model.Name;
            remoteModel.DeviceId = model.Id;
            remoteModel.DeviceType = model.DeviceType;
            remoteModel.BrandModel = model.BrandModel;
            remoteModel.RealId = string.Empty;

            remoteModel.CommunicateDeviceInfos = new List<CommunicateDeviceInfoModel>();
            var communicateDeviceInfo = new CommunicateDeviceInfoModel();
            communicateDeviceInfo.IpAddress = model.IpAddress;
            communicateDeviceInfo.Port = model.Port;
            remoteModel.CommunicateDeviceInfos.Add(communicateDeviceInfo);

            return remoteModel;
        }
    }
}
