using Newtonsoft.Json;

namespace CurrancyQuery_API.Models
{
    public class Currency
    {
        [JsonProperty("CurrencyCode")]
        public string Code { get; set; }

        [JsonProperty("Country")]
        public string NameCountry { get; set; }
    }
}
