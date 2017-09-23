using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.Models
{
    public abstract class Value
    {
        protected readonly Guid _guid;
        protected readonly DateTime _timestamp;
        protected readonly string _location;

        protected Value(DateTime timestamp, string location)
        {
            _guid = new Guid();
            _timestamp = timestamp;
            _location = location;
        }
    }


    /// <summary>
    /// Class for a dissolved oxygen value.
    /// </summary>
    public class ValueDO : Value
    {
        protected readonly int _value;
        protected ValueDO(DateTime timestamp, string location, int value) : base(timestamp, location)
        {
            _value = value;
        }
    }
}
