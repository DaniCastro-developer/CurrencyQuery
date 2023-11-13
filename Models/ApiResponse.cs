using Newtonsoft.Json;

namespace CurrancyQuery_API.Models
{
    public class ApiResponse
    {

        [JsonProperty("base")]
        public string BaseCurrency { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("time_last_updated")]
        public int TimeLastUpdated { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
