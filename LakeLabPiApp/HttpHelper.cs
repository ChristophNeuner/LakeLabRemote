using System.Net.Http;
using System.Text;
using LakeLabLib;
using Newtonsoft.Json;

namespace LakeLabPiApp
{
    class HttpHelper
    {
        public HttpResponseMessage PostData(string uri, ValueModel model)
        {
            string json = JsonConvert.SerializeObject(model);
            HttpClient client = new HttpClient();
            return client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json")).Result;
        }
    }
}
