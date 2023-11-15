using CurrancyQuery_API.Interfaz;
using CurrancyQuery_API.Models;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Text.Json;

namespace CurrancyQuery_API.Services
{
    public class CurrencyFileReader : ICurrencyFileReader
    {
        //TODO: limpiar esta parte
        //private readonly string _filePath = @"C:\\Users\\DanielaCastro\\Documents\\currencycodes.txt";

        private readonly string _filePath;

        public CurrencyFileReader(string filePath)
        {
            _filePath = filePath;
        }

        //Leer archivo txt y obtener data
        public List<Currency> ReadCurrenciesFromFile()
        {
            var json = File.ReadAllText(_filePath);
            var currenciesData = JsonConvert.DeserializeObject<CurrencyData>(json);

            return currenciesData?.Currencies;

            //try
            //{
            //    string jsonContent;

            //    using(StreamReader reader = new StreamReader(_filePath))
            //    {
            //        jsonContent = reader.ReadToEnd();
            //    }

            //    var result = JsonConvert.DeserializeObject<CurrencyData>(jsonContent);

            //    return result?.Currencies;

            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine($"Error al leer/deserializar el archivo: {ex.Message}");
            //    return null; // O maneja el error de alguna manera específica de tu aplicación
            //}
        }

        //Filtrar y obtener currency Code
        public virtual List<Currency> GetCurrencyCode(string searchName)
        {
            var currencies = ReadCurrenciesFromFile();

            // Filtra las monedas por el nombre proporcionado
            var result = currencies?.FindAll(c => c.NameCountry.Contains(searchName, StringComparison.OrdinalIgnoreCase));

            return result;
        }
    }
}
