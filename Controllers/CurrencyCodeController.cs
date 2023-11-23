using System.Text;
using CurrancyQuery_API.Interfaz;
using CurrancyQuery_API.Models;
using CurrancyQuery_API.Models.DTO;
using CurrancyQuery_API.Services;
using CurrencyQuery_API.Interfaz;
using CurrencyQuery_API.Models.DTO;
using CurrencyQuery_API.Models.RS;
using CurrencyQuery_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CurrencyQuery_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyCodeController : ControllerBase
    {
        private readonly IExchangeRate _exchangeRate;
        private readonly IPostBin _postBin;
        private readonly ICurrencyFileReader _fileReader;
        private readonly IConvertCurrencies _convertCurrencies;

        public CurrencyCodeController(IExchangeRate exchangeRate, IPostBin postBin, ICurrencyFileReader fileReader, IConvertCurrencies convertCurrencies)
        {
            _exchangeRate = exchangeRate;
            _postBin = postBin;
            _fileReader = fileReader;
            _convertCurrencies = convertCurrencies;
        }

        [HttpGet("getCurrencyCode/{searchName}")]
        public IActionResult GetCurrencyCode(string searchName)
        {
           
            var result = _fileReader.GetCurrencyCode(searchName);

            if(result.Count == 0)
            {
                var errorResponse = new
                {
                    Mensaje = GenericMessages.NoMatchesFound(searchName),
                    CodeStatus = 404,
                };

                return BadRequest(errorResponse);

            }

            return Ok(result);
        }

        //EndPoint 2
        [HttpGet("GetCurrencyChanges/{currencyCode}")]
        public async Task<IActionResult> GetCurrencyChanges(string currencyCode)
        {
            
            // Realizar la petición a la API externa (exchangerate-api)
            var apiResult = _exchangeRate.GetExchangeRate(currencyCode);

            string urlRQ = "GET/ " + Request.GetDisplayUrl();


            // Registrar en PostBin
            await _postBin.LogPostBin(urlRQ, apiResult.Result != null ? 200 : 404);


            if(apiResult.Result == null) {
                var errorResponse = new
                {
                    Mensaje = GenericMessages.NoMatchesFound(currencyCode),
                    CodeStatus = 404,
                };

                return BadRequest(errorResponse);
            }

            // Mapear respuesta según Dto
            var currencyChangeDto = new CurrencyChangeDto
            {
                CurrencyCode = currencyCode.ToUpper(),
                Values = apiResult.Result.Rates,
            };

            return Ok(currencyChangeDto);
        }

        //Endponit 3
        [HttpGet("ConvertCurrencies/{toCurrencyCode}/{fromCurrencyCode}")]
        public async Task<IActionResult> GetConvertCurrencies(string toCurrencyCode, string fromCurrencyCode)
        {

            var consult = _convertCurrencies.GetConvertCurrency(toCurrencyCode, fromCurrencyCode);

            string urlRQ = "GET/ " + Request.GetDisplayUrl();

            await _postBin.LogPostBin(urlRQ, consult != 0 ? 200 : 404);

            var convertResult = new ConvertCurrenciesDTO()
            {
                ToCurrency = toCurrencyCode.ToUpper(),
                FromCurrency = fromCurrencyCode.ToUpper(),
                Value = consult
            };

            var errorResponse = new
            {
                Mensaje = GenericMessages.GetBadRequestMessage(),
                CodeStatus = 404,
            };

            return consult != 0 ? Ok(convertResult) : BadRequest(errorResponse);
        }

    }
}
