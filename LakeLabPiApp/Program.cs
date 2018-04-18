using System;
using System.Net.Http;
using LakeLabLib;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace LakeLabPiApp
{
    class Program
    {
        Parameters parameters = LoadJson()
        

        private static string _uri = 
        private static string _databasePathDO = 
        private static string _databasePathTemp = 
        private static string _deviceName = 

        private static SQLiteDbReader _dbreader = new SQLiteDbReader();
        private static HttpHelper _httphelper = new HttpHelper();

        public static async Task Main(string[] args)
        {           
            while (true)
            {
                List<ValueModel>  valueModels = new List<ValueModel>();

                ValueModel modelDO = new ValueModel(_deviceName, Enums.SensorTypes.Dissolved_Oxygen);               
                modelDO.Items.AddRange(_dbreader.ReadDb(_databasePathDO));
                ValueModel modelTemp = new ValueModel(_deviceName, Enums.SensorTypes.Temperature);
                modelTemp.Items.AddRange(_dbreader.ReadDb(_databasePathTemp));

                valueModels.Add(modelDO);
                valueModels.Add(modelTemp);

                string response = await _httphelper.PostDataAsync(_uri, valueModels);
                Console.WriteLine(response);
                System.Threading.Thread.Sleep(60*5*1000);
            }
        }

        private static async Task<Parameters> LoadJson(string path)
        {
            Parameters parameters = new Parameters();
            using (StreamReader r = new StreamReader(path))
            {
                string json = await r.ReadToEndAsync();
                parameters = JsonConvert.DeserializeObject<Parameters>(json);
            }

            return parameters;
        }
    }

    public class Parameters
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("databasePathDO")]
        public string DatabasePathDO { get; set; }

        [JsonProperty("databasePathTemp")]
        public string DatabasePathTemp { get; set; }

        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }
    }
}
