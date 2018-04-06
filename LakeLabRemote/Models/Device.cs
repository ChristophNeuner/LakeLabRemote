using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LakeLabRemote.Models
{
    public class Device
    {
        public Device() { }

        public Device(string name, string lake, string location, string depth)
        {
            if (string.IsNullOrEmpty(nameof(name)))
                throw new NullReferenceException("Name must not be null or empty.");
            if (string.IsNullOrEmpty(nameof(lake)))
                throw new NullReferenceException("Lake must not be null or empty.");
            if (string.IsNullOrEmpty(nameof(location)))
                throw new NullReferenceException("Location must not be null or empty.");
            if (string.IsNullOrEmpty(nameof(depth)))
                throw new NullReferenceException("Depth must not be null or empty.");

            Id = new Guid();
            Name = name;
            Lake = lake;
            Location = location;
            Depth = depth;
            TimeOfCreation = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lake { get; set; }
        public string Location { get; set; }
        public string Depth { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public string Ip { get; set; }
        
        //TODO: Add a List with all sensortypes that are attached to the device
    }
}
