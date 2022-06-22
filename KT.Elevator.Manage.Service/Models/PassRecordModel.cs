using KT.Elevator.Manage.Service.Entities;
using System;
using System.Collections.Generic;

namespace KT.Elevator.Manage.Service.Models
{
    /// <summary>
    /// 通行记录
    /// </summary>
    public class PassRecordModel : BaseElevatorModel, ICloneable
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
        public string AccessType { get; set; }

        /// <summary>
        /// 通行码，IC卡、二维码、人脸ID
        /// </summary>
        public string PassRightSign { get; set; }

        /// <summary>
        /// 通行时间，2019-11-06 15:20:45
        /// </summary>
        public string PassLocalTime { get; set; }

        /// <summary>
        /// 通行时间
        /// </summary>
        public long PassTime { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Extra { get; set; }

        /// <summary>
        /// 出入方向
        /// </summary>
        public string WayType { get; set; }

        /// <summary>
        /// 备注，用于填写第三方特别信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 通行权限Id
        /// </summary>
        public string PassRightId { get; set; }

        /// <summary>
        /// 通行人脸图片
        /// </summary>
        public byte[] FaceImage { get; set; }

        /// <summary>
        /// 人脸图片大小
        /// </summary>
        public long FaceImageSize { get; set; }

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
            model.AccessType = entity.AccessType;
            model.PassRightSign = entity.PassRightSign;
            model.PassLocalTime = entity.PassLocalTime;
            model.Extra = entity.Extra;
            model.WayType = entity.WayType;
            model.Remark = entity.Remark;
            model.PassTime = entity.PassTime;
            model.PassRightId = entity.PassRightId;

            model.FaceImage = entity.FaceImage;
            model.FaceImageSize = entity.FaceImageSize;

            return model;
        }

        public static PassRecordEntity SetEntity(PassRecordEntity entity, PassRecordModel model)
        {
            entity.Id = model.Id;
            entity.EditedTime = model.EditedTime;

            entity.DeviceId = model.DeviceId;
            entity.DeviceType = model.DeviceType;
            entity.AccessType = model.AccessType;
            entity.PassLocalTime = model.PassLocalTime;
            entity.PassTime = model.PassTime;
            entity.Extra = model.Extra;
            entity.WayType = model.WayType;
            entity.Remark = model.Remark;
            entity.PassRightSign = model.PassRightSign;
            entity.PassRightId = model.PassRightId;

            entity.FaceImage = model.FaceImage;
            entity.FaceImageSize = model.FaceImageSize;

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

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
