using KT.Common.Core.Exceptions;
using KT.WinPak.SDK.V48.IServices;
using KT.WinPak.SDK.V48.Message;
using KT.WinPak.SDK.V48.Models;
using KT.WinPak.SDK.V48.Queries;
using Microsoft.Extensions.Logging;
using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.V48.Services
{
    public class CardSdkService : ICardSdkService
    {
        private IAllSdkService _allSdkService;
        private ICardHolderSdkService _cardHolderSdkService;
        private ILogger<CardSdkService> _logger;

        public CardSdkService(IAllSdkService allSdkService, ICardHolderSdkService cardHolderSdkService, ILogger<CardSdkService> logger)
        {
            _allSdkService = allSdkService;
            _cardHolderSdkService = cardHolderSdkService;
            _logger = logger;
        }

        /// <summary>
        /// 获取所有卡
        /// </summary>
        /// <returns></returns>
        public List<CardModel> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 新增卡
        /// </summary>
        /// <returns></returns>
        public CardModel Add(CardModel model)
        {
            //转换参数
            var entity = _allSdkService.GetCardClass();
            entity = CardModel.ToEntity(entity, model);

            //查询条件
            var query = new AddCardQuery();
            query.pcard = entity;

            //执行操作
            query = _allSdkService.AddCard(query);

            //返回结果
            if (query.pStatus != 0)
            {
                throw CustomException.Run("新增卡失败：" + MessageEnum.GetMessageByCode(query.pStatus));
            }
            model.CardID = query.pcard.CardID;
            return model;
        }

        /// <summary>
        /// 修改卡
        /// </summary>
        /// <returns></returns>
        public bool Edit(string bstrCardno, string bstrAcctName, string bstrSubAcctName, CardModel model)
        {
            //转换参数
            var entity = _allSdkService.GetCardClass();
            entity = CardModel.ToEntity(entity, model);

            //查询条件
            var query = new EditCardQuery();
            query.bstrCardno = bstrCardno;
            query.bstrAcctName = bstrAcctName;
            query.bstrSubAcctName = bstrSubAcctName;
            query.pcard = entity;

            //执行操作
            query = _allSdkService.EditCard(query);

            //返回结果
            if (query.pStatus != 0)
            {
                throw CustomException.Run("修改卡失败：" + MessageEnum.GetMessageByCode(query.pStatus));
            }
            return true;
        }

        /// <summary>
        /// 删除卡
        /// </summary>
        /// <returns></returns>
        public bool Delete(string bstrCardno, string bstrAcctName, string bstrSubAcctName)
        {
            //查询条件
            var query = new DeleteCardQuery();
            query.bstrCardno = bstrCardno;
            query.bstrAcctName = bstrAcctName;
            query.bstrSubAcctName = bstrSubAcctName;

            //执行操作
            query = _allSdkService.DeleteCard(query);

            //返回结果
            if (query.pStatus != 0)
            {
                throw CustomException.Run(string.Format("修改卡失败：CardNo:{0} {1}", bstrCardno, MessageEnum.GetMessageByCode(query.pStatus)));
            }
            return true;
        }

        public CardModel GetCardByCardNumber(string cardNumber, string accountName, string subAccountName)
        {
            //查询条件
            var query = new GetCardByCardNumberQuery();
            query.bstrCardno = cardNumber;
            query.bstrAcctName = accountName;
            query.bstrSubAcctName = subAccountName;

            //执行操作
            query = _allSdkService.GetCardByCardNumber(query);

            //返回结果
            var iCard = query.vcard;
            var result = CardModel.FromEntity(iCard);
            return result;
        }

        public CardAndCardHolderModel AddWithCardHolder(CardAndCardHolderModel model)
        {
            try
            {
                model.CardHolder = _cardHolderSdkService.Add(model.CardHolder);
                model.CardHolder.IsOperateSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("加持卡人失败：{0} ", ex);

                model.CardHolder.IsOperateSuccess = false;
                model.CardHolder.OperateMessage = ex.Message;
            }
            try
            {
                model.Card = Add(model.Card);
                model.Card.IsOperateSuccess = true;
            }
            catch (Exception ex)
            {
                _cardHolderSdkService.Delete(model.CardHolder.CardHolderID, 0, 0);
                _logger.LogInformation("加卡失败：{0} ", ex);
                model.Card.IsOperateSuccess = false;
                model.Card.OperateMessage = ex.Message;
            }

            return model;
        }

        public CardAndCardHolderModel EditWithCardHolder(CardAndCardHolderModel model)
        {
            if (model.CardHolder.CardHolderID <= 0 && model.Card.CardHolderID > 0)
            {
                model.CardHolder.CardHolderID = model.Card.CardHolderID;
            }
            else if (model.CardHolder.CardHolderID <= 0 && model.Card.CardHolderID <= 0)
            {
                var oldCard = GetCardByCardNumber(model.Card.CardNumber, model.Card.AccountName, model.Card.SubAccountName);
                if (oldCard == null)
                {
                    _logger.LogInformation($"加卡失败：找不到卡:{model.Card.CardNumber} ");
                    model.Card.IsOperateSuccess = false;
                    model.Card.OperateMessage = "";
                    return model;
                }
                else
                {
                    model.CardHolder.CardHolderID = oldCard.CardHolderID;
                    model.Card.CardHolderID = oldCard.CardHolderID;
                }
            }
            else if (model.CardHolder.CardHolderID > 0 && model.Card.CardHolderID <= 0)
            {
                model.Card.CardHolderID = model.CardHolder.CardHolderID;
            }

            try
            {
                model.CardHolder.IsOperateSuccess = _cardHolderSdkService.Edit(model.CardHolder.CardHolderID, model.CardHolder);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("加持卡人失败：{0} ", ex);

                model.CardHolder.IsOperateSuccess = false;
                model.CardHolder.OperateMessage = ex.Message;
            }
            try
            {
                model.Card.IsOperateSuccess = Edit(model.Card.CardNumber, model.Card.AccountName, model.Card.SubAccountName, model.Card);
            }
            catch (Exception ex)
            {
                _cardHolderSdkService.Delete(model.CardHolder.CardHolderID, 0, 0);
                _logger.LogInformation("加卡失败：{0} ", ex);
                model.Card.IsOperateSuccess = false;
                model.Card.OperateMessage = ex.Message;
            }

            return model;
        }
        public CardAndCardHolderModel DeleteWithCardHolder(CardAndCardHolderModel model)
        {
            try
            {
                Delete(model.Card.CardNumber, model.Card.AccountName, model.Card.SubAccountName);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("删除卡失败：{0} ", ex);
                model.Card.IsOperateSuccess = false;
                model.Card.OperateMessage = ex.Message;
            }
            model.Card.IsOperateSuccess = true;

            try
            {
                _cardHolderSdkService.Delete(model.CardHolder.CardHolderID, model.CardHolder.DeleteCardType, model.CardHolder.DeleteImageType);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("删除持卡人失败：{0} ", ex);
                model.CardHolder.IsOperateSuccess = false;
                model.CardHolder.OperateMessage = ex.Message;
            }
            model.CardHolder.IsOperateSuccess = true;

            return model;
        }

        public List<CardAndCardHolderModel> AddWithCardHolders(string accountName, string subAccountName, List<CardAndCardHolderModel> models)
        {
            List<CardAndCardHolderModel> cardAndCardHolders = new List<CardAndCardHolderModel>();
            foreach (var item in models)
            {
                item.Card.AccountName = accountName;
                item.Card.SubAccountName = subAccountName;
                item.CardHolder.AccountName = accountName;
                item.CardHolder.SubAccountName = subAccountName;
                var result = AddWithCardHolder(item);
                cardAndCardHolders.Add(result);
            }
            return cardAndCardHolders;
        }

        public List<CardAndCardHolderModel> DeleteWithCardHolders(string accountName, string subAccountName, List<CardAndCardHolderModel> models)
        {
            List<CardAndCardHolderModel> cardAndCardHolders = new List<CardAndCardHolderModel>();
            foreach (var item in models)
            {
                item.Card.AccountName = accountName;
                item.Card.SubAccountName = subAccountName;
                item.CardHolder.AccountName = accountName;
                item.CardHolder.SubAccountName = subAccountName;
                var result = DeleteWithCardHolder(item);
                cardAndCardHolders.Add(result);
            }
            return cardAndCardHolders;
        }
    }
}
