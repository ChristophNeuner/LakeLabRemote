using System;
using System.Net.Http;
using LakeLabLib;
using System.Threading.Tasks;

namespace LakeLabPiApp
{
    class Program
    {
        //private static string uri = "http://212.227.11.55:10/Values/ReceiveValues";
        private static string databasePath = @"D:\DO.sqlite";
        private static string uri = "http://localhost:50992/Values/ReceiveValues";
        private static string uriFalse = "http://localhost:50993";
        //private static string databasePath = @"/home/pi/iniac/data/DO.sqlite";
        private static string deviceName = "test";
        private static string sensorType = "do";
        private static ValueModel model;

        public static async Task Main(string[] args)
        {
            while (true)
            {
                model = new ValueModel(deviceName, sensorType);
                SQLiteDbReader dbreader = new SQLiteDbReader();
                model.Items.AddRange(dbreader.ReadDb(databasePath));
                HttpHelper httphelper = new HttpHelper();
                //string response = await httphelper.PostDataAsync(uriFalse, model);
                string response = await httphelper.PostDataAsync(uri, model);
                //Console.WriteLine(response.IsSuccessStatusCode);
                Console.WriteLine(response);

                System.Threading.Thread.Sleep(2000);
            }
        }
    }
}
