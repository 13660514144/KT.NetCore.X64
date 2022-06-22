using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class GetReadersByAccountNameQuery
    {
        public string bstrAcctName { get; set; }
        public List<IHWDevice> vReaders { get; set; }

        public void SetValues(object obj)
        {
            vReaders = new List<IHWDevice>();
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
                vReaders.Add((IHWDevice)item);
            }
        }
    }
}