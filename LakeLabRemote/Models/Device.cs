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
        [Key]
        public string Name { get; set; }
        public string Location { get; set; }
        public string Depth { get; set; }
        public string Ip { get; set; }
    }
}
