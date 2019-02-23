using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BenamixTest.Models;
using Newtonsoft.Json;

namespace BenamixTest.Services.Rest
{
    public class RestService : IRestService
    {
        public RestService()
        {
        }

        public async Task<RootObject> GetData()
        {
            RootObject obj= null;
            var client = new HttpClient();
            try
            {
                using (var responce = await client.GetAsync("https://api.binance.com/api/v1/depth?symbol=ETHBTC&limit=1000"))
                {
                    var json = await responce.Content.ReadAsStringAsync().ConfigureAwait(false);
                    obj = JsonConvert.DeserializeObject<RootObject>(json);
                }
            }
            catch (Exception)
            {

            }
            return obj;
        }
    }
}