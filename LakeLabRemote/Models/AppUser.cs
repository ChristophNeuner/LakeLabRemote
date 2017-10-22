using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LakeLabRemote.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<AppUserDevice> AppUserDevices { get; set; }

        public bool IsDeviceAccessible(Device device)
        {
            return true;
            //if(device == null)
            //{
            //    throw new NullReferenceException(nameof(device));
            //}
            
            ////TODO
        }
    }
}
