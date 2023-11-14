using CurrancyQuery_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CurrancyQuery_API.Services
{
    public class ExchangeRateService
    {
        public async Task<ApiResponse> GetExchangeRate(string currencyCode)
        {
            // URL de la API externa
            var apiUrl = $"https://api.exchangerate-api.com/v4/latest/{currencyCode}";

            using (HttpClient httpClient = new())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Clear();

                    // Realizar la petición GET
                    var response = httpClient.GetAsync(apiUrl).Result;

                    // Verificar si la solicitud fue exitosa (código de estado 200 OK)
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer y deserializar la respuesta JSON
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        var apiResult = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                        return apiResult;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones, por ejemplo, loggear el error
                    Console.WriteLine($"Error al realizar la petición a la API externa: {ex}");
                    return null;
                }
            }
        }
    }
}
