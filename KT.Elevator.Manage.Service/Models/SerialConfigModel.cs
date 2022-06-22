using KT.Common.Core.Utils;
using KT.Elevator.Manage.Service.Entities;
using System.Collections.Generic;

namespace KT.Elevator.Manage.Service.Models
{
    /// <summary>
    /// 串口读取信息
    /// 串口配置为公用配置，同一型号设备共用一条配置信息，非公用字段放设备信息中
    /// </summary>
    public class SerialConfigModel : BaseElevatorModel
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int Baudrate { get; set; }

        /// <summary>
        /// 数据位
        /// </summary>
        public int Databits { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        public int Stopbits { get; set; }

        /// <summary>
        /// 校验位
        /// </summary>
        public int Parity { get; set; }

        /// <summary>
        /// 数据读取超时
        /// </summary>
        public int ReadTimeout { get; set; }

        /// <summary>
        /// 编码方式
        /// </summary>
        public string Encoding { get; set; }

        public static SerialConfigEntity ToEntity(SerialConfigModel model)
        {
            if (model == null)
            {
                return null;
            }

            var entity = new SerialConfigEntity();

            entity = SetEntity(entity, model);

            return entity;
        }

        public static List<SerialConfigModel> ToModels(List<SerialConfigEntity> entities)
        {
            var models = new List<SerialConfigModel>();
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

        public static SerialConfigEntity SetEntity(SerialConfigEntity entity, SerialConfigModel model)
        {
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Baudrate = model.Baudrate;
            entity.Databits = model.Databits;
            entity.Stopbits = model.Stopbits;
            entity.Parity = model.Parity;
            entity.ReadTimeout = model.ReadTimeout;
            entity.Encoding = model.Encoding;
            entity.EditedTime = model.EditedTime;

            return entity;
        }

        public static SerialConfigModel ToModel(SerialConfigEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new SerialConfigModel();
            model = SetModel(model, entity);
            return model;
        }

        public static SerialConfigModel SetModel(SerialConfigModel model, SerialConfigEntity entity)
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
            model.Baudrate = entity.Baudrate;
            model.Databits = entity.Databits;
            model.Stopbits = entity.Stopbits;
            model.Parity = entity.Parity;
            model.ReadTimeout = entity.ReadTimeout;
            model.Encoding = entity.Encoding;
            model.EditedTime = entity.EditedTime;

            return model;
        }
    }
}
