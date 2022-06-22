using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace KT.Elevator.Manage.Service.Models
{
    public class CardDeviceModel : BaseElevatorModel
    {
        /// <summary>
        /// 边缘处理器自动生成Key
        /// </summary>
        public string DeviceKey { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 卡类型
        /// 区卡种,如IC、QR
        /// </summary>
        [JsonProperty("cardType")]
        public string DeviceType { get; set; }

        /// <summary>
        /// 品牌型号
        /// 区分同一卡种不同设备,如 S122读卡器、M31二维码扫描仪
        /// </summary>
        [JsonProperty("type")]
        public string BrandModel { get; set; }

        /// <summary>
        /// 串口名
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 关联边缘处理器Id
        /// </summary>
        public string ProcessorId { get; set; }

        /// <summary>
        /// 关联边缘处理器
        /// </summary>
        public ProcessorModel Processor { get; set; }

        /// <summary>
        /// 关联派梯设备Id
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }

        /// <summary>
        /// 关联派梯设备
        /// </summary>
        public HandleElevatorDeviceModel HandleElevatorDevice { get; set; }

        /// <summary>
        /// 关联串口配置Id
        /// </summary>
        public string SerialConfigId { get; set; }

        /// <summary>
        /// 关联串口配置
        /// </summary>
        public SerialConfigModel SerialConfig { get; set; }

        public static CardDeviceEntity ToEntity(CardDeviceModel model)
        {
            var entity = new CardDeviceEntity();
            entity = SetEntity(entity, model);
            return entity;
        }

        public static List<CardDeviceModel> ToModels(List<CardDeviceEntity> entities)
        {
            var models = new List<CardDeviceModel>();
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

        public static CardDeviceEntity SetEntity(CardDeviceEntity entity, CardDeviceModel model)
        {
            entity.Id = model.Id;
            entity.DeviceKey = model.DeviceKey;
            entity.Name = model.Name;
            entity.BrandModel = model.BrandModel;
            entity.DeviceType = model.DeviceType;
            entity.PortName = model.PortName;
            model.EditedTime = entity.EditedTime;

            //关联边缘处理器
            if (!string.IsNullOrEmpty(model.ProcessorId))
            {
                if (entity.Processor == null || entity.Processor.Id != model.ProcessorId)
                {
                    entity.Processor = new ProcessorEntity();
                    entity.Processor.Id = model.ProcessorId;
                }
            }

            //关联派梯设备
            if (!string.IsNullOrEmpty(model.HandleElevatorDeviceId))
            {
                if (entity.HandElevatorDevice == null || entity.HandElevatorDevice.Id != model.HandleElevatorDeviceId)
                {
                    entity.HandElevatorDevice = new HandleElevatorDeviceEntity();
                    entity.HandElevatorDevice.Id = model.HandleElevatorDeviceId;
                }
            }

            //关联串口配置
            if (!string.IsNullOrEmpty(model.SerialConfigId))
            {
                if (entity.SerialConfig == null || entity.SerialConfig.Id != model.SerialConfigId)
                {
                    entity.SerialConfig = new SerialConfigEntity();
                    entity.SerialConfig.Id = model.SerialConfigId;
                }
            }

            return entity;
        }
        public static CardDeviceModel ToModel(CardDeviceEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new CardDeviceModel();

            model = SetModel(model, entity);
            return model;
        }

        public static CardDeviceModel SetModel(CardDeviceModel model, CardDeviceEntity entity)
        {
            if (entity.Processor?.CardDevices != null)
            {
                entity.Processor.CardDevices = null;
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
            model.DeviceKey = entity.DeviceKey;
            model.Name = entity.Name;
            model.BrandModel = entity.BrandModel;
            model.DeviceType = entity.DeviceType;
            model.PortName = entity.PortName;
            model.EditedTime = entity.EditedTime;

            //关联边缘处理器
            model.ProcessorId = entity.Processor?.Id;
            model.Processor = ProcessorModel.SetModel(model.Processor, entity.Processor);

            //关联派梯设备
            model.HandleElevatorDeviceId = entity.HandElevatorDevice?.Id;
            model.HandleElevatorDevice = HandleElevatorDeviceModel.SetModel(model.HandleElevatorDevice, entity.HandElevatorDevice);

            //关联串口配置 
            model.SerialConfigId = entity.SerialConfig?.Id;
            model.SerialConfig = SerialConfigModel.SetModel(model.SerialConfig, entity.SerialConfig);

            return model;
        }

        internal static RemoteDeviceModel ToRemoteDevice(CardDeviceModel model)
        {
            var remoteModel = new RemoteDeviceModel();
            remoteModel.DeviceKey = model.DeviceKey;
            remoteModel.DeviceId = model.Id;
            remoteModel.DeviceType = model.DeviceType;
            remoteModel.BrandModel = model.BrandModel;
            remoteModel.DeviceId = model.Id;
            remoteModel.RealId = string.Empty;

            var ipAndPort = model.PortName.Split(':');
            remoteModel.IpAddress = ipAndPort[0];
            remoteModel.Port = Convert.ToInt32(ipAndPort[1]);

            return remoteModel;
        }
    }
}
