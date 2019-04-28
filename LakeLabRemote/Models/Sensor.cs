using LakeLabLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static LakeLabLib.Enums;

namespace LakeLabRemote.Models
{
    public class Sensor
    {
        [Key]
        public Guid Id { get; set; }
        public Enums.SensorTypes SensorType { get; set; }
        public Device Device { get; set; }
        public string Name { get; set; }
        public string Lake { get; set; }
        public string Location { get; set; }
        public Depth Depth { get; set; }
        public DateTime TimeOfCreation { get; set; }
    }
}
