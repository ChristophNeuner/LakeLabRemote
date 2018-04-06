using Newtonsoft.Json;
using System.Collections.Generic;

namespace LakeLabLib
{
    public sealed class PlotModel
    {
        public PlotModel(string deviceName, Enums.SensorTypes sensorType)
        {
            DeviceName = deviceName;
            SensorType = sensorType;
        }

        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }

        [JsonProperty("sensorType")]
        public Enums.SensorTypes SensorType { get; set; }

        [JsonProperty("items")]
        public List<PlotItemModel> Items { get; } = new List<PlotItemModel>();
    }
}
