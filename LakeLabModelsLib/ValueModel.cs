﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace LakeLabLib
{
    public sealed class ValueModel
    {
        public ValueModel(string deviceName, string sensorType)
        {
            DeviceName = deviceName;
            SensorType = sensorType;
        }

        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }

        [JsonProperty("sensorType")]
        public string SensorType { get; set; }

        [JsonProperty("items")]
        public List<ValueItemModel> Items { get; } = new List<ValueItemModel>();
    }
}
