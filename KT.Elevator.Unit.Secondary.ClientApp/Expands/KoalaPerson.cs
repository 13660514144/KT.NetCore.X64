using Newtonsoft.Json;

namespace KT.Elevator.Unit.Secondary.ClientApp.Expands
{
    public class KoalaPerson
    {
        [JsonProperty("person_id")]
        public long PersonId { get; set; }
    }
}
