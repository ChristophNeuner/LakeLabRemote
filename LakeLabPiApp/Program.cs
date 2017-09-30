using System;
using Microsoft.Data.Sqlite;
using LakeLabRemote.Controllers;
using Newtonsoft.Json;
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
            using (var connection = new SqliteConnection("" + new SqliteConnectionStringBuilder { DataSource = databasePath }))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var selectCommand = connection.CreateCommand();
                    selectCommand.Transaction = transaction;
                    selectCommand.CommandText = "SELECT timestamp, val1 FROM DO";
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        model = new ValueModel(deviceName, sensorType);

                        while(reader.Read())
                        {
                            //var value = reader.GetString(0) + " " + reader.GetString(1);
                            //Console.WriteLine(value);
                            model.Items.Add(new ValueItemModel(reader.GetDateTime(0), reader.GetFloat(1)));
                        }
                    }
                    transaction.Commit();
                }
            }

            //Console.WriteLine(model.DeviceName);
            //Console.WriteLine(model.SensorType);
            //foreach(var item in model.Items)
            //{
            //    Console.WriteLine(item.Data + " " + item.Timestamp);
            //}

            string json = JsonConvert.SerializeObject(model);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            Console.WriteLine(response.IsSuccessStatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.Read();
        }
    }
}
