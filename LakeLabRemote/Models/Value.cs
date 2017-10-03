using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.Models
{
    public abstract class Value
    {
        protected Value(DateTime timestamp, Device device, float data, string lake, string location, string depth)
        {
            Guid = new Guid();
            Timestamp = timestamp;
            Device = device;
            Data = data;
            Lake = lake;
            Location = location;
            Depth = depth;
        }

        protected Value() { }

        [Key]
        public Guid Guid { get; set; }
        public DateTime Timestamp { get; set; }
        public Device Device { get; set; }
        public float Data { get; set; }
        public string Lake { get; set; }
        public string Location { get; set; }
        public string Depth { get; set; }

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
            return a.Timestamp == b.Timestamp && a.Device.Name == b.Device.Name && a.Data == b.Data && a.Lake == b.Lake && a.Location == b.Location && a.Depth == b.Depth;
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
        public ValueDO(DateTime timestamp, Device device, float data, string lake, string location, string depth) : base(timestamp, device, data, lake, location, depth){}

        public ValueDO() { }
    }
}
