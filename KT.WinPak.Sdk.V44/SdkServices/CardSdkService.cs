using KT.Common.Core.Exceptions;
using KT.WinPak.SDK.IServices;
using KT.WinPak.SDK.Message;
using KT.WinPak.SDK.Models;
using KT.WinPak.SDK.Queries;
using Microsoft.Extensions.Logging;
using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.Services
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
            List<CardModel> results = new List<CardModel>();

            //查询所有账户 
            var getAccountsQuery = new GetAccountsQuery();
            getAccountsQuery = _allSdkService.GetAccounts(getAccountsQuery);

            //账户为空
            if (getAccountsQuery.vAccounts == null)
            {
                return results;
            }

            //转换账户信息
            object[] accounts = (object[])getAccountsQuery.vAccounts;
            if (accounts == null || accounts.Length <= 0)
            {
                return results;
            }

            //遍历账户查询数据
            foreach (var item in accounts)
            {
                IAccount account = (IAccount)item;

                //查询条件
                var query = new GetCardsByAccountNameQuery();
                query.bstrAcctName = account.AccountName;

                //执行查询
                query = _allSdkService.GetCardsByAccountName(query);

                //查询结果为空
                if (query.vCards == null)
                {
                    continue;
                }

                //转换结果数据 
                if (query.vCards == null || query.vCards.FirstOrDefault() == null)
                {
                    continue;
                }

                //处理返回数据
                foreach (var obj in query.vCards)
                {
                    var result = CardModel.FromEntity(obj);
                    result.AccountName = account.AccountName;
                    results.Add(result);
                }
            }

            return results;
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
        public bool Edit(string bstrCardno, string bstrAcctName, CardModel model)
        {
            //转换参数
            var entity = _allSdkService.GetCardClass();
            entity = CardModel.ToEntity(entity, model);

            //查询条件
            var query = new EditCardQuery();
            query.bstrCardno = bstrCardno;
            query.bstrAcctName = bstrAcctName;
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
        public bool Delete(string bstrCardno, string bstrAcctName)
        {
            //查询条件
            var query = new DeleteCardQuery();
            query.bstrCardno = bstrCardno;
            query.bstrAcctName = bstrAcctName;

            //执行操作
            query = _allSdkService.DeleteCard(query);

            //返回结果
            if (query.pStatus != 0)
            {
                throw CustomException.Run(string.Format("修改卡失败：CardNo:{0} {1}", bstrCardno, MessageEnum.GetMessageByCode(query.pStatus)));
            }
            return true;
        }

        public CardModel GetCardByCardNumber(string cardNumber, string accountName)
        {
            //查询条件
            var query = new GetCardByCardNumberQuery();
            query.bstrCardno = cardNumber;
            query.bstrAcctName = accountName;

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

        public CardAndCardHolderModel DeleteWithCardHolder(CardAndCardHolderModel model)
        {
            try
            {
                Delete(model.Card.CardNumber, model.Card.AccountName);
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

        public List<CardAndCardHolderModel> AddWithCardHolders(string accountName, List<CardAndCardHolderModel> models)
        {
            List<CardAndCardHolderModel> cardAndCardHolders = new List<CardAndCardHolderModel>();
            foreach (var item in models)
            {
                item.Card.AccountName = accountName;
                item.CardHolder.AccountName = accountName;
                var result = AddWithCardHolder(item);
                cardAndCardHolders.Add(result);
            }
            return cardAndCardHolders;
        }

        public List<CardAndCardHolderModel> DeleteWithCardHolders(string accountName, List<CardAndCardHolderModel> models)
        {
            List<CardAndCardHolderModel> cardAndCardHolders = new List<CardAndCardHolderModel>();
            foreach (var item in models)
            {
                item.Card.AccountName = accountName;
                item.CardHolder.AccountName = accountName;
                var result = DeleteWithCardHolder(item);
                cardAndCardHolders.Add(result);
            }
            return cardAndCardHolders;
        }

        public void DeleteCardAndHolders(int start, int end)
        {
            //查询所有账户 
            var getAccountsQuery = new GetAccountsQuery();
            getAccountsQuery = _allSdkService.GetAccounts(getAccountsQuery);

            //账户为空
            if (getAccountsQuery.vAccounts == null)
            {
                throw CustomException.Run("删除卡与持卡人失败：没有找到账户信息！");
            }

            //转换账户信息
            object[] accounts = (object[])getAccountsQuery.vAccounts;
            if (accounts == null || accounts.Length <= 0)
            {
                throw CustomException.Run("删除卡与持卡人失败：没有找到账户信息！");
            }

            //遍历账户查询数据
            foreach (var item in accounts)
            {
                IAccount account = (IAccount)item;

                //查询条件
                for (int i = start; i <= end; i++)
                {
                    var result = GetCardByCardNumber(i.ToString(), account.AccountName);
                    if (result != null)
                    {
                        try
                        {
                            Delete(result.CardNumber, account.AccountName);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation("删除指定卡号区域的卡与持卡人：", ex);
                        }
                        try
                        {
                            _cardHolderSdkService.Delete(result.CardHolderID, 1, 1);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation("删除指定卡号区域的卡与持卡人：", ex);
                        }
                    }
                }
            }
        }
    }
}
