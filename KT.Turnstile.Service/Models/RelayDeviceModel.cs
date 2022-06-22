using KT.Turnstile.Entity.Entities;
using System.Collections.Generic;
using KT.Turnstile.Common.Enums;
using KT.Common.Core.Utils;

namespace KT.Turnstile.Model.Models
{
    /// <summary>
    /// 继电器设备信息
    /// </summary>
    public class RelayDeviceModel : BaseTurnstileModel
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备类型,如泥人继电器、巨人继电器等
        /// <see cref="CardDeviceTypeEnum"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 通信类型，如TCP、UDP
        /// <see cref="CardDeviceTypeEnum"/>
        /// </summary>
        public string CommunicateType { get; set; }

        private string _ipAddress;
        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress
        {
            get { return _ipAddress; }
            set
            {
                _ipAddress = value;
            }
        }

        private int _port;
        /// <summary>
        /// 端口
        /// </summary>
        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                FullAddress = _ipAddress + ":" + _port;
            }
        }

        /// <summary>
        /// 完整地址
        /// [IP地址]:[端口]
        /// </summary>
        public string FullAddress { get; set; }

        /// <summary>
        /// 设备描述
        /// </summary>
        public string Remark { get; set; }

        ///// <summary>
        ///// 关联边缘处理器
        ///// </summary>
        //public string ProcessorId { get; set; }

        ///// <summary>
        ///// 关联边缘处理器
        ///// </summary>
        //public ProcessorModel Processor { get; set; }

        public static RelayDeviceEntity ToEntity(RelayDeviceModel model)
        {
            var entity = new RelayDeviceEntity();
            entity = SetEntity(entity, model);
            return entity;
        }

        public static List<RelayDeviceModel> ToModels(List<RelayDeviceEntity> entities)
        {
            var models = new List<RelayDeviceModel>();
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

        public static RelayDeviceEntity SetEntity(RelayDeviceEntity entity, RelayDeviceModel model)
        {
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Type = model.Type;
            entity.CommunicateType = model.CommunicateType;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.Remark = model.Remark;
            entity.EditedTime = model.EditedTime;

            ////关联边缘处理器
            //if (!string.IsNullOrEmpty(model.ProcessorId))
            //{
            //    if (entity.Processor == null || entity.Processor.Id != model.ProcessorId)
            //    {
            //        //修改关联数据时不能使用原来的关联实体类，否则修改出错 
            //        entity.Processor = new ProcessorEntity();
            //        entity.Processor.Id = model.ProcessorId;
            //    }
            //}

            return entity;
        }

        public static RelayDeviceModel ToModel(RelayDeviceEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new RelayDeviceModel();
            model = SetModel(model, entity);
            return model;
        }
        public static RelayDeviceModel SetModel(RelayDeviceModel model, RelayDeviceEntity entity)
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
            model.Name = entity.Name;
            model.Type = entity.Type;
            model.CommunicateType = entity.CommunicateType;
            model.IpAddress = entity.IpAddress;
            model.Port = entity.Port;
            model.Remark = entity.Remark;
            model.EditedTime = entity.EditedTime;

            ////关联边缘处理器
            //model.ProcessorId = entity.Processor?.Id;
            //model.Processor = ProcessorModel.SetModel(model.Processor, entity.Processor);

            return model;
        }
    }
}
