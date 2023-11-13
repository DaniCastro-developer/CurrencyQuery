using System.Text.Json.Nodes;
using CurrancyQuery_API.Interfaz;
using CurrancyQuery_API.Models;
using CurrancyQuery_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;

namespace CurrancyQuery_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyCodeController : ControllerBase
    {
        
        [HttpGet("getCurrencyCode")]
        public IActionResult GetCurrencyCode(string searchName)
        {
            if (string.IsNullOrEmpty(searchName))
            {
                return BadRequest("No se ha ingresado searchName");
            }

            var result = new CurrencyFileReader().GetCurrencyCode(searchName);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        //EndPoint 2
        [HttpGet("GetCurrencyChanges")]
        public async Task<IActionResult> GetCurrencyChanges(string currencyCode)
        {
            try
            {
                if (string.IsNullOrEmpty(currencyCode))
                {
                    return BadRequest("El código de moneda no puede estar vacío o nulo");
                }

                // Realizar la petición a la API externa (exchangerate-api)
                var apiResult = await GetExchangeRate(currencyCode);
                
                // Log RQ enviado
                var requestLog = new RequestLog
                {
                    Request = $"GET /api/Currency/GetCurrencyChanges?currencyCode={currencyCode}",
                    Fecha = DateTime.UtcNow,
                    Response = apiResult != null ? 200 : 404
                };

                // Registrar en PostBin
                await PostToPostBin(requestLog);

                if (apiResult != null)
                {
                    requestLog.Response = 200;
                    return Ok(apiResult.Rates);
                } else
                {
                    requestLog.Response = 404;
                    return NotFound("No se pudo obtener el cambio de la moneda");
                }     
                
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error en GetCurrencyChanges: {ex}");
                return StatusCode(500, "Error interno del servidor");
            }

        
        }
        [HttpGet("GetExchangeRate")]
        public async Task<ApiResponse> GetExchangeRate(string currencyCode)
        {
            // URL de la API externa
            var apiUrl = $"https://api.exchangerate-api.com/v4/latest/{currencyCode}";

            using (HttpClient httpClient = new HttpClient())
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

        private async Task PostToPostBin(RequestLog requestLog)
        {
            // Log a PostBin
            var postBinUrl = "https://www.toptal.com/developers/postbin/1699901571300-2177225337363";
            var logEntry = new LogEntry
            {
                Request = requestLog
            };

            using (var httpClient = new HttpClient())
            {
                await httpClient.PostAsJsonAsync(postBinUrl, logEntry);
            }
        }

    }
}
