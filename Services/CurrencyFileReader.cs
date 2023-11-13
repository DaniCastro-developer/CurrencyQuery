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
        //TO:DO limpiar esta parte
        private readonly string _filePath = @"C:\\Users\\DanielaCastro\\Documents\\currencycodes.txt";

        //Leer archivo txt y obtener data
        public List<Currency> ReadCurrenciesFromFile()
        {
            
            try
            {
                string jsonContent;

                using(StreamReader reader = new StreamReader(_filePath))
                {
                    jsonContent = reader.ReadToEnd();
                }

                var result = JsonConvert.DeserializeObject<CurrencyData>(jsonContent);

                return result?.Currencies;

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al leer/deserializar el archivo: {ex.Message}");
                return null; // O maneja el error de alguna manera específica de tu aplicación
            }
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
