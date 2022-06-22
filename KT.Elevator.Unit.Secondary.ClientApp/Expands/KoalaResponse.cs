using Newtonsoft.Json;

namespace KT.Elevator.Unit.Secondary.ClientApp.Expands
{
    public class KoalaResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("data")]
        public KoalaPerson Data { get; set; }

        [JsonProperty("page")]
        public object Page { get; set; }
    }
}
