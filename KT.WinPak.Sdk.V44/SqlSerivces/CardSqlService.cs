using KT.WinPak.SDK.IServices;
using KT.WinPak.SDK.ISqlDaos;
using KT.WinPak.SDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.SqlSerivces
{
    public class CardSqlService : ICardSqlService
    {
        private ICardSqlDao _cardSqlDao;
        public CardSqlService(ICardSqlDao cardSqlDao)
        {
            _cardSqlDao = cardSqlDao;
        }
        public List<CardModel> GetAll()
        {
            var entities = _cardSqlDao.GetAll();

            var models = CardModel.FromSqlEntities(entities);

            return models;
        }

        public CardModel Add(CardModel pcard)
        {
            throw new NotImplementedException();
        }

        public CardAndCardHolderModel AddWithCardHolder(CardAndCardHolderModel model)
        {
            throw new NotImplementedException();
        }

        public List<CardAndCardHolderModel> AddWithCardHolders(string accountName, List<CardAndCardHolderModel> models)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string bstrCardno, string bstrAcctName)
        {
            throw new NotImplementedException();
        }

        public void DeleteCardAndHolders(int start, int end)
        {
            throw new NotImplementedException();
        }

        public CardAndCardHolderModel DeleteWithCardHolder(CardAndCardHolderModel model)
        {
            throw new NotImplementedException();
        }

        public List<CardAndCardHolderModel> DeleteWithCardHolders(string accountName, List<CardAndCardHolderModel> models)
        {
            throw new NotImplementedException();
        }

        public bool Edit(string bstrCardno, string bstrAcctName, CardModel pcard)
        {
            throw new NotImplementedException();
        }

        public CardModel GetCardByCardNumber(string cardNumber, string accountName)
        {
            throw new NotImplementedException();
        }
    }
}
