using KT.Quanta.Service.Entities;
using System.Collections.Generic;

namespace KT.Quanta.Service.Models
{
    /// <summary>
    /// 通行权限
    /// </summary>
    public class ProcessorFloorModel : BaseQuantaModel
    {
        public ProcessorFloorModel()
        {
            FloorIds = new List<string>();
            Floors = new List<FloorModel>();
        }

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

            return entity;
        }

        public static ProcessorFloorEntity ToEntity(ProcessorFloorModel model)
        {
            var entity = new ProcessorFloorEntity();
            var result = SetEntity(entity, model);
            return result;
        }
    }
}
