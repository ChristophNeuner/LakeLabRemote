using System;
using System.Net.Http;
using LakeLabLib;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;

namespace LakeLabPiApp
{
    class Program
    {
        private static SQLiteDbReader _dbreader = new SQLiteDbReader();
        private static HttpHelper _httphelper = new HttpHelper();

        public static async Task Main(string[] args)
        {
            while (true)
            {
                Parameters parameters = new Parameters();
                string databasePathDO = "";
                string databasePathTemp = "";

                try
                {
                    if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        parameters = await LoadJsonAsync(".\\parameters.json");
                        databasePathDO = parameters.DatabasePathDOWindows;
                        databasePathTemp = parameters.DatabasePathTempWindows;
                    }
                        
                    else
                    {
                        parameters = await LoadJsonAsync(".//parameters.json");
                        databasePathDO = parameters.DatabasePathDOLinux;
                        databasePathTemp = parameters.DatabasePathTempLinux;
                    }
                    
                    string uri = parameters.Uri;                   
                    string deviceName = parameters.DeviceName;
                    int sleepTime = parameters.SleepTime;

                    List<ValueModel> valueModels = new List<ValueModel>();

                    ValueModel modelDO = new ValueModel(deviceName, Enums.SensorTypes.Dissolved_Oxygen);
                    modelDO.Items.AddRange(_dbreader.ReadDb(databasePathDO));
                    ValueModel modelTemp = new ValueModel(deviceName, Enums.SensorTypes.Temperature);
                    modelTemp.Items.AddRange(_dbreader.ReadDb(databasePathTemp));

                    valueModels.Add(modelDO);
                    valueModels.Add(modelTemp);

                    string response = await _httphelper.PostDataAsync(uri, valueModels);
                    Console.WriteLine(response);
                    System.Threading.Thread.Sleep(sleepTime);
                }

                catch (FileNotFoundException e)
                {
                    Console.WriteLine($"{e.FileName} not found");
                    continue;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }

        private static async Task<Parameters> LoadJsonAsync(string path)
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

        [JsonProperty("databasePathDOWindows")]
        public string DatabasePathDOWindows { get; set; }

        [JsonProperty("databasePathTempWindows")]
        public string DatabasePathTempWindows { get; set; }

        [JsonProperty("databasePathDOLinux")]
        public string DatabasePathDOLinux { get; set; }

        [JsonProperty("databasePathTempLinux")]
        public string DatabasePathTempLinux { get; set; }

        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }

        [JsonProperty("sleepTime")]
        public int SleepTime { get; set; }
    }
}
