using CurrancyQuery_API.Models;
using CurrancyQuery_API.Services;
using CurrencyQuery_API.Interfaz;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyQuery_API.Services
{
    public class ConvertCurrenciesService : IConvertCurrencies
    {
        private readonly IConfiguration _configuration;
        private readonly IExchangeRate _exchangeRate;

        public ConvertCurrenciesService(IConfiguration configuration, IExchangeRate exchange)
        {
            _configuration = configuration;
           _exchangeRate = exchange;
        }

        //ExchangeRateService exchange = new ExchangeRateService(_configuration);

        public decimal GetConvertCurrency(string toCurrency, string fromCurrency)
        {
            try
            {
                var result = _exchangeRate.GetExchangeRate(toCurrency);

                // Filtra las monedas por el nombre proporcionado
                var response = result?.Result.Rates.Where(w => w.Key == fromCurrency.ToUpper()).FirstOrDefault();


                return response.Value.Value;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al leer/deserializar el archivo: {ex.Message}");
                return 0;
            }
        }
    }
}
