using System.Net.Http;
using System.Text;
using LakeLabLib;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Authentication;

namespace LakeLabPiApp
{
    class HttpHelper
    {
        public async Task<string> PostDataAsync(string uri, List<ValueModel> valueModels)
        {
            using (var handler = new HttpClientHandler())
            {
                handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                string json = JsonConvert.SerializeObject(valueModels);

                using (var client = new HttpClient(handler))
                {
                    try
                    {
                        return await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync();
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }
            }           
        }
    }
}
