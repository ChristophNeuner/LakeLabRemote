using System;
using Microsoft.Data.Sqlite;
using LakeLabRemote.Controllers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LakeLabPiApp
{
    class Program
    {
        private static string uri = "http://localhost:50992/Values/Index";
        private static string databasePath = @"D:\DO.sqlite";
        private static string deviceName = "pi1";
        private static string sensorType = "do";
        private static ValueModel model;

        static void Main(string[] args)
        {
            model = new ValueModel(deviceName, sensorType);
            SQLiteDbReader dbreader = new SQLiteDbReader();
            model.Items.AddRange(dbreader.ReadDb(databasePath));
            HttpHelper httphelper = new HttpHelper();
            HttpResponseMessage response = httphelper.PostData(uri, model);
            //Console.WriteLine(response.IsSuccessStatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.Read();
        }
    }
}
