using CurrancyQuery_API.Models;

namespace CurrancyQuery_API.Interfaz
{
    public interface ICurrencyFileReader
    {
        List<Currency> GetCurrencyCode(string searchName);
    }
}
