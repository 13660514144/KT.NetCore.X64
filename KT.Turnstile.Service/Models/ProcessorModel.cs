using KT.Common.Core.Utils;
using KT.Turnstile.Entity.Entities;
using System.Collections.Generic;
using System.Linq;

namespace KT.Turnstile.Model.Models
{
    /// <summary>
    /// 边缘处理器
    /// </summary>
    public class ProcessorModel : BaseTurnstileModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// 最后同步数据时间
        /// </summary>
        public long SyncDataTime { get; set; }

        /// <summary>
        /// 是否正在同步数据，同步数据不能同时进行，不持久化
        /// </summary>
        public bool IsSyncingData { get; set; }

        ///// <summary>
        ///// 是否有未完成同步的数据完成，不持久化
        ///// 设备新接入时，会直接推送数据库中的所有数据，当前标识记录同步状态
        ///// 有新数据传入时，会自动推送当条数据，如果已经同步完成，会时时更新最后同步时间，否则不更新最后同步时间
        ///// </summary>
        /// <summary>
        /// 是否有推送错误数据
        /// </summary>
        public bool HasDistributeError { get; set; }

        /// <summary>
        /// 边缘处理器自动生成Key
        /// </summary>
        public string ProcessorKey { get; set; }

        /// <summary>
        /// 连接Id,每次连接都不一样，所以不用持久化
        /// </summary>
        public string ConnectionId { get; set; }

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
            model.Remark = entity.Remark;
            model.IpAddress = entity.IpAddress;
            model.Port = entity.Port;
            model.SyncDataTime = entity.SyncDataTime;
            model.HasDistributeError = entity.HasDistributeError;
            model.EditedTime = entity.EditedTime;
            model.ProcessorKey = entity.ProcessorKey;

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
            entity.Remark = model.Remark;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.SyncDataTime = model.SyncDataTime;
            entity.EditedTime = model.EditedTime;
            entity.HasDistributeError = model.HasDistributeError;
            entity.ProcessorKey = model.ProcessorKey;

            return entity;
        }

        public static ProcessorEntity ToEntity(ProcessorModel model)
        {
            var entity = new ProcessorEntity();

            entity = SetEntity(entity, model);

            return entity;
        }

        public string GetHttpAddress()
        {
            var url = "http://" + IpAddress + ":" + Port + "/";
            return url;
        }
    }
}
