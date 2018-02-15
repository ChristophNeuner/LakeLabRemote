using System.Net.Http;
using System.Text;
using LakeLabLib;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LakeLabPiApp
{
    class HttpHelper
    {
        public async Task<string> PostDataAsync(string uri, List<ValueModel> valueModels)
        {
            string json = JsonConvert.SerializeObject(valueModels);
            HttpClient client = new HttpClient();
            try
            {
                return await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync();
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
