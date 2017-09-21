using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.Models
{
    public class Test
    {
        [Key]
        public string Id { get; set; }
        public string Value { get; set; }
    }
}
