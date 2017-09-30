using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.Models
{
    public abstract class Value
    {
        protected Value(DateTime timestamp, Device device, int data)
        {
            Guid = new Guid();
            Timestamp = timestamp;
            Device = device;
            Data = data;
        }

        [Key]
        public Guid Guid { get; set; }
        public DateTime Timestamp { get; set; }
        public Device Device { get; set; }
        public int Data { get; set; }
    }


    /// <summary>
    /// Class for a dissolved oxygen value.
    /// </summary>
    public class ValueDO : Value
    {       
        public ValueDO(DateTime timestamp, Device device, int value) : base(timestamp, device, value){}      
    }
}
