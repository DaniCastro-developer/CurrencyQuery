using CurrancyQuery_API.Models;

namespace CurrencyQuery_API.Interfaz
{
    public interface IExchangeRate
    {
        Task<ApiResponse> GetExchangeRate(string currencyCode);
    }
}
