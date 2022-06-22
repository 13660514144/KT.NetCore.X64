using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class GetTimeZonesByAccountNameQuery
    {
        public string bstrAcctName { get; set; }
        public List<ITimeZone> vTimezones { get; set; }

        public void SetValues(object obj)
        {
            vTimezones = new List<ITimeZone>();
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
                vTimezones.Add((ITimeZone)item);
            }
        }
    }
}