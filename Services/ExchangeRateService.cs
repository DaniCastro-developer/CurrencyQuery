using CurrancyQuery_API.Models;
using CurrencyQuery_API.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CurrancyQuery_API.Services
{
    public class ExchangeRateService : IExchangeRate
    {
        private readonly IConfiguration _configuration;
        public ExchangeRateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ApiResponse> GetExchangeRate(string currencyCode)
        {
            // URL de la API externa
            var apiUrl = _configuration["UrlApiCurrencies"].Replace("{0}", currencyCode);

            using (HttpClient httpClient = new())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Clear();

                    // Realizar la petición GET
                    var response = httpClient.GetAsync(apiUrl).Result;

                    // Leer y deserializar la respuesta JSON
                    var jsonResponse = response.Content.ReadAsStringAsync().Result;
                    var apiResult = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                    return apiResult;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al realizar la petición a la API externa: {ex}");
                    return null;
                }
            }
        }
    }
}
