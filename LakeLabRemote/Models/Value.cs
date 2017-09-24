using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.Models
{
    public abstract class Value
    {
        protected Guid _guid;
        protected DateTime _timestamp;
        protected Device _device;
        protected Value(DateTime timestamp, Device device)
        {
            _guid = new Guid();
            _timestamp = timestamp;
            _device = device;
        }

        [Key]
        public Guid Guid { get { return _guid; } set { _guid = value; } }
        public DateTime Timestamp { get { return _timestamp; } set { _timestamp = value; } }
        public Device Device { get { return _device; } set { _device = value; } }
    }


    /// <summary>
    /// Class for a dissolved oxygen value.
    /// </summary>
    public class ValueDO : Value
    {
        private int _value;
        public ValueDO(DateTime timestamp, Device device, int value) : base(timestamp, device)
        {
            _value = value;
        }

        public int Value { get { return _value; } set { _value = value; } }
    }
}
