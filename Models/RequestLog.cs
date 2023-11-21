
using System.Runtime.Serialization;

namespace CurrancyQuery_API.Models
{
    public class RequestLog
    {
        public string Request { get; set; }
        public int Response { get; set; }
        public string Fecha { get; set; }

    }

    [DataContract]
    public class RequestLogWithValues
    {
        [DataMember]
        public string Request { get; set; }
        [DataMember]
        public int Response { get; set; }
        [DataMember]
        public string Fecha { get; set; }
        //public Dictionary<string, decimal>? Rates { get; set; }
        [DataMember]
        public IEnumerable<Object>? DataContent { get; set; } //Lista con objetos anónimos

        
    }
}
