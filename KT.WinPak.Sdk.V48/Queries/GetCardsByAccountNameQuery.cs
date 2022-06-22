using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class GetCardsByAccountNameQuery
    {
        public string bstrAcctName { get; set; }
        public string bstrSubAcctName { get; set; }
        public List<ICard> vCards { get; set; }

        public void SetValues(object obj)
        {
            vCards = new List<ICard>();
            if (obj == null)
            {
                return;
            }
            var objs = (object[])obj;
            if (objs == null || objs.FirstOrDefault() == null)
            {
                return;
            }
            foreach (var item in objs)
            {
                vCards.Add((ICard)item);
            }
        }
    }
}