using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LakeLabRemote.Models
{
    public class AppUser : IdentityUser
    {
        public List<Device> Devices { get; set; }

        public bool HasDevice(Device device)
        {
            if(device == null)
            {
                throw new NullReferenceException(nameof(device));
            }
            if (Devices == null)
                return false;
            else
                return Devices.Contains(device);
        }
    }
}
