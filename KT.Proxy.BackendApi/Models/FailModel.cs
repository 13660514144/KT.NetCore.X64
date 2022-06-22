using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Proxy.BackendApi.Models
{
    public class FailModel
    {
        [JsonProperty("id")]
        public string PersonId { get; set; }

        public string Reason { get; set; }
    }
}
