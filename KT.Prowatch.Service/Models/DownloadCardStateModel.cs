using KT.Common.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Prowatch.Service.Models
{
    public class DownloadCardStateModel
    {
        public string PersonId { get; set; }
        public string CardNo { get; set; }
        public string StateCode { get; set; }
        public long AddTime { get; set; }
        public int Times { get; set; }

        public DownloadCardStateModel(string personId, string cardNo, string stateCode)
        {
            PersonId = personId;
            CardNo = cardNo;
            StateCode = stateCode;
            AddTime = DateTimeUtil.UtcNowMillis();
            Times = 1;
        }
    }
}
