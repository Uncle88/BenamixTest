using System.Net.Http;
using System.Threading.Tasks;
using BenamixTest.Models;
using Newtonsoft.Json;

namespace BenamixTest.Services.Rest
{
    public class RestService : IRestService
    {
        public async Task<RootObject> GetData()
        {
            var client = new HttpClient();

            using (var responce = await client.GetAsync("https://api.binance.com/api/v1/depth?symbol=ETHBTC&limit=1000"))
            {
                var json = await responce.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<RootObject>(json);
            }
        }
    }
}