﻿using Newtonsoft.Json;
using System;

namespace LakeLabLib
{
    public class ValueItemModel
    {
        public ValueItemModel(DateTime timestamp, float data)
        {
            Timestamp = timestamp;
            Data = data;
        }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("data")]
        public float Data { get; set; }
    }
}
