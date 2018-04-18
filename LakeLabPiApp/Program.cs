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
        Json


        //private static string _uri = "http://localhost:50992/api/ReceiveValues";
        //private static string _databasePathDO = @"D:\DO.sqlite";
        //private static string _databasePathTemp = @"D:\RTD.sqlite";
               
        private static string _uri = "http://212.227.175.108/api/ReceiveValues";
        private static string _databasePathDO = @"/home/pi/iniac/data/DO.sqlite";
        private static string _databasePathTemp = @"/home/pi/iniac/data/RTD.sqlite";

        private static string _deviceName = "pi1";

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

        public void LoadJson()
        {
            using (StreamReader r = new StreamReader(".//parameters.json"))
            {
                string json = r.ReadToEnd();
                List<Parameters> parameters = JsonConvert.DeserializeObject<List<Parameters>>(json);
            }
        }
    }

    public class Parameters
    {
        public string
    }
}
