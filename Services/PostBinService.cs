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
            var postBinUrl = "https://www.toptal.com/developers/postbin/1699991205668-8590251745190";

            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.PostAsJsonAsync(postBinUrl, requestLog);
            }
        }
    }
}
