using CurrancyQuery_API.Models;

namespace CurrencyQuery_API.Interfaz
{
    public interface IPostBin
    {
        Task LogPostBin(string currencyCode, int status);
        Task LogPostBinWithValues(string currencyCode, ApiResponse result);
    }
}
