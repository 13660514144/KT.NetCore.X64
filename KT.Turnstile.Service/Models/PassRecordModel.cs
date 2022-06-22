using System;
using System.Collections.Generic;
using KT.Common.Core.Utils;
using KT.Turnstile.Entity.Entities;

namespace KT.Turnstile.Model.Models
{
    /// <summary>
    /// 通行记录
    /// </summary>
    public class PassRecordModel : BaseTurnstileModel
    {
        /// <summary>
        /// 设备ID，除了人脸摄像头、门禁读卡器是第三方系统的设备ID，其余都系本系统的设备ID
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 通行类型
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 通行码，IC卡、二维码、人脸ID
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 通行时间，2019-11-06 15:20:45
        /// </summary>
        public string PassLocalTime { get; set; }

        /// <summary>
        /// 通行时间
        /// </summary>
        public long PassTime { get; set; }

        /// <summary>
        /// 通行权限Id
        /// </summary>
        public string PassRightId { get; set; }

        /// <summary>
        /// 上传错误次数
        /// </summary>
        public int ErrorTimes { get; set; }

        public static PassRecordEntity ToEntity(PassRecordModel model)
        {
            var entity = new PassRecordEntity();
            entity = SetEntity(entity, model);
            return entity;
        }

        public static PassRecordModel ToModel(PassRecordEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new PassRecordModel();
            model = SetModel(model, entity);
            return model;
        }
        public static PassRecordModel SetModel(PassRecordModel model, PassRecordEntity entity)
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

            model.DeviceId = entity.DeviceId;
            model.DeviceType = entity.DeviceType;
            model.CardType = entity.CardType;
            model.CardNumber = entity.CardNumber;
            model.PassLocalTime = entity.PassLocalTime;
            model.PassTime = entity.PassTime;
            model.PassRightId = entity.PassRightId;

            return model;
        }

        public static PassRecordEntity SetEntity(PassRecordEntity entity, PassRecordModel model)
        {
            entity.Id = model.Id;
            entity.EditedTime = model.EditedTime;

            entity.DeviceId = model.DeviceId;
            entity.DeviceType = model.DeviceType;
            entity.CardType = model.CardType;
            entity.CardNumber = model.CardNumber;
            entity.PassLocalTime = model.PassLocalTime;
            entity.PassTime = model.PassTime;
            entity.PassRightId = model.PassRightId;

            return entity;
        }

        public static List<PassRecordModel> ToModels(List<PassRecordEntity> entities)
        {
            var results = new List<PassRecordModel>();
            if (entities == null)
            {
                return results;
            }
            foreach (var item in entities)
            {
                var result = ToModel(item);
                results.Add(result);
            }
            return results;
        }
    }
}
