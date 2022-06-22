using KT.WinPak.SDK.V48.Entities;
using KT.WinPak.SDK.V48.ISqlDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.V48.SqlDaos
{
    public class CardSqlDao : ICardSqlDao
    {
        private WINPAKPROContext _context;

        public CardSqlDao(WINPAKPROContext context)
        {
            _context = context;
        }

        public List<Card> GetAll()
        {
            var cards = _context.Cards.Where(x => x.Deleted == 0).ToList();
            if (cards != null)
            {
                foreach (var item in cards)
                {
                    int cardId = item.RecordId;
                    var lambda = from a in _context.CardAccessLevels
                                 join b in _context.AccessLevelPlus on a.AccessLevelId equals b.RecordId
                                 where a.CardId == cardId && a.Deleted == 0 && b.Deleted == 0
                                 select b;
                    item.AccessLevelPluses = lambda.ToList();

                    if (item.AccessLevelId.HasValue)
                    {
                        int accessLevelId = item.AccessLevelId.Value;
                        item.AccessLevelPlus = _context.AccessLevelPlus.FirstOrDefault(x => x.RecordId == accessLevelId);
                    }
                }
            }
            return cards;
        }
    }
}
