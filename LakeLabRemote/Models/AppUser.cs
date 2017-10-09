using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace LakeLabRemote.Models
{
    public class AppUser : IdentityUser
    {
        List<Device> Devices { get; set; }
    }
}
