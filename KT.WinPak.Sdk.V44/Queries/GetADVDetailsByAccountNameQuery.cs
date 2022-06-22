using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class GetADVDetailsByAccountNameQuery
    {
        public string bstrAcctName { get; set; }
        public List<IHWDevice> vDevices { get; set; }

        public void SetValues(object obj)
        {
            vDevices = new List<IHWDevice>();
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
                vDevices.Add((IHWDevice)item);
            }
        }
    }
}