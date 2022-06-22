using KT.WinPak.SDK.V48.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.ISqlDaos
{
    public interface ICardSqlDao
    {
        List<Card> GetAll();
    }
}
