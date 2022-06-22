using KT.WinPak.SDK.V48.Entities;
using KT.WinPak.SDK.V48.ISqlDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.V48.SqlDaos
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
            return _context.CardHolders.Where(x => x.Deleted == 0).ToList();
        }
    }
}
