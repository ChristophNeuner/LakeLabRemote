using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LakeLabLib;

namespace LakeLabRemote.Models
{
    public class Value
    {
        public Value(DateTime timestamp, Device device, float data, Enums.SensorTypes sensorType)
        {
            Guid = new Guid();
            Timestamp = timestamp;
            Device = device;
            Data = data;
            SensorType = sensorType;
            switch (SensorType)
            {
                case Enums.SensorTypes.Dissolved_Oxygen:
                    DataUnit = "mg/L";
                    break;
                case Enums.SensorTypes.Temperature:
                    DataUnit = "°C";
                    break;
            }
        }

        private Value() { }

        [Key]
        public Guid Guid { get; set; }
        public DateTime Timestamp { get; set; }
        public Device Device { get; set; }
        public float Data { get; set; }
        public Enums.SensorTypes SensorType { get; set; }
        public string DataUnit { get; set; }

        public static bool operator ==(Value a, Value b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Timestamp == b.Timestamp && a.Device == b.Device && a.Data == b.Data && a.SensorType == b.SensorType;
        }

        public static bool operator !=(Value a, Value b)
        {
            return !(a == b);
        }
    }
}
