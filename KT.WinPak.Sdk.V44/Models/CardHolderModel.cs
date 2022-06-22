using KT.Common.Core.Exceptions;
using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Models
{
    public class CardHolderModel
    {
        /// <summary>
        /// 删除卡类型，删除持卡人时使用
        /// 0 – Detach the cards associated with the specified card holder.
        /// 1 – Deletes the cards associated with specified card holder.
        /// </summary>
        public int DeleteCardType { get; set; }

        /// <summary>
        /// 删除图片类型，删除持卡人时使用
        /// 0 – Detach the photos and signatures attached to the specified card holder.
        /// 1 – Deletes the photos and signatures attached with the specified card holder.
        /// </summary>
        public int DeleteImageType { get; set; }


        #region BaseFied
        /// <summary>
        /// 操作卡错误消息
        /// </summary>
        public bool IsOperateSuccess { get; set; }
        /// <summary>
        /// 操作持卡人错误消息
        /// </summary>
        public string OperateMessage { get; set; }


        /// <summary>
        /// 姓
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 持卡人ID
        /// </summary>
        public int CardHolderID { get; set; }
        ///// <summary>
        ///// Array of String of Retrieved Notefield values associated with the cards. 
        ///// </summary>
        //public object NoteFields { get; set; }
        ///// <summary>
        ///// The NoteField object is used to access the WIN-PAK SE/PE Note field information. The note field contains additional information about the card holder.
        ///// </summary>
        //public string NoteField { get; set; }
        ///// <summary>
        ///// 头像
        ///// </summary>
        //public object Photo { get; set; }
        ///// <summary>
        ///// 签名
        ///// </summary>
        //public object Signature { get; set; }

        #endregion

        public static CardHolderClass ToEntity(CardHolderClass entity, CardHolderModel model)
        {
            if (model == null)
            {
                throw CustomException.Run("持卡人数据不能为空！");
            }
            //传回ID
            if (entity.CardHolderID > 0)
            {
                model.CardHolderID = entity.CardHolderID;
            }
            else
            {
                entity.CardHolderID = model.CardHolderID;
            }
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.AccountName = model.AccountName;


            //entity.NoteFields = model.NoteFields;
            //entity.NoteField = model.NoteField[""];
            //entity.Photo = model.Photo[0];
            //entity.Signature = model.Signature[0];

            return entity;
        }

        public static CardHolderModel FromEntity(ICardHolder entity)
        {
            if (entity == null)
            {
                return null;
            }

            CardHolderModel model = new CardHolderModel();
            model.FirstName = entity.FirstName;
            model.LastName = entity.LastName;
            model.AccountName = entity.AccountName;
            model.CardHolderID = entity.CardHolderID;

            //model.NoteFields = entity.NoteFields;
            //model.NoteField = entity.NoteField[""];
            //model.Photo = entity.Photo[0];
            //model.Signature = entity.Signature[0];

            return model;
        }

        internal static List<CardHolderModel> FromSqlEntities(List<Entities.CardHolder> entities)
        {
            var models = new List<CardHolderModel>();
            if (entities == null)
            {
                return models;
            }
            foreach (var item in entities)
            {
                var model = FromSqlEntity(item);
                models.Add(model);
            }
            return models;
        }

        private static CardHolderModel FromSqlEntity(Entities.CardHolder entity)
        {
            var model = new CardHolderModel();
            model.CardHolderID = entity.RecordId;
            model.FirstName = entity.FirstName;
            model.LastName = entity.LastName;
            model.AccountName = "";

            return model;
        }

    }
}
