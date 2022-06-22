using KT.WinPak.SDK.Entities;
using KT.WinPak.SDK.ISqlDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.SqlDaos
{
    public class CardHolderSqlDao : ICardHolderSqlDao
    {
        private WINPAKPROContext _context;

        public CardHolderSqlDao(WINPAKPROContext context)
        {
            _context = context;
        }

        public List<CardHolder> GetAll()
        {
            return _context.CardHolder.Where(x => x.Deleted == 0).ToList();
        }
    }
}
