using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KT.Quanta.Service.Models
{
    public class CardDeviceModel : BaseQuantaModel
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 卡类型
        /// 区卡种,如IC、QR
        /// </summary>
        [JsonProperty("equipmentType")]
        public string CardDeviceType { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
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
 
        public static CardDeviceEntity SetEntity(CardDeviceEntity entity, CardDeviceModel model)
        {
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.BrandModel = model.BrandModel;
            entity.CardDeviceType = model.CardDeviceType;
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
                if (entity.HandleElevatorDevice == null || entity.HandleElevatorDevice.Id != model.HandleElevatorDeviceId)
                {
                    entity.HandleElevatorDevice = new HandleElevatorDeviceEntity();
                    entity.HandleElevatorDevice.Id = model.HandleElevatorDeviceId;
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

        public static RemoteDeviceModel ToRemoteDevice(CardDeviceModel model)
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
