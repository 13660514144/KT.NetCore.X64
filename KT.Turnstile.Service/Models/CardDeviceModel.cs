using KT.Turnstile.Entity.Entities;
using System.Collections.Generic;
using KT.Turnstile.Common.Enums;
using KT.Common.Core.Utils;
using Newtonsoft.Json;

namespace KT.Turnstile.Model.Models
{
    /// <summary>
    /// 读卡器设备信息
    /// </summary>
    public class CardDeviceModel : BaseTurnstileModel
    {
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
        public string Type { get; set; }

        /// <summary>
        /// 卡类型
        /// 关联 <see cref="CardTypeEnum"  ，区卡种,如IC、QR
        /// </summary>
        [JsonProperty("cardType")]
        public string CardType { get; set; }

        /// <summary>
        /// 串口名
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 继电器输出口
        /// </summary>
        public int RelayDeviceOut { get; set; }

        /// <summary>
        /// 设备描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 关联边缘处理器Id
        /// </summary>
        public string ProcessorId { get; set; }

        /// <summary>
        /// 关联边缘处理器
        /// </summary>
        public ProcessorModel Processor { get; set; }

        /// <summary>
        /// 关联继电器Id
        /// </summary>
        public string RelayDeviceId { get; set; }

        /// <summary>
        /// 关联继电器
        /// </summary>
        public RelayDeviceModel RelayDevice { get; set; }

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
            entity.EditedTime = model.EditedTime;

            entity.Name = model.Name;
            entity.Type = model.Type;
            entity.CardType = model.CardType;
            entity.PortName = model.PortName;
            entity.RelayDeviceOut = model.RelayDeviceOut;
            entity.Remark = model.Remark;
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
            if (entity == null)
            {
                return null;
            }
            if (model == null)
            {
                return ToModel(entity);
            }
            model.Id = entity.Id;
            model.EditedTime = entity.EditedTime;

            model.Name = entity.Name;
            model.Type = entity.Type;
            model.CardType = entity.CardType;
            model.PortName = entity.PortName;
            model.RelayDeviceOut = entity.RelayDeviceOut;
            model.Remark = entity.Remark;
            model.HandleElevatorDeviceId = entity.HandleElevatorDeviceId;

            //关联边缘处理器
            model.ProcessorId = entity.Processor?.Id;
            model.Processor = ProcessorModel.SetModel(model.Processor, entity.Processor);

            //关联继电器
            model.RelayDeviceId = entity.RelayDevice?.Id;
            model.RelayDevice = RelayDeviceModel.SetModel(model.RelayDevice, entity.RelayDevice);

            //关联串口配置 
            model.SerialConfigId = entity.SerialConfig?.Id;
            model.SerialConfig = SerialConfigModel.SetModel(model.SerialConfig, entity.SerialConfig);

            return model;
        }
    }
}
