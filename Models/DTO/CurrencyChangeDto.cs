namespace CurrancyQuery_API.Models.DTO
{
    public class CurrencyChangeDto
    {
        public string CurrencyCode { get; set; }
        public Dictionary<string, decimal> Values { get; set; }
    }
}
