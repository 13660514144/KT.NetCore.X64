using KT.Turnstile.Entity.Entities;
using System.Collections.Generic;
using KT.Turnstile.Common.Enums;

namespace KT.Turnstile.Model.Models
{
    /// <summary>
    /// 继电器设备信息
    /// </summary>
    public class DistributeErrorModel : BaseTurnstileModel
    {
        /// <summary>
        /// 数据分发地址
        /// </summary>
        public string PartUrl { get; set; }

        /// <summary>
        /// 分发数据类名
        /// </summary>
        public string DataModelName { get; set; }

        /// <summary>
        /// 分发数据Id
        /// </summary>
        public string DataId { get; set; }

        /// <summary>
        /// 分发错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Json格式分发数据
        /// </summary>
        public string DataContent { get; set; }

        /// <summary>
        /// 推送类型
        /// <see cref="DistributeTypeEnum"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 分发错误次数
        /// </summary>
        public int ErrorTimes { get; set; }

        /// <summary>
        /// 关联边缘处理器
        /// </summary>
        public string ProcessorId { get; set; }

        /// <summary>
        /// 关联边缘处理器
        /// </summary>
        public ProcessorModel Processor { get; set; }

        public static DistributeErrorEntity ToEntity(DistributeErrorModel model)
        {
            var entity = new DistributeErrorEntity();
            entity = SetEntity(entity, model);
            return entity;
        }

        public static List<DistributeErrorModel> ToModels(List<DistributeErrorEntity> entities)
        {
            var models = new List<DistributeErrorModel>();
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

        public static DistributeErrorEntity SetEntity(DistributeErrorEntity entity, DistributeErrorModel model)
        {
            entity.Id = model.Id;
            entity.PartUrl = model.PartUrl;
            entity.DataModelName = model.DataModelName;
            entity.DataId = model.DataId;
            entity.ErrorMessage = model.ErrorMessage;
            entity.DataContent = model.DataContent;
            entity.Type = model.Type;
            entity.ErrorTimes = model.ErrorTimes;
            //为真正关联数据更新时间
            entity.EditedTime = model.EditedTime;

            //关联边缘处理器
            if (!string.IsNullOrEmpty(model.ProcessorId))
            {
                if (entity.Processor == null || entity.Processor.Id != model.ProcessorId)
                {
                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    entity.Processor = new ProcessorEntity();
                    entity.Processor.Id = model.ProcessorId;
                }
            }

            return entity;
        }
        public static DistributeErrorModel ToModel(DistributeErrorEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new DistributeErrorModel();
            model = SetModel(model, entity);
            return model;
        }

        public static DistributeErrorModel SetModel(DistributeErrorModel model, DistributeErrorEntity entity)
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
            model.PartUrl = entity.PartUrl;
            model.DataModelName = entity.DataModelName;
            model.DataId = entity.DataId;
            model.ErrorMessage = entity.ErrorMessage;
            model.DataContent = entity.DataContent;
            model.Type = entity.Type;
            model.ErrorTimes = entity.ErrorTimes;
            model.EditedTime = entity.EditedTime;

            //关联边缘处理器
            model.ProcessorId = entity.Processor?.Id;
            model.Processor = ProcessorModel.SetModel(model.Processor, entity.Processor);

            return model;
        }
    }
}
