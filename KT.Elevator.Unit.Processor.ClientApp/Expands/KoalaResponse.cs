using Newtonsoft.Json;

namespace KT.Elevator.Unit.Secondary.ClientApp.Expands
{
    public class KoalaResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("data")]
        public KaolaPerson Data { get; set; }

        [JsonProperty("page")]
        public string Page { get; set; }
    }

    public class KaolaPerson
    {
        [JsonProperty("person_id")]
        public long PersonId { get; set; }
    }
}
