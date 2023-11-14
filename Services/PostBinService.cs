using CurrancyQuery_API.Models;
using Newtonsoft.Json;
using System.Text;

namespace CurrancyQuery_API.Services
{
    public class PostBinService
    {
        //Log a postbin
        public async Task PostToPostBin(RequestLog requestLog)
        {
            // url PostBin
            var postBinUrl = "https://www.toptal.com/developers/postbin/1699966965998-4839934348128";

            using (var httpClient = new HttpClient())
            {
                await httpClient.PostAsJsonAsync(postBinUrl, requestLog);
            }
        }
    }
}
