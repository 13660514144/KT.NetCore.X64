using KT.Common.Core.Exceptions;
using KT.WinPak.SDK.V48.IServices;
using KT.WinPak.SDK.V48.Message;
using KT.WinPak.SDK.V48.Models;
using KT.WinPak.SDK.V48.Queries;
using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.V48.Services
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
            throw new NotImplementedException();
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
