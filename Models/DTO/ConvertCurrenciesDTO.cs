namespace CurrencyQuery_API.Models.DTO
{
    public class ConvertCurrenciesDTO
    {
        public string ToCurrency { get; set; }
        public string FromCurrency { get; set; }
        public decimal Value { get; set; }
    }
}
