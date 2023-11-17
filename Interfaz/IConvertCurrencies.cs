namespace CurrencyQuery_API.Interfaz
{
    public interface IConvertCurrencies
    {
        decimal GetConvertCurrency(string toCurrency, string fromCurrency);
    }
}
