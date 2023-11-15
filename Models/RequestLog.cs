using CurrancyQuery_API.Models.DTO;
using static CurrancyQuery_API.Models.DTO.ExchangeDollarDto;

namespace CurrancyQuery_API.Models
{
    public class RequestLog
    {
        public string Request { get; set; }
        public int Response { get; set; }
        public string Fecha { get; set; }

        public Dictionary<string, decimal>? Rates { get; set; }
    }


}
