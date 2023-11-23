using Newtonsoft.Json.Linq;
using System;

namespace CurrencyQuery_API
{
    public static class GenericMessages
    {
        public static string GetBadRequestMessage()
        {
            return "Ha ocurrido un error, por favor vuelva a intentarlo mas tarde";
        }

        public static string NoMatchesFound(string value)
        {
            return $"No se han encontrado coincidencias para el valor: {value}";
        }

    }
}
