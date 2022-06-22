using KT.Quanta.Service.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Models
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class FaceInfoModel : BaseQuantaModel
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型,Face,certificate,life
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 原图地址
        /// </summary>
        public string SourceUrl { get; set; }

        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string FaceUrl { get; set; }

        /// <summary>
        /// 人脸特征
        /// </summary>
        public byte[] Feature { get; set; }

        /// <summary>
        /// 特征值大小
        /// </summary>
        public int FeatureSize { get; set; }

        public static FaceInfoEntity ToEntity(FaceInfoModel model)
        {
            if (model == null)
            {
                return null;
            }

            var entity = new FaceInfoEntity();

            entity = SetEntity(entity, model);

            return entity;
        }

        public static List<FaceInfoModel> ToModels(List<FaceInfoEntity> entities)
        {
            var models = new List<FaceInfoModel>();
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

        public static FaceInfoEntity SetEntity(FaceInfoEntity entity, FaceInfoModel model)
        {
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Type = model.Type;
            entity.Extension = model.Extension;
            entity.SourceUrl = model.SourceUrl;
            entity.FaceUrl = model.FaceUrl;
            entity.Feature = model.Feature;
            entity.FeatureSize = model.FeatureSize;
            entity.EditedTime = model.EditedTime;

            return entity;
        }

        public static FaceInfoModel ToModel(FaceInfoEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new FaceInfoModel();
            model = SetModel(model, entity);
            return model;
        }

        public static FaceInfoModel SetModel(FaceInfoModel model, FaceInfoEntity entity)
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
            model.Extension = entity.Extension;
            model.SourceUrl = entity.SourceUrl;
            model.FaceUrl = entity.FaceUrl;
            model.Feature = entity.Feature;
            model.FeatureSize = entity.FeatureSize;
            model.EditedTime = entity.EditedTime;

            return model;
        }
    }
}
