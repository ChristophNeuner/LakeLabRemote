using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.Models.ViewModels
{
    public class AppUserDevicesModificationViewModel
    {
        [Required]
        public string UserId { get; set; }
        public string[] DeviceIdsToAdd { get; set; }
        public string[] DeviceIdsToRemove { get; set; }
    }
}
