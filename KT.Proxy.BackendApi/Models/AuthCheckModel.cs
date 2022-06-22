using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Proxy.BackendApi.Models
{
    public class AuthCheckModel
    {
        public string Phone { get; set; }
        public string AskCode { get; set; }
    }
}
