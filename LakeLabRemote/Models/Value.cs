using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.Models
{
    public abstract class Value
    {
        protected Value(DateTime timestamp, Device device, float data)
        {
            Guid = new Guid();
            Timestamp = timestamp;
            Device = device;
            Data = data;           
        }

        protected Value() { }

        [Key]
        public Guid Guid { get; set; }
        public DateTime Timestamp { get; set; }
        public Device Device { get; set; }
        public float Data { get; set; }

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
            return a.Timestamp == b.Timestamp && a.Device == b.Device && a.Data == b.Data;
        }

        public static bool operator !=(Value a, Value b)
        {
            return !(a == b);
        }
    }


    /// <summary>
    /// Class for a dissolved oxygen value.
    /// </summary>
    public class ValueDO : Value
    {       
        public ValueDO(DateTime timestamp, Device device, float data, float temperature) : base(timestamp, device, data)
        {
            Temperature = temperature;
        }

        public float Temperature { get; set; }

        public ValueDO() { }
    }

    /// <summary>
    /// Class for a temperature value.
    /// </summary>
    public class ValueTemp : Value
    {
        public ValueTemp(DateTime timestamp, Device device, float data) : base(timestamp, device, data) { }

        public ValueTemp() { }
    }
}
