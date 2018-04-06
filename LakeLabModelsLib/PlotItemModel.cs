using Newtonsoft.Json;
using System;

namespace LakeLabLib
{
    public class PlotItemModel
    {
        public PlotItemModel(long timestamp, float data)
        {
            Timestamp = timestamp;
            Data = data;
        }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("data")]
        public float Data { get; set; }
    }
}
