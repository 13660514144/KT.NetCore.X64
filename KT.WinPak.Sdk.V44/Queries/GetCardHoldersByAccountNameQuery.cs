using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class GetCardHoldersByAccountNameQuery
    {
        public string bstrAcctName { get; set; }
        public List<ICardHolder> vCardHolders { get; set; }

        public void SetValues(object obj)
        {
            vCardHolders = new List<ICardHolder>();
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
                vCardHolders.Add((ICardHolder)item);
            }
        }
    }
}