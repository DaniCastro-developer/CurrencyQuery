using System.Text;
using CurrancyQuery_API.Models;
using CurrancyQuery_API.Models.DTO;
using CurrancyQuery_API.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CurrancyQuery_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyCodeController : ControllerBase
    {
        private readonly ExchangeRateService _exchangeRateService;
        private readonly PostBinService _postBinService;

        public CurrencyCodeController(ExchangeRateService exchangeRateService, PostBinService postBinService)
        {
            _exchangeRateService = exchangeRateService;
            _postBinService = postBinService;
        }

        [HttpGet("getCurrencyCode")]
        public IActionResult GetCurrencyCode(string searchName)
        {
            if (string.IsNullOrEmpty(searchName))
            {
                return BadRequest("No se ha ingresado searchName");
            }

            var result = new CurrencyFileReader().GetCurrencyCode(searchName);

            if (result.Count != 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound($"No se ha encontrado CurrencyCode para {searchName}");
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
                var apiResult = _exchangeRateService.GetExchangeRate(currencyCode);


                // Log RQ enviado
                var requestLog = new RequestLog
                {
                    Request = $"GET /api/Currency/GetCurrencyChanges?currencyCode={currencyCode}",
                    Fecha = DateTime.UtcNow.ToString("dd-mm-yy hh:mm"),
                    Response = apiResult.Result != null ? 200 : 404
                };

                // Registrar en PostBin
                await _postBinService.PostToPostBin(requestLog);

                if (apiResult.Result != null)
                {
                    // Mapear respuesta según Dto
                    var currencyChangeDto = new CurrencyChangeDto
                    {
                        CurrencyCode = currencyCode.ToUpper(),
                        Values = apiResult.Result.Rates,
                    };

                    return Ok(currencyChangeDto);
                }
                else
                {
                    return NotFound($"No se ha encontrado valor para la moneda {currencyCode}");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error en GetCurrencyChanges: {ex}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

    }
}
