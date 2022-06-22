using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Service.Turnstile.Dtos
{
    /// <summary>
    /// 通行权限组
    /// </summary>
    public class TurnstileCardDeviceRightGroupModel : BaseQuantaModel
    {
        public TurnstileCardDeviceRightGroupModel()
        {
            CardDeviceIds = new List<string>();
            CardDevices = new List<TurnstileCardDeviceModel>();
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 关联读卡器id
        /// </summary>
        public List<string> CardDeviceIds { get; set; }

        /// <summary>
        /// 关联读卡器
        /// </summary>
        public List<TurnstileCardDeviceModel> CardDevices { get; set; }

        public static CardDeviceRightGroupEntity SetEntity(CardDeviceRightGroupEntity entity, TurnstileCardDeviceRightGroupModel model)
        {
            if (model == null)
            {
                entity = null;
                return entity;
            }
            if (entity == null)
            {
                entity = new CardDeviceRightGroupEntity();
            }

            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.EditedTime = model.EditedTime;

            //关联卡类型
            entity.RelationCardDevices = new List<CardDeviceRightGroupRelationCardDeviceEntity>();
            if (model.CardDeviceIds != null && model.CardDeviceIds.FirstOrDefault() != null)
            {
                foreach (var item in model.CardDeviceIds)
                {
                    var passAisleEntity = new CardDeviceRightGroupRelationCardDeviceEntity();
                    passAisleEntity.CardDeviceRightGroup = entity;

                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    passAisleEntity.CardDevice = new CardDeviceEntity();
                    passAisleEntity.CardDevice.Id = item;

                    entity.RelationCardDevices.Add(passAisleEntity);
                }
            }

            return entity;
        }

        public static CardDeviceRightGroupEntity ToEntity(TurnstileCardDeviceRightGroupModel model)
        {
            var entity = new CardDeviceRightGroupEntity();
            var result = SetEntity(entity, model);
            return result;
        }
    }
}
