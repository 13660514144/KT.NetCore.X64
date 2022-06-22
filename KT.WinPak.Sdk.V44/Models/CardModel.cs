using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.Models
{
    /// <summary>
    /// 卡信息
    /// </summary>
    public class CardModel
    {
        /// <summary>
        /// 卡号，修改卡时使用
        /// </summary>
        public string CardOldNumber { get; set; }

        /// <summary>
        /// 是否操作卡成功
        /// </summary>
        public bool IsOperateSuccess { get; set; }

        /// <summary>
        /// 是否操作持卡人成功
        /// </summary>
        public string OperateMessage { get; set; }


        #region BaseFied
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 门禁级别
        /// Using this property, you can set an access level name of a card if the Access Level mode is set as PRECISION for the WIN-PAK SE/PE application. 
        /// </summary>
        public string AccessLevelName { get; set; }
        /// <summary>
        /// 激活日期
        /// </summary>
        public string ActivationDate { get; set; }
        /// <summary>
        /// 过期日期
        /// </summary>
        public string ExpirationDate { get; set; }
        /// <summary>
        /// 持卡人ID
        /// </summary>
        public int CardHolderID { get; set; }
        /// <summary>
        /// 卡号PIN
        /// The Maximum length of the PIN is 6 
        /// </summary>
        public string PIN1 { get; set; }
        /// <summary>
        /// 卡状态
        /// 1 – Active 
        /// 2 – Inactive 
        /// 3 – Trace 
        /// 4 – Lost / Stolen
        /// </summary>
        public int CardStatus { get; set; }
        /// <summary>
        /// 发行次数
        /// the number of times the card is issued.
        /// </summary>
        public int Issue { get; set; }
        /// <summary>
        /// 卡ID
        /// This Card property is only for internal use. 
        /// </summary>
        public int CardID { get; set; }

        private List<string> _accessLevels = new List<string>();
        /// <summary>
        /// 门禁级别
        /// Using this property, you can set multiple access level names for a card. If the Access Level Mode is set as MULTIPLE in the WIN-PAK SE/PE application you can set a maximum of 6 access levels for a card. 
        /// </summary>
        public List<string> AccessLevels
        {
            get => _accessLevels;
            set
            {
                _accessLevels = value ?? new List<string>();
            }
        }
        /// <summary>
        /// 是否为临时卡
        /// </summary>
        public int NetAXSTempCard { get; set; }
        /// <summary>
        /// 卡类型
        /// The following are the NetAXS card states.
        /// 0  – Standard 
        /// 1  – Supervisor 
        /// 2  – VIP
        /// </summary>
        public int NetAXSCardType { get; set; }
        /// <summary>
        /// 是否为受限卡
        /// </summary>
        public int NetAXSLimitedCard { get; set; }
        /// <summary>
        /// 受限卡的使用限制
        /// The usage limit must be from 1 to 255.
        /// </summary>
        public int NetAXSUsageLimit { get; set; }
        #endregion

        public static CardModel FromEntity(ICard entity)
        {
            if (entity == null)
            {
                return null;
            }

            CardModel model = new CardModel();
            //传回ID
            if (entity.CardID > 0)
            {
                model.CardID = entity.CardID;
            }
            else
            {
                entity.CardID = model.CardID;
            }

            model.CardNumber = entity.CardNumber.Trim();
            model.AccountName = entity.AccountName;
            model.AccessLevelName = entity.AccessLevelName;
            model.ActivationDate = entity.ActivationDate;
            model.ExpirationDate = entity.ExpirationDate;
            model.CardHolderID = entity.CardHolderID;
            model.PIN1 = entity.PIN1;
            model.CardStatus = entity.CardStatus;
            model.Issue = entity.Issue;
            if (entity.AccessLevels != null)
            {
                model.AccessLevels = ((string[])entity.AccessLevels).ToList();
            }
            model.NetAXSTempCard = entity.NetAXSTempCard;
            model.NetAXSCardType = entity.NetAXSCardType;
            model.NetAXSUsageLimit = entity.NetAXSUsageLimit;
            model.NetAXSLimitedCard = entity.NetAXSLimitedCard;

            return model;
        }

        public static CardClass ToEntity(CardClass entity, CardModel model)
        {
            if (model == null)
            {
                throw CustomException.Run("卡数据不能为空！");
            }
            if (model.AccessLevels == null)
            {
                model.AccessLevels = new List<string>();
            }

            entity.CardNumber = model.CardNumber;
            entity.AccountName = model.AccountName;
            entity.AccessLevelName = model.AccessLevelName;
            entity.ActivationDate = model.ActivationDate;
            entity.ExpirationDate = model.ExpirationDate;
            entity.CardHolderID = model.CardHolderID;
            entity.PIN1 = model.PIN1;
            entity.CardStatus = model.CardStatus;
            entity.Issue = model.Issue;
            entity.CardID = model.CardID;
            entity.AccessLevels = model.AccessLevels.ToArray();
            entity.NetAXSTempCard = model.NetAXSTempCard;
            entity.NetAXSCardType = model.NetAXSCardType;
            entity.NetAXSUsageLimit = model.NetAXSUsageLimit;
            entity.NetAXSLimitedCard = model.NetAXSLimitedCard;

            return entity;
        }


        internal static List<CardModel> FromSqlEntities(List<Entities.Card> entities)
        {
            var models = new List<CardModel>();
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

        private static CardModel FromSqlEntity(Entities.Card entity)
        {
            var model = new CardModel();
            model.CardNumber = entity.CardNumber.Trim();
            if (entity.AccessLevelPlus != null)
            {
                model.AccessLevelName = entity.AccessLevelPlus.Name;
            }
            model.ActivationDate = entity.ActivationDate.ToDayString();
            model.ExpirationDate = entity.ExpirationDate.ToDayString();
            model.CardHolderID = entity.CardHolderId == null ? 0 : entity.CardHolderId.Value;
            model.PIN1 = entity.Pin1;
            model.CardStatus = ConvertUtil.ToInt32(entity.CardStatus, 0);
            model.Issue = ConvertUtil.ToInt32(entity.Issue, 0);
            if (entity.AccessLevelPluses != null)
            {
                model.AccessLevels = entity.AccessLevelPluses.Select(x => x.Name).ToList();
            }

            //model.NetAXSTempCard = entity.NetAXSTempCard;
            //model.NetAXSCardType = entity.NetAXSCardType;
            //model.NetAXSUsageLimit = entity.NetAXSUsageLimit;
            //model.NetAXSLimitedCard = entity.NetAXSLimitedCard;

            model.AccountName = "";

            return model;
        }
    }
}
