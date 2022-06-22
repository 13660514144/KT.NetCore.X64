using KT.WinPak.SDK.V48.IServices;
using KT.WinPak.SDK.V48.ISqlDaos;
using KT.WinPak.SDK.V48.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.SqlSerivces
{
    public class CardHolderSqlService : ICardHolderSqlService
    {
        private ICardHolderSqlDao _cardHolderSqlDao;
        public CardHolderSqlService(ICardHolderSqlDao  cardHolderSqlDao)
        {
            _cardHolderSqlDao = cardHolderSqlDao;
        }
        public List<CardHolderModel> GetAll()
        {
            var entities = _cardHolderSqlDao.GetAll();

            var models = CardHolderModel.FromSqlEntities(entities);

            return models;
        }

        public CardHolderModel Add(CardHolderModel model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int lCHId, int bDelCard, int bDelImage)
        {
            throw new NotImplementedException();
        }

        public bool Edit(int lCardholderID, CardHolderModel model)
        {
            throw new NotImplementedException();
        }

    }
}
