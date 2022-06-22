using KT.Quanta.Service.Entities;
using System.Collections.Generic;

namespace KT.Quanta.Service.Models
{
    /// <summary>
    /// 继电器设备信息
    /// </summary>
    public class DistributeErrorModel : BaseQuantaModel
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
        /// <see cref="KT.Quanta.Common.Enums.DistributeTypeEnum"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 分发错误次数
        /// </summary>
        public int ErrorTimes { get; set; }

        /// <summary>
        /// 推送设备Key
        /// </summary>
        public string DeviceId { get; set; }

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

            entity.DeviceId = model.DeviceId;

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
            model.DeviceId = entity.DeviceId; 

            return model;
        }
    }
}
