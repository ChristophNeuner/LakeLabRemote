using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LakeLabLib
{
    public class ValueItemModel
    {
        public ValueItemModel(DateTime timestamp, float data, float temperature)
        {
            Timestamp = timestamp;
            Data = data;
            Temperature = temperature;
        }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("data")]
        public float Data { get; set; }

        [JsonProperty("temperature")]
        public float Temperature { get; set; }
    }
}
