using CurrancyQuery_API.Models;
using CurrencyQuery_API.Interfaz;
using System.Text;
using System.Text.Json;

namespace CurrancyQuery_API.Services
{
    public class PostBinService : IPostBin
    {
        private readonly IConfiguration _configuration;

        public PostBinService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //Log a postbin
        public async Task LogPostBin(string urlRQ, int status)
        {
            try
            {
                // Log RQ enviado
                var requestLog = new RequestLog
                {
                    Request = urlRQ,
                    Fecha = DateTime.Now.ToString("dd-mm-yy hh:mm"),
                    Response = status,
                    //Rates

                };

                // url PostBin
                var postBinUrl = _configuration["PostBinUrlPost"];

                using (var httpClient = new HttpClient())
                {
                    var res = await httpClient.PostAsJsonAsync(postBinUrl, requestLog);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al realizar la petición a la API externa: {ex}");
            }
        }

        public async Task LogPostBinWithValues(string currencyCode, ApiResponse result)
        {

            IEnumerable<object>? ratesList = result?.Rates?.Select(s => new { Clave = s.Key, Valor = s.Value });

            try
            {
                // Log RQ enviado
                var requestLog = new RequestLogWithValues
                {
                    Request = "GET/ " + _configuration["UrlApiCurrencies"].Replace("{0}", currencyCode),
                    Fecha = DateTime.Now.ToString("dd-mm-yy hh:mm:ss"),
                    Response = result.Rates != null ? 200 : 204,
                    //Rates = result?.Rates,
                    DataContent = ratesList

                };

                // url PostBin
                var postBinUrl = _configuration["PostBinUrlPost"];

                //string json = JsonSerializer.Serialize(requestLog);

                using (var httpClient = new HttpClient())
                {
                    //var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var res = await httpClient.PostAsJsonAsync(postBinUrl, requestLog);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al realizar la petición a la API externa: {ex}");
            }
        }
    }
}
