using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KT.Prowatch.Service.Models
{
    public class RemovePersonModel
    {
        [JsonProperty("personId")]
        public string PersonId { get; set; }
    }
}