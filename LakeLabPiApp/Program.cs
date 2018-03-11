using System;
using System.Net.Http;
using LakeLabLib;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LakeLabPiApp
{
    class Program
    {
        private static string uri = "http://localhost:50992/api/ReceiveValues";
        private static string databasePathDO = @"D:\DO.sqlite";
        private static string databasePathTemp = @"D:\RTD.sqlite";

        //private static string uri = "http://212.227.175.108/api/ReceiveValues";
        //private static string databasePathDO = @"/home/pi/iniac/data/DO.sqlite";
        //private static string databasePathTemp = @"/home/pi/iniac/data/RTD.sqlite";

        private static string deviceName = "pi1";

        private static SQLiteDbReader dbreader = new SQLiteDbReader();
        private static HttpHelper httphelper = new HttpHelper();

        public static async Task Main(string[] args)
        {           
            while (true)
            {
                List<ValueModel>  valueModels = new List<ValueModel>();

                ValueModel modelDO = new ValueModel(deviceName, Enums.SensorTypes.Dissolved_Oxygen);               
                modelDO.Items.AddRange(dbreader.ReadDb(databasePathDO));
                ValueModel modelTemp = new ValueModel(deviceName, Enums.SensorTypes.Temperature);
                modelTemp.Items.AddRange(dbreader.ReadDb(databasePathTemp));

                valueModels.Add(modelDO);
                valueModels.Add(modelTemp);

                string response = await httphelper.PostDataAsync(uri, valueModels);
                Console.WriteLine(response);
                System.Threading.Thread.Sleep(60000);
            }
        }
    }
}
