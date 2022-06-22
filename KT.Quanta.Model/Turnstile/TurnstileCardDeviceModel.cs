using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KT.Quanta.Service.Turnstile.Dtos
{
    /// <summary>
    /// 读卡器设备信息
    /// </summary>
    public class TurnstileCardDeviceModel : BaseQuantaModel
    {
        public TurnstileCardDeviceModel()
        {

        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备类型
        /// 关联 <see cref="CardDeviceTypeEnum"/>
        /// 区分同一卡种不同设备,如 S122读卡器、M31二维码扫描仪
        /// </summary>
        [JsonProperty("type")]
        public string BrandModel { get; set; }

        /// <summary>
        /// 卡类型
        /// 关联 <see cref="AccessTypeEnum"  ，区卡种,如IC、QR
        /// </summary>
        [JsonProperty("cardType")]
        public string CardDeviceType { get; set; }

        /// <summary>
        /// 卡类型 
        /// </summary> 
        public string DeviceType { get; set; }

        /// <summary>
        /// 串口名
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 继电器输出口
        /// </summary>
        public int RelayDeviceOut { get; set; }

        /// <summary>
        /// 关联边缘处理器Id
        /// </summary>
        public string ProcessorId { get; set; }

        /// <summary>
        /// 关联边缘处理器
        /// </summary>
        public TurnstileProcessorModel Processor { get; set; }

        /// <summary>
        /// 关联继电器Id
        /// </summary>
        public string RelayDeviceId { get; set; }

        /// <summary>
        /// 关联继电器
        /// </summary>
        public TurnstileRelayDeviceModel RelayDevice { get; set; }

        /// <summary>
        /// 串口配置Id
        /// </summary>
        public string SerialConfigId { get; set; }

        /// <summary>
        /// 串口配置
        /// </summary>
        public SerialConfigModel SerialConfig { get; set; }

        /// <summary>
        /// 派梯设备id
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }

        public static CardDeviceEntity ToEntity(TurnstileCardDeviceModel model)
        {
            var entity = new CardDeviceEntity();
            entity = SetEntity(entity, model);
            return entity;
        }

        //public static List<TurnstileCardDeviceModel> ToModels(List<CardDeviceEntity> entities)
        //{
        //    var models = new List<TurnstileCardDeviceModel>();
        //    if (entities == null)
        //    {
        //        return models;
        //    }
        //    foreach (var item in entities)
        //    {
        //        var entity = ToModel(item);

        //        models.Add(entity);
        //    }
        //    return models;
        //}

        public static CardDeviceEntity SetEntity(CardDeviceEntity entity, TurnstileCardDeviceModel model)
        {
            entity.Id = model.Id;
            entity.EditedTime = model.EditedTime;

            entity.Name = model.Name;
            entity.BrandModel = model.BrandModel;
            entity.CardDeviceType = model.CardDeviceType;
            entity.DeviceType = model.DeviceType;
            entity.PortName = model.PortName;
            entity.RelayDeviceOut = model.RelayDeviceOut;
            entity.HandleElevatorDeviceId = model.HandleElevatorDeviceId;

            //关联边缘处理器
            if (!string.IsNullOrEmpty(model.ProcessorId))
            {
                if (entity.Processor == null || entity.Processor.Id != model.ProcessorId)
                {
                    entity.Processor = new ProcessorEntity();
                    entity.Processor.Id = model.ProcessorId;
                }
            }

            //关联继电器
            if (!string.IsNullOrEmpty(model.RelayDeviceId))
            {
                if (entity.RelayDevice == null || entity.RelayDevice.Id != model.RelayDeviceId)
                {
                    entity.RelayDevice = new RelayDeviceEntity();
                    entity.RelayDevice.Id = model.RelayDeviceId;
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
        //public static TurnstileCardDeviceModel ToModel(CardDeviceEntity entity)
        //{
        //    if (entity == null)
        //    {
        //        return null;
        //    }
        //    var model = new TurnstileCardDeviceModel();
        //    model = SetModel(model, entity);
        //    return model;
        //}

        //public static TurnstileCardDeviceModel SetModel(TurnstileCardDeviceModel model, CardDeviceEntity entity)
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
        //    model.EditedTime = entity.EditedTime;

        //    model.Name = entity.Name;
        //    model.BrandModel = entity.BrandModel;
        //    model.CardDeviceType = entity.CardDeviceType;
        //    model.DeviceType = entity.DeviceType;
        //    model.PortName = entity.PortName;
        //    model.RelayDeviceOut = entity.RelayDeviceOut;
        //    model.HandleElevatorDeviceId = entity.HandleElevatorDeviceId;

        //    //关联边缘处理器
        //    model.ProcessorId = entity.Processor?.Id;
        //    model.Processor = TurnstileProcessorModel.SetModel(model.Processor, entity.Processor);

        //    //关联继电器
        //    model.RelayDeviceId = entity.RelayDevice?.Id;
        //    model.RelayDevice = TurnstileRelayDeviceModel.SetModel(model.RelayDevice, entity.RelayDevice);

        //    //关联串口配置 
        //    model.SerialConfigId = entity.SerialConfig?.Id;
        //    model.SerialConfig = SerialConfigModel.SetModel(model.SerialConfig, entity.SerialConfig);

        //    return model;
        //}
        public static RemoteDeviceModel ToRemoteDevice(TurnstileCardDeviceModel model)
        {
            var remoteModel = new RemoteDeviceModel();
            remoteModel.Name = model.Name;
            remoteModel.DeviceId = model.Id;
            remoteModel.CardDeviceType = model.CardDeviceType;
            remoteModel.DeviceType = model.DeviceType;
            remoteModel.BrandModel = model.BrandModel;
            remoteModel.DeviceId = model.Id;
            remoteModel.RealId = string.Empty;

            var ipAndPort = model.PortName.Split(':');
            //remoteModel.IpAddress = ipAndPort[0];
            //remoteModel.Port = Convert.ToInt32(ipAndPort[1]);

            remoteModel.CommunicateDeviceInfos = new List<CommunicateDeviceInfoModel>();
            var communicateDeviceInfo = new CommunicateDeviceInfoModel();
            communicateDeviceInfo.IpAddress = ipAndPort[0];
            if (ipAndPort.Length > 1)
            {
                communicateDeviceInfo.Port = Convert.ToInt32(ipAndPort[1]);
            }
            else
            {
                communicateDeviceInfo.Port = 80;
            }
            remoteModel.CommunicateDeviceInfos.Add(communicateDeviceInfo);

            return remoteModel;
        }

    }
}
