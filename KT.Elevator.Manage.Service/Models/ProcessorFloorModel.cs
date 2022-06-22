using KT.Elevator.Manage.Service.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace KT.Elevator.Manage.Service.Models
{
    /// <summary>
    /// 通行权限
    /// </summary>
    public class ProcessorFloorModel : BaseElevatorModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 关联所在楼层Id
        /// </summary> 
        public string FloorId { get; set; }

        /// <summary>
        /// 关联所在楼层
        /// </summary> 
        public FloorModel Floor { get; set; }

        /// <summary>
        /// 人员Id
        /// </summary>
        public string ProcessorId { get; set; }

        /// <summary>
        /// 关联人员
        /// </summary>
        public ProcessorModel Processor { get; set; }

        ///// <summary>
        ///// 父权限Id
        ///// </summary>
        //public string ParentId { get; set; }

        ///// <summary>
        ///// 父权限，人脸关联卡号
        ///// </summary>
        //public ProcessorFloorModel Parent { get; set; }

        /// <summary>
        /// 关联可去楼层Ids
        /// </summary> 
        public ICollection<string> FloorIds { get; set; }

        /// <summary>
        /// 关联可去楼层
        /// </summary> 
        public ICollection<FloorModel> Floors { get; set; }

        public static ProcessorFloorEntity SetEntity(ProcessorFloorEntity entity, ProcessorFloorModel model)
        {
            if (model == null)
            {
                entity = null;
                return entity;
            }
            if (entity == null)
            {
                entity = new ProcessorFloorEntity();
            }

            entity.Id = model.Id;
            entity.SortId = model.SortId;
            entity.Name = model.Name;
            entity.EditedTime = model.EditedTime;

            //关联所在楼层
            if (!string.IsNullOrEmpty(model.FloorId))
            {
                if (entity.Floor == null || entity.Floor.Id != model.FloorId)
                {
                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    entity.Floor = new FloorEntity();
                    entity.Floor.Id = model.FloorId;
                }
            }

            //关联人员楼层
            if (!string.IsNullOrEmpty(model.ProcessorId))
            {
                if (entity.Processor == null || entity.Processor.Id != model.ProcessorId)
                {
                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    entity.Processor = new ProcessorEntity();
                    entity.Processor.Id = model.ProcessorId;
                }
            }

            ////关联权限
            //if (!string.IsNullOrEmpty(model.ParentId))
            //{
            //    if (entity.Parent == null || entity.Parent.Id != model.ParentId)
            //    {
            //        //修改关联数据时不能使用原来的关联实体类，否则修改出错 
            //        entity.Parent = new ProcessorFloorEntity();
            //        entity.Parent.Id = model.ParentId;
            //    }
            //}

            ////关联可去楼层
            //entity.Floors = new List<FloorEntity>();
            //if (model.FloorIds != null && model.FloorIds.FirstOrDefault() != null)
            //{
            //    foreach (var item in model.FloorIds)
            //    {
            //        var passAisleEntity = new ProcessorFloorRelationFloorEntity();
            //        passAisleEntity.ProcessorFloor = entity;

            //        //修改关联数据时不能使用原来的关联实体类，否则修改出错 
            //        passAisleEntity.Floor = new FloorEntity();
            //        passAisleEntity.Floor.Id = item;

            //        entity.RelationFloors.Add(passAisleEntity);
            //    }
            //}

            return entity;
        }

        public static List<ProcessorFloorModel> ToModels(List<ProcessorFloorEntity> entities)
        {
            var results = new List<ProcessorFloorModel>();
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

        public static ProcessorFloorEntity ToEntity(ProcessorFloorModel model)
        {
            var entity = new ProcessorFloorEntity();
            var result = SetEntity(entity, model);
            return result;
        }

        public static ProcessorFloorModel ToModel(ProcessorFloorEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var model = new ProcessorFloorModel();
            model = SetModel(model, entity);
            return model;
        }

        public static ProcessorFloorModel SetModel(ProcessorFloorModel model, ProcessorFloorEntity entity)
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
            model.SortId = entity.SortId;
            model.Name = entity.Name;
            model.EditedTime = entity.EditedTime;

            //关联所在楼层
            model.FloorId = entity.Floor?.Id;
            model.Floor = FloorModel.SetModel(model.Floor, entity.Floor);

            //关联人员楼层
            model.ProcessorId = entity.Processor?.Id;
            model.Processor = ProcessorModel.SetModel(model.Processor, entity.Processor);

            ////关联权限
            //model.ParentId = entity.Parent?.Id;
            //model.Parent = ProcessorFloorModel.SetModel(model.Parent, entity.Parent);

            ////关联可去楼层
            //model.FloorIds = new List<string>();
            //model.Floors = new List<FloorModel>();
            //if (entity.RelationFloors != null && entity.RelationFloors.FirstOrDefault() != null)
            //{
            //    foreach (var item in entity.RelationFloors)
            //    {
            //        var passAisleModel = FloorModel.ToModel(item.Floor);
            //        if (passAisleModel == null)
            //        {
            //            continue;
            //        }
            //        model.FloorIds.Add(passAisleModel.Id);
            //        model.Floors.Add(passAisleModel);
            //    }
            //}

            return model;
        }
    }
}
