using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class GetAccessLevelsByAccountNameQuery
    {
        public string bstrAccountName { get; set; }
        public string bstrSubAccountName { get; set; }
        public List<IAccessLevel> vAccessLevels { get; set; }
         
        public void SetValues(object obj)
        {
            vAccessLevels = new List<IAccessLevel>();
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
                vAccessLevels.Add((IAccessLevel)item);
            }
        }
    }
}
