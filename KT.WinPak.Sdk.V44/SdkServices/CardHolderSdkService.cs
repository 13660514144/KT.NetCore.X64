using KT.Common.Core.Exceptions;
using KT.WinPak.SDK.IServices;
using KT.WinPak.SDK.Message;
using KT.WinPak.SDK.Models;
using KT.WinPak.SDK.Queries;
using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class CardHolderSdkService : ICardHolderSdkService
    {
        private IAllSdkService _allSdkService;

        public CardHolderSdkService(IAllSdkService allSdkService)
        {
            _allSdkService = allSdkService;
        }

        public List<CardHolderModel> GetAll()
        {
            List<CardHolderModel> results = new List<CardHolderModel>();

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
                var query = new GetCardHoldersByAccountNameQuery();
                query.bstrAcctName = account.AccountName;

                //执行查询
                query = _allSdkService.GetCardHoldersByAccountName(query);

                //查询结果为空
                if (query.vCardHolders == null)
                {
                    continue;
                }

                //转换结果数据 
                if (query.vCardHolders == null || query.vCardHolders.FirstOrDefault() == null)
                {
                    continue;
                }

                //处理返回数据
                foreach (var obj in query.vCardHolders)
                {
                    var result = CardHolderModel.FromEntity(obj);
                    result.AccountName = account.AccountName;
                    results.Add(result);
                }
            }

            return results;
        }

        public CardHolderModel Add(CardHolderModel model)
        {
            //转换参数
            var entity = _allSdkService.GetCardHolderClass();
            entity = CardHolderModel.ToEntity(entity, model);

            //查询条件
            var query = new AddCardHolderQuery();
            query.pCH = entity;

            //执行操作
            query = _allSdkService.AddCardHolder(query);

            //返回结果
            if (query.pStatus != 0)
            {
                throw CustomException.Run("新增持卡人失败：" + MessageEnum.GetMessageByCode(query.pStatus));
            }
            model.CardHolderID = query.pCH.CardHolderID;
            return model;
        }

        public bool Edit(int lCardholderID, CardHolderModel model)
        {
            //转换参数
            var entity = _allSdkService.GetCardHolderClass();
            entity = CardHolderModel.ToEntity(entity, model);

            //查询条件
            var query = new EditCardHolderQuery();
            query.lCardholderID = lCardholderID;
            query.pCH = entity;

            //执行操作
            query = _allSdkService.EditCardHolder(query);

            //返回结果
            if (query.pStatus != 0)
            {
                throw CustomException.Run("新增持卡人失败：" + MessageEnum.GetMessageByCode(query.pStatus));
            }
            return true;
        }

        public bool Delete(int lCHId, int bDelCard, int bDelImage)
        {
            var query = new DeleteCardHolderQuery();
            query.lCHId = lCHId;
            query.bDelCard = bDelCard;
            query.bDelImage = bDelImage;

            query = _allSdkService.DeleteCardHolder(query);

            if (query.pStatus != 0)
            {
                throw CustomException.Run(string.Format("删除持卡人失败：{0} {1}", lCHId, MessageEnum.GetMessageByCode(query.pStatus)));
            }
            return true;
        }
    }
}
